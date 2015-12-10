// needed on OS X
// https://github.com/fsharp/FAKE/issues/739
#I "packages/FAKE/tools/"
// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.FscHelper
open Fake.Testing

// http://fsharp.github.io/FAKE/fsc.html

// Properties
let buildDir = "./build/"
let testDir  = "./build/test/"

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

// https://github.com/fsharp/FAKE/issues/1037
Target "Source.dll" (fun _ ->
  ["src/lib/dayOne.fs"] //!! "src/lib/*.fs"
  |> Fsc (fun p ->
           { p with Output = "./build/Source.dll"
                    FscTarget = Library })
)

Target "Tests.dll" (fun _ ->
  ["src/spec/dayOneSpec.fs"] //!! "src/spec/*.fs"
  |> Fsc (fun p ->
            { p with Output = "./build/test/Tests.dll"
                     FscTarget = Library
                     References =
                      [ "packages/System.Runtime/lib/netcore50/System.Runtime.dll"
                        "packages/xunit.runner.console/tools/xunit.abstractions.dll"
                        "packages/xunit.extensibility.execution/lib/net45/xunit.execution.desktop.dll"
                        "packages/xunit.extensibility.core/lib/dotnet/xunit.core.dll"
                        "packages/Unquote/lib/net40/Unquote.dll"
                        "build/Source.dll" ] }) 
)

// define test dlls
let testDlls = !! (testDir + "/Tests.dll")

Target "xUnitTest" (fun _ ->
    testDlls
        |> xUnit2 (fun p -> 
            {p with 
                ShadowCopy = false })
)

Target "Default" (fun _ ->
    trace "All set."
)

// Dependencies
"Clean"
  ==> "Source.dll"
  ==> "Tests.dll"
  ==> "xUnitTest"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
