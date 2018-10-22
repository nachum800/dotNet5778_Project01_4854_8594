/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BE
{
   public class Time:IComparable
   {
      
        private int _hour;
        private int _minute;

        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

       public Time(Time time)
       {
           if (time==null)
           {
               Hour = 8;
               Minute = 0;
            }
           else
           {
               Hour = time.Hour;
               Minute = time.Minute;
           }
        }

        public Time()
        {
        }






        #region Properties
        public int Hour { get => _hour;
            set{
                if ((value<7 || value >20)&&value!=0)
                   throw new Exception("Invalid input: Hours are from 8-20");
                _hour = value;
            }

        }

        public int Minute { get => _minute;
            set
            {
                if(value<0 || value>60)
                    throw new Exception("Invalid minute input");
                _minute = value;
            }
        }
        #endregion
#region Operators


       public  int ToInt()
       {
           
           return this.Hour * 60 + this.Minute;
       }
       
        public static Time operator+ (Time lfs ,Time rhs) //operator used to sum up amount of time
        {
            int timeSum = (lfs.Hour + rhs.Hour) * 60 + (lfs.Minute + rhs.Minute);
            Time result=new Time(timeSum/24,timeSum%60);
            return result;
        }

        public static Time operator -(Time time1, Time time2) //operator used to find out how many hours of work a day 
        {
            var hoursDifference = time1._hour - time2._hour;
            var minutesDifference = time1._minute - time2._minute;
            if (minutesDifference < 0)//fixes if minutes is smaller than 0
            {
                hoursDifference--;
                minutesDifference = 60 + minutesDifference;
            }
            if (hoursDifference < 0)
            {
                throw new Exception("Can't decrease");
            }
            return new Time(hoursDifference, minutesDifference);
        }
        #endregion

        public int CompareTo(object obj)//Icomparible Implementation
        {
            return (this.Hour * 60 + this.Minute) - (((Time)obj).Hour * 60 + ((Time)obj).Minute);
        }

        public override string ToString()
        {
            if (Hour < 10)
                if (Minute < 10)
                    return $"0{Hour}:0{Minute}";
                else
                    return $"0{Hour}:{Minute}";
            else if (Minute < 10)
                return $"{Hour}:0{Minute}";
            else
                return $"{Hour}:{Minute}";
        }
    }

}
