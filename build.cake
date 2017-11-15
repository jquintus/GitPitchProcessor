///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var framework = Argument("framework", "netcoreapp2.0");
var slnFile = "src/gpp.sln";

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
.Does(()=> {
    DotNetCoreClean(slnFile);
});

Task("Build")
.IsDependentOn("Clean")
.Does(()=> {
    var buildSettings = new DotNetCoreBuildSettings
     {
         Framework = framework,
         Configuration = configuration,
     };
    DotNetCoreBuild(slnFile);
});

Task("Publish-AppVeyor")
.IsDependentOn("Build")
.WithCriteria(BuildSystem.IsRunningOnAppVeyor)
.Does(()=> {
    var output = "src/gpp/bin/" + configuration + "/" + framework + "/gpp.dll";
    BuildSystem.AppVeyor.UploadArtifact(output);
});

Task("Publish")
.IsDependentOn("Publish-AppVeyor");

Task("Default")
.IsDependentOn("Publish");

RunTarget(target);