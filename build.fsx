// needed on OS X
// https://github.com/fsharp/FAKE/issues/739
#I "packages/FAKE/tools/"
// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.FscHelper

#I "packages/System.Runtime/lib/netcore50/"
#I "packages/xunit.runner.console/tools/"
#I "packages/xunit.extensibility.execution/lib/net45/"
#I "packages/xunit.extensibility.core/lib/portable-net45+win8+wp8+wpa81/"
#I "packages/Unquote/lib/net40/"

// http://fsharp.github.io/FAKE/fsc.html

// Properties
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

// https://github.com/fsharp/FAKE/issues/1037
Target "Source.dll" (fun _ ->
  ["src/lib/dayOne.fs"] //!! "src/lib/*.fs"
  |> Fsc (fun p ->
           { p with Output = "Source.dll"
                    FscTarget = Library })
)

Target "Tests.dll" (fun _ ->
  ["src/test/dayOneTests.fs"] //!! "src/spec/*.fs"
  |> Fsc (fun p ->
            { p with Output = "Tests.dll"
                     FscTarget = Library })
)

Target "Default" (fun _ ->
    trace "All set."
)

// Dependencies
"Clean"
  ==> "Source.dll"
  ==> "Tests.dll"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
