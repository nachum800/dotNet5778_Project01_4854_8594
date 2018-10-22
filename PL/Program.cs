/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization.Configuration;
using BE;
using BL;

namespace PL
{
    internal class Program
    {
        private static IBL bl;

        private static void Main(string[] args)
        {
            int MenuChoice=0;
            int tempChoice = 0;
             bl = BLSingleton.GetBL;
             Console.WriteLine("Welcome to the Nanny DataBase!!");
            do
            {
                try
                {

                    Console.WriteLine(
                        "Main Menu:\nFor nannies please enter -0\nFor Mothers please enter -1\nFor Children please enter -2\nFor contracts please enter -3\nto exit press -4 ");
                    tempChoice = Convert.ToInt32(Console.ReadLine());
                   

                    switch (tempChoice)
                    {
                        case 0://nanny menu

                            do
                            {
                                Console.WriteLine(
                                    "\nAdd Nanny-1\nRemove Nanny-2\nUpdate Nanny-3\nGet all your contracts-4\nexit-5");
                                MenuChoice = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    switch (MenuChoice)
                                    {
                                        case 1://add Nanny
                                            var nanny = new Nanny();
                                            Console.WriteLine("Nanny ID:");
                                            nanny.ID = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("First name:");
                                            nanny.FirstName = Console.ReadLine();
                                            Console.WriteLine("Last name:");
                                            nanny.LastName = Console.ReadLine();
                                            Console.WriteLine("Nanny Address:");
                                            nanny.Address = Console.ReadLine();
                                            Console.WriteLine("Birthday");
                                            nanny.Birthday = Convert.ToDateTime(Console.ReadLine());
                                            Console.WriteLine("Maximum Age:");
                                            nanny.MaximumAge = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Minimum Age:");
                                            nanny.MinimumAge = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Years of experience:");
                                            nanny.Experience = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Floor:");
                                            nanny.Floor = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Do you have an elevator?");
                                            nanny.IsElevator = YesOrNo(Console.ReadLine()?.ToUpper());
                                            Console.WriteLine("Please write any recommendations you may have:");
                                            nanny.Recommendation = Console.ReadLine();
                                            Console.WriteLine("Do you have Tamat vacation?");
                                            nanny.Vacation = YesOrNo(Console.ReadLine()?.ToUpper());

                                            Console.WriteLine("Hourly Wage:");
                                            nanny.HourlyWage = Convert.ToDouble(Console.ReadLine());
                                            Console.WriteLine("Monthly Wage:");
                                            nanny.MonthlyWage = Convert.ToDouble(Console.ReadLine());
                                            Console.WriteLine("Maximum Amount of kids:");
                                            nanny.KidsCapacity = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Do you keep kosher?");
                                            nanny.KosherFood = YesOrNo(Console.ReadLine()?.ToUpper());
                                            nanny.Schedule = new Schedule[6];
                                            Console.WriteLine("Enter days of work and hours");
                                            for (var i = 0; i < 6; i++)
                                            {
                                                nanny.Schedule[i] = new Schedule();
                                                Console.WriteLine($"Do you want work on: {(DayOfWeek) i}?");

                                                nanny.Schedule[i].IsWorking = YesOrNo(Console.ReadLine()?.ToUpper());

                                                if (nanny.Schedule[i].IsWorking)
                                                {
                                                    Console.WriteLine("Start Time:");
                                                    var str = Console.ReadLine()?.Split(':', '.', ' ');
                                                    nanny.Schedule[i].StartTime = new Time(Convert.ToInt32(str[0]),
                                                        Convert.ToInt32(str[1]));
                                                    Console.WriteLine("End Time");
                                                    str = Console.ReadLine()?.Split(':', '.', ' ');
                                                    var temp = new Time(Convert.ToInt32(str[0]),
                                                        Convert.ToInt32(str[1]));
                                                    while (temp.CompareTo(nanny.Schedule[i].StartTime) <= 0)
                                                    {
                                                        Console.WriteLine("Invalid hours!");
                                                        str = Console.ReadLine()?.Split(':');
                                                        temp = new Time(Convert.ToInt32(str[0]),
                                                            Convert.ToInt32(str[1]));
                                                    }
                                                    nanny.Schedule[i].EndTime = temp;
                                                }
                                            }

                                            Console.WriteLine("Phone number:");
                                            nanny.Telephone = Console.ReadLine();
                                            Console.WriteLine(nanny);
                                            bl.AddNanny(nanny);
                                            break;
                                        case 2://remove nanny
                                            Console.WriteLine("Please enter id of nanny to remove:");
                                            if (bl.RemoveNanny(Convert.ToInt32(Console.ReadLine())))
                                                Console.WriteLine("Nanny removed");
                                            break;
                                        case 3://update nanny
                                            Console.WriteLine("Please insert nanny ID");
                                            var ID = Convert.ToInt32(Console.ReadLine());
                                            var tempnanny = bl.GetNanny(ID);
                                            if (tempnanny == null)
                                                throw new Exception("nanny not found!");
                                            Console.WriteLine(tempnanny);
                                            Console.WriteLine(@"Choose field to change and press 0 to finish
""Address""
""kids capacity""
""elevator""
""Experience""
""Phone number""
""Hours""
""Hourly wage""
""Monthly wage""
""Recomendation""");
                                            var tempMenuChoice = Console.ReadLine().ToLower();
                                            string tempAddress;
                                            while (tempMenuChoice != "0")
                                            {
                                                switch (tempMenuChoice)
                                                {
                                                    case "address":
                                                        Console.WriteLine("please enter wanted address:");
                                                        tempAddress = Console.ReadLine();
                                                        tempnanny.Address = tempAddress;
                                                        break;
                                                    case "kids capacity":
                                                        Console.WriteLine("please enter amount of kids:");
                                                        tempnanny.KidsCapacity = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case "elevator":
                                                        Console.WriteLine("Do you have an elevator?");
                                                        tempnanny.IsElevator = YesOrNo(Console.ReadLine()?.ToUpper());
                                                        break;
                                                    case "experience":
                                                        Console.WriteLine(" years of experience:");
                                                        tempnanny.Experience = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case "phone number":
                                                        Console.WriteLine("Insert phone number:");
                                                        tempnanny.Telephone = Console.ReadLine();
                                                        break;
                                                    case "hours":
                                                        for (var i = 0; i < 6; i++)
                                                        {
                                                            Console.WriteLine($"Do you want work on: {(DayOfWeek) i}?");

                                                            tempnanny.Schedule[i].IsWorking =
                                                                YesOrNo(Console.ReadLine()?.ToUpper());
                                                            if (tempnanny.Schedule[i].IsWorking)
                                                            {
                                                                Console.WriteLine("Start Time:");
                                                                var str = Console.ReadLine()?.Split(':', '.', ' ');
                                                                tempnanny.Schedule[i].StartTime =
                                                                    new Time(Convert.ToInt32(str[0]),
                                                                        Convert.ToInt32(str[1]));
                                                                Console.WriteLine("End Time");
                                                                str = Console.ReadLine()?.Split(':', '.', ' ');
                                                                var temp = new Time(Convert.ToInt32(str[0]),
                                                                    Convert.ToInt32(str[1]));
                                                                while (temp.CompareTo(tempnanny.Schedule[i]
                                                                           .StartTime) <= 0)
                                                                {
                                                                    Console.WriteLine("Invalid hours!");
                                                                    str = Console.ReadLine()?.Split(':');
                                                                    temp = new Time(Convert.ToInt32(str[0]),
                                                                        Convert.ToInt32(str[1]));
                                                                }
                                                                tempnanny.Schedule[i].EndTime = temp;
                                                            }
                                                        }
                                                        break;

                                                    case "hourly wage":
                                                        Console.WriteLine("enter new wage(per hour):");
                                                        var rate = Console.ReadLine()?.ToUpper();
                                                        tempnanny.HourlyWage = Convert.ToInt32(Console.ReadLine());

                                                        break;
                                                    case "monthly wage":

                                                        Console.WriteLine(@"What is your new wage(per month)");
                                                        tempnanny.MonthlyWage = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case "recommendation":
                                                        Console.WriteLine("Please enter the recommendation");
                                                        tempnanny.Recommendation = Console.ReadLine();
                                                        break;
                                                    case "0":
                                                        break;
                                                    default:
                                                        Console.WriteLine("Wrong input! Please try again");
                                                        break;
                                                }
                                                if (tempMenuChoice != "0")
                                                    Console.WriteLine("Your MenuChoice?");
                                                tempMenuChoice = Console.ReadLine();
                                            }
                                            Console.WriteLine(tempnanny);
                                            bl.UpdateNanny(tempnanny);
                                            break;


                                        case 4://get all contracts
                                            Console.WriteLine("Enter Id of nanny");
                                            nanny = bl.GetNanny(Convert.ToInt32(Console.ReadLine()));

                                            if (nanny != null)
                                            {
                                                var contracts = new List<Contract>();
                                                contracts = bl.GetContracts(c => c.NannyId == nanny.ID)?.ToList();

                                                if (contracts.Count > 0)
                                                    foreach (var contract in contracts)
                                                        Console.WriteLine(contract);
                                                else throw new Exception("Nanny has no contract...");
                                            }
                                            else
                                            {
                                                throw new Exception("No nanny with same id...");
                                            }
                                            break;
                                        case 5://exit
                                            Console.WriteLine("Goodbye...");
                                            break;
                                        default:

                                            Console.WriteLine("Wrong input!...");
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            } while (MenuChoice != 5);

                            break;
                        case 1://mother menu
                            do
                            {
                                 try
                                {
                                    Console.WriteLine(
                                    "\nAdd Mother-1\nRemove Mother-2\nUpdate Mother-3\nGet all your contracts-4\nexit-5");
                                      MenuChoice = Convert.ToInt32(Console.ReadLine());
                               

                                    switch (MenuChoice)
                                    {
                                        case 1://add mother
                                            var mother = new Mother();
                                            Console.WriteLine("Mother ID:");
                                            mother.ID = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("First name:");
                                            mother.FirstName = Console.ReadLine();
                                            Console.WriteLine("Last name:");
                                            mother.LastName = Console.ReadLine();
                                            Console.WriteLine("Mother Address:");
                                            mother.Address = Console.ReadLine();
                                            Console.WriteLine("What is your search area?");
                                            mother.SearchArea = Console.ReadLine();
                                            Console.WriteLine("Wanted years of experience:");
                                            mother.WantedExperience = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Do you want an elevator?");
                                            mother.WantsElevator = YesOrNo(Console.ReadLine()?.ToUpper());
                                            Console.WriteLine("Do you want Tamat vacation");
                                            mother.Vacation = YesOrNo(Console.ReadLine()?.ToUpper());
                                            Console.WriteLine("Do you keep kosher?");
                                            mother.KosherFood = YesOrNo(Console.ReadLine());
                                            Console.WriteLine(@"What is your budget?(In New Israeli Shekel)");
                                            mother.Budget = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Do you want monthly or hourly rate");
                                            var rate = Console.ReadLine()?.ToUpper();
                                            mother.MonthlyOrHourly =
                                                rate == "MONTHLY" ? MonthlyOrHourly.Monthly : MonthlyOrHourly.Hourly;
                                            Console.WriteLine("What is your max wanted distance from nanny(in KM)");
                                            mother.MaxDistance = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Do you want recommendations?");
                                            mother.Recommendation = YesOrNo(Console.ReadLine()?.ToUpper());

                                            mother.Schedule = new Schedule[6];
                                            Console.WriteLine("enter days of work and hours");
                                            for (var i = 0; i < 6; i++)
                                            {
                                                mother.Schedule[i] = new Schedule();
                                                Console.WriteLine($"Do you want nanny on: {(DayOfWeek) i}?");

                                                mother.Schedule[i].IsWorking = YesOrNo(Console
                                                    .ReadLine()
                                                    ?.ToUpper());
                                                if (mother.Schedule[i].IsWorking)
                                                {
                                                    Console.WriteLine("Start Time:");
                                                    var str = Console.ReadLine()?.Split(':', '.', ' ');
                                                    mother.Schedule[i].StartTime = new Time(Convert.ToInt32(str[0]),
                                                        Convert.ToInt32(str[1]));
                                                    Console.WriteLine("End Time");
                                                    str = Console.ReadLine()?.Split(':', '.', ' ');
                                                    var temp = new Time(Convert.ToInt32(str[0]),
                                                        Convert.ToInt32(str[1]));
                                                    while (temp.CompareTo(mother.Schedule[i].StartTime) <= 0)
                                                    {
                                                        Console.WriteLine("Wrong input!");
                                                        str = Console.ReadLine()?.Split(':');
                                                        temp = new Time(Convert.ToInt32(str[0]),
                                                            Convert.ToInt32(str[1]));
                                                    }
                                                    mother.Schedule[i].EndTime = temp;
                                                }
                                            }
                                            Console.WriteLine("phone number:");
                                            mother.Telephone = Console.ReadLine();
                                            bl.AddMother(mother);
                                            break;
                                        case 2://remove mother
                                            Console.WriteLine("please enter id of mother to remove:");
                                            if (bl.RemoveMother(Convert.ToInt32(Console.ReadLine())))
                                                Console.WriteLine("Mother removed");
                                            else
                                                throw new Exception("Could not erase mother");
                                            break;
                                        case 3://update mother
                                            Console.WriteLine("Please insert mother ID");
                                            var ID = Convert.ToInt32(Console.ReadLine());
                                            var tempMother = bl.GetMother(ID);
                                            if (tempMother == null)
                                                throw new Exception("Mother not found!");
                                            Console.WriteLine(tempMother);
                                            Console.WriteLine(@"Choose field to change and press 0 to finish
""Address""
""Search area""
""Wants elevator""
""Wanted experience""
""Phone number""
""Hours""
""Max Distance""
""Rate""
""Budget""");
                                            var tempMenuChoice = Console.ReadLine().ToLower();
                                            string tempAddress;
                                            while (tempMenuChoice != "0")
                                            {
                                                switch (tempMenuChoice)
                                                {
                                                    case "address":
                                                        Console.WriteLine("please enter wanted address");
                                                        tempAddress = Console.ReadLine();
                                                        tempMother.Address = tempAddress;
                                                        break;
                                                    case "search area":
                                                        Console.WriteLine("please enter wanted address");
                                                        tempAddress = Console.ReadLine();
                                                        tempMother.Address = tempAddress;
                                                        break;
                                                    case "wants elevator":
                                                        Console.WriteLine("Do you want an elevator?");
                                                        tempMother.WantsElevator =
                                                            YesOrNo(Console.ReadLine()?.ToUpper());
                                                        break;
                                                    case "wanted experience":
                                                        Console.WriteLine("Wanted years of experience:");
                                                        tempMother.WantedExperience =
                                                            Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case "phone number":
                                                        Console.WriteLine("Insert phone number:");
                                                        tempMother.Telephone = Console.ReadLine();
                                                        break;
                                                    case "hours":
                                                        Console.WriteLine("enter days of work and hours");
                                                        for (var i = 0; i < 6; i++)
                                                        {
                                                            Console.WriteLine(
                                                                $"Do you want nanny on: {(DayOfWeek) i}?");

                                                            tempMother.Schedule[i].IsWorking = YesOrNo(Console
                                                                .ReadLine()
                                                                ?.ToUpper());
                                                            if (tempMother.Schedule[i].IsWorking)
                                                                if (tempMother.Schedule[i].IsWorking)
                                                                {
                                                                    Console.WriteLine("Start Time:");
                                                                    var str = Console.ReadLine()?.Split(':');
                                                                    tempMother.Schedule[i].StartTime =
                                                                        new Time(Convert.ToInt32(str[0]),
                                                                            Convert.ToInt32(str[1]));
                                                                    Console.WriteLine("End Time");
                                                                    str = Console.ReadLine()?.Split(':');
                                                                    tempMother.Schedule[i].EndTime =
                                                                        new Time(Convert.ToInt32(str[0]),
                                                                            Convert.ToInt32(str[1]));
                                                                }
                                                        }
                                                        break;
                                                    case "max distance":
                                                        Console.WriteLine(
                                                            "What is your max wanted distance from nanny(in KM)");
                                                        tempMother.MaxDistance = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case "rate":
                                                        Console.WriteLine("Do you want monthly or hourly rate");
                                                        rate = Console.ReadLine()?.ToUpper();
                                                        tempMother.MonthlyOrHourly =
                                                            rate == "MONTHLY"
                                                                ? MonthlyOrHourly.Monthly
                                                                : MonthlyOrHourly.Hourly;
                                                        break;
                                                    case "budget":
                                                        Console.WriteLine(
                                                            @"What is your budget?(In New Israeli Shekel)");
                                                        tempMother.Budget = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case "0":
                                                        break;
                                                    default:
                                                        Console.WriteLine("Wrong input! Please try again");
                                                        break;
                                                }
                                                if (tempMenuChoice != "0")
                                                    Console.WriteLine("Your MenuChoice?");
                                                tempMenuChoice = Console.ReadLine();
                                            }
                                            bl.UpdateMother(tempMother);
                                            break;
                                        case 4://get all mothers contracts
                                            Console.WriteLine("please enter mothers id");
                                            tempMother = bl.GetMother(Convert.ToInt32(Console.ReadLine()));
                                            if (tempMother == null)
                                                throw new Exception("Mother not found!");
                                            var tempContracts = bl.GetContracts(c => c.MotherId == tempMother.ID).ToList();
                                            if (tempContracts.Count == 0)
                                                throw new Exception("Mother has no contracts...");
                                            foreach (var tempContract in tempContracts)
                                                Console.WriteLine(tempContract);
                                            break;
                                        case 5://exit
                                            break;
                                        default:
                                            Console.WriteLine("Wrong input!...");
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);

                                }
                            } while (MenuChoice != 5);
                            break;
                        case 2://child menu
                            do
                            {
                                Console.WriteLine(
                                    "\nAdd Child-1\nRemove Child-2\nUpdate Child-3\nGet all your contracts-4\nexit-5");
                                MenuChoice = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    switch (MenuChoice)
                                    {
                                        case 1://adds child
                                            Console.WriteLine("Please enter mother ID:");
                                            var motherID = Convert.ToInt32(Console.ReadLine());
                                            if (bl.GetMother(motherID) == null)
                                            {
                                                throw new Exception("No mother with same ID!");
                                            }
                                            var child = new Child();
                                            child.MotherID = bl.GetMother(motherID).ID;
                                            Console.WriteLine("Child ID:");
                                            child.ID = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Child name:");
                                            child.Name = Console.ReadLine();
                                            Console.WriteLine("Birthday");
                                            child.Birthday = Convert.ToDateTime(Console.ReadLine());
                                            Console.WriteLine("Any special need?");
                                            child.SpecialNeeds = YesOrNo(Console.ReadLine()?.ToUpper());
                                            if (child.SpecialNeeds)
                                            {
                                                Console.WriteLine("Please insert description of the needs");
                                                child.Needs = Console.ReadLine();
                                            }
                                            bl.AddChild(child);
                                            break;
                                        case 2://remove child
                                            Console.WriteLine("Please enter id of child to remove:");
                                            if (bl.RemoveChild(Convert.ToInt32(Console.ReadLine())))
                                                Console.WriteLine("Child removed");
                                            break;
                                        case 3://update child
                                            Console.WriteLine("Please insert child ID");
                                            var ID = Convert.ToInt32(Console.ReadLine());
                                            var tempChild = bl.GetChild(ID);
                                            if (tempChild == null)
                                                throw new Exception("Child not found!");
                                            Console.WriteLine(tempChild);
                                            Console.WriteLine(@"Choose field to change and press 0 to finish
""Special needs""");
                                            var tempMenuChoice = Console.ReadLine().ToLower();

                                            while (tempMenuChoice != "0")
                                            {
                                                switch (tempMenuChoice)
                                                {
                                                    case "special needs":
                                                        Console.WriteLine("Does the child have special needs");
                                                        tempChild.SpecialNeeds = YesOrNo(Console.ReadLine());
                                                        if (tempChild.SpecialNeeds)
                                                        {
                                                            Console.WriteLine("Please insert description of the needs");
                                                            tempChild.Needs = Console.ReadLine();
                                                        }
                                                        break;
                                                    case "0":
                                                        break;
                                                    default:
                                                        Console.WriteLine("Wrong input! Please try again");
                                                        break;
                                                }
                                                if (tempMenuChoice != "0")
                                                    Console.WriteLine("Your MenuChoice?");
                                                tempMenuChoice = Console.ReadLine();
                                            }
                                            Console.WriteLine(tempChild);
                                            bl.UpdateChild(tempChild);
                                            break;
                                        case 4://get all childs contracts
                                            Console.WriteLine("Enter Id of child");
                                            child = bl.GetChild(Convert.ToInt32(Console.ReadLine()));

                                            if (child != null)
                                            {
                                                var contracts = new List<Contract>();
                                                contracts = bl.GetContracts(c => c.ChildId == child.ID)?.ToList();

                                                if (contracts.Count > 0)
                                                    foreach (var contract in contracts)
                                                        Console.WriteLine(contract);
                                                else throw new Exception("Nanny has no contract...");
                                            }
                                            else
                                                throw new Exception("No nanny with same id...");
                                            break;
                                        case 5://exit
                                            Console.WriteLine("Goodbye...");
                                            break;
                                        default:
                                            Console.WriteLine("Wrong input!...");
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            } while (MenuChoice != 5);
                            break;
                        case 3://contract menu
                            do
                            {

                                try
                                {
                                    Console.WriteLine(
                                        "\nTo add contract press-1\nTo Remove contract press-2\nTo update contract press-3\nTo exit press-4");
                                    MenuChoice = Convert.ToInt32(Console.ReadLine());
                                    switch (MenuChoice)
                                    {
                                        case 1://add contract
                                            Console.WriteLine("please enter mother Id");
                                            int x;
                                            Mother mother = bl.GetMother(Convert.ToInt32(Console.ReadLine()));
                                            if (mother != null)
                                            {
                                                var children = bl.GetChildrenByMother(mother.ID).ToList();
                                                if (children.Count > 0)
                                                {
                                                    for (int i = 0; i < children.Count; i++)
                                                    {
                                                        Console.WriteLine($"{i}:  {children[i]}");
                                                    }
                                                    do
                                                    {
                                                        Console.WriteLine(
                                                            "Please choose a child you want a nanny for:");
                                                        x = Convert.ToInt32((Console.ReadLine()));
                                                        if ((x < 0 || x > children.Count))
                                                        {
                                                            Console.WriteLine("invalid input..");
                                                        }
                                                    } while (x < 0 || x > children.Count);
                                                    Child child = children[x];
                                                    var nannies = BL_Tool.MatchingNannies(child.ID).ToList();
                                                    for (int i = 0; i < nannies.Count; i++)
                                                    {
                                                        Console.WriteLine($"{i}:  {nannies[i]}");
                                                    }
                                                    do
                                                    {
                                                        Console.WriteLine("Please choose a Nanny:");
                                                        x = Convert.ToInt32((Console.ReadLine()));
                                                        if ((x < 0 || x > nannies.Count))
                                                        {
                                                            Console.WriteLine("invalid input..");
                                                        }
                                                    } while (x < 0 || x > nannies.Count);
                                                    Nanny nanny = nannies[x];
                                                    Contract contract = new Contract
                                                    {
                                                        ChildId = child.ID,
                                                        MotherId = mother.ID,
                                                        NannyId = nanny.ID,

                                                    };
                                                    string start, end;
                                                    do
                                                    {
                                                        Console.WriteLine("please enter start date:(mm/dd/yyyy)");
                                                        start = Console.ReadLine();
                                                        Console.WriteLine("please enter end date:(mm/dd/yyyy)");
                                                        end = Console.ReadLine();
                                                        if (Convert.ToDateTime(start) > Convert.ToDateTime(end))
                                                            Console.WriteLine("invalid dates...");


                                                    } while (Convert.ToDateTime(start) > Convert.ToDateTime(end));
                                                    contract.StartDate = Convert.ToDateTime(start);
                                                    contract.EndDate = Convert.ToDateTime(end);
                                                    contract.HourlyWage = nanny.HourlyWage;
                                                    contract.MonthlyWage = nanny.MonthlyWage;
                                                    contract.Rate = mother.MonthlyOrHourly;
                                                    contract.Salary = BL_Tool.CalculateSalary(nanny, mother);
                                                    Console.WriteLine("Was there an interview?");
                                                    contract.Interview = YesOrNo(Console.ReadLine()?.ToUpper());
                                                    Console.WriteLine(contract);
                                                    Console.WriteLine("Is the contract OK?");
                                                    bool z = YesOrNo(Console.ReadLine()?.ToUpper());
                                                    if (z)
                                                    {
                                                        bl.AddContract(contract);
                                                        Console.WriteLine("Contract added");
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("contract canceled");
                                                    }

                                                }
                                                else throw new Exception("Mother has no children");

                                            }
                                            else throw new Exception("no mother with same id...");

                                            break;
                                        case 2://remove contract
                                            Console.WriteLine("please enter contact number to remove");
                                            if (bl.RemoveContract(Convert.ToInt32(Console.ReadLine())))
                                                Console.WriteLine("Contract removed!");
                                            break;
                                        case 3://update contract
                                            Console.WriteLine("Please enter contact number to update");
                                            Contract temp = bl.GetContract(Convert.ToInt32(Console.ReadLine()));
                                            if (temp == null)
                                                throw new Exception("No contract with same contract number...");
                                            Console.WriteLine(temp);
                                            string ans;
                                            do
                                            {
                                                Console.WriteLine(@"Choose field to change and press 0 to finish
""Dates""
""Interview""");
                                                ans = Console.ReadLine().ToLower();
                                                string start, end;
                                                switch (ans)
                                                {
                                                    case "dates":
                                                        do
                                                        {
                                                            Console.WriteLine("please enter start date:(mm/dd/yyyy)");
                                                            start = Console.ReadLine();
                                                            Console.WriteLine("please enter end date:(mm/dd/yyyy)");
                                                            end = Console.ReadLine();
                                                            if (Convert.ToDateTime(start) > Convert.ToDateTime(end))
                                                                Console.WriteLine("invalid dates...");


                                                        } while (Convert.ToDateTime(start) > Convert.ToDateTime(end));
                                                        temp.StartDate = Convert.ToDateTime(start);
                                                        temp.EndDate = Convert.ToDateTime(end);
                                                        temp.Salary = BL_Tool.CalculateSalary(bl.GetNanny(temp.NannyId),
                                                            bl.GetMother(temp.MotherId));

                                                        break;
                                                    case "interview":
                                                        Console.WriteLine("Was there an interview?");
                                                        temp.Interview = YesOrNo(Console.ReadLine()?.ToUpper());
                                                        break;
                                                    case "0":
                                                        break;
                                                    default:
                                                        Console.WriteLine("Wrong input...");
                                                        break;
                                                }
                                            } while (ans != "0");
                                            Console.WriteLine(temp);
                                            Console.WriteLine("Is the contract OK?");
                                            bool y = YesOrNo(Console.ReadLine()?.ToUpper());

                                            if (y)//conformation
                                            {
                                                bl.UpdateContract(temp);
                                                Console.WriteLine("Contract updated");
                                            }
                                            else
                                            {
                                                throw new Exception("Contract canceled");
                                            }

                                            break;
                                        case 4:
                                            Console.WriteLine("Goodbye...");
                                            break;
                                        default:
                                            Console.WriteLine("Wrong input");
                                            break;
                                    }


                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);

                                }

                            } while (MenuChoice != 4);
                            break;
                        case 4://exit
                            Console.WriteLine("Thank you for using our services we hope to see you again...");
                            break;

                        default:
                            Console.WriteLine("wrong input...");
                            break;
                    }//main menu 
                  
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
            } while (tempChoice != 4);
        }

        public static bool YesOrNo(string result)
        {
            return result == "YES" || result == "Y" || result == "TRUE"||result=="OK";
        }
    }
}