/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDal
    {
      void AddNanny(Nanny nanny);
       bool RemoveNanny(int id);
       void UpdateNanny(Nanny nanny);
       Nanny GetNanny(int id);

       void AddMother(Mother mother);
       bool RemoveMother(int id);
       void UpdateMother(Mother mother);
        Mother GetMother(int id);

      void AddChild(Child child);
       bool RemoveChild(int id);
       void UpdateChild(Child child);
        Child GetChild(int id);

       void AddContract(Contract contract);
        bool RemoveContract(int contractNumber);
        void UpdateContract(Contract contract);
        Contract GetContract(int contractNumber);

        IEnumerable<Nanny> GetNannies(Func<Nanny,bool>predicate=null);
        IEnumerable<Mother> GetMothers(Func<Mother, bool> predicate = null);
        IEnumerable<Child> GetChildrenByMother(int id);
        IEnumerable<Contract> GetContracts(Func<Contract, bool> predicate = null);





    }
}
