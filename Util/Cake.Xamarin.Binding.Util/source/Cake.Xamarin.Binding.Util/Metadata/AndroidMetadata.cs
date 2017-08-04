using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Cake.Xamarin.Binding.Util
{
	static class AndroidMetadata
	{
		public static List<MetadataMemberInfo> FindMissing(string assemblyFile)
		{
			var results = new List<MetadataMemberInfo>();

			var assembly = AssemblyDefinition.ReadAssembly(assemblyFile);

			var types = new List<TypeDefinition>();


			Action<IMemberDefinition> addMember = (IMemberDefinition member) =>
			{
				if (!results.Any(r => r.NetMember.FullName.Equals(member.FullName)))
					results.Add(new MetadataMemberInfo { NetMember = member });
			};

			// Get *ALL* types and nested types from both assemblies
			foreach (var m in assembly.Modules)
				types.AddRange(m.GetAllTypes());

			foreach (var t in types)
			{
				if (t.IsNotPublic)
					continue;

				var printedType = false;

				foreach (var m in t.Methods)
				{
					if (!m.IsPublic)
						continue;
					
					if (!m.HasParameters)
						continue;

					foreach (var p in m.Parameters) {
						if (IsBadName(p.Name)) {
							addMember(m);
							break;
						}
					}
				}

				foreach (var f in t.Fields)
				{
					if (!f.IsPublic)
						continue;

					if (IsBadName(f.Name))
						addMember(f);
				}

				foreach (var p in t.Properties)
				{
					if (IsBadName(p.Name))
						addMember(p);
				}
			}

			return results;
		}

		static Regex rxBadName = new Regex("^[pP]{1}[0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		static bool IsBadName(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;

			name = name.Trim();

			return rxBadName.IsMatch(name);
		}
	}

	public class MetadataMemberInfo
	{
		public IMemberDefinition NetMember { get; set; }
	}
}
