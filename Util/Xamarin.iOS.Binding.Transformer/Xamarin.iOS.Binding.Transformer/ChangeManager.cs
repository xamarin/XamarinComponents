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
        /// Compare ApiDefintion files
        /// </summary>
        /// <param name="original">Original API defintion</param>
        /// <param name="updated">Updated API definition</param>
        /// <returns></returns>
        internal static Metadata Compare(ApiDefinition original, ApiDefinition updated)
        {
            var metaData = new Metadata();

            //compare usings
            CompareUsings(original.Usings.Items, updated.Usings.Items, metaData);

            //compare types
            var typeDict = CompareTypes(original.GetTypes(), updated.GetTypes(), metaData);


            //compar delegates
            CompareDelegates(original.GetDelegates(), updated.GetDelegates(), typeDict, metaData);

            return metaData;
        }

        #region Private Methods

        private static List<Attr> ProcessDiffs(string aPath, Dictionary<string, object> changes)
        {
            var results = new List<Attr>();

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

            return results;
        }

        /// <summary>
        /// Compare Types
        /// </summary>
        /// <param name="orgTypes"></param>
        /// <param name="updatedTypes"></param>
        /// <returns></returns>
        private static Dictionary<string,string> CompareTypes(IEnumerable<ApiClass> orgTypes, IEnumerable<ApiClass> updatedTypes, Metadata metadata)
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

            return typeList;
        }

        private static void CompareDelegates(List<ApiDelegate> orgDelegates, List<ApiDelegate> updateDelegates, Dictionary<string, string> typeDict, Metadata metadata)
        {
            var diffs = BuildDiffs<ApiDelegate>(updateDelegates, orgDelegates);

            //add removed types to the meta data
            metadata.AddRemoveNodes(diffs.Removed);


            //add new types to the meta data 
            metadata.AddNewNodes(diffs.Added);
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
                var results = ProcessDiffs(oldType.Path, changes);

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

                var results =  ProcessDiffs(prop.Path, changes);

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
            var oldValues = originalMethod.GetValues();
            var newValues = newMethod.GetValues();

            var changes = oldValues.FindChanges(newValues);

            var results = ProcessDiffs(originalMethod.Path, changes);

            //now work through the parameters
            foreach (var newPar in newMethod.Parameters)
            {
                var oldPar = originalMethod.Parameters[newMethod.Parameters.IndexOf(newPar)];

                var oldVals = oldPar.GetValues();
                var newVals = newPar.GetValues();

                var pChanges = oldVals.FindChanges(newVals);

                if (pChanges.Count > 0)
                {
                    var additionalRes = ProcessDiffs(oldPar.Path, pChanges);

                    results.AddRange(additionalRes);
                }
                    

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

        #endregion
    }
}
