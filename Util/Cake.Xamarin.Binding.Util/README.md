Cake.Xamarin.Binding.Util
=========================

An Add-In for [Cake Build](http://cakebuild.net) which contains some useful aliases for Xamarin's binding projects.


**FindObfuscations**

Finds likely obfuscated types and members based on the java type or member name.
NOTE: The default regex pattern looks for types or members with only 3 character long names.

```csharp
FindObfuscationResults FindObfuscations (FilePath assembly, bool ignoreDefaultExceptions = false, bool ignoreMembers = false, bool ignoreTypes = false, string[] regexPatterns = null);
```

**FindMissingMetadata**

Finds members of types in an assembly which do not have proper names (eg: p0, P1, etc) which means they are missing metadata from the Android binding.

```csharp
List<MetadataMemberInfo> FindMissingMetadata (FilePath assembly)
```