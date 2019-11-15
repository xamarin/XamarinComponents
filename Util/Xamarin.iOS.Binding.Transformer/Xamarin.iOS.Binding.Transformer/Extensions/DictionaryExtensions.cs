using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.iOS.Binding.Transformer;

namespace System.Collections.Generic
{ 
    public static class DictionaryExtensions
    {
        public static Dictionary<string, object> FindChanges(this Dictionary<string, object> source, Dictionary<string, object>  target)
        {
            var dict = new Dictionary<string, object>();

            foreach (var aProp in source.Keys)
            {
                var oPropValue = source[aProp];

                if (target.ContainsKey(aProp))
                {
                    var nPropValye = target[aProp];

                    if (!nPropValye.Equals(oPropValue))
                    {
                        if (!(oPropValue is NotSet && nPropValye is NotSet))
                        {
                            dict.Add(aProp, nPropValye);
                        }
                        
                    }
                }
                else
                {
                    //property not found
                    Console.WriteLine($"Property not found on new object: {aProp}");
                    
                }

                
            }

            return dict;
        }
    }
}
