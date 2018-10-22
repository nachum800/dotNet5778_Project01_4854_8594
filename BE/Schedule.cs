/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Schedule
    {
        public Schedule()
        {
            IsWorking = false;
        }

        public Schedule(bool value, Time startTime, Time endTime)
        {
            this.IsWorking = value;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public bool IsWorking  { get; set; }
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }

        public override string ToString()
        {
            return $"Start time:{StartTime} End time:{EndTime}";
        }
    }
}
