using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Models.Metadata;

namespace Xamarin.iOS.Binding.Transformer
{
    public static class ApiDefinitionExtension
    {
        /// <summary>
        /// Writes the defintion to xml file
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void WriteToFile(this ApiDefinition target, string fileName)
        {
            var serializer = new XmlSerializer(typeof(ApiDefinition));

            var xWriterSetting = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true,
            };

            using (StreamWriter str = new StreamWriter(fileName))
            using (XmlWriter xml = XmlWriter.Create(str, xWriterSetting))
            {
                serializer.Serialize(xml, target, target.XmlNamespaces);
            }
        }


        /// <summary>
        /// Transforms the ApiDefintion with the specified transform file.
        /// </summary>
        /// <param name="target">The ApiDefinition</param>
        /// <param name="transformFile">The transform filename</param>
        public static void Transform(this ApiDefinition target, string transformFile)
        {
            var transform = Metadata.Load(transformFile);

            target.Transform(transform);
        }

        /// <summary>
        /// Transforms the ApiDefintion with the specified transform file.
        /// </summary>
        /// <param name="target">The ApiDefinition</param>
        /// <param name="transform">The Metadata object</param>
        public static void Transform(this ApiDefinition target, Metadata transform)
        {
            var tree = target.BuildTreePath();

            // work through removed
            foreach (var removed in transform.RemoveNodes)
            {
                //get the item to remove via its path
                var item = tree[removed.Path];

                //check for null
                if (item != null)
                {
                    //if the item has a parent
                    if (item.Parent != null)
                    {
                        //remove the child from the parent
                        item.Parent.Remove(item);
                    }
                }
      
                
            }

            // work through added
            foreach (var added in transform.AddNodes)
            {
                //get the parent item from the path
                var item = tree[added.Path];

                    if (item != null)
                    {
                        //if the class property has been set then its a class
                        if (added.Class != null)
                        {
                            item.Add(added.Class);
                        }

                        //if the delegate property has been set then its a delegate
                        if (added.Delegate != null)
                        {
                            item.Add(added.Delegate);
                        }

                        //if the method property has been set then its a Method
                        if (added.Method != null)
                        {
                            item.Add(added.Method);
                        }

                        //if the Property property has been set then its a Prperty
                        if (added.Property != null)
                        {
                            item.Add(added.Property);
                        }

                        //if the Parameter property has been set then its a parameter
                        if (added.Parameter != null)
                        {
                            item.Add(added.Parameter);
                        }
                    }
            }

            // work through altered
            foreach (var altered in transform.Changes)
            {
                //get the parent item from the path
                var item = tree[altered.Path];

                if (item != null)
                {
                    item.ApplyChanges(altered);
                }

            }
        }
    }
}
