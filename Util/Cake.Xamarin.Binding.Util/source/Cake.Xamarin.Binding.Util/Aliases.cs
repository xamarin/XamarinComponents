using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Xamarin.Binding.Util
{
	[CakeNamespaceImport("Cake.Xamarin.Binding.Util")]
	[CakeAliasCategory("Xamarin Binding Utilities")]
	public static class BindingUtilAliases
	{
		/// <summary>
		/// Finds likely obfuscated types and members based on the java type or member name.
		/// The default regex pattern looks for types or members with only 3 character long names.
		/// </summary>
		/// <returns>The obfuscations.</returns>
		/// <param name="context">The context.</param>
		/// <param name="assembly">The assembly file to search in.</param>
		/// <param name="ignoreDefaultExceptions">If set to <c>true</c> ignore default exceptions (such as to, for, at, etc).</param>
		/// <param name="ignoreMembers">If set to <c>true</c> ignore members.</param>
		/// <param name="ignoreTypes">If set to <c>true</c> ignore types.</param>
		/// <param name="regexPatterns">Custom regex patterns to use instead of the default.  These are based on a single line regex.</param>
		[CakeMethodAlias]
		public static FindObfuscationResults FindObfuscations (this ICakeContext context, FilePath assembly, bool ignoreDefaultExceptions = false, bool ignoreMembers = false, bool ignoreTypes = false, string[] regexPatterns = null)
		{
			return Obfuscations.Find(assembly.MakeAbsolute(context.Environment).FullPath, ignoreDefaultExceptions, ignoreMembers, ignoreTypes, regexPatterns);
		}

		/// <summary>
		/// Finds members of types in an assembly which do not have proper names (eg: p0, P1, etc) which means they are missing metadata from the Android binding.
		/// </summary>
		/// <returns>The missing metadata.</returns>
		/// <param name="context">The context.</param>
		/// <param name="assembly">The assembly file to search in.</param>
		[CakeMethodAlias]
		public static List<MetadataMemberInfo> FindMissingMetadata (this ICakeContext context, FilePath assembly)
		{
			return AndroidMetadata.FindMissing(assembly.MakeAbsolute(context.Environment).FullPath);
		}
	}
}
