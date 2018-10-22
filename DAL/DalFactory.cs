/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DalFactory
    {
        public static IDal GetDal()
        {
            return new Dal_XML_imp();
        }
    }
}
