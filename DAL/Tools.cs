/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;

namespace DAL
{
   public static class  Tools
    {
        public static Mother Clone(this Mother mother)
        {
            Mother clone = new Mother
            {
                ID = mother.ID,
                FirstName = mother.FirstName,
                LastName = mother.LastName,
                Schedule = mother.Schedule.ToArray(),
                SearchArea = mother.SearchArea,
                Telephone = mother.Telephone,
                Address = mother.Address,
                MonthlyOrHourly = mother.MonthlyOrHourly,
                Budget=mother.Budget,
                KosherFood = mother.KosherFood,
                MaxDistance = mother.MaxDistance,
                WantsElevator=mother.WantsElevator,
                WantedExperience=mother.WantedExperience,
                Vacation =mother.Vacation,
                Recommendation=mother.Recommendation
               
           };

            return clone;
        }
        public static Nanny Clone(this Nanny nanny)
        {
            Nanny clone = new Nanny
            {
                ID = nanny.ID,
                FirstName = nanny.FirstName,
                LastName = nanny.LastName,
                Address = nanny.Address,
                Birthday=nanny.Birthday,
                Experience = nanny.Experience,
                Floor = nanny.Floor,
                HourlyWage = nanny.HourlyWage,
                MonthlyWage = nanny.MonthlyWage,
                IsElevator = nanny.IsElevator,
                KidsCapacity = nanny.KidsCapacity,
                MinimumAge = nanny.MinimumAge,
                MaximumAge = nanny.MaximumAge,
                Telephone = nanny.Telephone,
                Schedule = nanny.Schedule.ToArray(),
                Recommendation = nanny.Recommendation,
                Vacation = nanny.Vacation,
                KosherFood = nanny.KosherFood

                 
            };

            return clone;
        }
        public static Child Clone(this Child child)
        {
            Child clone = new Child
            {
                ID = child.ID,
                MotherID = child.MotherID,
                Name = child.Name,
                Birthday = child.Birthday,
                SpecialNeeds = child.SpecialNeeds,
                Needs=child.Needs

            };

            return clone;
        }

        public static Contract Clone(this Contract contract)
        {
            Contract clone = new Contract
            {
                ChildId = contract.ChildId,
                MotherId = contract.MotherId,
                ContractNumber = contract.ContractNumber,
                EndDate = contract.EndDate,
                HourlyWage = contract.HourlyWage,
                Interview = contract.Interview,
                MonthlyWage = contract.MonthlyWage,
                NannyId = contract.NannyId,
                Rate = contract.Rate,
                Signed = contract.Signed,
                StartDate = contract.StartDate,
                Salary=contract.Salary 
            };
            return clone;
        }



        public static XElement toXML(this Time time, string attribute = "undefined")
        {
            if(time==null)
                return new XElement("Time", new XAttribute("type", attribute),
                    new XElement("Hour", 0),
                    new XElement("Minute", 0));
            return new XElement("Time", new XAttribute("type", attribute),
                new XElement("Hour", time.Hour),
                new XElement("Minute", time.Minute));

        }

        public static Time ToTime(this XElement TimeXml)
        {
            var result = from t in TimeXml.Elements("Time")
                select new Time(
                    int.Parse(t.Element("Hour")?.Value),
                    int.Parse(t.Element("Minute")?.Value));
            return result.FirstOrDefault();
        }

        public static XElement toXML(this Mother mother)
        {
            foreach (var schedule in mother.Schedule)
            {
                if (!schedule.IsWorking)
                {
                    schedule.StartTime = new Time(0, 0);
                    schedule.EndTime = new Time(0, 0);
                }
            }
            return new XElement("Mother",
                new XElement("ID", mother.ID),
                new XElement("FirstName", mother.FirstName),
                new XElement("LastName", mother.LastName),
                new XElement("Address", mother.Address),
                new XElement("SearchArea", mother.SearchArea),
                new XElement("MaxDistance", mother.MaxDistance),
                new XElement("Schedule",
                    from d in mother.Schedule
                    select new XElement($"Day", new XElement("IsWorking", d.IsWorking),
                         new XElement($"Start", new XElement("Hour", d.StartTime.Hour),
                                new XElement("Minute", d.StartTime.Minute)),
                            new XElement($"End", new XElement("Hour", d.EndTime.Hour),
                                new XElement("Minute", d.EndTime.Minute))
                    )
                ),
                new XElement("Telephone", mother.Telephone),
                new XElement("Rate", mother.MonthlyOrHourly),
                new XElement("Budget", mother.Budget),
                new XElement("KosherFood", mother.KosherFood),
                new XElement("WantsExperience", mother.WantedExperience),
                new XElement("WantsElevator", mother.WantsElevator),
                new XElement("Vacation", mother.Vacation),
                new XElement("Recommendation", mother.Recommendation)
            );
        }


        public static XElement toXML(this Nanny nanny)
        {
            foreach (var schedule in nanny.Schedule)
            {
                if (!schedule.IsWorking)
                {
                    schedule.StartTime=new Time(0,0);
                    schedule.EndTime=new Time(0,0);
                }
            }
            return new XElement("Nanny",
                new XElement("ID", nanny.ID),
                new XElement("FirstName", nanny.FirstName),
                new XElement("LastName", nanny.LastName),
                new XElement("Birthday", nanny.Birthday),
                new XElement("Telephone", nanny.Telephone),
                new XElement("Address", nanny.Address),
                new XElement("Floor", nanny.Floor),
                new XElement("IsElevator", nanny.IsElevator),
                new XElement("Experience", nanny.Experience),
                new XElement("KidsCapacity", nanny.KidsCapacity),
                new XElement("MinimumAge", nanny.MinimumAge),
                new XElement("MaximumAge", nanny.MaximumAge),
                new XElement("HourlyWage", nanny.HourlyWage),
                new XElement("MonthlyWage", nanny.MonthlyWage),
                new XElement("Vacation", nanny.Vacation),
                new XElement("Recommendation", nanny.Recommendation),
                new XElement("KosherFood", nanny.KosherFood),
                new XElement("Schedule",
                    from d in nanny.Schedule
                    select new XElement($"Day", new XElement("IsWorking", d.IsWorking),
                        new XElement($"Start", new XElement("Hour", d.StartTime.Hour),
                                new XElement("Minute", d.StartTime.Minute)),
                            new XElement($"End", new XElement("Hour", d.EndTime.Hour),
                                new XElement("Minute", d.EndTime.Minute))
                    )
                )
            );
        }

        public static XElement toXML(this Child child)
        {
            return new XElement("Child",
                new XElement("ID", child.ID),
                new XElement("Name", child.Name),
                new XElement("MotherID", child.MotherID),
                new XElement("Birthday", child.Birthday.ToShortDateString()),
                new XElement("SpecialNeeds", child.SpecialNeeds),
                (child.SpecialNeeds) ? new XElement("Needs", child.Needs) : new XElement("Needs", "")
                );
        }




        public static XElement toXML(this Contract contract)
        {
            return new XElement("Contract",
                new XElement("ContractNumber", contract.ContractNumber),
                new XElement("MotherId", contract.MotherId),
                new XElement("NannyId", contract.NannyId),
                new XElement("ChildId", contract.ChildId),
                new XElement("Interview", contract.Interview),
                new XElement("HourlyWage", contract.HourlyWage),
                new XElement("MonthlyWage", contract.MonthlyWage),
                new XElement("Salary", contract.Salary),
                new XElement("Rate", contract.Rate),
                new XElement("StartDate", contract.StartDate.ToShortDateString()),
                new XElement("EndDate", contract.EndDate.ToShortDateString()),
                new XElement("Signed", contract.Signed)

            );
        }
        public static Contract ToContract(this XElement contractXml)
    {
        Contract result = new Contract();
        
        result.ContractNumber = Int32.Parse(contractXml.Element("ContractNumber").Value);
        result.ChildId = Int32.Parse(contractXml.Element("ChildId").Value);
        result.MotherId = Int32.Parse(contractXml.Element("MotherId").Value);
        result.NannyId = Int32.Parse(contractXml.Element("NannyId").Value);
        result.StartDate = Convert.ToDateTime(contractXml.Element("StartDate")?.Value);
        result.EndDate = Convert.ToDateTime(contractXml.Element("EndDate")?.Value);
        result.Rate =BE.Tools.toMonthlyOrHourly(contractXml.Element("Rate").Value);
        result.HourlyWage = Double.Parse(contractXml.Element("HourlyWage").Value);
        result.MonthlyWage = Double.Parse(contractXml.Element("MonthlyWage").Value);
        result.Interview = Boolean.Parse(contractXml.Element("Interview")?.Value);
        result.Salary = Double.Parse(contractXml.Element("Salary").Value);
        result.Signed = Boolean.Parse(contractXml.Element("Signed")?.Value);
            
        return result;
    }

    public static Child ToChild(this XElement childXml)
        {
            Child result = null;
            if (childXml == null)
            {
                return result;
            }
            else
            {
                result=new Child
                {
                    Name = childXml.Element("Name").Value,
                    Birthday = Convert.ToDateTime(childXml.Element("Birthday").Value),
                    ID = int.Parse(childXml.Element("ID").Value),
                    MotherID = int.Parse(childXml.Element("MotherID").Value),
                    Needs = childXml.Element("Needs").Value,
                    SpecialNeeds = bool.Parse(childXml.Element("SpecialNeeds").Value)
                };
                return result;
            }
        }

        /// <summary>
        /// Return Nanny from XML file
        /// </summary>
        /// <param name="nannyXml"></param>
        /// <returns></returns>
        public static Nanny ToNanny(this XElement nannyXml)
        {
            Nanny result = null;
            if (nannyXml == null)
            {
                return result;
            }
            else
            {
                result = new Nanny();

                result.ID = int.Parse(nannyXml.Element("ID").Value);
                result.FirstName = nannyXml.Element("FirstName").Value;
                result.LastName = nannyXml.Element("LastName").Value;
                result.Address = nannyXml.Element("Address").Value;
                result.Birthday = Convert.ToDateTime(nannyXml.Element("Birthday").Value);
                result.Floor = Convert.ToInt32(nannyXml.Element("Floor").Value);
                result.Recommendation = nannyXml.Element("Recommendation").Value;
                result.Vacation = bool.Parse(nannyXml.Element("Vacation").Value);
                result.KosherFood = bool.Parse(nannyXml.Element("KosherFood").Value);
                result.Telephone = nannyXml.Element("Telephone").Value;
                result.MonthlyWage = Convert.ToDouble(nannyXml.Element("MonthlyWage")?.Value);
                result.HourlyWage = Convert.ToDouble(nannyXml.Element("HourlyWage")?.Value);
                result.Experience = int.Parse(nannyXml.Element("Experience").Value);
                result.IsElevator = bool.Parse(nannyXml.Element("IsElevator").Value);
                result.KidsCapacity = int.Parse(nannyXml.Element("KidsCapacity").Value);
                result.MaximumAge = int.Parse(nannyXml.Element("MaximumAge").Value);
                result.MinimumAge = int.Parse(nannyXml.Element("MinimumAge").Value);
                result.Schedule=new Schedule[6];
                int i = 0;
                foreach (var xElement in nannyXml.Element("Schedule").Elements("Day"))
                {
                    var work = Convert.ToBoolean(xElement.Element("IsWorking").Value);
                    Time begin = new Time();
                    Time end = new Time();
                    foreach (var element in xElement.Elements("Start"))
                        begin = new Time(Int32.Parse(element.Element("Hour").Value),
                            Int32.Parse(element.Element("Minute").Value));
                    foreach (var element in xElement.Elements("End"))
                        end = new Time(Int32.Parse(element.Element("Hour").Value),
                            Int32.Parse(element.Element("Minute").Value));
                        result.Schedule[i++]= new Schedule(work, begin, end);
                }
                return result;
            }
        }
        public static Mother ToMother(this XElement motherXml)
        {
            Mother result = null;
            if (motherXml == null)
            {
                return result;
            }
            else
            {
                result = new Mother
                {
                    ID = int.Parse(motherXml.Element("ID").Value),
                    FirstName = motherXml.Element("FirstName").Value,
                    LastName = motherXml.Element("LastName").Value,
                    SearchArea = motherXml.Element("SearchArea").Value,
                    Address = motherXml.Element("Address").Value,
                    Telephone = motherXml.Element("Telephone").Value,
                    
                    MonthlyOrHourly = motherXml.Element("Rate").Value.toMonthlyOrHourly(),
                    Recommendation =
                        Convert.ToBoolean(motherXml.Element("Recommendation")?.Value),
                    Vacation = Convert.ToBoolean(motherXml.Element("Vacation")?.Value),
                    KosherFood = Convert.ToBoolean(motherXml.Element("KosherFood")?.Value),
                    WantsElevator = Convert.ToBoolean(motherXml.Element("WantsElevator")?.Value),
                    Budget = Convert.ToInt32(motherXml.Element("Budget")?.Value),
                    MaxDistance = Convert.ToInt32(motherXml.Element("MaxDistance")?.Value),
                    WantedExperience = Convert.ToInt32(motherXml.Element("WantedExperience")?.Value)
                };
                result.Schedule = new Schedule[6];
                int i = 0;
                foreach (var xElement in motherXml.Element("Schedule").Elements("Day"))
                {
                    var work = Convert.ToBoolean(xElement.Element("IsWorking").Value);
                    Time begin = new Time();
                    Time end = new Time();
                    foreach (var element in xElement.Elements("Start"))
                        begin = new Time(Int32.Parse(element.Element("Hour").Value),
                            Int32.Parse(element.Element("Minute").Value));
                    foreach (var element in xElement.Elements("End"))
                        end = new Time(Int32.Parse(element.Element("Hour").Value),
                            Int32.Parse(element.Element("Minute").Value));
                    result.Schedule[i++] = new Schedule(work, begin, end);
                }

                if (result.SearchArea == "")
                    result.SearchArea = null;
                return result;
            }
        }
    }
}
