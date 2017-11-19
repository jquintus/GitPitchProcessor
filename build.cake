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
.IsDependentOn("Clean")
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
        ArgumentCustomization = args => args.Append($"--no-restore")
     };

    DotNetCoreBuild(slnFile, buildSettings);
});

Task("Test")
.IsDependentOn("Build")
.Does(() => {
         
    var settings = new DotNetCoreTestSettings
    {
         Configuration = configuration,
         Framework = framework,
         NoBuild = true,
    };
    
    var projects = GetFiles("./src/**/*.Tests.fsproj");
    foreach (var project in projects)
    {
        DotNetCoreTest(project.FullPath, settings);
    }
});

Task("Publish-AppVeyor")
.IsDependentOn("Build")
.WithCriteria(BuildSystem.IsRunningOnAppVeyor)
.Does(()=> {
    var output = "src/GitPitchProcessor/bin/" + configuration + "/" + framework + "/GitPitchProcessor.dll";
    BuildSystem.AppVeyor.UploadArtifact(output);
});

Task("Publish")
.IsDependentOn("Test")
.IsDependentOn("Publish-AppVeyor");

Task("Default")
.IsDependentOn("Publish");

RunTarget(target);
