using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    
    public static class ObjectExtensions
    {
        public static bool HigherValue(this object a, object b) {

            if (a.GetType() != b.GetType())
            {
                throw new Exception("Compared object has a different type");
            }
            else if (a is string)
            {
                return (a as string).Length >= (b as string).Length ? true : false;
            }
            else if (a is int)
            {

                return ((int)a >= (int)b) ? true : false;
            }
            else if (a is char)
            {

                return ((char)a >= (char)b) ? true : false;
            }
            else if (a is double) {

                return ((double)a >= (double)b) ? true : false;
            }
            else
            {

                throw new NotImplementedException($"Function is not inplemented for type {a.GetType().ToString()}");
            }
        }
    }
}
