# Xamarin.AndroidBinderator

An engine to generate Xamarin Binding projects from Maven repositories with a JSON config and razor templates.


## Features
 - Dependency chain automatically inferred from Maven
 - Simple JSON Config file to list Maven artifacts to generate binding projects for


## Usage

### JSON Config File

The JSON Config file (`config.json`) should contain information about all of the Maven artifacts you would like to generate binding projects for, as well as some basic information about templates and file paths.

Here is what a basic config file might look like:

```json
[
    {
        "mavenRepositoryType" : "Google",
        "slnFile" : "generated/AndroidSupport.sln",
        "additionalProjects" : [
            "source/buildtasks/support-annotations/Support-Annotations-BuildTasks.csproj"
        ],
        "templates" : [
            {
                "templateFile": "source/AndroidSupportTargets.cshtml",
                "outputFileRule" : "generated/{groupid}.{artifactid}/{nugetid}.targets"
            },
            {
                "templateFile": "source/AndroidSupportProject.cshtml",
                "outputFileRule" : "generated/{groupid}.{artifactid}/{groupid}.{artifactid}.csproj"
            }
        ],
        "artifacts" : [
            {
                "groupId" : "com.android.support",
                "artifactId" : "support-annotations",
                "version" : "28.0.0-beta01",
                "nugetId" : "Xamarin.Android.Support.Annotations"
            },
            {
                "groupId" : "com.android.support",
                "artifactId" : "support-compat",
                "version" : "28.0.0-beta01",
                "nugetId" : "Xamarin.Android.Support.Compat"
            },
        ]
    }
]
```

#### Basic Template values
You can see the basics like `mavenRepositoryType` (which can be `Google`, `Url` or `Directory` - if using Url or Directory you must also specify `mavenRepositoryLocation` containing the url or folder of the repo).

You can also specify a `slnFile` to be generated, relative to the base path you specify either in the config file (`"basePath" : "/some/path/"`) or when calling `BinderateAsync(..)`.

#### Templates
The `templates` section is a list of razor template files to run for each artifact, as well as an output file name rule, which can contain the placeholders: `{groupid}`, `{artifactid}`, `{name}` (project name), and `{nugetid}`.

#### Artifacts

You must add an entry for each Maven artifact you want to generate bindings for.  This should contain the Maven Group ID, Maven Artifact ID, Maven version to bind, and the NuGet Package ID to generate.

The config file also should contain Maven artifact information for any dependencies required by the bindings to be generated, which you do not also want to generate bindings for.  You must specify the Maven `groupId`, `artifactId`, and `version` you want to _binderate_,

For example if you have a Maven artifact you want _Binderate_, which depends on  Android Support v4, you can specify this artifact in the config file like this:

```json
{
    "groupId" : "com.android.support",
    "artifactId" : "support-v4",
    "version" : "26.1.0",
    "nugetId" : "Xamarin.Android.Support.v4",
    "nugetVersion" : "27.0.2",
    "dependencyOnly" : true
},
```

Notice we specified that this is a `dependencyOnly`, which means it won't have a binding generated for it, and we specified a `nugetVersion` to use in the `PackageReference`, even though the `version` is different, since this is the version that the maven artifact depending on this package is looking for to satisfy the dependency.


### Creating Template Files

Templates are created with Razor syntax.  You can specify multiple template files, and each template file will be run once per artifact to have bindings generated for, as per your JSON config file.

The `@Model` passed into each template looks like this:

@Model
- string Id
- string Name
- string MavenGroupId
- string NuGetPackageId
- string NuGetVersion
- string AssemblyName
- string[] ProjectReferences
- MavenArtifactModel[] MavenArtifacts
- NuGetDependencyModel[] NuGetDependencies
- BindingConfig Config

@MavenArtifactModel
- string MavenGroupId
- string MavenArtifactId
- string MavenArtifactVersion
- string MavenArtifactPackaging
- string MavenArtifactMd5
- string DownloadedArtifact
- string ProguardFile

@NuGetDependencyModel
- bool IsProjectReference
- string NuGetPackageId
- string NuGetVersion
- MavenArtifactModel MavenArtifact

### Running the Binderator

You can pull the binderator in as a nuget package.  Once this is done, it's trivial to run:

```csharp
var engine = new AndroidBinderator.Engine();
await engine.BinderateAsync("/path/to/config.json", "/path/to/base/output/");
```


