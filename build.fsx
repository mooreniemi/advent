// needed on OS X
// https://github.com/fsharp/FAKE/issues/739
#I "packages/FAKE/tools/"
// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.FscHelper
open Fake.Testing
open Fake.FileUtils

// Properties
let buildDir = "./build/"
let testDir  = "./build/test/"

let depDlls = [ "packages/FsUnit/lib/net45/FsUnit.NUnit.dll"
                "packages/FsUnit/lib/net45/NHamcrest.dll"
                "packages/NUnit/lib/nunit.framework.dll"
                "build/Source.dll" ]

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

// http://fsharp.github.io/FAKE/fsc.html
Target "Source.dll" (fun _ ->
  ["src/lib/dayOne.fs"] //!! "src/lib/*.fs"
  // https://github.com/fsharp/FAKE/issues/1037
  |> Fsc (fun p ->
           { p with Output = "./build/Source.dll"
                    FscTarget = Library })
)

Target "TestDeps" (fun _ ->
  // http://fsharp.github.io/FAKE/apidocs/fake-filehelper.html
  CopyFiles testDir depDlls
)

Target "Tests.dll" (fun _ ->
  ["src/spec/dayOneSpec.fs"] //!! "src/spec/*.fs"
  |> Fsc (fun p ->
            { p with Output = "./build/test/Tests.dll"
                     FscTarget = Library
                     // OtherParams = ["--standalone"]
                     References = depDlls })
)

// define test dlls
let testDlls = !! (testDir + "/Tests.dll")
// http://fsharp.github.io/FAKE/apidocs/fake-nunitcommon.html
Target "NUnitTest" (fun _ ->
    testDlls
        |> NUnit (fun p ->
            { p with
                ToolPath = "packages/NUnit.Runners/tools/"
                // https://github.com/fsharp/FAKE/issues/1010
                ToolName = "nunit-console.exe"
                OutputFile = testDir + "TestResults.xml"})
)

Target "Default" (fun _ ->
    trace "All set."
)

// Dependencies
"Clean"
  ==> "Source.dll"
  ==> "TestDeps"
  ==> "Tests.dll"
  ==> "NUnitTest"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
