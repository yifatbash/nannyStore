using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        public static IDAL GetDAL()
        {
            return new Dal_XML_imp();
            //return new DAL_imp();
        }
    }
}
