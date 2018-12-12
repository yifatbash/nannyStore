using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DS;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        string nannyPath = @"nannyXml.xml";
        string motherPath = @"motherXml.xml";
        XElement childRoot;
        string childPath = @"childXml.xml";
        XElement contractRoot;
        string contractPath = @"contractXml.xml";
        XElement configRoot;
        string configPath = @"config.xml";

        public Dal_XML_imp()
        {

            if (!File.Exists(childPath))
                CreateFiles(childPath);
            else
                LoadData(childPath);


            if (!File.Exists(contractPath))
                CreateFiles(contractPath);
            else
                LoadData(contractPath);

            if (!File.Exists(configPath))
                CreateFiles(configPath);
            else
                LoadData(configPath);

            DataSource.motherList = LoadFromXML<List<Mother>>(motherPath);
            if (DataSource.motherList == null)
            {
                DataSource.motherList = new List<Mother>();
            }
            DataSource.nannyList = LoadFromXML<List<Nanny>>(nannyPath);
            if (DataSource.nannyList == null)
            {
                DataSource.nannyList = new List<Nanny>();
            }
        }

        private void CreateFiles(string path)
        {
            switch(path)
            {
                case "childXml.xml":
                    childRoot = new XElement("children");
                    childRoot.Save(childPath);
                    break;
                case "contractXml.xml":
                    contractRoot = new XElement("contracts");
                    contractRoot.Save(contractPath);
                    break;
                case "config.xml":
                    configRoot = new XElement("config");
                    XElement contractIdCode = new XElement("contractIdCode", "10000001");
                    configRoot.Add(contractIdCode);
                    configRoot.Save(configPath);
                    break;
            }

        }

        private void LoadData(string path)
        {
            try
            {
                switch(path)
                {
                    case "contractXml.xml":
                        contractRoot = XElement.Load(contractPath);
                        break;
                    case "childXml.xml":
                        childRoot = XElement.Load(childPath);
                        break;
                    case "config.xml":
                        configRoot = XElement.Load(configPath);
                        break;
                }  
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        XElement ConvertContractToElement(Contract student)
        {
            XElement contractElement = new XElement("contract");

            foreach (PropertyInfo item in typeof(Contract).GetProperties())
                contractElement.Add
                    (
                    new XElement(item.Name, item.GetValue(student, null)?.ToString())
                    );

            return contractElement;
        }

        BE.Contract ConvertElementToContract(XElement element)
        {
            Contract contract = new Contract();

            foreach (PropertyInfo item in typeof(Contract).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                if (item.CanWrite)
                    item.SetValue(contract, convertValue);
            }

            return contract;
        }

        public void SaveContractListLinq(List<Contract> contractList)
        {
            var c = from con in contractList
                    select ConvertContractToElement(con);

            contractRoot = new XElement("contracts", c);
            contractRoot.Save(contractPath);
        }

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)
        {
            if(File.Exists(path))
            {
                FileStream file = new FileStream(path, FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                if (file.Length != 0)
                {
                    T result = (T)xmlSerializer.Deserialize(file);
                    file.Close();
                    return result;
                }

                file.Close();
                return default(T);
            }
            else
            {
                FileStream file = new FileStream(path, FileMode.Create);
                return default(T);
            }
         }


        #region Nanny Function
        /// <summary>
        /// the function get a new nanny and add to the list
        /// </summary>
        /// <param name="myNanny">the new nanny should be added</param>
        public void AddNanny(Nanny myNanny)
        {
            Nanny _nanny = GetNanny(myNanny.Id);
            if (_nanny != null) //check if nanny id already exist
                throw new Exception("The Nanny id is already exist in the DB"); //Case the Nanny id exists
            DataSource.nannyList.Add(myNanny); //Add the Nanny

            SaveToXML<List<Nanny>>(DataSource.nannyList, nannyPath);
        }
        /// <summary>
        /// the function get a nanny and remove from the list
        /// </summary>
        /// <param name="id">the id of nanny should be removed</param>
        public bool RemoveNanny(int id)
        {
            Nanny myNanny = GetNanny(id);
            if (myNanny == null) //check if nanny with the same id exists in the list
                throw new Exception("Nanny with the same id was not found..");
            try
            {
                DataSource.contractList= GetContractList().ToList();
                DataSource.contractList.RemoveAll(c => c.NannyId == id); //remove all the nanny's contracts that can be deleted (checked in the BL)
                DataSource.nannyList.Remove(myNanny);  //remove the nanny

                SaveContractListLinq(DataSource.contractList);
                SaveToXML<List<Nanny>>(DataSource.nannyList, nannyPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// the function get a nanny and update here details
        /// </summary>
        /// <param name="myNanny">the nanny should be updated</param>
        public void UpdateNannyDetails(Nanny myNanny)
        {
            int index = DataSource.nannyList.FindIndex(n => n.Id == myNanny.Id);
            if (index == -1)
                throw new Exception("Nanny with the same id was not found...");
            DataSource.nannyList[index] = myNanny;

            SaveToXML<List<Nanny>>(DataSource.nannyList, nannyPath);
        }
        /// <summary>
        /// function search for a nanny according to given id
        /// </summary>
        /// <param name="id">wanted nanny id</param>
        /// <returns>return the first nanny with the given id in case it exists in the list</returns>
        public Nanny GetNanny(int id)
        {
            return DataSource.nannyList.FirstOrDefault<Nanny>(n => n.Id == id);
        }
        /// <summary>
        /// the function get condition and return all the nannies that fit the given condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>all the nannies that are fit the given condition</returns>
        public IEnumerable<Nanny> GetNannyList(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.nannyList;

            return DataSource.nannyList.Where<Nanny>(predicate);
        }
        #endregion

        #region Mother Functions
        /// <summary>
        /// the function get a new mother and add to the list
        /// </summary>
        /// <param name="myMother">the new mother should be added</param>
        public void AddMother(Mother myMother)
        {
            Mother _mother = GetMother(myMother.Id);
            if (_mother != null) //check if mother id already exist
                throw new Exception("The mother id is already exist in the DB"); //Case the mother id exists
            DataSource.motherList.Add(myMother); //Add the mother

            SaveToXML<List<Mother>>(DataSource.motherList, motherPath);
        }

        /// <summary>
        /// the function get a mother and remove from the list
        /// </summary>
        /// <param name="id">the id of mother should be removed</param>
        public bool RemoveMother(int id)
        {
            Mother myMother = GetMother(id);
            if (myMother == null) //check if mother with the same id exists in the list
                throw new Exception("Mother with the same id was not found..");

            try
            {
                //collect the mother's contracts
                var contractsToDelete = from contract in GetContractList()
                                        let child = GetChild(contract.ChildId)
                                        where child.MotherId == id
                                        select contract;
                foreach (var item in contractsToDelete) //remove all the mother's contracts that can be deleted (checked in the BL)
                    DataSource.contractList.RemoveAll(c => c.ContractId == item.ContractId);
                SaveContractListLinq(DataSource.contractList);

                DataSource.childList.RemoveAll(c => c.MotherId == id); //remove all the mother's children
                SaveToXML<List<Child>>(DataSource.childList, childPath);

                DataSource.motherList.Remove(myMother);  //remove the mother
                SaveToXML<List<Mother>>(DataSource.motherList, motherPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// the function get a mother and update here details
        /// </summary>
        /// <param name="myMother">the mother should be updated</param>
        public void UpdateMotherDetails(Mother myMother)
        {
            int index = DataSource.motherList.FindIndex(m => m.Id == myMother.Id);
            if (index == -1)
                throw new Exception("Mother with the same id was not found...");
            DataSource.motherList[index] = myMother;
            SaveToXML<List<Mother>>(DataSource.motherList, motherPath);
        }

        /// <summary>
        /// function search for a mother according to given id
        /// </summary>
        /// <param name="id">wanted mother id</param>
        /// <returns>return the first mother with the given id in case it exists in the list</returns>
        public Mother GetMother(int id)
        {
            return DataSource.motherList.FirstOrDefault<Mother>(m => m.Id == id);
        }

        /// <summary>
        /// the function get condition and return all the mothers that fit the given condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>all the mothers that are fit the given condition</returns>
        public IEnumerable<Mother> GetMotherList(Func<Mother, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.motherList;

            return DataSource.motherList.Where<Mother>(predicate);
        }
        #endregion

        #region Child Function
        /// <summary>
        /// the function get a new child and add to the list
        /// </summary>
        /// <param name="myNanny">the new child should be added</param>
        public void AddChild(Child myChild)
        {
            Child _child = GetChild(myChild.Id);
            if (_child != null) //check if child id already exist
                throw new Exception("The child id is already exist in the DB"); //Case the child id exists
            Mother _mother = GetMother(myChild.MotherId);
            if (_mother == null)
                throw new Exception("Mother doesn't exist");

            childRoot.Add(
                new XElement("child",
                    new XElement("id", myChild.Id),
                    new XElement("motherId", myChild.MotherId),
                    new XElement("firstName", myChild.FirstName),
                    new XElement("birthDate", myChild.BirthDate),
                    new XElement("SpecialNeedsChild",
                        new XElement("isSpecialNeedsChild", myChild.IsSpecialNeedsChild),
                        new XElement("specialNeedsDescription", myChild.SpecialNeeds)
                    ),
                    new XElement("FoodAllergy",
                        new XElement("isFoodAllergy", myChild.IsFoodAllergy),
                        new XElement("foodAllergyDescription", myChild.FoodAllergy)
                    ),
                    new XElement("MedicinesAllergy",
                        new XElement("isMedicinesAllergy", myChild.IsMedicinesAllergy),
                        new XElement("medicinesAllergyDescription", myChild.MedicinesAllergy)
                    ),
                    new XElement("isBreastMilk", myChild.IsBreastMilk),
                    new XElement("comments", myChild.Comments)
                    ));

            childRoot.Save(childPath);
        }
        /// <summary>
        /// the function get a child and remove from the list
        /// </summary>
        /// <param name="id">the id of child should be removed</param>
        public bool RemoveChild(int id)
        {
            try
            {
                XElement childToRemove = (from c in childRoot.Elements()
                                          where int.Parse(c.Element("id").Value) == id
                                          select c).FirstOrDefault();

                if (childToRemove == null) //check if child with the same id exists in the list
                    throw new Exception("Child with the same id not found...");

                //remove the child's contracts
                XElement contractToRemove = (from c in contractRoot.Elements()
                                 where int.Parse(c.Element("childId").Value) == id
                                 select c).FirstOrDefault();
                if(contractToRemove!=null)
                    contractToRemove.Remove();
                contractRoot.Save(contractPath);

                //remove the child
                childToRemove.Remove();
                childRoot.Save(childPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// the function get a child and update his details
        /// </summary>
        /// <param name="myChild">the child should be updated</param>
        public void UpdateChildDetails(Child myChild)
        {
            XElement toUpdate = (from c in childRoot.Elements()
                                 where int.Parse(c.Element("id").Value) == myChild.Id
                                 select c).FirstOrDefault();

            if (toUpdate == null)
                throw new Exception("Child with the same id not found...");

            toUpdate.Element("motherId").Value = myChild.MotherId.ToString();
            toUpdate.Element("firstName").Value = myChild.FirstName;
            toUpdate.Element("birthDate").Value = myChild.BirthDate.ToString();
            toUpdate.Element("SpecialNeedsChild").Element("isSpecialNeedsChild").Value = myChild.IsSpecialNeedsChild.ToString();
            toUpdate.Element("SpecialNeedsChild").Element("specialNeedsDescription").Value = myChild.SpecialNeeds;
            toUpdate.Element("FoodAllergy").Element("isFoodAllergy").Value = myChild.IsFoodAllergy.ToString();
            toUpdate.Element("FoodAllergy").Element("foodAllergyDescription").Value = myChild.FoodAllergy.ToString();
            toUpdate.Element("MedicinesAllergy").Element("isMedicinesAllergy").Value = myChild.IsMedicinesAllergy.ToString();
            toUpdate.Element("MedicinesAllergy").Element("medicinesAllergyDescription").Value = myChild.MedicinesAllergy.ToString();
            toUpdate.Element("isBreastMilk").Value = myChild.IsBreastMilk.ToString();
            toUpdate.Element("comments").Value = myChild.Comments;

            childRoot.Save(childPath);
        }
        /// <summary>
        /// function search for a child according to given id
        /// </summary>
        /// <param name="id">wanted child id</param>
        /// <returns>return the first child with the given id in case it exists in the list</returns>
        public Child GetChild(int id)
        {
            LoadData(childPath);
            XElement x_child = null;

            try
            {
                x_child = (from c in childRoot.Elements()
                           where int.Parse(c.Element("id").Value) == id
                           select c).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (x_child == null)
                return null;

            Child child = new Child
            {
                Id = int.Parse(x_child.Element("id").Value),
                MotherId = int.Parse(x_child.Element("motherId").Value),
                FirstName = x_child.Element("firstName").Value,
                BirthDate = DateTime.Parse(x_child.Element("birthDate").Value),
                IsSpecialNeedsChild = bool.Parse(x_child.Element("SpecialNeedsChild").Element("isSpecialNeedsChild").Value),
                SpecialNeeds = x_child.Element("SpecialNeedsChild").Element("specialNeedsDescription").Value,
                IsFoodAllergy = bool.Parse(x_child.Element("FoodAllergy").Element("isFoodAllergy").Value),
                FoodAllergy = x_child.Element("FoodAllergy").Element("foodAllergyDescription").Value,
                IsMedicinesAllergy = bool.Parse(x_child.Element("MedicinesAllergy").Element("isMedicinesAllergy").Value),
                MedicinesAllergy = x_child.Element("MedicinesAllergy").Element("medicinesAllergyDescription").Value,
                IsBreastMilk = bool.Parse(x_child.Element("isBreastMilk").Value),
                Comments = x_child.Element("comments").Value
            };

            return child;
        }
        /// <summary>
        /// the function get condition and return all the children that fit the given condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>all the children that are fit the given condition</returns>
        public IEnumerable<Child> GetChildList(Func<Child, bool> predicate = null)
        {
            LoadData(childPath);
            List<Child> childList;
            try
            {
                if (predicate == null)
                {
                    childList = (from c in childRoot.Elements()
                                 select new Child
                                 {
                                     Id = int.Parse(c.Element("id").Value),
                                     MotherId = int.Parse(c.Element("motherId").Value),
                                     FirstName = c.Element("firstName").Value,
                                     BirthDate = DateTime.Parse(c.Element("birthDate").Value),
                                     IsSpecialNeedsChild = bool.Parse(c.Element("SpecialNeedsChild").Element("isSpecialNeedsChild").Value),
                                     SpecialNeeds = c.Element("SpecialNeedsChild").Element("specialNeedsDescription").Value,
                                     IsFoodAllergy = bool.Parse(c.Element("FoodAllergy").Element("isFoodAllergy").Value),
                                     FoodAllergy = c.Element("FoodAllergy").Element("foodAllergyDescription").Value,
                                     IsMedicinesAllergy = bool.Parse(c.Element("MedicinesAllergy").Element("isMedicinesAllergy").Value),
                                     MedicinesAllergy = c.Element("MedicinesAllergy").Element("medicinesAllergyDescription").Value,
                                     IsBreastMilk = bool.Parse(c.Element("isBreastMilk").Value),
                                     Comments = c.Element("comments").Value
                                 })?.ToList();
                }
                else
                {
                    childList = (from c in childRoot.Elements()
                                 let ch = new Child
                                 {
                                     Id = int.Parse(c.Element("id").Value),
                                     MotherId = int.Parse(c.Element("motherId").Value),
                                     FirstName = c.Element("firstName").Value,
                                     BirthDate = DateTime.Parse(c.Element("birthDate").Value),
                                     IsSpecialNeedsChild = bool.Parse(c.Element("SpecialNeedsChild").Element("isSpecialNeedsChild").Value),
                                     SpecialNeeds = c.Element("SpecialNeedsChild").Element("specialNeedsDescription").Value,
                                     IsFoodAllergy = bool.Parse(c.Element("FoodAllergy").Element("isFoodAllergy").Value),
                                     FoodAllergy = c.Element("FoodAllergy").Element("foodAllergyDescription").Value,
                                     IsMedicinesAllergy = bool.Parse(c.Element("MedicinesAllergy").Element("isMedicinesAllergy").Value),
                                     MedicinesAllergy = c.Element("MedicinesAllergy").Element("medicinesAllergyDescription").Value,
                                     IsBreastMilk = bool.Parse(c.Element("isBreastMilk").Value),
                                     Comments = c.Element("comments").Value
                                 }
                                 where predicate(ch)
                                 select ch)?.ToList();
                }

            }
            catch
            {
                childList = null;
            }

            if (childList == null)
                childList = new List<Child>();
            return childList;
        }
        #endregion

        #region Contract Functions
        /// <summary>
        /// the function get a new contract and add to the list
        /// </summary>
        /// <param name="myContract">the new contract should be added</param>
        public void AddContract(Contract myContract)
        {
            if (myContract.ContractId == 0) //if contarct doesn't have a running number already
            {
                myContract.ContractId = int.Parse(configRoot.Element("contractIdCode").Value); //intilaze contractId with uniqe running number
                int newCode = int.Parse(configRoot.Element("contractIdCode").Value) + 1;
                configRoot.Element("contractIdCode").SetValue(newCode.ToString());
                configRoot.Save(configPath);
                //myContract.ContractId = Contract.contractIdCode; //intilaze contractId with uniqe running number
                //Contract.contractIdCode++;
            }
            contractRoot.Add(ConvertContractToElement(myContract));//Add the contract
            contractRoot.Save(contractPath);
        }
        /// <summary>
        /// the function get a contract and remove from the list
        /// </summary>
        /// <param name="id">the id of contract should be removed</param>
        public bool RemoveContract(int contractId)
        {
            try
            {
                XElement contractToRemove = (from c in contractRoot.Elements()
                                     where int.Parse(c.Element("ContractId").Value) == contractId
                                     select c).FirstOrDefault();
                contractToRemove.Remove();
                contractRoot.Save(contractPath);
                return true; // removed successfully 
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// the function get a contract and update it's details
        /// </summary>
        /// <param name="myContract">the contract should be updated</param>
        public void UpdateContractDetails(Contract myContract)
        {
            //XElement contractToUpdate = (from c in contractRoot.Elements()
            //                     where int.Parse(c.Element("ContractId").Value) == myContract.ContractId
            //                     select c).FirstOrDefault();

            //if(contractToUpdate == null)
            //    throw new Exception("Contract was not found...");

            //myContract.ContractId = int.Parse(contractToUpdate.Element("ContractId").Value);

            //foreach (PropertyInfo item in typeof(Contract).GetProperties())
            //    contractToUpdate.Element(item.Name).SetValue(item.GetValue(myContract));
            DataSource.contractList = GetContractList().ToList();
            int index = DataSource.contractList.FindIndex(c => c.ContractId == myContract.ContractId);
            if (index == -1)
                throw new Exception("Contract was not found...");
            DataSource.contractList[index] = myContract;
            SaveContractListLinq(DataSource.contractList);
         }
        /// <summary>
        /// function search for a contract according to given contract id
        /// </summary>
        /// <param name="id">wanted contract id</param>
        /// <returns>return the first contract with the given id in case it exists in the list</returns>
        public Contract GetContract(int id)
        {
            LoadData(contractPath);
            XElement contractElement;
            try
            {
                contractElement = (from c in contractRoot.Elements()
                                       where int.Parse(c.Element("ContractId").Value) == id
                                   select c).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            if (contractElement == null)
                return null;

            Contract contract = ConvertElementToContract(contractElement);
            return contract;
        }
        /// <summary>
        /// the function get condition and return all the contracts that fit the given condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>all the contracts that are fit the given condition</returns>
        public IEnumerable<Contract> GetContractList(Func<Contract, bool> predicate = null)
        {
            LoadData(contractPath);
            List<Contract> contractList;
            try
            {
                if (predicate == null)
                {
                    contractList = (from c in contractRoot.Elements()
                                    select ConvertElementToContract(c))?.ToList();
                }
                else
                {
                    contractList = (from c in contractRoot.Elements()
                                    let contract = ConvertElementToContract(c)
                                    where predicate(contract)
                                    select ConvertElementToContract(c))?.ToList();
                }
            }
            catch
            {
                contractList = null;
            }

            if (contractList == null)
                contractList = new List<Contract>();

            return contractList;
        }
        #endregion
    }

}

