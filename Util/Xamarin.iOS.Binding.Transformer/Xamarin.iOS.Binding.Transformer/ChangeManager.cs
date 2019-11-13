using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin.iOS.Binding.Transformer
{
    public static class ChangeManager
    {

        public static void Compare(Dictionary<string,ApiObject> original, Dictionary<string, ApiObject> newversion)
        {
            Console.WriteLine($"Original item count: {original.Keys.Count} - New Item count: {newversion.Keys.Count}\n");

            var orgKeys = original.Keys.ToList();
            var newKeys = newversion.Keys.ToList();

            var removed = new Dictionary<string, ApiObject>();
            var added = new Dictionary<string, ApiObject>();
            var existing = new Dictionary<string, ApiObject>();

            orgKeys.ForEach(x =>
            {
                if (!newKeys.Contains(x))
                    removed.Add(x, original[x]);
            });

            var removableActual = FlattenRemovals(removed);
            Console.WriteLine($"Removed Items: {removableActual.Count}\n");

            newKeys.ForEach(x =>
            {
                if (!orgKeys.Contains(x))
                    added.Add(x, newversion[x]);
            });

            Console.WriteLine($"Added Items: {added.Count}\n");

            orgKeys.ForEach(x =>
            {
                if (newKeys.Contains(x))
                    existing.Add(x, original[x]);
            });

            Console.WriteLine($"Existing Items: {existing.Count}\n");

            Console.WriteLine($"Total Items in new: {existing.Count + added.Count}\n");

            
        }

        private static Dictionary<string, ApiObject> FlattenRemovals(Dictionary<string, ApiObject> removed)
        {
            var actutalsToRemove = new Dictionary<string, ApiObject>();

            foreach (var aKeyPath in removed.Keys)
            {
                var removable = ProcessNode(removed[aKeyPath], removed);

                if (!actutalsToRemove.ContainsKey(removable.Path))
                    actutalsToRemove.Add(removable.Path, removable);
            }

            return actutalsToRemove;
        }

        private static ApiObject ProcessNode(ApiObject apiObject, Dictionary<string, ApiObject> removed)
        {
            if (apiObject.Parent != null && removed.ContainsKey(apiObject.Parent.Path))
            {
                return ProcessNode(apiObject.Parent, removed);
            }
            else
            {
                return apiObject;
            }
        }
    }
}
