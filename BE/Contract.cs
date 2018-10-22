/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Contract
    {
      
        public int ContractNumber { get; set; }
        public int NannyId { get; set; }
        public int ChildId { get; set; }
        public int MotherId { get; set; }
        public bool Interview { get; set; }
        public bool Signed { get; set; }
        public double HourlyWage { get; set; }
        public double MonthlyWage { get; set; }
        public double Salary { get; set; }
        public MonthlyOrHourly Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
           return this.ToStringProperty();
        }

        public Contract GetCopy()
        {
            return (Contract)this.MemberwiseClone();
        }
    }
}
