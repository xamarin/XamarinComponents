using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer.Models.Metadata
{
    public static class MetadataExtensions
    {
        public static void WriteToFile(this Metadata target, string fileName)
        {
            var serializer = new XmlSerializer(typeof(Metadata));

            var xWriterSetting = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true,
            };

            //using (StreamWriter str = new StreamWriter(fileName))
            using (StringWriter textWriter = new StringWriter())
            using (XmlWriter xml = XmlWriter.Create(textWriter, xWriterSetting))
            {
                serializer.Serialize(xml, target, target.XmlNamespaces);

                var output = textWriter.ToString();
                output = output.Replace("<blank />", "");

                File.WriteAllText(fileName, output);
            }
        }
    }
}
