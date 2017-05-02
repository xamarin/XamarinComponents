Validate Xamarin.Forms with Android Support Libraries
=====================================================

This script is intended to run in CI and be used to help validate that Xamarin.Forms still properly builds from source against a particular version and build of Android Support Libraries and Google Play Services.

The script does the following:

 1. Removes `<Reference ..` elements from some *.csproj* files pertaining to Android Support and Play Services
 2. Removes items from the *packages.config* files pertaining to Android Support and Play Services
 3. Creates Paket config files  (*paket.dependencies* in the root and *paket.references* for all relevant *.csproj* files)
 4. Run paket forcing an update of packages
 5. Running a nuget restore
 6. Building the relevant *.csproj* targets in the solution.
 


Notes:

 - Paket is used to install the desired versions of packages since it's able to modify the .csproj from the command line (including importing .props/.targets files) more easily than through nuget.
 - Windows is not currently supported with this build script.
 - It's assumed that the CI job will pull in nuget packages from a previously successful upstream build for the version of things we're testing and put them in `./output`