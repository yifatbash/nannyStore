using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace BE
{
    static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                try
                {
                    if (!typeof(IEnumerable<Schedule>).IsAssignableFrom(item.PropertyType))
                        str += "\n" + item.Name + ": " + item.GetValue(t, null);
                }
                catch (Exception)
                {

                }
            return str;
        }

        public static T GetCopy<T>(this T t) where T:new()
        {
            T result = new T();

            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (item.CanRead && item.CanWrite)
                {
                    object val = item.GetValue(t, null);
                    item.SetValue(result, val);
                }

            }

            return result;
        }


    }
}
