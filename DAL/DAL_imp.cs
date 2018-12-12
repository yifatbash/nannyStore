using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using static DS.DataSource;

namespace DAL
{
    public class DAL_imp : IDAL
    {

        #region Nanny Functions
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

            DataSource.contractList.RemoveAll(c => c.NannyId == id); //remove all the nanny's contracts that can be deleted (checked in the BL)
            return DataSource.nannyList.Remove(myNanny);  //remove the nanny
        }

        /// <summary>
        /// the function get a nanny and update here details
        /// </summary>
        /// <param name="myNanny">the nanny should be updated</param>
        public void UpdateNannyDetails(Nanny myNanny)
        {
            int index = nannyList.FindIndex(n => n.Id == myNanny.Id);
            if (index == -1)
                throw new Exception("Nanny with the same id was not found...");
            nannyList[index] = myNanny;
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
        public IEnumerable<Nanny>  GetNannyList(Func<Nanny, bool> predicate= null)
        {
            if (predicate == null)
                return DataSource.nannyList;

            return nannyList.Where<Nanny>(predicate);
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

            //collect the mother's contracts
            var contractsToDelete = from contract in GetContractList() 
                                   let child = GetChild(contract.ChildId)
                                   where child.MotherId == id
                                   select contract;
            foreach(var item in contractsToDelete) //remove all the mother's contracts that can be deleted (checked in the BL)
                DataSource.contractList.RemoveAll(c => c.ContractId == item.ContractId);

            DataSource.childList.RemoveAll(c => c.MotherId == id); //remove all the mother's children

            return DataSource.motherList.Remove(myMother);  //remove the mother
        }

        /// <summary>
        /// the function get a mother and update here details
        /// </summary>
        /// <param name="myMother">the mother should be updated</param>
        public void UpdateMotherDetails(Mother myMother)
        {
            int index = motherList.FindIndex(m => m.Id == myMother.Id);
            if (index == -1)
                throw new Exception("Mother with the same id was not found...");
            motherList[index] = myMother;
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

            return motherList.Where<Mother>(predicate);
        }
        #endregion

        #region Child Functions
        /// <summary>
        /// the function get a new child and add to the list
        /// </summary>
        /// <param name="myNanny">the new child should be added</param>
        public void AddChild(Child myChild)
        {
            Child _child = GetChild(myChild.Id);
            if (_child != null) //check if child id already exist
                throw new Exception("The child id is already exist in the DB"); //Case the child id exists
            if (!GetMotherList().Any<Mother>(m => m.Id == myChild.MotherId))
                throw new Exception("Mother doesn't exist");
            DataSource.childList.Add(myChild); //Add the child
        }

        /// <summary>
        /// the function get a child and remove from the list
        /// </summary>
        /// <param name="id">the id of child should be removed</param>
        public bool RemoveChild(int id)
        {
            Child myChild = GetChild(id);
            if (myChild == null) //check if child with the same id exists in the list
                throw new Exception("Child with the same id was not found..");
            DataSource.contractList.RemoveAll(c => c.ChildId == id); //remove all the child's contracts

            return DataSource.childList.Remove(myChild);  //remove the child
        }

        /// <summary>
        /// the function get a child and update his details
        /// </summary>
        /// <param name="myChild">the child should be updated</param>
        public void UpdateChildDetails(Child myChild)
        {
            int index = childList.FindIndex(n => n.Id == myChild.Id);
            if (index == -1)
                throw new Exception("Child with the same id was not found...");
           childList[index] = myChild;
        }

        /// <summary>
        /// function search for a child according to given id
        /// </summary>
        /// <param name="id">wanted child id</param>
        /// <returns>return the first child with the given id in case it exists in the list</returns>
        public Child GetChild(int id)
        {
            return DataSource.childList.FirstOrDefault<Child>(c => c.Id == id);
        }

        /// <summary>
        /// the function get condition and return all the children that fit the given condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>all the children that are fit the given condition</returns>
        public IEnumerable<Child> GetChildList(Func<Child, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.childList;

            return childList.Where<Child>(predicate);
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
                myContract.ContractId = Contract.contractIdCode; //intilaze contractId with uniqe running number
                Contract.contractIdCode++;
            }

            DataSource.contractList.Add(myContract); //Add the contract
        }

        /// <summary>
        /// the function get a contract and remove from the list
        /// </summary>
        /// <param name="id">the id of contract should be removed</param>
        public bool RemoveContract(int contractId)
        {
            try
            {
                DataSource.contractList.RemoveAll(c => c.ContractId == contractId); //remove contract
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
            int index = contractList.FindIndex(c => c.ContractId == myContract.ContractId);
            if (index == -1)
                throw new Exception("Contract was not found...");
            
            contractList[index] = myContract;
        }

        /// <summary>
        /// function search for a contract according to given contract id
        /// </summary>
        /// <param name="id">wanted contract id</param>
        /// <returns>return the first contract with the given id in case it exists in the list</returns>
        public Contract GetContract(int id)
        {
            return DataSource.contractList.FirstOrDefault<Contract>(c => c.ContractId == id);
        }

        /// <summary>
        /// the function get condition and return all the contracts that fit the given condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>all the contracts that are fit the given condition</returns>
        public IEnumerable<Contract> GetContractList(Func<Contract, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.contractList;

            return contractList.Where<Contract>(predicate);
        }
        #endregion

    }
}
