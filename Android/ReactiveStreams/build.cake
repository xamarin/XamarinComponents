var TARGET = Argument ("t", Argument ("target", "ci"));

var REACTIVE_STREAMS_VERSION = "1.0.3";

var REACTIVE_STREAMS_NUGET_VERSION = REACTIVE_STREAMS_VERSION;
var REACTIVE_STREAMS_JAR_URL = $"https://search.maven.org/remotecontent?filepath=org/reactivestreams/reactive-streams/{REACTIVE_STREAMS_VERSION}/reactive-streams-{REACTIVE_STREAMS_VERSION}.jar";
var REACTIVE_STREAMS_SOURCES_URL = $"https://search.maven.org/remotecontent?filepath=org/reactivestreams/reactive-streams/{REACTIVE_STREAMS_VERSION}/reactive-streams-{REACTIVE_STREAMS_VERSION}-sources.jar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (REACTIVE_STREAMS_JAR_URL, "./externals/reactivestreams.jar");
	DownloadFile (REACTIVE_STREAMS_SOURCES_URL, "./externals/reactivestreams-sources.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/ReactiveStreams/ReactiveStreams.csproj", "/Project/PropertyGroup/PackageVersion", REACTIVE_STREAMS_NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./ReactiveStreams.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./ReactiveStreams.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget");

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

RunTarget (TARGET);
