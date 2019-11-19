using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.iOS.Binding.Transformer.Models.Metadata;

namespace Xamarin.iOS.Binding.Transformer
{
    public static class ChangeManager
    {
        public const string MetaDataFileName = "Metadata.xml";

        public static void Compare(Dictionary<string,ApiObject> originalItems, Dictionary<string, ApiObject> newItems, string outputPath = null)
        {
            Console.WriteLine($"Original item count: {originalItems.Keys.Count} - New Item count: {newItems.Keys.Count}\n");

            var orgKeys = originalItems.Keys.ToList();
            var newKeys = newItems.Keys.ToList();

            var removed = FindMissing(originalItems, newItems).FlattenDict();
            var added = FindMissing(newItems, originalItems).FlattenDict();

            Console.WriteLine($"Removed Items: {removed.Count}\n");
            Console.WriteLine($"Added Items: {added.Count}\n");

            var existing = new List<string>();
            orgKeys.ForEach(x =>
            {
                if (!(originalItems[x] is ApiDefinition))
                {
                    if (newKeys.Contains(x))
                        existing.Add(x);
                }
            });

            Console.WriteLine($"Existing Items: {existing.Count}\n");

            Console.WriteLine($"Total Items in new: {existing.Count + added.Count}\n");

            if (!string.IsNullOrWhiteSpace(outputPath) && Directory.Exists(outputPath))
            {
                var addedFile = "added.txt";
                var removedFile = "removed.txt";
                var existingFile = "existing.txt";

                File.WriteAllLines(Path.Combine(outputPath, addedFile), added.Keys);
                File.WriteAllLines(Path.Combine(outputPath, removedFile), removed.Keys);
                File.WriteAllLines(Path.Combine(outputPath, existingFile), existing);

            }

            var metaData = new Metadata();

            //add removed nodes
            foreach (var aItem in removed)
            {
                metaData.RemoveNodes.Add(new Remove_Node()
                {
                    Path = aItem.Key,
                });
            }

            //add the added nodes
            foreach (var aItem in added)
            {
                var aApiObject = aItem.Value;

                var newAdded = new Add_Node()
                {
                    Path = aApiObject.Parent.Path,
                };

                if (aApiObject is ApiUsing)
                {
                    newAdded.Using = aApiObject as ApiUsing;
                }
                else if (aApiObject is ApiClass)
                {
                    newAdded.Class = aApiObject as ApiClass;
                }
                else if (aApiObject is ApiDelegate)
                {
                    newAdded.Delegate = aApiObject as ApiDelegate;
                }
                else if (aApiObject is ApiMethod)
                {
                    newAdded.Method = aApiObject as ApiMethod;
                }
                else if (aApiObject is ApiProperty)
                {
                    newAdded.Property = aApiObject as ApiProperty;
                }
                else if (aApiObject is ApiParameter)
                {
                    newAdded.Parameter = aApiObject as ApiParameter;
                }
                else
                {

                }

                metaData.AddNodes.Add(newAdded);
            }

            //calculate the nodes
            var changes = CalculateChanges(existing, originalItems, newItems);

            Console.WriteLine($"Changes to existing items: {changes.Count}\n");

            metaData.Changes = changes;

            metaData.WriteToFile(Path.Combine(outputPath, MetaDataFileName));


        }

        /// <summary>
        /// Flattens the dictionary to any parent items in the dict
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        private static Dictionary<string, ApiObject> FlattenDict(this Dictionary<string, ApiObject> dict)
        {
            var results = new Dictionary<string, ApiObject>();

            foreach (var aKeyPath in dict.Keys)
            {
                var removable = ProcessNode(dict[aKeyPath], dict);

                if (!results.ContainsKey(removable.Path))
                    results.Add(removable.Path, removable);
            }

            return results;
        }

        private static ApiObject ProcessNode(ApiObject apiObject, Dictionary<string, ApiObject> dict)
        {
            if (apiObject.Parent != null && dict.ContainsKey(apiObject.Parent.Path))
            {
                return ProcessNode(apiObject.Parent, dict);
            }
            else
            {
                return apiObject;
            }
        }

        private static Dictionary<string, ApiObject> FindMissing(Dictionary<string, ApiObject> source, Dictionary<string, ApiObject> target)
        {
            var result = new Dictionary<string, ApiObject>();

            var sourceKeys = source.Keys.ToList();
            var targetKeys = target.Keys.ToList();

            sourceKeys.ForEach(x =>
            {
                if (!targetKeys.Contains(x))
                    result.Add(x, source[x]);
            });

            return result;
        }

        public static List<Attr> CalculateChanges(List<string> existingPaths, Dictionary<string, ApiObject> originalItems, Dictionary<string, ApiObject> newItems)
        {
            var results = new List<Attr>();

            foreach (var aPath in existingPaths)
            {
                var origItem = originalItems[aPath];
                var newItem = newItems[aPath];

                var propValues = origItem.GetValues();
                var newPropValues = newItem.GetValues();

                var changes = propValues.FindChanges(newPropValues);

                if (changes.Keys.Count > 0)
                {
                    if (changes.Keys.Count == 1)
                    {
                        var propName = changes.Keys.First();
                        var prop = changes[propName];

                        var propValue = (prop.ToString().Equals("notset", StringComparison.OrdinalIgnoreCase)) ? string.Empty : prop.ToString();

                        var newAttr = new Attr()
                        {
                            Path = aPath,
                            Name = propName,
                            Value = propValue,
                        };

                        results.Add(newAttr);
                    }
                    else
                    {
                        var newAttr = new Attr()
                        {
                            Path = aPath,
                        };

                        //approach one - all elements detailed individually
                        foreach (var propName in changes.Keys)
                        {
                            var prop = changes[propName];
                            var propValue = (prop.ToString().Equals("notset", StringComparison.OrdinalIgnoreCase)) ? string.Empty : prop.ToString();

                            var newAttrProp = new AttrProperty()
                            {
                                Name = propName,
                                Value = propValue,
                            };

                            newAttr.Properties.Add(newAttrProp);
                        }

                        results.Add(newAttr);
                    }
  
                }
            }

            return results;
        }
    }
}
