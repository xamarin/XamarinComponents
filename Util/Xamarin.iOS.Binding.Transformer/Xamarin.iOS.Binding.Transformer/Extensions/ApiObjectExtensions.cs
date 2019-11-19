using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.iOS.Binding.Transformer.Models.Metadata;
using System.Reflection;

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

                object aValue = null;

                //get either the value on the object or return notset for nullable properties
                if (aProp.PropertyType.Equals(typeof(string)))
                {
                    if (string.IsNullOrEmpty(aProp.GetValue(target)?.ToString()))
                    {
                        aValue = NotSet.Empty;
                    }
                    else
                    {
                        aValue = aProp.GetValue(target);
                    }
                }
                else
                {
                    aValue = aProp.GetValue(target) ?? NotSet.Empty;
                }
                

                //add to the dictionary
                dict.Add(aName, aValue);

            }

            return dict;
        }

        public static void ApplyChanges(this ApiObject target, Attr attr)
        {
            var props = target.GetType().GetProperties();

            // if the attributes have been set than process the prop name and value
            if (!string.IsNullOrWhiteSpace(attr.Name))
            {
                var aProp = props.FirstOrDefault(x => x.Name.Equals(attr.Name, StringComparison.OrdinalIgnoreCase));

                if (aProp != null)
                {
                    aProp.SetValueFromString(target, attr.Value);
                }
            }

            // if there are multiple properties then process each one on the current ApiObject
            foreach (var aChange in attr.Properties)
            {
                var aProp = props.FirstOrDefault(x => x.Name.Equals(aChange.Name, StringComparison.OrdinalIgnoreCase));

                if (aProp != null)
                {
                    aProp.SetValueFromString(target, aChange.Value);
                }
            }
        }


        private static void SetValueFromString(this PropertyInfo prop, object target,  string value)
        {
            if (target == null)
                return;

            //create an empty object to assign a value too
            object setable = null;

            //if the value can't be set as a string the set work out the property type and convert it instead
            if (prop.PropertyType.Equals(typeof(bool)))
                setable = Convert.ToBoolean(value);
            else if (prop.PropertyType.Equals(typeof(string)))
            {
                if (!value.Equals("notset", StringComparison.OrdinalIgnoreCase))
                    setable = value;
            }
            else
                Console.WriteLine($"Unsupported property type: {prop.PropertyType}");

            //if a value type has been found
            if (setable != null)
                prop.SetValue(target, setable);
        }
    }
}
