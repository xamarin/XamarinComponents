using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.iOS.Binding.Transformer
{
    public static class ApiObjectExtensions
    {
        public static Dictionary<string,object> GetValues(this ApiObject target)
        {
            var dict = new Dictionary<string, object>();

            var props = target.GetType().GetProperties();

            foreach (var aProp in props)
            {
                
                var atrrs = aProp.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name.Equals("ChangeIgnoreAttribute"));

                //is the property ignorable
                if (atrrs != null)
                {
                    //yes, then skip
                    continue;
                }
                
                var aName = aProp.Name;
                var aValue = aProp.GetValue(target);

                if (aValue == null)
                {
                    Console.WriteLine(aName);
                }
            }

            return dict;
        }
    }
}
