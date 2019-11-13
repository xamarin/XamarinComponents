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

        public static void Compare(Dictionary<string,ApiObject> original, Dictionary<string, ApiObject> newversion, string outputPath = null)
        {
            Console.WriteLine($"Original item count: {original.Keys.Count} - New Item count: {newversion.Keys.Count}\n");

            var orgKeys = original.Keys.ToList();
            var newKeys = newversion.Keys.ToList();

            var removed = FindMissing(original, newversion).FlattenDict();
            var added = FindMissing(newversion, original).FlattenDict();

            Console.WriteLine($"Removed Items: {removed.Count}\n");
            Console.WriteLine($"Added Items: {added.Count}\n");

            var existing = new Dictionary<string, ApiObject>();
            orgKeys.ForEach(x =>
            {
                if (newKeys.Contains(x))
                    existing.Add(x, original[x]);
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
                File.WriteAllLines(Path.Combine(outputPath, existingFile), existing.Keys);

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
    }
}
