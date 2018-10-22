/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp:IDal
    {
        public void AddNanny(Nanny nanny)
        {
          
            if(IdCheck(nanny.ID))
                throw new Exception("ID already exists...");          
            DS.DataSource.NannyList.Add(nanny.Clone());
        }

        public bool RemoveNanny(int id)
        {
            Nanny temp = GetNanny(id);
            if (temp == null)
                throw new Exception("No nanny with same id was found... ");
            if(GetContracts(x=>x.NannyId==id).Any())
                throw new Exception("Nanny still has existing contracts...");
            var toErase = DataSource.NannyList.First(m => m.ID == id);
            return DS.DataSource.NannyList.Remove(toErase);

        }

        public void UpdateNanny(Nanny nanny)
        {
            int index = DS.DataSource.NannyList.FindIndex(x => x.ID == nanny.ID);
            if(index==-1)
                throw new Exception("No Nanny with same id was found... ");
            DataSource.NannyList[index] = nanny.Clone();
        }
        
        public Nanny GetNanny(int id)
        {
            return DataSource.NannyList.FirstOrDefault(n => n.ID == id)?.Clone();
        }

        public void AddMother(Mother mother)
        {
            if(IdCheck(mother.ID))
                throw new Exception("ID already exists...");
            DS.DataSource.MotherList.Add(mother.Clone());
        }

        public bool RemoveMother(int id)
        {

            Mother temp = GetMother(id);
            if (temp == null)
                throw new Exception("No Mother with same id was found... ");
            List<Child> children = GetChildrenByMother(temp.ID).ToList();
            foreach (Child kid in children)
                RemoveChild(kid.ID);
            var toErase = DataSource.MotherList.First(m => m.ID == id);
            return DS.DataSource.MotherList.Remove(toErase);
            
        }

        public void UpdateMother(Mother mother)
        {
            int index = DS.DataSource.MotherList.FindIndex(x => x.ID == mother.ID);
            if (index == -1)
                throw new Exception("No mother with same id was found... ");
            DataSource.MotherList[index] = mother.Clone();
        }

        public Mother GetMother(int id)
        {
            return DataSource.MotherList.FirstOrDefault(m => m.ID == id)?.Clone();
        }

        public void AddChild(Child child)
        {
            if(IdCheck(child.ID))
                throw new Exception("ID already exists...");
            DS.DataSource.ChildrenList.Add(child.Clone());
        }

        public bool RemoveChild(int id)
        {
            Child temp = GetChild(id);
            if (temp == null)
                throw new Exception("No child with the same ID found...");
            DS.DataSource.ContractList.RemoveAll(c => c.ChildId == id);
            var toErase = DataSource.ChildrenList.First(m => m.ID == id);
            return DS.DataSource.ChildrenList.Remove(toErase);
        }

        public void UpdateChild(Child child)
        {
            int index = DS.DataSource.ChildrenList.FindIndex(c => c.ID == child.ID);
            if (index == -1)
                throw new Exception("No child with same id was found... ");
            DataSource.ChildrenList[index] = child.Clone();
        }

        public Child GetChild(int id)
        {
            return DataSource.ChildrenList.FirstOrDefault(c => c.ID == id)?.Clone();
        }

        private static int _contractID = 0;

        public void AddContract(Contract contract)
        {
            Nanny nanny = GetNanny(contract.NannyId);
            if (nanny == null)
                throw new Exception(" no Nanny with this ID already exists...");
            Mother mother = GetMother(contract.MotherId);
            if (mother == null)
                throw new Exception("no Mother with this ID already exists...");
            contract.ContractNumber = ++_contractID;

            Func<Contract, bool> predicate = item =>
            {
                bool b1 = item.NannyId == nanny.ID && item.ChildId == contract.ChildId;
                return b1;
            };
            if (DataSource.ContractList.Any(predicate)) 
                throw new Exception("contract already exists!");

            DataSource.ContractList.Add(contract.Clone());
            


        }

        public bool RemoveContract(int contractNumber)
        {
            Contract contract = GetContract(contractNumber);
            if(contract==null)
                throw new Exception("no contract with the same Contract Id exists...");
            var toErase = DataSource.ContractList.First(m => m.ContractNumber == contractNumber);
            return DS.DataSource.ContractList.Remove(toErase);
        }

        public void UpdateContract(Contract contract)
        {
            int index = DS.DataSource.ContractList.FindIndex(c => c.ContractNumber == contract.ContractNumber);
            if (index == -1)
                throw new Exception("No contract with same contract Id was found... ");
            DataSource.ContractList[index] = contract.Clone();
        }

        public Contract GetContract(int contractNumber)
        {
            return DataSource.ContractList.FirstOrDefault(c => c.ContractNumber == contractNumber)?.Clone();
        }

        public IEnumerable<Nanny> GetNannies(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.NannyList.AsEnumerable();

            return DataSource.NannyList.Where(predicate).AsEnumerable();
        }

        public IEnumerable<Mother> GetMothers(Func<Mother, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.MotherList.AsEnumerable();

            return DataSource.MotherList.Where(predicate).AsEnumerable();
        }
    

        public IEnumerable<Child> GetChildrenByMother(int id)//returns a list of children that belong to the mother
        {
            Mother mother = GetMother(id);
            if (!(DS.DataSource.MotherList.Exists(temp => temp.ID == mother.ID)))
                throw new Exception($"There is no mother: {mother?.FirstName} {mother?.LastName}");
            if (DS.DataSource.ChildrenList.Count == 0)
            {
                throw new Exception("There are no children in ChildrenList");
            }
            List<Child> result = new List<Child>();
            result.AddRange(DS.DataSource.ChildrenList.FindAll(temp => temp.MotherID == mother.ID));//creates the list
            if (result.Count == 0)//if the there is a empty list
                throw new Exception($"{mother.FirstName} {mother.LastName} has no children on the list.");
            return result;
        }

        public IEnumerable<Contract> GetContracts(Func<Contract, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.ContractList.AsEnumerable();

            return DataSource.ContractList.Where(predicate).AsEnumerable();
        }
        public bool IdCheck(int id)
        {
            var ans1 = GetMothers(m => m.ID == id).Any();
            if (!ans1)
                return false;
            var ans2 = GetNannies(n => n.ID == id).Any();
            if (!ans2)
                return false;
            var moms = GetMothers();
            var children=new List<Child>();
            foreach (var mother in moms)
            {
                children.AddRange(GetChildrenByMother(mother.ID));
            }
            var ans3 = children.Exists(c => c.ID == id);
            return ans3;
        }
    }
}
