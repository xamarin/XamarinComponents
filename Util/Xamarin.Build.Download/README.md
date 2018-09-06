# Xamarin.Build.Download

The Xamarin.Build.Download NuGet is intended to be consumed from MSBuild targets in other NuGets in order to download
third-party native library archives automatically and inject them into the build process.

## USAGE

Add a dependency on the Xamarin.Build.Download NuGet. When it is installed into a project, its targets will
be imported into the project. You can then use their functionality from your NuGet's build targets as follows.

Use `XamarinBuildDownload` items to specify archives to download and unpack. They must specify a unique versioned id
for the ItemSpec (Include attribute). This will be used as the global download cache key so must be unique to the
archive. However, multiple NuGets may refer to the the same archive using the same key.

The download URL must be specified in the `Url` metadata. A SHA1 hash may optionally be specified in the `Sha1`
metadata, and will be verified if provided. The archive kind must be specified in the `Kind` metadata, if it cannot
be inferred from the filename. Valid values are `Zip` or `Tgz`.

```xml
<ItemGroup>
	<XamarinBuildDownload Include="foo-1.2.3">
		<Url>http://example.com/foo-1.2.3.tgz</Url>
		<Kind>Tgz</Kind>
		<Sha1>0c4a8a9c12305e8d41e8e3c8a3a2ce066c508f68</Sha1>
	</XamarinBuildDownload>
</ItemGroup>
```

Other NuGets may download the same library, and should use the same key to prevent it being downloaded and
unpacked multiple times.

Define a new build target with a name unique to your NuGet. It should insert items into the build process that refer
to files from the unpacked archive, which will be in subdirectories of the `$(XamarinBuildDownloadDir)` directory
corresponding to the `Id` metadata for each archive:

```xml
<Target Name="_AddMyNugetIdDownloadedItems">
	<ItemGroup>
		<BundleResource Include="$(XamarinBuildDownloadDir)foo-1.2.3\media\bar.png">
			<LogicalName>bar.png</LogicalName>
		</BundleResource>
		<NativeReference Include="$(XamarinBuildDownloadDir)foo-1.2.3\lib\baz.a">
			<Kind>Static</Kind>
			<ForceLoad>True</ForceLoad>
		</NativeReference>
	</ItemGroup>
</Target>
```

Add your target as an `XamarinIncludeDownloadedItemsTarget` item so that it is called at the appropriate place
in the build process:

```xml
<ItemGroup>
	<XamarinBuildMergeDownloads Include="_AddMyNugetIdDownloadedItems"/>
</ItemGroup>
```

## EXAMPLE

In this example, the download is only performed if the project is a Xamarin.iOS executable project.

```xml
<Project DefaultTargets="Build" ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
	<ItemGroup Condition="'$(OutputType)'!='Library' And '$(TargetFramework)'=='Xamarin.iOS'">
		<XamarinBuildDownload Include="foo-1.2.3">
			<Url>http://example.com/foo-1.2.3.tgz</Url>
			<Kind>Tgz</Kind>
			<Sha1>0c4a8a9c12305e8d41e8e3c8a3a2ce066c508f68</Sha1>
		</XamarinBuildDownload>
		<XamarinBuildMergeDownloads Include="_AddMyNugetIdDownloadedItems"/>
	</ItemGroup>

	<Target Name="_AddMyNugetIdDownloadedItems">
		<ItemGroup>
			<BundleResource Include="$(XamarinBuildDownloadDir)foo-1.2.3\media\bar.png">
				<LogicalName>bar.png</LogicalName>
			</BundleResource>
			<NativeReference Include="$(XamarinBuildDownloadDir)foo-1.2.3\lib\baz.a">
				<Kind>Static</Kind>
				<ForceLoad>True</ForceLoad>
			</NativeReference>
		</ItemGroup>
	</Target>
</Project>
```

## TODO

* Implement download cache pruning
* Reference counting for cleaning up old unpacked archives
* Remove iOSReferenceMerge once Xamarin.iOS supports all the NativeReference metadata
