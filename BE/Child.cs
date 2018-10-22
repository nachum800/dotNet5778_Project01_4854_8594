/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Child
    {

        public int ID { get; set; }
        public int MotherID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public bool SpecialNeeds { get; set; }
        public string Needs { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

        public Child GetCopy()
        {
            return (Child)this.MemberwiseClone();
        }
    }
}
