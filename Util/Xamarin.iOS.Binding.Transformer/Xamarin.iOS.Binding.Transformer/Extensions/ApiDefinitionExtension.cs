using System;
using System.IO;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public static class ApiDefinitionExtension
    {
        public static void WriteToFile(this ApiDefinition target, string fileName)
        {
            var serializer = new XmlSerializer(typeof(ApiDefinition));

            using (StreamWriter str = new StreamWriter(fileName))
            {
                serializer.Serialize(str, target, target.XmlNamespaces);
            }
        }
    }
}
