using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Core
{
    public class GenericValueConverter
    {
        private TypeIdentifier identifier = new TypeIdentifier();
        public T FindMatch<T>(List<string> line)
        {
            foreach (var item in line)
            {
                if (TryParse<T>(item, out T res))
                    return res;
            }
            throw new Exception("Suitable value was not found");
        }
        public bool TryParse<T>(string str, out T result)
        {
            try
            {
                if (typeof(T).FullName == "System.String")
                {
                    dynamic represented_type = identifier.RepresentsType(str);
                    result = ConvertTo<T>(represented_type);
                }
                else
                    result = ConvertTo<T>(str);

                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
        public T ConvertTo<T>(dynamic value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(value);
        }

    }
}
