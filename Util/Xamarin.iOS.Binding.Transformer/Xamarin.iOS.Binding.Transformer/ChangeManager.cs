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

        /// <summary>
        /// Compare and out the metadata transform
        /// </summary>
        /// <param name="original">Original ApiDefintion</param>
        /// <param name="updated">Updated ApiDefintion</param>
        /// <param name="outputPath">Output path</param>
        public static Metadata Compare(ApiDefinition original, ApiDefinition updated, string outputPath = null)
        {
            var orgStack = original.BuildTreePath();
            var updatedStack = updated.BuildTreePath();

            return Compare(orgStack, updatedStack, outputPath);
        }

        private static Metadata Compare(Dictionary<string,ApiObject> originalItems, Dictionary<string, ApiObject> newItems, string outputPath = null)
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

            if (!string.IsNullOrWhiteSpace(outputPath))
            {
                if (!Directory.Exists(outputPath))
                    Directory.CreateDirectory(outputPath);

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

            if (!string.IsNullOrWhiteSpace(outputPath))
                metaData.WriteToFile(Path.Combine(outputPath, MetaDataFileName));

            return metaData;

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

                ProcessDiffs(aPath, changes, results);
            }

            return results;
        }

        private static void ProcessDiffs(string aPath, Dictionary<string, object> changes, List<Attr> results)
        {
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

        internal static Metadata CompareNew(ApiDefinition original, ApiDefinition updated)
        {
            var metaData = new Metadata();

            //compare usings
            CompareUsings(original.Usings.Items, updated.Usings.Items, metaData);

            //compare types
            CompareTypes(original.GetTypes(), updated.GetTypes(), metaData);


            return metaData;
        }

        /// <summary>
        /// Compare Types
        /// </summary>
        /// <param name="orgTypes"></param>
        /// <param name="updatedTypes"></param>
        /// <returns></returns>
        private static void CompareTypes(IEnumerable<ApiClass> orgTypes, IEnumerable<ApiClass> updatedTypes, Metadata metadata)
        {

            var diffs = BuildDiffs<ApiClass>(updatedTypes, orgTypes);


            //add removed types to the meta data
            metadata.AddRemoveNodes(diffs.Removed);


            //add new types to the meta data 
            metadata.AddNewNodes(diffs.Added);

           
            //build list of original types and their new names
            var typeList = BuildTypes(diffs.Existing);

            //work through the existing types
            foreach (var newItem in diffs.Existing)
            {
                var oldType = orgTypes.FirstOrDefault(x => x.Path.Equals(newItem.Path));

                CompareType(newItem, oldType, typeList, metadata);

            }
        }

        /// <summary>
        /// Build the list of orgininal and updated class tyes
        /// </summary>
        /// <param name="existing"></param>
        /// <returns></returns>
        private static Dictionary<string,string> BuildTypes(IEnumerable<ApiClass> existing)
        {
            var results = new Dictionary<string, string>();

            foreach (var item in existing)
            {
                var oldType = item.NativeName;
                var newtype = item.Name;

                results.Add(newtype, oldType);
                
            }

            return results;
        }

        /// <summary>
        /// Compare the type and build the changes
        /// </summary>
        /// <param name="newType">New type</param>
        /// <param name="oldType">Orginal type</param>
        /// <param name="typeList">List of type changes</param>
        /// <param name="metadata">the metadata object</param>
        private static void CompareType(ApiClass newType, ApiClass oldType, Dictionary<string, string> typeList, Metadata metadata)
        {
            //find differences in the type values
            var oldVals = oldType.GetValues();
            var newVals = newType.GetValues();

            //find the changes
            var changes = oldVals.FindChanges(newVals);

            //check for changes
            if (changes.Count > 0)
            {
                //process and add to metadata
                var results = new List<Attr>();
                ProcessDiffs(oldType.Path, changes, results);
                metadata.Changes.AddRange(results);
            }


            CompareMethods(newType.Methods, oldType.Methods, typeList, metadata);
            CompareProperties(newType.Properties, oldType.Properties, typeList, metadata);


        }


        /// <summary>
        /// Find changes in properties
        /// </summary>
        /// <param name="newProperties">New properties</param>
        /// <param name="oldProperties">Orgiginal properties</param>
        /// <param name="typeList">List of type changes</param>
        /// <param name="metadata">the metadata object</param>
        private static void CompareProperties(List<ApiProperty> newProperties, List<ApiProperty> oldProperties, Dictionary<string, string> typeList, Metadata metadata)
        {
            //find the properties that no longer exists in the new type
            var diffs = BuildDiffs<ApiProperty>(newProperties, oldProperties);

            metadata.AddRemoveNodes(diffs.Removed);
            metadata.AddNewNodes(diffs.Added);

            foreach (var prop in diffs.Existing)
            {
                //find the matching properties in the old type
                var oldProp = oldProperties.FirstOrDefault(x => x.Path.Equals(prop.Path));

                var propValues = oldProp.GetValues();
                var newPropValues = prop.GetValues();

                var changes = propValues.FindChanges(newPropValues);

                var results = new List<Attr>();

                ProcessDiffs(prop.Path, changes, results);

                metadata.Changes.AddRange(results);

            }
        }

        /// <summary>
        /// Compare all of the methods in the type with the ones from the original type
        /// </summary>
        /// <param name="newMethods"></param>
        /// <param name="oldmethods"></param>
        /// <param name="typeList">List of type changes</param>
        /// <param name="metadata">the metadata object</param>
        private static void CompareMethods(List<ApiMethod> newMethods, List<ApiMethod> oldmethods, Dictionary<string, string> typeList, Metadata metadata)
        {
            foreach (var meth in newMethods)
            {
                var oldMeths = oldmethods.Where(x => x.NativeName.Equals(meth.NativeName));

                if (oldMeths.Any())
                {
                    if (oldMeths.Count() > 1)
                    {


                        var foundMethod = false;

                        foreach (var oldMethod in oldMeths)
                        {
                            var found = 0;

                            if (oldMethod.Parameters.Count == meth.Parameters.Count)
                            {
                                foreach (var par in meth.Parameters)
                                {
                                    var oldPar = oldMethod.Parameters[meth.Parameters.IndexOf(par)];

                                    if (typeList.ContainsKey(par.Type))
                                    {
                                        var orgType = typeList[par.Type];

                                        if (oldPar.Type.Equals(orgType, StringComparison.OrdinalIgnoreCase))
                                        {
                                            found = found + 1;
                                        }
                                    }
                                    else if (par.Type.StartsWith("I"))
                                    {
                                        //this will assume that the new type is an interface replacing the old type
                                        var cleanName = par.Type.Substring(1).ToLower();

                                        if (oldPar.Type.ToLower().Contains(cleanName))
                                        {
                                            found = found + 1;
                                        }
                                    }
                                    
                                }

                                if (found == meth.Parameters.Count)
                                {
                                    var results = BuildMethodDiffs(oldMethod, meth);

                                    metadata.Changes.AddRange(results);

                                    foundMethod = true;
                                }
                            }
                        }

                        if (foundMethod != true)
                        {
                            var newAdded = new Add_Node()
                            {
                                Path = meth.Parent.Path,
                                Method = meth,
                            };

                            metadata.AddNodes.Add(newAdded);
                        }
                    }
                    else
                    {
                        var results = BuildMethodDiffs(oldMeths.First(), meth);

                        metadata.Changes.AddRange(results);
                    }
                }
                else
                {
                    //
                    var newAdded = new Add_Node()
                    {
                        Path = meth.Parent.Path,
                        Method = meth,
                    };

                    metadata.AddNodes.Add(newAdded);
                }
            }
        }

        /// <summary>
        /// Build the diff values for the new method compared with the original
        /// </summary>
        /// <param name="originalMethod"></param>
        /// <param name="newMethod"></param>
        /// <returns></returns>
        private static List<Attr> BuildMethodDiffs(ApiMethod originalMethod, ApiMethod newMethod)
        {
            var results = new List<Attr>();

            var oldValues = originalMethod.GetValues();
            var newValues = newMethod.GetValues();

            var changes = oldValues.FindChanges(newValues);

            ProcessDiffs(originalMethod.Path, changes, results);

            //now work through the parameters
            foreach (var newPar in newMethod.Parameters)
            {
                var oldPar = originalMethod.Parameters[newMethod.Parameters.IndexOf(newPar)];

                var oldVals = oldPar.GetValues();
                var newVals = newPar.GetValues();

                var pChanges = oldVals.FindChanges(newVals);

                if (pChanges.Count > 0)
                    ProcessDiffs(oldPar.Path, pChanges, results);

            }

            return results;
        }
        /// <summary>
        /// Compare usings
        /// </summary>
        /// <param name="orgUsings"></param>
        /// <param name="newItems"></param>
        /// <returns></returns>
        private static void CompareUsings(IEnumerable<ApiUsing> orgUsings, IEnumerable<ApiUsing> newItems, Metadata metadata)
        {
            var added = newItems.Where(x => !orgUsings.Select(y => y.Path).Contains(x.Path)).ToList();
            var removed = orgUsings.Where(x => !newItems.Select(y => y.Path).Contains(x.Path)).ToList();
            var existing = newItems.Where(x => orgUsings.Select(y => y.Path).Contains(x.Path)).ToList();


            foreach (var aItem in removed)
                metadata.RemoveNodes.Add(new Remove_Node() { Path = aItem.Path });

            //add new types to the meta data 
            foreach (var aItem in added)
            {
                var newAdded = new Add_Node()
                {
                    Path = aItem.Parent.Path,
                    Using = aItem,
                };

                metadata.AddNodes.Add(newAdded);
            }
        }

        /// <summary>
        /// Build the Diffs for types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newProperties"></param>
        /// <param name="oldProperties"></param>
        /// <returns></returns>
        private static (IEnumerable<T> Added, IEnumerable<T> Removed, IEnumerable<T> Existing) BuildDiffs<T>(IEnumerable<T> newItems, IEnumerable<T> originalItems) where T : ApiObject
        {
            var orgValues = originalItems.Select(x => new { Path = x.Path, Item = x as T });
            var newValues = newItems.Select(x => new { Path = x.Path, Item = x as T });

            var added = newValues.Where(x => !orgValues.Select(y => y.Path).Contains(x.Path)).Select(x => x.Item);
            var removed = orgValues.Where(x => !newValues.Select(y => y.Path).Contains(x.Path)).Select(x => x.Item);
            var existing = newValues.Where(x => orgValues.Select(y => y.Path).Contains(x.Path)).Select(x => x.Item);

            return (added, removed, existing);
        }
    }
}
