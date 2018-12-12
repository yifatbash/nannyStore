using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using Newtonsoft.Json;

namespace BL
{
    public class BL_imp : IBL
    {
        DAL.IDAL dal;
        Random r = new Random();

        public BL_imp()
        {
            dal = DAL.FactoryDal.GetDAL();
        }

        #region Nanny Functions

        public void AddNanny(Nanny myNanny)
        {
            int age = DateTime.Now.Year - myNanny.BirthDate.Year;
            if (age < 18)
                throw new Exception("Nanny is not 18 years old yet");
            if (myNanny.MinAge > myNanny.MaxAge)
                throw new Exception("Max age must be bigger than min age");
            if (myNanny.MinAge < 3)
                throw new Exception("Child can't be younger than 3 month old");
            if (myNanny.EnanblePayForHour && myNanny.HourlyRate == null)
                throw new Exception("Must enter a hourly rate");
            if (!myNanny.NannySchedule.Any(d => d.IsWorkDay == true))
                throw new Exception("Must enter weekly hours");

            var workDays = from d in myNanny.NannySchedule
                           where d.IsWorkDay == true && (d.StartHour == TimeSpan.Zero || d.EndHour == TimeSpan.Zero)
                           select d;
            if (workDays.Count() > 0)
                throw new Exception("Please enter the missing hours");

            dal.AddNanny(myNanny);
        }

        public bool RemoveNanny(int id)
        {
            List<Contract> contracts = GetContractList().ToList<Contract>();
            if (contracts.Any<Contract>(c => c.NannyId == id && IsValidContract(c.ContractId))) //if nanny has vaild-signed contracts- it can't be deleted
                throw new Exception("Nanny could not be removed while has a valid contract");

            return dal.RemoveNanny(id);
        }

        public void UpdateNannyDetails(Nanny myNanny)
        {
            int nannyAge = DateTime.Now.Year - myNanny.BirthDate.Year;
            if (nannyAge < 18)
                throw new Exception("Nanny is not 18 years old yet");
            if (myNanny.MinAge > myNanny.MaxAge)
                throw new Exception("Max age must be bigger than min age");
            if (myNanny.MinAge < 3)
                throw new Exception("Child can't be younger than 3 month old");
            if (myNanny.EnanblePayForHour && myNanny.HourlyRate == null)
                throw new Exception("Must enter a hourly rate");

            dal.UpdateNannyDetails(myNanny);
        }

        public Nanny GetNanny(int id)
        {
            return dal.GetNanny(id);
        }

        public IEnumerable<Nanny> GetNannyList(Func<Nanny, bool> predicate = null)
        {
            return dal.GetNannyList(predicate);
        }
        #endregion

        #region Mother Functions

        public void AddMother(Mother myMother)
        {
            if (!myMother.WantedNannySchedule.Any(d => d.IsWorkDay == true))
                throw new Exception("Must enter wanted hours");

            var workDays = from d in myMother.WantedNannySchedule
                           where d.IsWorkDay == true && (d.StartHour == TimeSpan.Zero || d.EndHour == TimeSpan.Zero)
                           select d;
            if (workDays.Count() > 0)
                throw new Exception("Please enter the missing hours");

            dal.AddMother(myMother);
        }

        public bool RemoveMother(int id)
        {
            List<Contract> contracts = GetContractList().ToList<Contract>();
            //collect all the mother's valid-signed contract
            var sons = from contract in contracts
                       let child = GetChild(contract.ChildId)
                       where child.MotherId == id && IsValidContract(contract.ContractId) //if mother has a child with vaild-signed contracts- it can't be deleted
                       select contract;

            contracts = sons.ToList<Contract>();
            if (contracts.Count() > 0)
                throw new Exception("Mother could not be removed while has a child with valid contract");

            return dal.RemoveMother(id);
        }

        public void UpdateMotherDetails(Mother myMother)
        {
            dal.UpdateMotherDetails(myMother);
        }

        public Mother GetMother(int id)
        {
            return dal.GetMother(id);
        }

        public IEnumerable<Mother> GetMotherList(Func<Mother, bool> predicate = null)
        {
            return dal.GetMotherList(predicate);
        }
        #endregion

        #region Child Functions

        public void AddChild(Child myChild)
        {
            dal.AddChild(myChild);
        }

        public bool RemoveChild(int id)
        {
            return dal.RemoveChild(id); //child can be removed even if he has a vaild contract
        }

        public void UpdateChildDetails(Child myChild)
        {
            int childAge = GetChildAge(myChild.Id);
            List<Contract> childContract = GetContractList(c => c.ChildId == myChild.Id).ToList();//return the child's contract
            if (childContract.Count() > 0) //if child has contract
                foreach (var c in childContract)
                {
                    Nanny myNanny = GetNanny(c.NannyId);
                    if (childAge < 3 || childAge < myNanny.MinAge) //In case child can grow up and join the nanny later
                        c.IsSignedContract = false; //set sign to FALSE so in the future we will be able to set the sign as TRUE
                    if (childAge > myNanny.MaxAge) //In case child is bigger than the max age and can't join the nanny later
                        RemoveContract(c.ContractId); //remove the contract
                }

            dal.UpdateChildDetails(myChild);
        }

        public Child GetChild(int id)
        {
            return dal.GetChild(id);
        }

        public IEnumerable<Child> GetChildList(Func<Child, bool> predicate = null)
        {
            return dal.GetChildList(predicate);
        }

        /// <summary>
        /// the function get child and calculate his age in month
        /// </summary>
        /// <param name="childId">our child id</param>
        /// <returns>child age in month</returns>
        public int GetChildAge(int childId)
        {
            Child myChild = GetChild(childId);
            if (myChild == null)
                throw new Exception("Child with the same id was not found");
            int childAge = (DateTime.Now.Year - myChild.BirthDate.Year) * 12 + (DateTime.Now.Month - myChild.BirthDate.Month); //calculate the child age in months
            return childAge;
        }
        #endregion

        #region Contract Functions

        public void AddContract(Contract myContract)
        {
            Child myChild = GetChild(myContract.ChildId); //the child that is in the contract
            Nanny myNanny = GetNanny(myContract.NannyId); //the nanny that is in the contract

            if (myNanny == null) //check if nanny exist
                throw new Exception("Nanny with the same id was not found..");
            if (myChild == null) //check if child exist
                throw new Exception("Child with the same id was not found..");

            int childAge = GetChildAge(myChild.Id); //calculate the child age in months
            if (childAge < 3)
                throw new Exception("Child is not 3 months old yet");

            List<Contract> contracts = GetContractList().ToList<Contract>();
            if (contracts.Any<Contract>(c => c.ChildId == myChild.Id && c.IsSignedContract == true))//find if the child of the given contract already signed up with a nanny
                throw new Exception("Child already has a nanny");

            if (childAge < myNanny.MinAge || childAge > myNanny.MaxAge)
                throw new Exception("child is not within the age range of the nanny");

            if (myContract.IsSignedContract == true) //check if nanny already have the maximum number of signed contract- then can't sign the current contract
            {
                //collect all the signed contracts of the nanny (that are valid)
                var signedContract = from c in GetContractList()
                                     where c.IsSignedContract == true && IsValidContract(c.ContractId)
                                     select c;
                int numOfsignedContract = signedContract.Count();


                if (numOfsignedContract >= myNanny.MaxNumOfChildren)
                    throw new Exception("It is impossible to sign a contract beyond the maximum number of children allowed for a nanny ");
            }

            if (myNanny.EnanblePayForHour == false && myContract.PayPerHourOrMonth == PaymentPer.Hour)
                throw new Exception("Mother can't pay per hour, nanny doesn't enable..");

            if (myContract.StartContractDate > myContract.EndContractDate) //check the date validation
                throw new Exception("Start contract date can't be later than end contract date");

            //set those fields just if they are empty
            if (myContract.WeeklyHours == 0)
                myContract.WeeklyHours = CalculateContractWeeklyHours(myContract);
            if (myContract.HourlyRate == null)
                myContract.HourlyRate = myNanny.HourlyRate;
            if (myContract.MonthlyRate == 0)
                myContract.MonthlyRate = myNanny.MonthlyRate;

            dal.AddContract(myContract);
        }

        public bool RemoveContract(int contractId)
        {
            Contract myContract = GetContract(contractId);
            if (myContract == null)
                throw new Exception("contract with the same id was not found..");
            if (IsValidContract(myContract.ContractId))//check if contract is valid
                throw new Exception("Can't remove a valid contract");
            return dal.RemoveContract(contractId); //contract can be removed even if it is vaild (case mother want to end before the end date)
        }

        public void UpdateContractDetails(Contract myContract)
        {
            Child myChild = GetChild(myContract.ChildId); //the child that is in the contract
            Nanny myNanny = GetNanny(myContract.NannyId); //the nanny that is in the contract

            if (myNanny == null) //check if nanny exist
                throw new Exception("Nanny with the same id was not found..");
            if (myChild == null) //check if child exist
                throw new Exception("Child with the same id was not found..");

            int childAge = GetChildAge(myChild.Id); //calculate the child age in months
            if (childAge < 3)
                throw new Exception("Child is not 3 months old yet");

            if (childAge < myNanny.MinAge || childAge > myNanny.MaxAge)
                throw new Exception("child is not within the age range of the nanny");

            if (myContract.IsSignedContract == true) //check if nanny already have the maximum number of signed contract- then can't sign the current contract
            {
                //collect all the signed contracts of the nanny (that are valid)
                var signedContract = from c in GetContractList()
                                     where c.IsSignedContract == true && c.NannyId == myContract.NannyId && IsValidContract(c.ContractId)
                                     select c;
                int numOfsignedContract = signedContract.Count();

                if (numOfsignedContract >= myNanny.MaxNumOfChildren)
                    throw new Exception("It is impossible to sign a contract beyond the maximum number of children allowed for a nanny ");
            }

            if (myContract.StartContractDate > myContract.EndContractDate) //check the date validation
                throw new Exception("Start contract date can't be later than end contract date");

            if (myNanny.EnanblePayForHour == false && myContract.PayPerHourOrMonth == PaymentPer.Hour)
                throw new Exception("Mother can't pay per hour, nanny doesn't enable..");

            //set those fields just if they are empty
            if (myContract.WeeklyHours == 0)
                myContract.WeeklyHours = CalculateContractWeeklyHours(myContract);
            if (myContract.HourlyRate == null)
                myContract.HourlyRate = myNanny.HourlyRate;
            if (myContract.MonthlyRate == 0)
                myContract.MonthlyRate = myNanny.MonthlyRate;

            dal.UpdateContractDetails(myContract);
        }

        public Contract GetContract(int id)
        {
            return dal.GetContract(id);
        }

        public IEnumerable<Contract> GetContractList(Func<Contract, bool> predicate = null)
        {
            return dal.GetContractList(predicate);
        }

        public int CountContractsList(Func<Contract, bool> predicate = null)
        {
            return GetContractList(predicate).Count<Contract>();
        }

        /// <summary>
        /// the function get contract and check if it is vaild-signed contract
        /// </summary>
        /// <param name="contractId">our contract id</param>
        /// <returns>true- if it is vaild-signed contract, false- if not </returns>
        public bool IsValidContract(int contractId)
        {
            Contract myContract = GetContract(contractId);
            if (myContract == null)
                throw new Exception("Contract with the same id was not found");

            if (myContract.EndContractDate >= DateTime.Now && myContract.StartContractDate <= DateTime.Now && myContract.IsSignedContract == true)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// the function get contract and claculate the number of nanny work hours
        /// </summary>
        /// <param name="myContract">the contract we calculate it's weeklyHours</param>
        /// <returns>num of WeeklyHours </returns>
        public float CalculateContractWeeklyHours(Contract myContract)
        {
            Nanny myNanny = GetNanny(myContract.NannyId);

            TimeSpan numOfHours;
            numOfHours = TimeSpan.Zero;

            for (int i = 0; i < 6; i++) //go over the nanny work days and sum the number of work hours
            {
                if (myNanny.NannySchedule[i].IsWorkDay == true)
                    numOfHours += (myNanny.NannySchedule[i].EndHour - myNanny.NannySchedule[i].StartHour);
            }

            return numOfHours.Days * 24 + numOfHours.Hours + (float)numOfHours.Minutes / 60; //return the num of WeeklyHours 
        }

        /// <summary>
        /// the function get contract and calculate the neto rate mother should pay per month 
        /// </summary>
        /// <param name="myContract">the given contract</param>
        /// <returns>contract NetoRate</returns>
        public float CalculateContractRate(Contract myContract)
        {
            Nanny myNanny = GetNanny(myContract.NannyId);
            Child myChild = GetChild(myContract.ChildId);

            //go over the contract list and count the number of siblings with the same nanny
            List<Contract> contracts = GetContractList().ToList();
            var sons = from contract in contracts
                       let child = GetChild(contract.ChildId)
                       where child.MotherId == myChild.MotherId && contract.NannyId == myContract.NannyId
                       select contract;
            int numOfsiblings = sons.ToList().Count() - 1; //in case the function called after add the new contract object to the list the child himself is included in the sons- we have to remove 1 to get just the siblings

            //Calculate the neto rate
            float rate;
            if (myContract.PayPerHourOrMonth == PaymentPer.Hour && myContract.HourlyRate != null) //if mother chose to pay per hour
                rate = myContract.WeeklyHours * 4 * (float)myContract.HourlyRate;
            else //if mother chose to pay per month
                rate = myContract.MonthlyRate;

            //calculate discount (in %)
            int counter = 0;
            double discount = 0;

            while (counter < numOfsiblings)
            {
                discount += 0.02;
                counter++;
            }


            rate = (float)(1 - discount) * rate;//calculate rate after discount

            //rate = SingleMotherDiscount(myChild.MotherId, rate);//another discount for a single mother

            return rate;//return the NetoRate
        }
        /// <summary>
        /// the function makes another discout (of 10%) for a single mother
        /// </summary>
        /// <param name="motherId">our mother id</param>
        /// <param name="rate"> return the rate after discount of 10%</param>
        /// <returns></returns>
        public float SingleMotherDiscount(int motherId, float rate)
        {
            Mother myMother = GetMother(motherId);
            if (myMother == null)
                throw new Exception("mother with the same id was not found");

            if (myMother.IsSingleMother)
                return rate = (float)0.9 * rate;
            else
                return rate;

        }

        /// <summary>
        /// the function search if exist contract between given nanny and given child
        /// </summary>
        /// <param name="nannyId">given nanny id</param>
        /// <param name="childId">given child id</param>
        /// <returns>the contract (if not exist- throw Exception)</returns>
        public Contract SearchContract(int nannyId, int childId)
        {
            IEnumerable<Contract> contracts = GetContractList();
            if (!contracts.Any<Contract>(c => c.NannyId == nannyId && c.ChildId == childId))
                throw new Exception("No Contract was found between nanny and child..");

            return contracts.FirstOrDefault<Contract>(c => c.NannyId == nannyId && c.ChildId == childId);

        }
        #endregion

        /// <summary>
        /// the function calculate the distance between mother to nanny
        /// </summary>
        /// <param name="motherId"></param>
        /// <param name="nannyId"></param>
        /// <returns>the distance</returns>
        public float Distance(int motherId, int nannyId)
        {
            Mother myMother = GetMother(motherId);
            Nanny myNanny = GetNanny(nannyId);
            if (myNanny == null)
                throw new Exception("Nanny with the same id was not found");
            if (myMother == null)
                throw new Exception("Mother with the same id was not found");

            string nannyAddress = myNanny.Address;
            string motherAddress;
            if (myMother.WantedNannyAddress == null)
                motherAddress = myMother.HomeAddress;
            else
                motherAddress = myMother.WantedNannyAddress;

            return CalculateDistance(motherAddress, nannyAddress);
        }

        #region Functions Return Special NannyLists 
        
        /// <summary>
        ///  the function get mother and max distance and return the nannies located nearness
        /// </summary>
        /// <param name="motherId">the mother that search for close nanny</param>
        /// /// <param name="maxKm">the max wanted distance to search a nanny</param>
        /// <returns>return the nannies located nearness(up to max km)</returns>
        public List<Nanny> GetAllNannyByDistance(int motherId, int maxKm)
        {
            Mother myMother = GetMother(motherId);
            if (myMother == null)
                throw new Exception("mother with the same id was not found");

            //order the nannies from the closest to the farthest
            var closeNannies = from n in GetNannyList()
                               let nannyId = n.Id
                               let distance = Distance(myMother.Id, n.Id)
                               where distance <= maxKm //choose the nannies located up to wanted km
                               orderby distance
                               select n;

            //List<Nanny> closeNannies = new List<Nanny>();
            //foreach (var item in nannyByLocation ) //go over the nannies and choose the nannies located up to wanted km
            //{
            //    if (!(item.distance > (float)maxKm))
            //        closeNannies.Add(GetNanny(item.nannyId));
            //}

            return closeNannies.ToList(); //return the closest nannies
        }

        /// <summary>
        /// the function get mother and return all the nannies work in the hours mother want nanny
        /// </summary>
        /// <param name="motherId">the mother search for a nanny</param>
        /// <param name="almostMatch">if true-return almost match nannies in case no match nanny was found"</param>
        /// <returns>return all the nannies work in the hours mother want nanny</returns>
        public List<Nanny> GetAllMatchHoursNanny(int motherId, bool almostMatch = true)
        {
            Mother myMother = GetMother(motherId);
            if (myMother == null)
                throw new Exception("mother with the same id was not found");

            //return the true if nanny  works in the necessary hours
            Func<Nanny, bool> predicate = n =>
             {
                 for (int i = 0; i < 6; i++) //go over the days
                 {
                     if (n.NannySchedule[i].IsWorkDay == false && myMother.WantedNannySchedule[i].IsWorkDay == true)
                     {//if mother wants nanny for this day, but nanny doesn't work- no match
                         return false;
                     }
                     if (n.NannySchedule[i].IsWorkDay && myMother.WantedNannySchedule[i].IsWorkDay)//just if nanny works on the mother's wanted days- checks the hours
                         if (n.NannySchedule[i].StartHour > myMother.WantedNannySchedule[i].StartHour || n.NannySchedule[i].EndHour < myMother.WantedNannySchedule[i].EndHour)
                         {//if nanny work less than necessary- no match
                             return false;
                         }
                 }
                 return true;
             };

            List<Nanny> nannies = GetNannyList(predicate).ToList<Nanny>();

            if (nannies.Count() == 0 && almostMatch == true) //if no nanny works in the hours mother wants nanny- return 5 almost match nannies
                return GetFiveAlmostMatchHoursNanny(motherId);
            else
                return nannies; //return the match nannies 
        }

        /// <summary>
        /// for each nanny the function calculates how many hours are missing in order to fit the mother's hours
        /// </summary>
        /// <param name="motherId">mother that search for a nanny</param>
        /// <returns>the 5 nannies that almost fit to the wanted hours</returns>
        public List<Nanny> GetFiveAlmostMatchHoursNanny(int motherId)
        {
            TimeSpan numOfHours = new TimeSpan(0, 0, 0);
            Mother myMother = GetMother(motherId);
            if (myMother == null)
                throw new Exception("mother with the same id was not found");

            Dictionary<int, int> Difference = new Dictionary<int, int>(); //the dictionary saves how many hours mom needs that nanny can't provide  
            foreach (Nanny n in GetNannyList()) //go over the nanny list
            {
                numOfHours = TimeSpan.Zero;//set the numOfHours to be 0
                for (int i = 0; i < 6; i++) //go over the days
                {
                    if (n.NannySchedule[i].IsWorkDay == false && myMother.WantedNannySchedule[i].IsWorkDay == true)
                    {//calculate how many hours mom want on the day nanny unavaliable
                        numOfHours += myMother.WantedNannySchedule[i].EndHour - myMother.WantedNannySchedule[i].StartHour;
                    }
                    if (n.NannySchedule[i].IsWorkDay && myMother.WantedNannySchedule[i].IsWorkDay)
                    {
                        if (n.NannySchedule[i].StartHour > myMother.WantedNannySchedule[i].StartHour)
                        { //calculate how many hours mom wants more than num of hours nanny provides in the start of the day
                            numOfHours += n.NannySchedule[i].StartHour - myMother.WantedNannySchedule[i].StartHour;
                        }
                        if (n.NannySchedule[i].EndHour < myMother.WantedNannySchedule[i].EndHour)
                        {//calculate how many hours mom wants more than num of hours nanny provides in the end of the day
                            numOfHours += myMother.WantedNannySchedule[i].EndHour - n.NannySchedule[i].EndHour;
                        }
                    }
                }
                Difference.Add(n.Id, numOfHours.Hours); //add the nanny and the num of hours to the dictionary
            }

            var priority = from n in Difference
                           orderby n.Value //order by number of difference hours
                           select n;

            List<Nanny> nannies = new List<Nanny>();
            int counter = 0;
            foreach (var item in priority) //go over the ordered nannies and add the first 5 nannies to the list
            {
                if (counter >= 5)
                    break;
                nannies.Add(GetNanny(item.Key)); //add the nanny to the list
                counter++;
            }

            return nannies; //return the first 5 nannies that are almost match

        }

        /// <summary>
        /// the function go over the nannies list and check which nannies have vacations as TMT vacations
        /// </summary>
        /// <returns>nannies have vacations as TMT vacations</returns>
        public List<Nanny> GetNannyWithTMTList()
        {
            List<Nanny> nannies = GetNannyList(n => n.IsVacationAsTMT == true).ToList<Nanny>();
            return nannies;
        }

        /// <summary>
        /// order nannies according to their price
        /// </summary>
        /// /// <param name="maxPrice">returns nannies that cost up to the "maxPrice"</param>
        /// <param name="monthlyRate">if true- order by monthly rate, else- order by hourhly rate </param>
        /// <returns>OrderedNannyList</returns>
        public List<Nanny> GetNannyByPrice(float maxPrice = 3000, bool monthlyRate = true)
        {
            IEnumerable<Nanny> order;
            if (monthlyRate == true) //return nannies ordered by MonthlyRate
                order = from n in GetNannyList()
                        where n.MonthlyRate <= maxPrice
                        orderby n.MonthlyRate
                        select n;
            else //return nannies ordered by HourlyRate
                order = from n in GetNannyList()
                        where n.EnanblePayForHour==true && n.HourlyRate <= maxPrice
                        orderby n.HourlyRate
                        select n;

            return order.ToList();
        }

        /// <summary>
        /// return nannies according to a wanted child age
        /// </summary>
        /// <param name="age">the wanted age</param>
        /// <param name="max"> if true- return according to the max age, else- return according to the min age </param>
        /// <returns>nannyList</returns>
        public List<Nanny> GetNannyByChildAge(int age)
        {
            if (age < 3)
                throw new Exception("Child can't be younger than 3 month old");

         return GetNannyList(n => n.MaxAge >= age && n.MinAge <= age).ToList<Nanny>();

        }

        /// <summary>
        /// the function get the mother constraints and return fit nannies
        /// </summary>
        /// <param name="motherId">the mother that seach for nanny</param>
        /// <param name="childAge">the age of the child mother search a nanny for him</param>
        /// <param name="maxPrice">the maximum rate mother agrees pay</param>
        /// <param name="almostMatchNanny">if true-return the five almost match(in case no match nanny was find by hours), else-return just the much nannies</param>
        /// <param name="maxDistance">the max distance mother search a nanny</param>
        /// <param name="monthlyRate">if true- return the nannies according to mounthly rate, else- return the nannies according to hourly rate</param>
        /// <returns></returns>
        public List<Nanny> GetNannyByMotherConstraints(int motherId, int childAge, int maxPrice, bool monthlyRate, bool almostMatchNanny = true, int maxDistance = 50)
        {
            List<Nanny> byAgeList = GetNannyByChildAge(childAge);
            List<Nanny> byHoursList = GetAllMatchHoursNanny(motherId, almostMatchNanny);
            List<Nanny> byPriceList = new List<Nanny>();
            if (monthlyRate == true)
                byPriceList = GetNannyByPrice(maxPrice, true);
            else 
                byPriceList = GetNannyByPrice(maxPrice, false);
            List<Nanny> ByDistanceList = GetAllNannyByDistance(motherId, maxDistance);

            List<Nanny> finalList = new List<Nanny>();
            foreach (Nanny n in byAgeList)
            {
                if (byHoursList.Any(nan => nan.Id == n.Id) && byPriceList.Any(nan => nan.Id == n.Id) && ByDistanceList.Any(nan => nan.Id == n.Id))
                    finalList.Add(n);
            }

            return finalList;
        }

        #endregion

        #region Functions Return Special Children Lists 
        /// <summary>
        ///the function go over the childList and checks which children have no nanny
        /// </summary>
        /// <returns>list of children have no nanny</returns>
        public List<Child> GetNoNannyChildrenList()
        {
            List<Contract> contracts = GetContractList().ToList<Contract>();
            //get all the children that have a nanny
            List<Child> nannyChildren = GetChildList(child => contracts.Any<Contract>(contract => contract.ChildId == child.Id && IsValidContract(contract.ContractId) == true)).ToList<Child>();
            //get all the children list
            List<Child> children = GetChildList().ToList();
            //remove the children with a nanny from all the children
            foreach (Child nc in nannyChildren)
            {
                Child ch = children.FirstOrDefault<Child>(c => c.Id == nc.Id);
                children.Remove(ch);
            }
                
            //return the no nanny children 
            return children;
        }

        /// <summary>
        /// the function return the children of specific nanny
        /// </summary>
        /// <param name="nannyId">the Wanted NannyId</param>
        /// <returns></returns>
        public List<Child> GetChildrenListByNannyId(int nannyId)
        {
            var children = from c in GetContractList(c => c.NannyId == nannyId)
                           let child = GetChild(c.ChildId)
                           select child;

            return children.ToList();
        }

        /// <summary>
        /// the function return all allergy children of a spacific nanny
        /// </summary>
        /// <param name="nannyId"></param>
        /// <returns>all allergy children of a spacific nanny</returns>
        public List<Child> GetAllergyChildren(int nannyId)
        {
            var allergyChildren = from c in GetContractList(c => c.NannyId == nannyId)
                                  let child = GetChild(c.ChildId)
                                  where child.IsFoodAllergy || child.IsMedicinesAllergy
                                  select child;

            return allergyChildren.ToList();
        }
        #endregion

        #region IGrouping Functions

        /// <summary>
        /// the function group nannies by the children age range (3-5,6-8,9-12...)
        /// </summary>
        /// <param name="order">if true- order the groups from the smallest ages to the biggest</param>
        /// <param name="max">if true- the groups are divided by max age, if false- the groups are divided by min age</param>
        /// <returns>nannis group by children age</returns>
        public IEnumerable<IGrouping<string, Nanny>> GroupNannyByChildAge(bool order = false, bool max = true)
        {
            if (max)
            {
                var ageGroup = from n in GetNannyList()
                               group n by maxAgeRange(n.MaxAge / 3);

                if (order)
                    return ageGroup.OrderBy(a => a.Key);

                return ageGroup;
            }
            else
            {
                var ageGroup = from n in GetNannyList()
                               group n by minAgeRange(n.MinAge / 3);

                if (order)
                    return ageGroup.OrderBy(a => a.Key);

                return ageGroup;
            }

            string maxAgeRange(int a)
            {
                switch (a)
                {
                    case 1:
                        return "Max age: 3-5 months";
                    case 2:
                        return "Max age: 6-8 months";
                    case 3:
                        return "Max age: 9-11 months";
                    default:
                        return "Max age: 1 year+";
                }
            }

            string minAgeRange(int a)
            {
                switch (a)
                {
                    case 1:
                        return "Min age: 3-5 months";
                    case 2:
                        return "Min age: 6-8 months";
                    case 3:
                        return "Min age: 9-11 months";
                    default:
                        return "Min age: 1 year+";
                }
            }
        }

        /// <summary>
        /// the function group contracts by distance between mother location to nanny loction
        /// </summary>
        /// <param name="order">if true- order the groups from the closest to the farthest</param>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Contract>> GroupContractByDistance(bool order = false)
        {
            var distanceGroup = from c in GetContractList()
                                let nanny = GetNanny(c.NannyId)
                                let child = GetChild(c.ChildId)
                                let mother = GetMother(child.MotherId)
                                let distance = Distance(mother.Id, nanny.Id)
                                group c by distanceRange((int)distance / 5000);

            if (order)
                return distanceGroup.OrderBy(d => d.Key);

            string distanceRange(int d)
            {
                switch (d)
                {
                    case 0:
                        return "1km-5km";
                    case 1:
                        return "5km-10km";
                    case 2:
                        return "10km-15km";
                    case 3:
                        return "15km-20km";
                    case 4:
                        return "20km-25km";
                    case 5:
                        return "25km-30km";
                    default:
                        return "30km+";
                }
            }
            return distanceGroup;
        }

        /// <summary>
        /// the function group the contracts by rate (0-1000, 1000-2000, 2000-3000...)
        /// </summary>
        /// <param name="order">if true- order the contracts from the cheapest and most expensive</param>
        /// <returns>the contract group by rate </returns>
        public IEnumerable<IGrouping<string, Contract>> GroupContractByRate(bool order = true)
        {
            var rateGroups = from c in GetContractList()
                             let netoRate = CalculateContractRate(c)
                             group c by rateRange((int)netoRate / 1000);

            if (order)
                return rateGroups.OrderBy(r => r.Key);

            string rateRange(int rate)
            {
                switch (rate)
                {
                    case 0:
                        return "1₪-1000₪";
                    case 1:
                        return "1000₪-2000₪";
                    case 2:
                        return "2000₪-3000₪";
                    default:
                        return "3000₪+";
                }
            }


            return rateGroups;
        }

        /// <summary>
        /// the function get nanny and group her children by breast milk- create 2 groups: children eat breast milk or children eat 
        /// </summary>
        /// <param name="nannyId">our nanny id</param>
        /// <returns> 2 groups: children eat breast milk or children eat </returns>
        public IEnumerable<IGrouping<string, Child>> GroupChildrenByBreastMilk(int nannyId)
        {
            if (GetNanny(nannyId) == null)
                throw new Exception("nanny with the same id was not found");

            var nannyChildren = from c in GetContractList(c => c.NannyId == nannyId)
                                select GetChild(c.ChildId);

            var groupChildren = from c in nannyChildren
                                group c by breastMilkMark(c.IsBreastMilk == true);

            string breastMilkMark(bool flag)
            {
                if (flag)
                    return "Breast Milk";
                else
                    return "Formula";
            }

            return groupChildren;
        }

        /// <summary>
        /// the function group mothers by statuses: Student, Worker, MaternityLeave, HouseWife
        /// </summary>
        /// <returns>the mother' groups</returns>
        public IEnumerable<IGrouping<MotherStatus, Mother>> GroupMotherByStatus()
        {
            var groupMother = from m in GetMotherList()
                              group m by m.Status;

            return groupMother;
        }
        #endregion

        #region Initialize Functions

        public void InitNannyList()
        {
            Nanny myNanny = new Nanny
            {
                Id = 1234,
                FirstName = "Sara",
                LastName = "Cohen",
                BirthDate = DateTime.Parse("12.12.1990"),
                PhoneNumber = "0541234567",
                Address = "Kanfei Nesharim Street, Jerusalem, Israel",
                IsElevator = true,
                Floor = 3,
                NumberOfExperienceYears = 3,
                MaxNumOfChildren = 10,
                MinAge = 3,
                MaxAge = 7,
                EnanblePayForHour = true,
                HourlyRate = 10,
                MonthlyRate = 2000,
                IsVacationAsTMT = false,
                Recommendations = "Recommendations",
                SpecialActivities = "SpecialActivities",
                IsKosherFood = true,
                IsReligiousEducation = true,
                Comments = "Comments",
                //BankAccount = new Bank(),
            };
            myNanny.NannySchedule[0].IsWorkDay = true;
            myNanny.NannySchedule[1].IsWorkDay = true;
            myNanny.NannySchedule[2].IsWorkDay = true;
            myNanny.NannySchedule[0].StartHour += new TimeSpan(8, 30, 0);
            myNanny.NannySchedule[0].EndHour += new TimeSpan(14, 0, 0);
            myNanny.NannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
            myNanny.NannySchedule[1].EndHour += new TimeSpan(14, 0, 0);
            myNanny.NannySchedule[2].StartHour += new TimeSpan(8, 0, 0);
            myNanny.NannySchedule[2].EndHour += new TimeSpan(13, 0, 0);
            try
            {
                AddNanny(myNanny);
            }
            catch (Exception e)
            {
                throw e;
            }

            myNanny = new Nanny
            {
                Id = 1235,
                FirstName = "Aviva",
                LastName = "Levi",
                BirthDate = DateTime.Parse("12.12.1970"),
                PhoneNumber = "0541234567",
                Address = "Elkana Street, Jerusalem, Israel",
                IsElevator = true,
                Floor = 2,
                NumberOfExperienceYears = 15,
                MaxNumOfChildren = 7,
                MinAge = 6,
                MaxAge = 10,
                EnanblePayForHour = true,
                HourlyRate = 40,
                MonthlyRate = 2500,
                IsVacationAsTMT = true,
                Recommendations = "Recommendations",
                SpecialActivities = "SpecialActivities",
                IsKosherFood = true,
                IsReligiousEducation = false,
                Comments = "Comments",
                //BankAccount = new Bank(),
            };
            myNanny.NannySchedule[0].IsWorkDay = true;
            myNanny.NannySchedule[1].IsWorkDay = true;
            myNanny.NannySchedule[2].IsWorkDay = true;
            myNanny.NannySchedule[0].StartHour += new TimeSpan(8, 30, 0);
            myNanny.NannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
            myNanny.NannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
            myNanny.NannySchedule[1].EndHour += new TimeSpan(13, 0, 0);
            myNanny.NannySchedule[2].StartHour += new TimeSpan(9, 0, 0);
            myNanny.NannySchedule[2].EndHour += new TimeSpan(15, 0, 0);
            try
            {
                AddNanny(myNanny);
            }
            catch (Exception e)
            {
                throw e;
            }

            myNanny = new Nanny
            {
                Id = 1236,
                FirstName = "Avital",
                LastName = "Levi",
                BirthDate = DateTime.Parse("12.12.1986"),
                PhoneNumber = "0541234567",
                Address = "HaRechavim Street, Jerusalem, Israel",
                IsElevator = true,
                Floor = 2,
                NumberOfExperienceYears = 7,
                MaxNumOfChildren = 7,
                MinAge = 6,
                MaxAge = 10,
                EnanblePayForHour = true,
                HourlyRate = 40,
                MonthlyRate = 2500,
                IsVacationAsTMT = true,
                Recommendations = "Recommendations",
                SpecialActivities = "SpecialActivities",
                IsKosherFood = true,
                IsReligiousEducation = false,
                Comments = "Comments",
                //BankAccount = new Bank(),
            };
            myNanny.NannySchedule[0].IsWorkDay = true;
            myNanny.NannySchedule[1].IsWorkDay = true;
            myNanny.NannySchedule[2].IsWorkDay = true;
            myNanny.NannySchedule[0].StartHour += new TimeSpan(9, 0, 0);
            myNanny.NannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
            myNanny.NannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
            myNanny.NannySchedule[1].EndHour += new TimeSpan(14, 0, 0);
            myNanny.NannySchedule[2].StartHour += new TimeSpan(10, 0, 0);
            myNanny.NannySchedule[2].EndHour += new TimeSpan(17, 0, 0);
            try
            {
                AddNanny(myNanny);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InitMotherList()
        {
            Mother myMother = new Mother
            {
                Id = 3234,
                LastName = "Cohen",
                FirstName = "Miri",
                PhoneNumber = "054444444",
                HomeAddress = "Jaffa Street, Jerusalem, Israel",
                Comments = "Comments",
                Status = MotherStatus.Student,
                IsSingleMother = true,
            };
            for (int i = 0; i < 3; i++)
            {
                myMother.WantedNannySchedule[i].IsWorkDay = true;
            }
            myMother.WantedNannySchedule[0].StartHour += new TimeSpan(8, 30, 0);
            myMother.WantedNannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
            myMother.WantedNannySchedule[1].StartHour += new TimeSpan(10, 0, 0);
            myMother.WantedNannySchedule[1].EndHour += new TimeSpan(14, 0, 0);
            myMother.WantedNannySchedule[2].StartHour += new TimeSpan(8, 0, 0);
            myMother.WantedNannySchedule[2].EndHour += new TimeSpan(14, 0, 0);
            try
            {
                AddMother(myMother);
            }
            catch (Exception e)
            {
                throw e;
            }

            myMother = new Mother
            {
                Id = 3235,
                LastName = "Dan",
                FirstName = "Oriya",
                PhoneNumber = "054444444",
                HomeAddress = "Jaffa Street, Jerusalem, Israel",
                Comments = "Comments",
                Status = MotherStatus.Worker,
                IsSingleMother = false,
            };
            for (int i = 0; i < 3; i++)
            {
                myMother.WantedNannySchedule[i].IsWorkDay = true;
            }
            myMother.WantedNannySchedule[0].StartHour += new TimeSpan(8, 30, 0);
            myMother.WantedNannySchedule[0].EndHour += new TimeSpan(17, 0, 0);
            myMother.WantedNannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
            myMother.WantedNannySchedule[1].EndHour += new TimeSpan(15, 0, 0);
            myMother.WantedNannySchedule[2].StartHour += new TimeSpan(9, 0, 0);
            myMother.WantedNannySchedule[2].EndHour += new TimeSpan(13, 0, 0);
            try
            {
                AddMother(myMother);
            }
            catch (Exception e)
            {
                throw e;
            }

            myMother = new Mother
            {
                Id = 3236,
                LastName = "Levi",
                FirstName = "Mor",
                PhoneNumber = "054444444",
                HomeAddress = "Jaffa Street, Jerusalem, Israel",
                Comments = "Comments",
                Status = MotherStatus.MaternityLeave,
                IsSingleMother = false,
            };
            for (int i = 0; i < 3; i++)
            {
                myMother.WantedNannySchedule[i].IsWorkDay = true;
            }
            myMother.WantedNannySchedule[0].StartHour += new TimeSpan(9, 0, 0);
            myMother.WantedNannySchedule[0].EndHour += new TimeSpan(14, 0, 0);
            myMother.WantedNannySchedule[1].StartHour += new TimeSpan(8, 0, 0);
            myMother.WantedNannySchedule[1].EndHour += new TimeSpan(14, 0, 0);
            myMother.WantedNannySchedule[2].StartHour += new TimeSpan(8, 30, 0);
            myMother.WantedNannySchedule[2].EndHour += new TimeSpan(16, 0, 0);
            try
            {
                AddMother(myMother);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InitChildList()
        {
            try
            {
                AddChild(new Child
                {
                    Id = 2234,
                    MotherId = 3234,
                    FirstName = "Yossi",
                    BirthDate = DateTime.Parse("2.7.2017"),
                    IsSpecialNeedsChild = false,
                    SpecialNeeds = "",
                    IsFoodAllergy = false,
                    FoodAllergy = "",
                    IsMedicinesAllergy = false,
                    MedicinesAllergy = "",
                    IsBreastMilk = true,
                    Comments = ""
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                AddChild(new Child
                {
                    Id = 2235,
                    MotherId = 3235,
                    FirstName = "Ron",
                    BirthDate = DateTime.Parse("3.5.2017"),
                    IsSpecialNeedsChild = true,
                    SpecialNeeds = "SpecialNeeds",
                    IsFoodAllergy = true,
                    FoodAllergy = "FoodAllergy",
                    IsMedicinesAllergy = true,
                    MedicinesAllergy = "IsMedicinesAllergy",
                    IsBreastMilk = true,
                    Comments = "Comments"
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                AddChild(new Child
                {
                    Id = 2236,
                    MotherId = 3236,
                    FirstName = "Aviv",
                    BirthDate = DateTime.Parse("3.6.2017"),
                    IsSpecialNeedsChild = true,
                    SpecialNeeds = "SpecialNeeds",
                    IsFoodAllergy = true,
                    FoodAllergy = "FoodAllergy",
                    IsMedicinesAllergy = true,
                    MedicinesAllergy = "IsMedicinesAllergy",
                    IsBreastMilk = true,
                    Comments = "Comments"
                });
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public void InitContractList()
        {
            Contract myContract = new Contract
            {
                NannyId = 1234,
                ChildId = 2234,
                IsIntroductoryMeeting = true,
                IsSignedContract = true,
                PayPerHourOrMonth = PaymentPer.Hour,
                StartContractDate = new DateTime(2017, 9, 1),
                EndContractDate = new DateTime(2018, 9, 1),
                PaidBy = WayOfPayment.BankTransfer
            };
            try
            {
                AddContract(myContract);
            }
            catch (Exception e)
            {
                throw e;
            }

            myContract = new Contract
            {
                NannyId = 1234,
                ChildId = 2236,
                IsIntroductoryMeeting = true,
                IsSignedContract = true,
                PayPerHourOrMonth = PaymentPer.Month,
                StartContractDate = new DateTime(2017, 9, 1),
                EndContractDate = new DateTime(2018, 9, 1),
                PaidBy = WayOfPayment.BankTransfer
            };
            try
            {
                AddContract(myContract);
            }
            catch (Exception e)
            {
                throw e;
            }

            myContract = new Contract
            {
                NannyId = 1235,
                ChildId = 2235,
                IsIntroductoryMeeting = false,
                IsSignedContract = false,
                PayPerHourOrMonth = PaymentPer.Month,
                StartContractDate = new DateTime(2017, 9, 1),
                EndContractDate = new DateTime(2018, 9, 1),
                PaidBy = WayOfPayment.BankTransfer
            };
            try
            {
                AddContract(myContract);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Init()
        {
            if (dal is DAL_imp)
            {
                try
                {
                    InitNannyList();
                    InitMotherList();
                    InitChildList();
                    InitContractList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        #endregion

        #region Google Functions
        static string API_KEY = ConfigurationSettings.AppSettings.Get("googleApiKey");

        public List<string> GetPlaceAutoComplete(string str)
        {
            List<string> result = new List<string>();
            GoogleMapsApi.Entities.PlaceAutocomplete.Request.PlaceAutocompleteRequest request = new GoogleMapsApi.Entities.PlaceAutocomplete.Request.PlaceAutocompleteRequest();
            request.ApiKey = API_KEY;
            request.Input = str;

            var response = GoogleMaps.PlaceAutocomplete.Query(request);

            foreach (var item in response.Results)
            {
                result.Add(item.Description);
            }

            return result;
        }

        public int CalculateDistance(string source, string dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = source,
                Destination = dest,
            };
            try
            {
                DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
                Route route = drivingDirections.Routes.First();
                Leg leg = route.Legs.First();
                return leg.Distance.Value;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion
    }
}
