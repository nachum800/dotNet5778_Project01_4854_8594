/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLSingleton
    {
        private static IBL instance;

        protected BLSingleton()
        {
        }

        public static IBL GetBL
        {
            get
            {
                if (instance == null)
                {
                    instance = new IBL_imp();
                    //((IBL_imp) instance).init();
                }
                return instance;

            }

        }
    }
}
