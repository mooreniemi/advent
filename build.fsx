// needed on OS X
// https://github.com/fsharp/FAKE/issues/739
#I "packages/FAKE/tools/"
// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"

open Fake

#I "packages/System.Runtime/lib/netcore50/"
#I "packages/xunit.runner.console/tools/"
#I "packages/xunit.extensibility.execution/lib/net45/"
#I "packages/xunit.extensibility.core/lib/portable-net45+win8+wp8+wpa81/"
#I "packages/Unquote/lib/net40/"

// Properties
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

Target "BuildApp" (fun _ ->
   !! "src/app/**/*.csproj"
     |> MSBuildRelease buildDir "Build"
     |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
    !! "src/test/**/*.csproj"
      |> MSBuildDebug testDir "Build"
      |> Log "TestBuild-Output: "
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// Dependencies
"Clean"
  ==> "BuildApp"
  ==> "BuildTest"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
