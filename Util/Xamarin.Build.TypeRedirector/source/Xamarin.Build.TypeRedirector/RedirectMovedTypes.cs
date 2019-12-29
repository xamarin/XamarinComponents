using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace Xamarin.Build.TypeRedirector
{
    public class RedirectMovedTypes : Task
    {
        [Required]
        public ITaskItem[] Assemblies { get; set; }

        public ITaskItem[] References { get; set; }

        public override bool Execute()
        {
            foreach (var assemblySpec in Assemblies)
            {
                var assembly = assemblySpec.ItemSpec;
                if (string.IsNullOrEmpty(assembly) || !File.Exists(assembly))
                    continue;

                Log.LogMessage($"Redirecting types for {assembly}...");

                var tempFile = assembly + ".temp.dll";

                using var resolver = new AssemblyResolver();
                foreach (var reference in References)
                {
                    var r = reference.ItemSpec;
                    if (string.IsNullOrEmpty(r) || !File.Exists(r))
                        continue;

                    resolver.AddAssembly(r);
                }

                var readerParams = new ReaderParameters
                {
                    ReadSymbols = true,
                    ReadWrite = true,
                    AssemblyResolver = resolver,
                };
                using (var assemblyDefinition = AssemblyDefinition.ReadAssembly(assembly, readerParams))
                {
                    RedirectTypes(assemblyDefinition, assembly);

                    var writerParams = new WriterParameters
                    {
                        WriteSymbols = true,
                        SymbolWriterProvider = new PortablePdbWriterProvider(),
                    };
                    assemblyDefinition.Write(writerParams);
                }

                // remove any .dll.mdb taht we don't want
                TryDelete(assembly + ".mdb");
            }

            return true;
        }

        private void RedirectTypes(AssemblyDefinition assembly, string assemblyPath)
        {
            // try get the mappings, and bail if nothing was needed
            var mappings = GetTypeMappings(assembly, assemblyPath);
            if (mappings == null)
                return;

            // find and re-map the assembly for required types
            var types = assembly.MainModule.GetTypeReferences();
            foreach (var map in mappings)
            {
                var type = types.FirstOrDefault(t => t.FullName == map.Key);

                // type was not used
                if (type == null)
                    continue;

                var scope = assembly.MainModule.AssemblyReferences.FirstOrDefault(a => a.FullName == map.Value.FullName);

                // scope was not found, add it
                if (scope == null)
                {
                    scope = map.Value;

                    // add the required assembly
                    Log.LogMessage($"The assembly did not reference {scope}, adding it...");

                    assembly.MainModule.AssemblyReferences.Add(scope);
                }

                Log.LogMessage($"Redirecting {type.FullName} to {scope.Name}...");
                type.Scope = scope;
            }

            // remove assembly references that are no longer used
            var usedScopes = types.Select(t => t.Scope).OfType<AssemblyNameReference>();
            var unused = assembly.MainModule.AssemblyReferences.Except(usedScopes).ToArray();
            foreach (var u in unused)
            {
                Log.LogMessage($"Removing reference to {u.Name} because it is no longer used...");
                assembly.MainModule.AssemblyReferences.Remove(u);
            }
        }

        private Dictionary<string, AssemblyNameReference> GetTypeMappings(AssemblyDefinition assembly, string assemblyPath)
        {
            // we weren't able to detect, so skip
            var targetFramework = DetectTargetFramework(assembly);
            if (targetFramework == null)
            {
                Log.LogWarning($"Unknown target framework for {assemblyPath}.");
                return null;
            }

            // we don't handle this, so skip
            if (!Mappings.MovedTypes.TryGetValue(targetFramework, out var mappings))
            {
                Log.LogMessage($"No mappings found for {assemblyPath}.");
                return null;
            }

            return mappings;
        }

        private string DetectTargetFramework(AssemblyDefinition assembly)
        {
            foreach (var attr in assembly.CustomAttributes)
            {
                if (attr.AttributeType.FullName != "System.Runtime.Versioning.TargetFrameworkAttribute")
                    continue;

                var tfv = attr.ConstructorArguments[0].Value.ToString();
                var id = new FrameworkName(tfv).Identifier;

                Log.LogMessage($"Found [TargetFramework] with value of '{tfv}' ({id}).");

                return id;
            }

            return null;
        }

        private static void TryDelete(string file)
        {
            if (!File.Exists(file))
                return;

            try
            {
                File.Delete(file);
            }
            catch
            {
            }
        }

        public sealed class AssemblyResolver : DefaultAssemblyResolver
        {
            public void AddAssembly(string assembly)
            {
                RegisterAssembly(AssemblyDefinition.ReadAssembly(assembly, new ReaderParameters
                {
                    AssemblyResolver = this,
                }));
            }
        }
    }
}
