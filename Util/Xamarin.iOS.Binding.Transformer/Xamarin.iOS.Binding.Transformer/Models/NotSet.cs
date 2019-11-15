using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.iOS.Binding.Transformer
{
    /// <summary>
    /// Class to state if a property value has not been set
    /// </summary>
    public class NotSet
    {
        public static NotSet Empty => new NotSet();
    }
}
