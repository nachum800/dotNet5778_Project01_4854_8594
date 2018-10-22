/*Written by Matanya Glik && Nachum Shtauber
I.d: 305498594   && 311604854*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
       private static List<Nanny> _nannyList=new List<Nanny>();
       private static List<Mother> _motherList=new List<Mother>();
       private static List<Child> _childrenList=new List<Child>();
       private static List<Contract> _contractList=new List<Contract>();

        public static List<Nanny> NannyList { get => _nannyList; }
        public static List<Mother> MotherList { get => _motherList; }
        public static List<Child> ChildrenList { get => _childrenList; }
        public static List<Contract> ContractList { get => _contractList; }
    }
}