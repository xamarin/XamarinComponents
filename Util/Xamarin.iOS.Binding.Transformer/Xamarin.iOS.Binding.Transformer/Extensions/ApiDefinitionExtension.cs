using System;
using System.Collections.Generic;
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
        public static Metadata Transform(this ApiDefinition target, string transformFile)
        {
            var transform = Metadata.Load(transformFile);

            return target.Transform(transform);
        }

        /// <summary>
        /// Transforms the ApiDefintion with the specified transform file.
        /// </summary>
        /// <param name="target">The ApiDefinition</param>
        /// <param name="transform">The Metadata object</param>
        public static Metadata Transform(this ApiDefinition target, Metadata transform)
        {
            var output = transform.Clone();

            var tree = target.BuildTreePath();

            var unUsedRemoved = new List<Remove_Node>();
            var missingAttr = new List<Attr>();

            // work through removed
            foreach (var removed in output.RemoveNodes)
            {
                //get the item to remove via its path
                if (tree.ContainsKey(removed.Path))
                {
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
                else
                {
                    unUsedRemoved.Add(removed);
                    Console.WriteLine($"Removed Path not found: {removed.Path}");
                }
            }

            foreach (var aRemove in unUsedRemoved)
                output.RemoveNodes.Remove(aRemove);

            // work through added
            foreach (var added in output.AddNodes)
            {
                if (tree.ContainsKey(added.Path))
                {
                    //get the parent item from the path
                    var item = tree[added.Path];

                    if (item != null)
                    {
                        ApiObject apiObject = null;

                        //if the class property has been set then its a class
                        if (added.Class != null)
                            apiObject = added.Class;

                        //if the delegate property has been set then its a delegate
                        if (added.Delegate != null)
                            apiObject = added.Delegate;

                        //if the method property has been set then its a Method
                        if (added.Method != null)
                            apiObject = added.Method;

                        //if the Property property has been set then its a Prperty
                        if (added.Property != null)
                            apiObject = added.Property;

                        //if the Parameter property has been set then its a parameter
                        if (added.Parameter != null)
                            apiObject = added.Parameter;

                        if (added.Using != null)
                            apiObject = added.Using;

                        var propPath = apiObject.GetProposedPath(item);

                        if (tree.ContainsKey(propPath))
                        {
                            Console.WriteLine($"Cannot add node at path {propPath} as it is already defined.  If the item has been rename use a attr node instead");
                        }
                        else
                        {
                            item.Add(apiObject);
                        }

                        
                    }
                }
                else
                {
                    Console.WriteLine($"Added parent Path not found: {added.Path}");
                }
            }

            // work through altered
            foreach (var altered in output.Changes)
            {
                if (tree.ContainsKey(altered.Path))
                {
                    //get the parent item from the path
                    var item = tree[altered.Path];

                    if (item != null)
                    {
                        item.ApplyChanges(altered);
                    }
                }
                else
                {
                    missingAttr.Add(altered);

                    Console.WriteLine($"Altered Path not found: {altered.Path}");
                }
            }

            foreach (var aRemove in missingAttr)
                output.Changes.Remove(aRemove);

            target.UpdateHierachy();

            if ((output.RemoveNodes.Count != transform.RemoveNodes.Count) 
                || (output.Changes.Count != transform.Changes.Count))
            {
                return output;
            }

            return null;
        }
    }
}
