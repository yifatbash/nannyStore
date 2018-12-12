using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Nanny Function
        void AddNanny(Nanny myNanny);
        bool RemoveNanny(int id);
        void UpdateNannyDetails(Nanny myNanny);
        Nanny GetNanny(int id);
        IEnumerable<Nanny> GetNannyList(Func<Nanny, bool> predicate = null);
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
        IEnumerable<Child> GetChildList(Func<Child, bool> predicate = null); //group by mothers
        int GetChildAge(int childId);
        #endregion

        #region Contract Function
        void AddContract(Contract myContract);
        bool RemoveContract(int contractId);
        void UpdateContractDetails(Contract myContract);
        Contract GetContract(int id);
        IEnumerable<Contract> GetContractList(Func<Contract, bool> predicate = null);
        Contract SearchContract(int nannyId, int childId);
        float CalculateContractRate(Contract myContract);
        bool IsValidContract(int contractId);
        float CalculateContractWeeklyHours(Contract myContract);
        float SingleMotherDiscount(int motherId, float rate);
        #endregion

        float Distance(int motherId, int nannyId);

        #region Functions Return Special NannyLists
        //Dictionary<Nanny, float> GetAllNannyByDistance(int motherId, int maxKm);
        List<Nanny> GetAllNannyByDistance(int motherId, int maxKm);
        List<Nanny> GetAllMatchHoursNanny(int motherId, bool almostMatch = true);
        List<Nanny> GetFiveAlmostMatchHoursNanny(int motherId);
        List<Nanny> GetNannyWithTMTList();
        List<Nanny> GetNannyByChildAge(int age);
        List<Nanny> GetNannyByPrice(float maxPrice = 3000, bool monthlyRate = true);
        List<Nanny> GetNannyByMotherConstraints(int motherId, int childAge, int maxPrice, bool monthlyRate, bool almostMatchNanny = true, int maxDistance = 50);
        #endregion

        #region IGrouping Functions
        IEnumerable<IGrouping<string, Nanny>> GroupNannyByChildAge(bool order = false, bool max=true);
        IEnumerable<IGrouping<string, Contract>> GroupContractByDistance(bool order = false);
        IEnumerable<IGrouping<string, Contract>> GroupContractByRate(bool order = true);
        IEnumerable<IGrouping<string, Child>> GroupChildrenByBreastMilk(int nannyId);
        IEnumerable<IGrouping<MotherStatus, Mother>> GroupMotherByStatus();
        #endregion

        #region Functions Return Special ChildLists
        List<Child> GetNoNannyChildrenList();
        List<Child> GetChildrenListByNannyId(int nannyId);
        List<Child> GetAllergyChildren(int nannyId);
        #endregion

        #region initialization functions
        void InitNannyList();
        void InitChildList();
        void InitMotherList();
        void InitContractList();
        void Init();
        #endregion

        #region Google Functions
        List<string> GetPlaceAutoComplete(string str);
        int CalculateDistance(string source, string dest);
        #endregion
    }
}
