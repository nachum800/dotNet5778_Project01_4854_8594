using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BE;
using BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bl_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testGit()
        {
            Assert.AreEqual("1","1");
        }
        [TestMethod]
        public void TestMethod1()
        {
            var nanny = new Nanny
            {
                Schedule = new Schedule[6]
                {
                    new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)},
                    new Schedule(),
                    new Schedule(),
                    new Schedule(),
                    new Schedule(),
                    new Schedule()
                },
                ID = 123,
                Birthday = new DateTime(day: 23, month: 11, year: 1999)
            };
            Console.WriteLine(nanny);
        }

        [TestMethod]
        public void Test2()
        {
            var imp = new IBL_imp();
            foreach (var mother in imp.GetMothers())
            {
                Console.WriteLine(mother);
                Console.WriteLine();
            }
            foreach (var nanny in imp.GetNannies())
            {
                Console.WriteLine(nanny);
                Console.WriteLine();
            }
            var chidren = new List<Child>();
            foreach (var mother in imp.GetMothers())
                chidren.AddRange(imp.GetChildrenByMother(mother.ID));
            foreach (var child in chidren)
            {
                Console.WriteLine(child);
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void MatchigDifference_HalfHoursOfDifference()
        {
            try
            {
                var imp = BLSingleton.GetBL;
                imp.AddNanny(new Nanny
                {
                    Address = "hgefen 3, hashmonaim",
                    Schedule = new Schedule[6]
                    {
                        new Schedule {EndTime = new Time(16, 0), IsWorking = true, StartTime = new Time(8, 0)},
                        new Schedule {EndTime = new Time(16, 58), IsWorking = false, StartTime = new Time(8, 23)},
                        new Schedule {EndTime = new Time(16, 0), IsWorking = true, StartTime = new Time(8, 0)},
                        new Schedule {EndTime = new Time(15, 30), IsWorking = true, StartTime = new Time(7, 30)},
                        new Schedule {IsWorking = false},
                        new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)}
                    },
                    ID = 8080,
                    Birthday = new DateTime(day: 23, month: 11, year: 1999),
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
                    Vacation = true,
                    KosherFood = true,
                    Recommendation = "very good!",
                    Telephone = "0545444564"
                });
                imp.AddMother(new Mother
                {
                    ID = 305,
                    Address = "Hatavor 4,chashmonaim",
                    FirstName = "aliza",
                    LastName = "shtauber",
                    MonthlyOrHourly = MonthlyOrHourly.Monthly,
                    SearchArea = "chashmonaim,IL",
                    Schedule = new Schedule[6]
                    {
                        new Schedule {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                        new Schedule {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                        new Schedule {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                        new Schedule {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(16, 0)},
                        new Schedule {IsWorking = false, StartTime = new Time(8, 0), EndTime = new Time(16, 0)},
                        new Schedule {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)}
                    },
                    Telephone = "0524847200"
                });
                var x = BL_Tool.GetMomNannyHourDifference(imp.GetNanny(8080), imp.GetMother(305));
                var y = BL_Tool.GetHoursSum(imp.GetMother(305));
                Console.WriteLine($"x: {x}");
                Console.WriteLine($"y: {y}");
                Console.WriteLine(y - x);
                Assert.AreEqual(y - x, 0.5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public void CheckCalculator()
        {
            var bl = BLSingleton.GetBL;
            var mothers = bl.GetMothers()?.ToList();
            var child = bl.GetChildrenByMother(mothers[0].ID)?.ToList();
            var nannies = BL_Tool.MatchingNannies(child[0].ID);
            foreach (var nanny in nannies)
            {
                Console.WriteLine(nanny);
                Console.WriteLine();
            }
        }


        [TestMethod]
        public void TestremoveAll()
        {
            DS.DSxml.Nannies.RemoveAll();
            DS.DSxml.Mothers.RemoveAll();
            DS.DSxml.SaveNannies();
            DS.DSxml.SaveMothers();
        }

        [TestMethod]
        public void TestGrouping()
        {
            var imp = BLSingleton.GetBL;
            var group = BL_Tool.AgeGroup(true, true);
            foreach (var VARIABLE in group)
            {
                Console.WriteLine($"Key: {VARIABLE.Key}");
                foreach (var nanny in VARIABLE)
                {
                    Console.WriteLine(nanny);
                    Console.WriteLine();
                }
            }
        }

        [TestMethod]
        public void RemoveNanny_TrueExpected()
        {
            var bl = BLSingleton.GetBL;
            Assert.IsTrue(bl.RemoveNanny(123));
        }

        [TestMethod]
        public void RemoveMother_TrueExpected()
        {
            var bl = BLSingleton.GetBL;
            Assert.IsTrue(bl.RemoveMother(311));
        }

        [TestMethod]
        public void RemoveChild_ExceptionExpected()
        {
            try
            {
                var bl = BLSingleton.GetBL;
                bl.RemoveChild(456878);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.AreEqual("No child with the same id...", e.Message);
            }
        }

        [TestMethod]
        public void RemoveNanny_ExceptionExpected()
        {
            try
            {
                var bl = BLSingleton.GetBL;
                bl.RemoveNanny(456);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Nanny still has existing contracts...", e.Message);
            }
        }

        [TestMethod]
        public void RemoveContract_TrueExpected()
        {
            var bl = BLSingleton.GetBL;
            Assert.IsTrue(bl.RemoveContract(100001));
        }

        [TestMethod]
        public void GetAllContractOfNanny()
        {
            var bl = BLSingleton.GetBL;
            var nanny = bl.GetNanny(456);
            var con = bl.GetContracts(c => c.NannyId == nanny.ID);
            foreach (var contract in con)
                Console.WriteLine(contract);
        }

        [TestMethod]
        public void AddNannyWithExistingId()
        {
            try
            {
                var bl = BLSingleton.GetBL;
                bl.AddNanny(new Nanny
                {
                    Address = "Ha-Gefen 3, Khashmona'im",
                    Schedule = new Schedule[6]
                    {
                        new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)},
                        new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)},
                        new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)},
                        new Schedule {EndTime = new Time(12, 58), IsWorking = true, StartTime = new Time(8, 23)},
                        new Schedule(),
                        new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)}
                    },
                    ID = 123,
                    Birthday = new DateTime(day: 23, month: 11, year: 1999),
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
                    Vacation = true,
                    KosherFood = true,
                    Recommendation = "very good!",
                    Telephone = "0545444564"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.AreEqual(e.Message, "ID already exists...");
            }
        }

        [TestMethod]
        public void CheckNannySalary_TrueExpected()
        {
            try
            {
                var imp = BLSingleton.GetBL;
                imp.AddNanny(new Nanny
                {
                    Address = "hgefen 3, hashmonaim",
                    Schedule = new Schedule[6]
                    {
                        new Schedule {EndTime = new Time(16, 0), IsWorking = true, StartTime = new Time(8, 0)},
                        new Schedule {EndTime = new Time(16, 58), IsWorking = false, StartTime = new Time(8, 23)},
                        new Schedule {EndTime = new Time(16, 0), IsWorking = true, StartTime = new Time(8, 0)},
                        new Schedule {EndTime = new Time(15, 30), IsWorking = true, StartTime = new Time(7, 30)},
                        new Schedule {IsWorking = false},
                        new Schedule {EndTime = new Time(16, 58), IsWorking = true, StartTime = new Time(8, 23)}
                    },
                    ID = 8080,
                    Birthday = new DateTime(day: 23, month: 11, year: 1999),
                    Experience = 5,
                    FirstName = "Barcha",
                    LastName = "Shmuelovitch",
                    Floor = 2,
                    HourlyWage = 100,
                    IsElevator = true,
                    MinimumAge = 3,
                    MaximumAge = 15,
                    KidsCapacity = 6,
                    MonthlyWage = 5000,
                    Vacation = true,
                    KosherFood = true,
                    Recommendation = "very good!",
                    Telephone = "0545444564"
                });
                imp.AddMother(new Mother
                {
                    ID = 305,
                    Address = "Hatavor 4,chashmonaim",
                    FirstName = "aliza",
                    LastName = "shtauber",
                    MonthlyOrHourly = MonthlyOrHourly.Hourly,
                    SearchArea = "chashmonaim,IL",
                    Schedule = new Schedule[6]
                    {
                        new Schedule {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                        new Schedule {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                        new Schedule {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(15, 30)},
                        new Schedule {IsWorking = true, StartTime = new Time(8, 30), EndTime = new Time(16, 0)},
                        new Schedule {IsWorking = false, StartTime = new Time(8, 0), EndTime = new Time(16, 0)},
                        new Schedule {IsWorking = false, StartTime = new Time(8, 30), EndTime = new Time(15, 30)}
                    },
                    Telephone = "0524847200"
                });
                Assert.AreEqual(8400,BL_Tool.CalculateSalary(imp.GetNanny(8080),imp.GetMother(305)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        [TestMethod]
        public void TestXml()
        {
            DS.DSxml.Nannies.RemoveAll();
            DS.DSxml.Mothers.RemoveAll();
            var imp = BLSingleton.GetBL;
          

            Mother m = imp.GetMother(205564);
            Mother n = imp.GetMother(311);
            Console.WriteLine(m);
            Console.WriteLine();
            Console.WriteLine(n);
        }
    }
}