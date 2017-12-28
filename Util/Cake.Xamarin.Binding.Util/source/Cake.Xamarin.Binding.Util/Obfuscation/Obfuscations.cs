using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cake.Core.IO;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Cake.Xamarin.Binding.Util
{
	class Obfuscations
	{
		static string[] OBFUSCATED_EXCEPTIONS = { "or", "if", "not", "get", "set", "add", "and", "tag", "api", "end", "put", "log", "url" };
		static bool showHelp = false;
		static bool ignoreDefaultExceptions = false;
		static List<string> exceptions = new List<string>();
		static List<string> regexes = new List<string>();
		static bool ignoreMembers = false;
		static bool ignoreTypes = false;

		public static FindObfuscationResults Find(string assemblyFile, bool ignoreDefaultExceptions = false, bool ignoreMembers = false, bool ignoreTypes = false, string[] regexPatterns = null)
		{
			if (!ignoreDefaultExceptions)
				exceptions.AddRange(OBFUSCATED_EXCEPTIONS);

			if (!regexes.Any())
				regexes.Add("^[a-zA-Z]{1,3}$");

			//regexes.Add("^internal/.*$");

			var assembly = AssemblyDefinition.ReadAssembly(assemblyFile);

			var types = new List<TypeDefinition>();

			var results = new FindObfuscationResults ();
			var obfuscatedTypes = new Dictionary<string, ObfuscatedTypeInfo>();
			var obfuscatedMembers = new Dictionary<string, ObfuscatedMemberInfo>();

			// Get *ALL* types and nested types from both assemblies
			foreach (var m in assembly.Modules)
			{
				types.AddRange(m.GetAllTypes());
			}

			foreach (var t in types)
			{

				var tj = GetJavaName(t.CustomAttributes);

				if (!ignoreTypes)
				{
					if (!string.IsNullOrEmpty(tj))
					{
						if (!obfuscatedTypes.ContainsKey(tj))
							obfuscatedTypes.Add(tj, new ObfuscatedTypeInfo { NetType = t, JavaType = tj });
					}
				}

				if (!ignoreMembers)
				{
					foreach (var m in t.Methods)
					{
						if (!m.HasCustomAttributes)
							continue;

						var j = GetJavaName(m.CustomAttributes);

						if (!string.IsNullOrEmpty(j))
						{
							if (!obfuscatedMembers.ContainsKey(j))
								obfuscatedMembers.Add(j, new ObfuscatedMemberInfo { NetType = m.DeclaringType, NetMember = m, JavaMember = j });
						}
					}

					foreach (var f in t.Fields)
					{
						if (!f.HasCustomAttributes)
							continue;

						var j = GetJavaName(f.CustomAttributes);

						if (!string.IsNullOrEmpty(j))
						{
							if (!obfuscatedMembers.ContainsKey(j))
								obfuscatedMembers.Add(j, new ObfuscatedMemberInfo { NetType = f.DeclaringType, NetMember = f, JavaMember = j });
						}
					}

					foreach (var p in t.Properties)
					{
						if (!p.HasCustomAttributes)
							continue;

						var j = GetJavaName(p.CustomAttributes);

						if (!string.IsNullOrEmpty(j))
						{
							if (!obfuscatedMembers.ContainsKey(j))
								obfuscatedMembers.Add(j, new ObfuscatedMemberInfo { NetType = p.DeclaringType, NetMember = p, JavaMember = j });
						}
					}
				}
			}

			if (obfuscatedTypes != null && obfuscatedTypes.Count > 0)
				results.Types.AddRange(obfuscatedTypes.Values);

			if (obfuscatedMembers != null && obfuscatedMembers.Count > 0)
				results.Members.AddRange(obfuscatedMembers.Values);

			return results;
		}



		static void parseExceptions(string e)
		{
			if (string.IsNullOrEmpty(e))
				return;

			var items = e.Split(',', ';');

			if (items == null)
				return;

			foreach (var i in items)
			{
				if (string.IsNullOrEmpty(i))
					continue;

				exceptions.Add(i.Trim());
			}
		}

		static string GetJavaName(Mono.Collections.Generic.Collection<CustomAttribute> attributes)
		{
			string name = null;

			foreach (var a in attributes)
			{
				if (a.AttributeType.Name == "RegisterAttribute")
				{

					if (a.HasConstructorArguments)
					{
						name = a.ConstructorArguments[0].Value as string;
						break;
					}
				}
			}

			if (string.IsNullOrEmpty(name))
				return null;


			var fullName = name;

			if (name.Contains('/'))
				name = name.Substring(name.LastIndexOf('/') + 1);

			if (exceptions.Contains(name.ToLowerInvariant()))
				return null;


			var matched = false;
			foreach (var r in regexes)
			{
				if (Regex.IsMatch(name, r, RegexOptions.Singleline) || Regex.IsMatch(fullName, r, RegexOptions.Singleline))
				{
					matched = true;
					break;
				}
			}

			if (!matched)
				return null;

			return fullName;
		}

		static void ExtractTypes(List<TypeDefinition> types, TypeDefinition typeDef)
		{
			if (!typeDef.IsPublic)
				return;

			types.Add(typeDef);

			foreach (var nestedType in typeDef.NestedTypes)
				ExtractTypes(types, nestedType);
		}
	}

	public class ObfuscatedTypeInfo
	{
		public TypeDefinition NetType { get; set; }
		public string JavaType { get; set; }
	}

	public class ObfuscatedMemberInfo
	{
		public TypeDefinition NetType { get; set; }
		public IMemberDefinition NetMember { get; set; }
		public string JavaMember { get; set; }
	}

	public class FindObfuscationResults
	{
		public List<ObfuscatedTypeInfo> Types { get; set; } = new List<ObfuscatedTypeInfo>();
		public List<ObfuscatedMemberInfo> Members { get; set; } = new List<ObfuscatedMemberInfo>();
	}
}
