using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using DS;
using static DS.DSxml;

namespace DAL
{
    internal class Dal_XML_imp : IDal
    {
        public void AddNanny(Nanny nanny)
        {
            
            if (!idCheck(nanny.ID))
            {
                Nannies.Add(nanny.toXML());
                SaveNannies();
            }
            else throw new Exception("ID already exists...");
        }

        public bool RemoveNanny(int id)
        {
            if (Contracts.Elements().Any(contract => Convert.ToInt32(contract.Element("NannyID")) == id))
            {
                throw new Exception("Nanny still has existing contracts...");
            }
            var temp = (from nanny in Nannies.Elements()
                where Convert.ToInt32(nanny.Element("ID").Value) == id
                select nanny).FirstOrDefault();
            if (temp == null)
                throw new Exception("No nanny with same id was found... ");
            temp.Remove();
            SaveNannies();
            return true;
        }

        public void UpdateNanny(Nanny n)
        {
            var updateNanny = (from nanny in Nannies.Elements()
                where Convert.ToInt32(nanny.Element("ID").Value) == n.ID
                select nanny).FirstOrDefault();
            if (updateNanny == null)
                throw new Exception("No mother with the same id...");
            var i = 0;
            foreach (var xElement in updateNanny.Elements())
                if (xElement.Name.ToString() == "Schedule")
                    foreach (var xElement1 in updateNanny.Element("Schedule").Elements("Day"))
                    {
                        xElement1.Element("IsWorking").Value = n.Schedule[i].IsWorking.ToString();
                        foreach (var element in xElement1.Element("Start").Elements())
                            switch (element.Name.ToString())
                            {
                                case "Hour":

                                    element.Value = (!(n.Schedule[i].IsWorking))?"0": n.Schedule[i].StartTime.Hour.ToString();
                                    break;
                                case "Minute":
                                    element.Value = (!(n.Schedule[i].IsWorking)) ? "0" : n.Schedule[i].StartTime.Minute.ToString();
                                    break;
                            }
                        foreach (var element in xElement1.Element("End").Elements())
                            switch (element.Name.ToString())
                            {
                                case "Hour":
                                    element.Value = (!(n.Schedule[i].IsWorking)) ? "0" : n.Schedule[i].EndTime.Hour.ToString();
                                    break;
                                case "Minute":
                                    element.Value = (!(n.Schedule[i].IsWorking)) ? "0" : n.Schedule[i].EndTime.Minute.ToString();
                                    break;
                            }
                        i++;
                    }
                else
                    updateNanny.Element(xElement.Name).Value = xElement.Value;

            SaveNannies();
        }

        public Nanny GetNanny(int id)
        {
            return ((from nanny in Nannies.Elements()
                where Convert.ToInt32(nanny.Element("ID").Value) == id
                select nanny).FirstOrDefault()).ToNanny();
        }

        public void AddMother(Mother mother)
        {

            if (idCheck(mother.ID))
            {
                throw new Exception("ID already exists...");
            }
            Mothers.Add(mother.toXML());
            SaveMothers();
        }

        public bool RemoveMother(int id)
        {
            var temp = (from mother in Mothers.Elements()
                where Convert.ToInt32(mother.Element("ID").Value) == id
                select mother).FirstOrDefault();
            if (temp == null)
                throw new Exception("No mother with the same id...");
            temp.Remove();
            SaveMothers();
            var kids = GetChildrenByMother(id).ToList();
            foreach (Child kid in kids)
                RemoveChild(kid.ID);
            var contracts = GetContracts(c => c.MotherId == id).ToList();
            foreach (var c in contracts)
            {
                RemoveContract(c.ContractNumber);
            }
            return true;
        }

        public void UpdateMother(Mother m)
        {
            XElement updateMother = (from mother in Mothers.Elements()
                where Convert.ToInt32(mother.Element("ID").Value) == m.ID
                select mother).FirstOrDefault();
            if (updateMother == null)
                throw new Exception("No mother with the same id...");
            int i = 0;
            foreach (var xElement in m.toXML().Elements())
            {
                if (xElement.Name.ToString() == "Schedule")
                    foreach (var xElement1 in updateMother.Element("Schedule").Elements("Day"))
                    {
                        xElement1.Element("IsWorking").Value = m.Schedule[i].IsWorking.ToString();
                        foreach (var element in xElement1.Element("Start").Elements())
                            switch (element.Name.ToString())
                            {
                                case "Hour":

                                    element.Value = (!(m.Schedule[i].IsWorking)) ? "0" : m.Schedule[i].StartTime.Hour.ToString();
                                    break;
                                case "Minute":
                                    element.Value = (!(m.Schedule[i].IsWorking)) ? "0" : m.Schedule[i].StartTime.Minute.ToString();
                                    break;
                            }
                        foreach (var element in xElement1.Element("End").Elements())
                            switch (element.Name.ToString())
                            {
                                case "Hour":
                                    element.Value = (!(m.Schedule[i].IsWorking)) ? "0" : m.Schedule[i].EndTime.Hour.ToString();
                                    break;
                                case "Minute":
                                    element.Value = (!(m.Schedule[i].IsWorking)) ? "0" : m.Schedule[i].EndTime.Minute.ToString();
                                    break;
                            }
                        i++;
                    }
                else
                    updateMother.Element(xElement.Name).Value = xElement.Value;
            }
            SaveMothers();
        }

        public Mother GetMother(int id)
        {
            var m = (from mother in Mothers.Elements()
                where Convert.ToInt32(mother.Element("ID").Value) == id
                select mother).FirstOrDefault().ToMother();
            return m;
        }
        public void AddChild(Child child)
        {
            if (!idCheck(child.ID))
            {
                Children.Add(child.toXML());
                SaveChildren();
            }
            else throw new Exception("ID already exists...");
        }

        public bool RemoveChild(int id)
        {
            var temp = (from child in Children.Elements()
                where Convert.ToInt32(child.Element("ID").Value) == id
                select child).FirstOrDefault();
            if (temp == null)
                throw new Exception("No child with the same id...");
            foreach (var contract in GetContracts().Where(c=>c.ChildId==id))
            {
                RemoveContract(contract.ContractNumber);
            }
            temp.Remove();
            SaveChildren();
            SaveContracts();
            return true;
        }

        public void UpdateChild(Child c)
        {
            XElement updateChild = (from child in Children.Elements()
                where Convert.ToInt32(child.Element("ID").Value) == c.ID
                select child).FirstOrDefault();
            if (updateChild == null)
                throw new Exception("No child with the same id...");
            foreach (var xElement in c.toXML().Elements())
            {
                updateChild.Element(xElement.Name).Value = xElement.Value;
            }
            
                
            SaveChildren();
        }

        public Child GetChild(int id)
        {
            return ((from child in Children.Elements()
                where Convert.ToInt32(child.Element("ID").Value) == id
                select child).FirstOrDefault()).ToChild();
        }

         public void AddContract(Contract c)
        {
            Nanny nanny = GetNanny(c.NannyId);
            if (nanny == null)
                throw new Exception("no Nanny with this ID already exists...");
            Mother mother = GetMother(c.MotherId);
            if (mother == null)
                throw new Exception("no Mother with this ID already exists...");

            Func<Contract, bool> predicate = item =>
            {
                bool b1 = (item.NannyId == nanny.ID && item.ChildId == c.ChildId);
                return b1;
            };
            if (GetContracts().Any(cont => cont.ContractNumber == c.ContractNumber)||GetContracts().Any(predicate))
            {
                throw new Exception("Contract already exist!");
            }
            XElement contract = c.toXML();
            contract.Element("ContractNumber").Value = (MaxContractID() + 1).ToString();
            DS.DSxml.Contracts.Add(contract);
            DS.DSxml.SaveContracts();
        }

        public bool RemoveContract(int contractNumber)
        {
            var temp = (from contract in Contracts.Elements()
                where Convert.ToInt32(contract.Element("ContractNumber").Value) == contractNumber
                select contract).FirstOrDefault();
            if (temp == null)
                throw new Exception("No contract with the same contract number...");
            temp.Remove();
            SaveContracts();
            return true;
        }

        public void UpdateContract(Contract contract)
        {
            XElement updateContract = (from cont in Contracts.Elements()
                where Convert.ToInt32(cont.Element("ContractNumber").Value) == contract.ContractNumber
                select cont).FirstOrDefault();
            if (updateContract == null)
                throw new Exception("No child with the same id...");
            foreach (var xElement in contract.toXML().Elements())
            {
                updateContract.Element(xElement.Name).Value = xElement.Value;
            }
            SaveContracts();
        }

        public Contract GetContract(int contractNumber)
        {
            return ((from contract in Contracts.Elements()
                where Convert.ToInt32(contract.Element("ContractNumber").Value) == contractNumber
                     select contract).FirstOrDefault()).ToContract();
        }

        public IEnumerable<Nanny> GetNannies(Func<Nanny, bool> predicate = null)
        {
            XElement root = DS.DSxml.Nannies;
            List<Nanny> result = new List<Nanny>();
            foreach (var n in root.Elements("Nanny"))
            {
                result.Add(n.ToNanny());
            }
            return (predicate==null)?result.AsEnumerable():result.Where(predicate).AsEnumerable();
        }

        public IEnumerable<Mother> GetMothers(Func<Mother, bool> predicate = null)
        {
            XElement root = DS.DSxml.Mothers;
            List<Mother> result = new List<Mother>();
            foreach (var m in root.Elements("Mother"))
            {
                result.Add(m.ToMother());
            }
            return (predicate == null) ? result.AsEnumerable() : result.Where(predicate).AsEnumerable();
        }

        public IEnumerable<Child> GetChildrenByMother(int id)
        {
            XElement root = DS.DSxml.Children;
            List<Child> result = new List<Child>();
            foreach (var c in root.Elements("Child"))
            {
                result.Add(c.ToChild());
            }
            return result.Where(c => c.MotherID == id).AsEnumerable();
        }

        public IEnumerable<Contract> GetContracts(Func<Contract, bool> predicate = null)
        {
            XElement root = DS.DSxml.Contracts;
            List<Contract> result = new List<Contract>();
            foreach (var c in root.Elements("Contract"))
            {
                result.Add(c.ToContract());
            }
            return (predicate==null)? result.AsEnumerable(): result.Where(predicate).AsEnumerable();
        }
       
         

        private int MaxContractID()
        {
            int result;
            var kayam = DS.DSxml.Contracts.Elements("Contract").Any();

            if (!kayam)
            {
                result = 100000;
            }
            else
            {
                result = (from c in DS.DSxml.Contracts.Elements("Contract")
                          select Int32.Parse(c.Element("ContractNumber").Value)).Max();
            }

            return result;

        }
       
        public IEnumerable<Mother> getAllMothers()
        {
            XElement root = DS.DSxml.Mothers;
            List<Mother> result = new List<Mother>();
            foreach (var m in root.Elements("Mother"))
            {
                result.Add(m.ToMother());
            }
            return result.AsEnumerable();
        }

        public bool idCheck(int id)
        {
            var check1 = (from mother in Mothers.Elements()
                where Convert.ToInt32(mother.Element("ID").Value) == id
                select mother).Any();
            var check2 = (from nanny in Nannies.Elements()
                where Convert.ToInt32(nanny.Element("ID").Value) == id
                select nanny).Any();
            var check3 = (from child in Children.Elements()
                where Convert.ToInt32(child.Element("ID").Value) == id
                select child).Any();
            return check3 || check2 || check1;
        }
    }
}
