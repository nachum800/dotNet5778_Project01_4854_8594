/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Mother
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string SearchArea { get; set; }
        public MonthlyOrHourly MonthlyOrHourly { get; set; }
        public Schedule[] Schedule { get; set; }
        public int Budget { get; set; }
        public bool KosherFood { get; set; }
        public int MaxDistance { get; set; }
        public bool WantsElevator { get; set; }
        public int WantedExperience { get; set; }
        public bool Vacation { get; set; }
        public bool Recommendation { get; set; }

        public override string ToString()
        {
            string result = this.ToStringProperty();
            DayOfWeek Day;
            for (int i = 0; i < 6; i++)
            {
                if (Schedule[i].IsWorking)
                {
                    Day = (DayOfWeek) i;
                    result += $"\n{Day}: {Schedule[i].ToString()}";
                }


            }
            return result;
        }
        public Mother GetCopy()
        {
            return (Mother)this.MemberwiseClone();
        }
    }
}
