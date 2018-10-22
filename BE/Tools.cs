/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public static class Tools
    {
        public static MonthlyOrHourly toMonthlyOrHourly(this string Type)
        {
            if (Type=="Monthly")
            {
                return MonthlyOrHourly.Monthly;
            }
            return MonthlyOrHourly.Hourly;
        }
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if(item.PropertyType != typeof(Schedule[]))
                    if (item.PropertyType == typeof(DateTime))
                    {
                        DateTime temp =(DateTime)item.GetValue(t,null);
                        str += "\n" + item.Name + ": " + temp.ToString("d");
                    }
                else
                    str += "\n" + item.Name + ": " + item.GetValue(t, null);
            }
            return str;
        }
    }
}