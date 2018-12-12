using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public static class DataSource
    {
        public static List<Nanny> nannyList;
        public static List<Mother> motherList;
        public static List<Child> childList;
        public static List<Contract> contractList;

        static DataSource()
        {
            nannyList = new List<Nanny>();
            motherList = new List<Mother>();
            childList = new List<Child>();
            contractList = new List<Contract>();
        }
    }
}
