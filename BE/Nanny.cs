/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Nanny
    {

        public string LastName { get ; set; }
        public string FirstName { get ; set; }
        public int ID { get; set; }
        public DateTime Birthday { get ; set; }
        public string Telephone { get; set ; }
        public string Address { get; set; }
        public bool IsElevator { get; set; }
        public int Floor { get; set; }
        public int Experience { get;set; }
        public int KidsCapacity { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set ; }
        public double HourlyWage { get ; set;}
        public double MonthlyWage { get; set; }
        public Schedule[] Schedule { get; set; }
        public bool Vacation { get; set; }
        public string Recommendation{ get; set; }
        public bool KosherFood { get; set; }
      


        public override string ToString()
        {
            string result = this.ToStringProperty();
            DayOfWeek Day;
            for (int i = 0; i < 6; i++)
            {
                if (Schedule[i].IsWorking)
                {
                    Day = (DayOfWeek)i;
                    result += $"\n{Day}:\n{Schedule[i].ToString()}";
                }
            }
            return result;
        }

        public Nanny GetCopy()
        {
            return (Nanny) this.MemberwiseClone();
        }
    }
}
