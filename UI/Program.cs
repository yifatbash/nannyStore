using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            BL.IBL bl = BL.FactoryBL.GetBL();

            bl.Init();

            #region Nanny Functions

            Nanny myNanny = new Nanny
            {
                Id = 1237,
                FirstName = "Shir",
                LastName = "Tevet",
                BirthDate = DateTime.Parse("12.12.1997"),
                PhoneNumber = "0541234567",
                Address = "Yaffo,Jerusalem,Israel",
                IsElevator = true,
                Floor = 3,
                MaxNumOfChildren = 10,
                MinAge = 3,
                MaxAge = 7,
                EnanblePayForHour = true,
                HourlyRate = 10,
                MonthlyRate = 2000,
                IsVacationAsTMT = true,
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
            myNanny.NannySchedule[0].StartHour += new TimeSpan(10, 0, 0);
            myNanny.NannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
            myNanny.NannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
            myNanny.NannySchedule[1].EndHour += new TimeSpan(14, 0, 0);
            myNanny.NannySchedule[2].StartHour += new TimeSpan(8, 0, 0);
            myNanny.NannySchedule[2].EndHour += new TimeSpan(14, 0, 0);

            try
            {
                Console.WriteLine("-----add nanny-----");
                bl.AddNanny(myNanny);
                foreach (Nanny n in bl.GetNannyList())
                {
                    Console.WriteLine(n);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----remove nanny-----");
                //bl.RemoveNanny(1236);
                foreach (Nanny n in bl.GetNannyList())
                {
                    Console.WriteLine(n);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----update nanny-----");
                Nanny nannyToUpdate = new Nanny
                {
                    Id = 1237,
                    FirstName = "Shir",
                    LastName = "Tevet",
                    BirthDate = DateTime.Parse("12.12.1997"),
                    PhoneNumber = "0541234567",
                    Address = "Yaffo,Jerusalem,Israel",
                    IsElevator = true,
                    Floor = 3,
                    MaxNumOfChildren = 10,
                    MinAge = 3,
                    MaxAge = 7,
                    EnanblePayForHour = true,
                    HourlyRate = 10,
                    MonthlyRate = 2000,
                    IsVacationAsTMT = true,
                    Recommendations = "Recommendations",
                    SpecialActivities = "SpecialActivities",
                    IsKosherFood = true,
                    IsReligiousEducation = true,
                    Comments = "Comments",
                    //BankAccount = new Bank(),
                };
                nannyToUpdate.NannySchedule[0].IsWorkDay = true;
                nannyToUpdate.NannySchedule[1].IsWorkDay = true;
                nannyToUpdate.NannySchedule[2].IsWorkDay = true;
                nannyToUpdate.NannySchedule[0].StartHour += new TimeSpan(10, 0, 0);
                nannyToUpdate.NannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
                nannyToUpdate.NannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
                nannyToUpdate.NannySchedule[1].EndHour += new TimeSpan(17, 0, 0);
                nannyToUpdate.NannySchedule[2].StartHour += new TimeSpan(7, 0, 0);
                nannyToUpdate.NannySchedule[2].EndHour += new TimeSpan(17, 0, 0);

                bl.UpdateNannyDetails(nannyToUpdate);
                foreach (Nanny n in bl.GetNannyList())
                {
                    Console.WriteLine(n);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----get nanny-----");
                Console.WriteLine(bl.GetNanny(1237));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion

            #region Mother Functions

            Mother myMother = new Mother
            {
                Id = 3237,
                LastName = "Ben-David",
                FirstName = "Tal",
                PhoneNumber = "054444444",
                HomeAddress = "Ben-Tzion,Jerusalem,Israel",
                Comments = "Comments",
                Status = MotherStatus.HouseWife,
                IsSingleMother = true,
            };
            for (int i = 0; i < 3; i++)
            {
                myMother.WantedNannySchedule[i].IsWorkDay = true;
            }
            myMother.WantedNannySchedule[0].StartHour += new TimeSpan(8, 30, 0);
            myMother.WantedNannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
            myMother.WantedNannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
            myMother.WantedNannySchedule[1].EndHour += new TimeSpan(14, 0, 0);
            myMother.WantedNannySchedule[2].StartHour += new TimeSpan(8, 0, 0);
            myMother.WantedNannySchedule[2].EndHour += new TimeSpan(14, 0, 0);
            try
            {
                Console.WriteLine("-----add mother-----");
                bl.AddMother(myMother);
                foreach (Mother m in bl.GetMotherList())
                {
                    Console.WriteLine(m);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----remove mother-----");
               // bl.RemoveMother(3236);
                foreach (Mother m in bl.GetMotherList())
                {
                    Console.WriteLine(m);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            try
            {
                Console.WriteLine("-----update mother-----");
                Mother motherToUpdate = new Mother
                {
                    Id = 3237,
                    LastName = "Ben-David",
                    FirstName = "Tal",
                    PhoneNumber = "054444444",
                    HomeAddress = "Ben-Tzion,Jerusalem,Israel",
                    Comments = "Comments",
                    Status = MotherStatus.Worker,
                    IsSingleMother = false,
                };
                for (int i = 0; i < 3; i++)
                {
                    motherToUpdate.WantedNannySchedule[i].IsWorkDay = true;
                }
                motherToUpdate.WantedNannySchedule[0].StartHour += new TimeSpan(8, 30, 0);
                motherToUpdate.WantedNannySchedule[0].EndHour += new TimeSpan(16, 0, 0);
                motherToUpdate.WantedNannySchedule[1].StartHour += new TimeSpan(9, 0, 0);
                motherToUpdate.WantedNannySchedule[1].EndHour += new TimeSpan(17, 0, 0);
                motherToUpdate.WantedNannySchedule[2].StartHour += new TimeSpan(8, 0, 0);
                motherToUpdate.WantedNannySchedule[2].EndHour += new TimeSpan(20, 0, 0);

                bl.UpdateMotherDetails(motherToUpdate);
                foreach (Mother m in bl.GetMotherList())
                {
                    Console.WriteLine(m);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----get mother-----");
                Console.WriteLine(bl.GetMother(2237));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion

            #region Child Functions

            Child myChild = new Child
            {
                Id = 2237,
                MotherId = 3237,
                FirstName = "Chen",
                BirthDate = DateTime.Parse("3.4.2017"),
                IsSpecialNeedsChild = true,
                SpecialNeeds = "SpecialNeeds",
                IsFoodAllergy = true,
                FoodAllergy = "FoodAllergy",
                IsMedicinesAllergy = true,
                MedicinesAllergy = "IsMedicinesAllergy",
                IsBreastMilk = true,
                Comments = "Comments"
            };

            try
            {
                Console.WriteLine("-----add child-----");
                bl.AddChild(myChild);
                foreach (Child c in bl.GetChildList())
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----remove child-----");
                //bl.RemoveChild(2234);
                foreach (Child c in bl.GetChildList())
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----update child-----");
                Child childToUpdate = new Child
                {
                    Id = 2237,
                    MotherId = 3237,
                    FirstName = "Chen",
                    BirthDate = DateTime.Parse("3.7.2017"),
                    IsSpecialNeedsChild = false,
                    SpecialNeeds = "",
                    IsFoodAllergy = true,
                    FoodAllergy = "FoodAllergy",
                    IsMedicinesAllergy = true,
                    MedicinesAllergy = "IsMedicinesAllergy",
                    IsBreastMilk = true,
                    Comments = "Comments"
                };

                bl.UpdateChildDetails(childToUpdate);
                foreach (Child c in bl.GetChildList())
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----get child-----");
                Console.WriteLine(bl.GetChild(2237));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion

            #region Contract Functions

            Contract myContract = new Contract
            {
                NannyId = 1237,
                ChildId = 2237,
                IsIntroductoryMeeting = false,
                IsSignedContract = false,
                PayPerHourOrMonth = PaymentPer.Hour,
                StartContractDate = new DateTime(2017, 9, 1),
                EndContractDate = new DateTime(2018, 9, 1),
                PaidBy = WayOfPayment.BankTransfer
            };

            try
            {
                Console.WriteLine("-----add contract-----");
                bl.AddContract(myContract);
                foreach (Contract c in bl.GetContractList())
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----remove contract-----");
                //bl.RemoveContract(10000003);
                foreach (Contract c in bl.GetContractList())
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----update contract-----");
                Contract contractToUpdate = new Contract
                {
                    NannyId = 1234,
                    ChildId = 2236,
                    IsIntroductoryMeeting = false,
                    IsSignedContract = false,
                    PayPerHourOrMonth = PaymentPer.Month,
                    StartContractDate = new DateTime(2017, 9, 1),
                    EndContractDate = new DateTime(2018, 9, 1),
                    PaidBy = WayOfPayment.BankTransfer
                };
                bl.UpdateContractDetails(contractToUpdate);

                foreach (Contract c in bl.GetContractList())
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("-----get contract-----");
                Console.WriteLine(bl.GetContract(10000004));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion

            try
            {
                Console.WriteLine("\n----GetAllNannyByDistance----");
                var nanniesByDistance = bl.GetAllNannyByDistance(3234, 40);
                foreach (var n in nanniesByDistance)
                {
                    Console.WriteLine("nannyId: {0}", n.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var matchNannies = bl.GetAllMatchHoursNanny(3234);
                Console.WriteLine("\n----NannyHours----");
                string result;
                foreach (Nanny n in bl.GetNannyList())
                {
                    result = "\n";
                    for (int i = 0; i < n.NannySchedule.Length; i++)
                    {
                        if (n.NannySchedule[i].IsWorkDay)
                        {
                            result += n.NannySchedule[i].Day + ": "; //print the days that nanny works
                            result += n.NannySchedule[i].StartHour.ToString() + " - " + n.NannySchedule[i].EndHour.ToString() + "\n";//print the hours per a day that nanny works
                        }

                    }
                    Console.WriteLine(result);
                }
                Console.WriteLine("\n----MotherHours----");
                result = "\n";
                for (int i = 0; i < bl.GetMother(3234).WantedNannySchedule.Length; i++)
                {
                    if (bl.GetMother(3234).WantedNannySchedule[i].IsWorkDay)
                    {
                        result += bl.GetMother(3234).WantedNannySchedule[i].Day + ": "; //print the days that nanny works
                        result += bl.GetMother(3234).WantedNannySchedule[i].StartHour.ToString() + " - " + bl.GetMother(3234).WantedNannySchedule[i].EndHour.ToString() + "\n";//print the hours per a day that nanny works
                    }

                }
                Console.WriteLine(result);
                Console.WriteLine("\n----GetAllMatchHoursNanny----");
                foreach (Nanny n in matchNannies)
                {
                    Console.WriteLine(n);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var noNannyChildren = bl.GetNoNannyChildrenList();
                Console.WriteLine("\n----AllContractsList----");
                foreach (Contract c in bl.GetContractList())
                {
                    Console.WriteLine(c);
                }
                Console.WriteLine("\n----GetNoNannyChildrenList----");
                foreach (Child c in noNannyChildren)
                {
                    Console.WriteLine(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllNannyList----");
                foreach (Nanny n in bl.GetNannyList())
                {
                    Console.WriteLine("nannyId: {0}, isTMT: {1}", n.Id , n.IsVacationAsTMT);
                }
                Console.WriteLine("\n----GetNannyWithTMTList----");
                var TMTNannyList = bl.GetNannyWithTMTList();
                foreach (Nanny n in TMTNannyList)
                {
                    Console.WriteLine("nannyId: {0}, isTMT: {1}", n.Id, n.IsVacationAsTMT);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllNannyList----");
                foreach (Nanny n in bl.GetNannyList())
                {
                    Console.WriteLine("nannyId: {0}, minAge: {1}, maxAge: {2}", n.Id, n.MinAge, n.MaxAge);
                }
                Console.WriteLine("\n----GroupNannyByChildAge----");
                var maxAgeNannyGroups = bl.GroupNannyByChildAge(true, true);
                var minAgeNannyGroups = bl.GroupNannyByChildAge(true, false);
                foreach (var g in maxAgeNannyGroups)
                {
                    //switch(g.Key)
                    //{
                    //    case 1:
                    //        Console.WriteLine("maxAge: 3-5:");
                    //        break;
                    //    case 2:
                    //        Console.WriteLine("maxAge: 6-8:");
                    //        break;
                    //    case 3:
                    //        Console.WriteLine("maxAge: 9-11:");
                    //        break;
                    //    default:
                    //        Console.WriteLine("maxAge: 1 year+: ");
                    //        break;

                    //}
                    
                    foreach (var n in g)
                    {
                        Console.WriteLine("nannyId: {0}", n.Id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var minAgeNannyGroups = bl.GroupNannyByChildAge(true, false);
                foreach (var g in minAgeNannyGroups)
                {
                    //switch (g.Key)
                    //{
                    //    case 1:
                    //        Console.WriteLine("minAge: 3-5:");
                    //        break;
                    //    case 2:
                    //        Console.WriteLine("minAge: 6-8:");
                    //        break;
                    //    case 3:
                    //        Console.WriteLine("minAge: 9-11:");
                    //        break;
                    //    default:
                    //        Console.WriteLine("minAge: 1 year+: ");
                    //        break;

                    //}

                    foreach (var n in g)
                    {
                        Console.WriteLine("nannyId: {0}", n.Id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----GroupContractByDistance----");
                var distanceGroups = bl.GroupContractByDistance(false);
                var orderDistanceGroups = bl.GroupContractByDistance(true);
                foreach (var g in orderDistanceGroups)
                {
                    //switch ((int)g.Key)
                    //{
                    //    case 0:
                    //        Console.WriteLine("Distance: 0-1:");
                    //        break;
                    //    case 1:
                    //        Console.WriteLine("Distance: 2-3:");
                    //        break;
                    //    case 2:
                    //        Console.WriteLine("Distance: 4-5:");
                    //        break;
                    //    case 3:
                    //        Console.WriteLine("Distance: 5-6:");
                    //        break;
                    //    case 4:
                    //        Console.WriteLine("Distance: 7-8:");
                    //        break;
                    //    case 5:
                    //        Console.WriteLine("Distance: 9-10:");
                    //        break;
                    //    default:
                    //        break;
                    //}

                    foreach (var c in g)
                    {
                        Console.WriteLine("contractId: {0}", c.ContractId);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllContractsList----");
                foreach (Contract c in bl.GetContractList())
                {
                    Console.WriteLine(c);
                }
                Console.WriteLine("\n----GroupContractByRate----");
                var rateGroups = bl.GroupContractByRate(true);
                foreach (var g in rateGroups)
            {
                    //switch ((int)g.Key)
                    //{
                    //    case 0:
                    //        Console.WriteLine("up to 1000:");
                    //        break;
                    //    case 1:
                    //        Console.WriteLine("1000-2000:");
                    //        break;
                    //case 2:
                    //    Console.WriteLine("2000-3000:");
                    //    break;
                    //default:
                    //        Console.WriteLine("3000+:");
                    //        break;
                    //}

                    foreach (var c in g)
                    {
                        Console.WriteLine("contractId: {0}, NetoRate: {1}", c.ContractId, bl.CalculateContractRate(c));
                    }
                }
        }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllChildInNannyList----");
                foreach (Contract c in bl.GetContractList(c => c.NannyId == 1237))
                {
                    Console.WriteLine("childId: {0}, BreastMilk: {1}", c.ChildId, bl.GetChild(c.ChildId).IsBreastMilk);
                }
                Console.WriteLine("\n----GroupChildrenByBreastMilk----");
                var milkGroups = bl.GroupChildrenByBreastMilk(1237);
                foreach (var g in milkGroups)
                {
                    //switch (g.Key)
                    //{
                    //    case true:
                    //        Console.WriteLine("BreastMilk:");
                    //        break;
                    //    case false:
                    //        Console.WriteLine("NoBreastMilk:");
                    //        break;
                    //    //    default:
                    //    //        break;
                    //}

                    //    foreach (var c in g)
                    //    {
                    //        Console.WriteLine("childId: {0}", c.Id, c.IsBreastMilk);
                    //    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllMothersStatus----");
                foreach (Mother m in bl.GetMotherList())
                {
                    Console.WriteLine("motherId: {0}, status: {1}", m.Id, m.Status);
                }
                Console.WriteLine("\n----GroupMotherByStatus----");
                var statusGroups = bl.GroupMotherByStatus();
                foreach (var g in statusGroups)
                {
                    switch (g.Key)
                    {
                        case MotherStatus.HouseWife:
                            Console.WriteLine("HouseWife:");
                            break;
                        case MotherStatus.MaternityLeave:
                            Console.WriteLine("MaternityLeave:");
                            break;
                        case MotherStatus.Student:
                            Console.WriteLine("Student:");
                            break;
                        case MotherStatus.Worker:
                            Console.WriteLine("Worker:");
                            break;
                        default:
                            break;
                    }

                    foreach (var m in g)
                    {
                        Console.WriteLine("motherId: {0}", m.Id );
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----GetChildAge----");
                foreach (Child c in bl.GetChildList())
                {
                    Console.WriteLine("childId: {0}, birthDate: {1}, Age: {2}", c.Id, c.BirthDate.ToShortDateString(), bl.GetChildAge(c.Id));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllContractsList----");
                foreach (Contract c in bl.GetContractList())
                {
                    Console.WriteLine("contractId: {0}, nannyId: {1} , childId: {2}", c.ContractId,c.NannyId,c.ChildId);
                }
                Console.WriteLine("\n----SearchContract----");
                Console.WriteLine(bl.SearchContract(1239, 2237));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("\n----AllChildList----");
                foreach (Child c in bl.GetChildList())
                {
                    Console.WriteLine("childId: {0}, IsFoodAllergy: {1}, IsMedicinesAllergy: {2}", c.Id, c.IsFoodAllergy, c.IsMedicinesAllergy );
                }
                Console.WriteLine("\n----GetAllergyChildren----");
                foreach (Nanny n in bl.GetNannyList())
                {
                    Console.WriteLine("nannyId: {0}", n.Id);
                    foreach (Child c in bl.GetAllergyChildren(n.Id))
                    {
                        Console.WriteLine("childId: {0}, IsFoodAllergy: {1}, IsMedicinesAllergy: {2}", c.Id, c.IsFoodAllergy, c.IsMedicinesAllergy);
                    }
                }       
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();


        }
    }
}
