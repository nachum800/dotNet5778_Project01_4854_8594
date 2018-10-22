/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;

namespace BL
{
    public static partial class BL_Tool
    {
        private static IBL Bl = BLSingleton.GetBL;

      

         /// <summary>
         /// function returns age of person in months
         /// </summary>
         /// <param name="date"></param>
         /// <returns></returns>
        public static int GetAgeInMonth(DateTime date)
        {
            int years = DateTime.Now.Year - date.Year;

            //if ((date.Month > DateTime.Now.Month) ||
            //    (date.Month == DateTime.Now.Month && date.Day > DateTime.Now.Day))
            //    years--;

            int month = DateTime.Now.Month - date.Month;
            if (date.Day > DateTime.Now.Day)
                month--;
            return (years * 12) + month;
        }

        /// <summary>
        /// function calculates salary for a contract based on monthly/hourly wage
        /// </summary>
        /// <param name="nanny"></param>
        /// <param name="mother"></param>
        /// <returns></returns>
        public static double CalculateSalary(Nanny nanny, Mother mother)
        {
            int numberOfChildren =
                GetNumberOfContractsByCondition(c => c.MotherId == mother.ID && c.NannyId == nanny.ID);
            double discount = 1 - 0.02 * numberOfChildren;
            switch (mother.MonthlyOrHourly)
            {
                case MonthlyOrHourly.Monthly:

                    return nanny.MonthlyWage * discount;

                case MonthlyOrHourly.Hourly:

                    return GetMomNannyHourDifference(nanny,mother) * 4 * nanny.HourlyWage * discount;
                default:
                    return nanny.MonthlyWage * discount;
            }

        }

       /// <summary>
       /// function checks if hour of mother and a nanny are compatible
       /// </summary>
       /// <param name="nannyID"></param>
       /// <param name="childID"></param>
       /// <returns></returns>

        public static bool HoursCompatable(int nannyID, int childID)
        {
            Nanny nanny = Bl.GetNanny(nannyID);
            Child child = Bl.GetChild(childID);
            Mother mother = Bl.GetMother(child.MotherID);

            for (int i = 0; i < 6; i++)
            {
                if (mother.Schedule[i].IsWorking && nanny.Schedule[i].IsWorking || !mother.Schedule[i].IsWorking)
                {
                    if (!mother.Schedule[i].IsWorking)
                        continue;
                    if (mother.Schedule[i].StartTime.CompareTo(nanny.Schedule[i].StartTime) < 0 ||
                        mother.Schedule[i].EndTime.CompareTo(nanny.Schedule[i].EndTime) > 0)
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// checks if child is within the age range of the nanny's age range
        /// </summary>
        /// <param name="nanny"></param>
        /// <param name="child"></param>
        /// <returns>bool</returns>

        public static bool CheckAgeIsInRange(Nanny nanny, Child child)
        {
            int age = GetAgeInMonth(child.Birthday);
            return age >= nanny.MinimumAge && age <= nanny.MaximumAge;
        }
        public static int CalculateDistance(string source, string dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Driving,
                Origin = source,
                Destination = dest,
            };
            DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = drivingDirections.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value / 1000;
        }
        /// <summary>
        /// returns a list of children with no contracts
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Child> NannylessChildren()
        {
            {
                var chidren = new List<Child>();
                foreach (var mother in Bl.GetMothers())
                {
                    chidren.AddRange(Bl.GetChildrenByMother(mother.ID));
                }
                return chidren.Where(child => Bl.GetContracts().All(c => c.ChildId != child.ID));

            }
        }
        /// <summary>
        /// gets all nannies that work by the TAMAT standard
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Nanny> TamatNannies()
        {
            return Bl.GetNannies(x => x.Vacation);
        }
        /// <summary>
        /// function groups nanny based on their age group
        /// </summary>
        /// <param name="max"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<int, Nanny>> AgeGroup(bool max, bool sort = false)
        {
            var nannies = from nanny in Bl.GetNannies()
                group nanny by max ? nanny.MaximumAge / 3 : nanny.MinimumAge / 3;
            return sort ? nannies.OrderBy(x => x.Key) : nannies;
        }

        /// <summary>
        /// groups nannies based on distance groups
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<int, Contract>> DistanceGroup(bool sort = false)
        {
            var Contracts = from contract in Bl.GetContracts()
                group contract by CalculateDistance(Bl.GetMother(contract.MotherId).SearchArea,
                                      Bl.GetNanny(contract.NannyId).Address) / 5;
            return sort ? Contracts.OrderBy(x => x.Key) : Contracts;
        }

        /// <summary>
        /// function to check if nanny is over 18 years old
        /// </summary>
        /// <param name="nanny"></param>
        /// <returns></returns>
        public static bool IsOver18Years(Nanny nanny)
        {
            return (GetAgeInMonth(nanny.Birthday) / 12) >= 18;
        }

        /// <summary>
        /// checks age of child if is over 3 months
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public static bool IsChildOver3Month(Child child)
        {

            return GetAgeInMonth(child.Birthday) >= 3;
        }
        public static int GetNumberOfContractsByCondition(Func<Contract, bool> predicate)
        {
            return Bl.GetContracts(predicate).Count();
        }


        /// <summary>
        /// function that finds matching nannies based on the mother requests and returns the closest 5 if there is no perfect match
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public static IEnumerable<Nanny> MatchingNannies(int childId)
        {
            Child child = Bl.GetChild(childId);
            Mother mother = Bl.GetMother(child.MotherID);
           var nannyList= Bl.GetNannies(n => MotherRequirements(n,child,mother).All(b=>b));
            return nannyList.Count()>0?nannyList:ClosestMatch(child);
        }
        /// <summary>
        /// checks if the nanny is in the search area of the mother
        /// </summary>
        /// <param name="mother"></param>
        /// <param name="nAddress"></param>
        /// <returns></returns>
        private static bool NannyInRange(Mother mother,string nAddress)
        {
            return CalculateDistance(mother.SearchArea??mother.Address,nAddress) <= mother.MaxDistance;
        }

        /// <summary>
        /// get the difference between the the times wanted to the times given
        /// </summary>
        /// <param name="nanny"></param>
        /// <param name="mother"></param>
        /// <returns>the sum of the hours</returns>
        public static double GetMomNannyHourDifference(Nanny nanny, Mother mother)
        {
            double x = 0;
            for (int i = 0; i < 6; i++)
            {
                if (nanny.Schedule[i].IsWorking && mother.Schedule[i].IsWorking)
                {
                    x += (Math.Min(nanny.Schedule[i].EndTime.ToInt(), mother.Schedule[i].EndTime.ToInt()) -
                          Math.Max(nanny.Schedule[i].StartTime.ToInt(), mother.Schedule[i].StartTime.ToInt())) / 60d;
                }
            }
            return x;
        }

        /// <summary>
        /// get the time difference between 2 nannies and checks if their times are overlapping
        /// </summary>
        /// <param name="nanny1"></param>
        /// <param name="nanny2"></param>
        /// <returns>the sum of the hours</returns>
        public static bool OverlappingTime(Nanny nanny1, Nanny nanny2)
        {
            double matchingHours = 0;
            for (int i = 0; i < 6; i++)
            {
                if (nanny1.Schedule[i].IsWorking && nanny2.Schedule[i].IsWorking)
                {
                    matchingHours += (Math.Min(nanny1.Schedule[i].EndTime.ToInt(), nanny2.Schedule[i].EndTime.ToInt()) -
                                      Math.Max(nanny1.Schedule[i].StartTime.ToInt(), nanny2.Schedule[i].StartTime.ToInt())) / 60d;
                }
            }
            return matchingHours > 0;
        }
        /// <summary>
        /// get the sum of the hours of mother or nanny
        /// </summary>
        /// <param name="MotherOrNanny">type Mother or Nanny</param>
        /// <returns></returns>
        public static double GetHoursSum(object MotherOrNanny)
        {
            if (!(MotherOrNanny is Nanny) && !(MotherOrNanny is Mother))
                return -1;
            double HoursSum = 0;
            if (MotherOrNanny is Nanny temp1)
            {
                HoursSum = temp1.Schedule.Where(schedule => schedule.IsWorking).Aggregate(HoursSum, (current, schedule) => current + (schedule.EndTime - schedule.StartTime).ToInt());
            }
            if (MotherOrNanny is Mother temp)
            {
                HoursSum = temp.Schedule.Where(schedule => schedule.IsWorking).Aggregate(HoursSum, (current, schedule) => current + (schedule.EndTime - schedule.StartTime).ToInt());
            }
            return HoursSum / 60d;

        }

        /// <summary>
        /// checks all services needed by the mother if is provided by nanny
        /// </summary>
        /// <param name="nanny"></param>
        /// <param name="child"></param>
        /// <param name="mother"></param>
        /// <returns></returns>
        public static bool[] MotherRequirements(Nanny nanny, Child child, Mother mother)
        {
            bool[] Req=new bool[9];
            Req[(int) Requirements.AgeRange] = CheckAgeIsInRange(nanny, child);
            Req[(int) Requirements.Kosher] = mother.KosherFood && nanny.KosherFood || !mother.KosherFood;
            Req[(int) Requirements.Hours] = HoursCompatable(nanny.ID, child.ID);
            Req[(int) Requirements.Budget] = CalculateSalary(nanny, mother) <= mother.Budget;
            Req[(int) Requirements.LocationRange] = NannyInRange(mother, nanny.Address);
            Req[(int) Requirements.Experience] = nanny.Experience >= mother.WantedExperience;
            Req[(int)Requirements.Recommendation]= mother.Recommendation && nanny.Recommendation!=null || !mother.Recommendation;
            Req[(int) Requirements.Elevator] = mother.WantsElevator && nanny.IsElevator || !mother.WantsElevator;
            Req[(int) Requirements.Vacation] = mother.Vacation == nanny.Vacation;

            return Req;
        }

        /// <summary>
        /// calculates the score of each nanny(if there are no perfect matching nannies)
        /// </summary>
        /// <param name="child"></param>
        /// <param name="nanny"></param>
        /// <returns></returns>
        public static double MatchCalculator(Child child, Nanny nanny)
        {
            Mother mother = Bl.GetMother(child.MotherID);
            var MatchArray = MotherRequirements(nanny, child, mother);
            double sum = 100;
            if (!MatchArray[(int)Requirements.Kosher] || !MatchArray[(int)Requirements.AgeRange])
                return 0;
            if (!MatchArray[(int)Requirements.Hours])
                sum -= 3 * ((GetHoursSum(mother)) - (GetMomNannyHourDifference(nanny, mother)));
            if (!MatchArray[(int)Requirements.Elevator])
                sum -= (5 + nanny.Floor);
            if (!MatchArray[(int)Requirements.Experience])
                sum -= 5;
            if (!MatchArray[(int)Requirements.LocationRange])
                sum -= 0.01 * (CalculateDistance(mother.SearchArea ?? mother.Address, nanny.Address) - mother.MaxDistance);
            if (!MatchArray[(int)Requirements.Vacation])
                sum -= 3;
            if (!MatchArray[(int) Requirements.Recommendation])
                sum -= 7.5;
            if(!MatchArray[(int)Requirements.Budget])
                sum-= 0.01 * (CalculateSalary(nanny, mother) - mother.Budget);
            return (sum <= 0) ? 0 : sum;
        }
        /// <summary>
        /// function gets the score of each nanny and then returns the top five
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public static IEnumerable<Nanny> ClosestMatch(Child child)
        {
            Mother mother = Bl.GetMother(child.MotherID);
            var CloseNannies = Bl.GetNannies().ToList();
            CloseNannies.OrderByDescending(n => MatchCalculator(child, n));
            List<Nanny>Top5Nannies=new List<Nanny>();
            for (int i = 0; i < 5; i++)
            {
                if (i>=CloseNannies.Count)
                    return CloseNannies;
                else
                {
                   Top5Nannies.Add(CloseNannies[i]); 
                }
            }
            return Top5Nannies.AsEnumerable();
        }

      
    }
}
