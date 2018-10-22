/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using Contract = BE.Contract;

namespace BL
{
    public class IBL_imp : IBL
    {
        private IDal dal;

        public IBL_imp()
        {
            dal = DAL.DalFactory.GetDal();
        }

        internal void init()
        {
            this.AddNanny(new Nanny
            {
                Address = "Ha-Gefen 3, Khashmona'im",
                Schedule = new Schedule[6]
                {
                    new Schedule() {EndTime = new Time(16, 00), IsWorking = true, StartTime = new Time(8, 30)},
                    new Schedule() {EndTime = new Time(16, 00), IsWorking = true, StartTime = new Time(8, 30)},
                    new Schedule() {EndTime = new Time(16, 00), IsWorking = true, StartTime = new Time(8, 30)},
                    new Schedule() {EndTime = new Time(16, 00), IsWorking = true, StartTime = new Time(8, 30)},
                    new Schedule() {EndTime = new Time(16, 00), IsWorking = true, StartTime = new Time(8, 30)},
                    new Schedule() {EndTime = new Time(16, 00), IsWorking = true, StartTime = new Time(8, 30)}
                },
                ID = 123,
                Birthday = new DateTime(day: 23, month: 11, year: 1990),
                Experience = 5,
                FirstName = "Barcha",
                LastName = "Shmuelovitch",
                Floor = 2,
                HourlyWage = 29.35,
                IsElevator = true,
                MinimumAge = 3,
                MaximumAge = 15,
                KidsCapacity = 6,
                MonthlyWage = 5000,
                Vacation = false,
                KosherFood = true,
                Recommendation = "very good!",
                Telephone = "0545444564"

            });
            this.AddNanny(new Nanny
            {
                Address = "Dan Pagis 11, Jerusalem",
                Schedule = new Schedule[6]
                {
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)}
                },
                ID = 456,
                Birthday = new DateTime(day: 23, month: 11, year: 1991),
                Experience =5,
                FirstName = "Sharon",
                LastName = "Levi",
                Floor = 0,
                HourlyWage = 29.35,
                IsElevator = true,
                MinimumAge = 3,
                MaximumAge = 36,
                KidsCapacity = 6,
                MonthlyWage = 3000,
                Vacation = true,
                KosherFood = true,
                Recommendation = "very good!",
                Telephone = "0545444564"
            });
            this.AddMother(new Mother
            {
                ID = 311,
                Address = "HaTavor 4, Khashmona'im",
                FirstName = "Aliza",
                LastName = "shtauber",
                MonthlyOrHourly = MonthlyOrHourly.Monthly,
                SearchArea = "HaTavor 4, Khashmona'im",
                Schedule = new Schedule[6]
                {
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)}
                },
                Telephone = "0524847200",
                Budget = 25000,
                MaxDistance = 500,
                WantedExperience = 4,
                WantsElevator = true, KosherFood = true, Recommendation =true, Vacation = true
            });
            this.AddMother(new Mother
            {
                ID = 604,
                Address = "Dan Pagis 3, Jerusalem",
                FirstName = "matanya",
                LastName = "glik",
                MonthlyOrHourly = MonthlyOrHourly.Hourly,
                SearchArea = null,
                Schedule = new Schedule[6]
                {
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(16, 30)},
                    new Schedule() {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)}

                },
                Telephone = "058475689",
                Budget = 7000,
                MaxDistance = 50,
                WantedExperience = 1,
                WantsElevator = true
            });
            this.AddChild(new Child
            {
                ID = 789,
                Birthday = new DateTime(day: 04, month: 5, year: 2017),
                MotherID = 604,
                Name = "Chiam",
                SpecialNeeds = false

            });
            this.AddChild(new Child
            {
                ID = 4568,
                Birthday = new DateTime(day: 18, month: 9, year: 2017),
                MotherID = 311,
                Name = "Binyamin",
                SpecialNeeds = false

            });
            this.AddContract(new Contract()
            {
                ChildId = 4568,
                NannyId = 456,
                MotherId = 311,
                HourlyWage = this.GetNanny(456).HourlyWage,
                MonthlyWage = this.GetNanny(456).MonthlyWage,
                Rate = this.GetMother(311).MonthlyOrHourly,
                EndDate = new DateTime(2018, 1, 1),
                Signed = true,
                Interview = false,
                StartDate = DateTime.Now,
                Salary = BL_Tool.CalculateSalary(GetNanny(456), GetMother(311))
            });
            this.AddContract(new Contract()
            {
                ChildId = 789,
                NannyId = 456,
                MotherId = 604,
                HourlyWage = this.GetNanny(456).HourlyWage,
                MonthlyWage = this.GetNanny(456).MonthlyWage,
                Rate = this.GetMother(604).MonthlyOrHourly,
                EndDate = new DateTime(2018, 1, 1),
                Signed = true,
                Interview = false,
                StartDate = DateTime.Now,
                Salary = BL_Tool.CalculateSalary(GetNanny(456), GetMother(604))
            });

        }

        public void AddNanny(Nanny nanny)
        {
            if (BL_Tool.IsOver18Years(nanny))
                dal.AddNanny(nanny);
            else
                throw new Exception("nanny must be older than 18");
        }

      
        public bool RemoveNanny(int id)
        {
            return dal.RemoveNanny(id);
        }

        public void UpdateNanny(Nanny nanny)
        {
            dal.UpdateNanny(nanny);
        }

        public Nanny GetNanny(int id)
        {
            return dal.GetNanny(id);
        }

        public void AddMother(Mother mother)
        {
            dal.AddMother(mother);
        }

        public bool RemoveMother(int id)
        {
            return dal.RemoveMother(id);
        }

        public void UpdateMother(Mother mother)
        {
            dal.UpdateMother(mother);
        }

        public Mother GetMother(int id)
        {
            return dal.GetMother(id);
        }

        public void AddChild(Child child)
        {
            dal.AddChild(child);
        }

        public bool RemoveChild(int id)
        {
            return dal.RemoveChild(id);
        }

        public void UpdateChild(Child child)
        {
            dal.UpdateChild(child);
        }

        public Child GetChild(int id)
        {
            return dal.GetChild(id);
        }

        public void AddContract(Contract contract)
        {
            Nanny nanny = GetNanny(contract.NannyId);
            Child child = GetChild(contract.ChildId);
            Mother mother = GetMother((child.MotherID));

            if (!BL_Tool.IsChildOver3Month(child))
                throw new Exception("Child is too young...");
            int ammountOfContracts =BL_Tool. GetNumberOfContractsByCondition(n => n.NannyId == nanny.ID);
            if (ammountOfContracts >= nanny.KidsCapacity)
                throw new Exception("Nanny is full...");
            if (!BL_Tool.CheckAgeIsInRange(nanny, child))
                throw new Exception("Child's age is not within the nanny's age range...");
            var contracts = GetContracts(c => c.ChildId == child.ID);
            foreach (var item in contracts)
            {
                if (BL_Tool.OverlappingTime(nanny, GetNanny(item.NannyId)))
                {
                    throw new Exception($"Warning:{child.Name} is already registered in those hours");
                }
            }
            contract.Signed = true;
            dal.AddContract(contract);
        }

      
      

      
        

        public bool RemoveContract(int contractNumber)
        {
            return dal.RemoveContract(contractNumber);
        }

       

        public void UpdateContract(Contract contract)
        {
           dal.UpdateContract(contract);
        }

        public Contract GetContract(int contractNumber)
        {
            return dal.GetContract(contractNumber);
        }

        public IEnumerable<Nanny> GetNannies(Func<Nanny, bool> predicate = null)
        {
            return dal.GetNannies(predicate);
        }

        public IEnumerable<Mother> GetMothers(Func<Mother, bool> predicate = null)
        {
            return dal.GetMothers(predicate);
        }

        public IEnumerable<Child> GetChildrenByMother(int id)
        {
            return dal.GetChildrenByMother(id);
        }

        public IEnumerable<Contract> GetContracts(Func<Contract, bool> predicate = null)
        {
            return dal.GetContracts(predicate);
        }

      

     

      
    }
}
