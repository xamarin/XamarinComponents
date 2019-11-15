using System;
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
                var aName = aProp.Name;
                var aValue = aProp.GetValue(target);

                Console.WriteLine($"aName");


            }

            return dict;
        }
    }
}
