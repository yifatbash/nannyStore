using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        #region Nanny Function
        void AddNanny(Nanny myNanny);
        bool RemoveNanny(int id);
        void UpdateNannyDetails(Nanny myNanny);
        Nanny GetNanny(int id);
        IEnumerable<Nanny> GetNannyList(Func<Nanny,bool> predicate = null);
        #endregion

        #region Mother Function
        void AddMother(Mother myMother);
        bool RemoveMother(int id);
        void UpdateMotherDetails(Mother myMother);
        Mother GetMother(int id);
        IEnumerable<Mother> GetMotherList(Func<Mother, bool> predicate = null);

        #endregion

        #region Child Function
        void AddChild(Child myChild);
        bool RemoveChild(int id);
        void UpdateChildDetails(Child myChild);
        Child GetChild(int id);
        IEnumerable<Child> GetChildList(Func<Child, bool> predicate = null); 
        #endregion

        #region Contract Function
        void AddContract(Contract myContract);
        bool RemoveContract(int contractId);
        void UpdateContractDetails(Contract myContract);
        Contract GetContract(int id);
        IEnumerable<Contract> GetContractList(Func<Contract, bool> predicate = null);
        #endregion

    }
}
