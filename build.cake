///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var framework = Argument("framework", "netcoreapp2.0");
var slnFile = "src/GitPitchProcessor.sln";

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
.Does(()=> {
    DotNetCoreClean(slnFile);
});

Task("Restore")
.Does(()=> {
    DotNetCoreRestore(slnFile);
});

Task("Build")
.IsDependentOn("Restore")
.Does(()=> {
    var buildSettings = new DotNetCoreBuildSettings
     {
         Framework = framework,
         Configuration = configuration,
     };
    DotNetCoreBuild(slnFile, buildSettings);
});

Task("Publish-AppVeyor")
.IsDependentOn("Build")
.WithCriteria(BuildSystem.IsRunningOnAppVeyor)
.Does(()=> {
    var output = "src/GitPitchProcessor/bin/" + configuration + "/" + framework + "/GitPitchProcessor.dll";
    BuildSystem.AppVeyor.UploadArtifact(output);
});

Task("Publish")
.IsDependentOn("Publish-AppVeyor");

Task("Default")
.IsDependentOn("Publish");

RunTarget(target);
