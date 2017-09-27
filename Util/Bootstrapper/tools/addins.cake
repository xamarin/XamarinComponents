// Tools needed by cake addins
#tool nuget:?package=XamarinComponent&version=1.1.0.42
#tool nuget:?package=ILRepack&version=2.0.13

// Dependencies of Cake Addins - this should be removed once 
// Cake 0.23 is out
#addin nuget:?package=SharpZipLib&version=0.86.0
#addin nuget:?package=Newtonsoft.Json&version=9.0.1
#addin nuget:?package=semver&version=2.0.4
#addin nuget:?package=YamlDotNet&version=4.2.1

// Cake Addins
#addin nuget:?package=Cake.FileHelpers&version=2.0.0
#addin nuget:?package=Cake.Json&version=2.0.28
#addin nuget:?package=Cake.Yaml&version=2.0.0
#addin nuget:?package=Cake.Xamarin&version=2.0.0
#addin nuget:?package=Cake.XCode&version=3.0.0
#addin nuget:?package=Cake.Xamarin.Build&version=3.0.3
