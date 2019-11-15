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
                //check to see if the is an ignore attribute attached to the property
                var atrrs = aProp.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name.Equals("ChangeIgnoreAttribute"));

                //is the property ignorable
                if (atrrs != null)
                {
                    //yes, then skip
                    continue;
                }
                
               
                //get the property name
                var aName = aProp.Name;

                //get either the value on the object or return notset for nullable properties
                var aValue = aProp.GetValue(target) ?? NotSet.Empty;

                //add to the dictionary
                dict.Add(aName, aValue);

            }

            return dict;
        }
    }
}
