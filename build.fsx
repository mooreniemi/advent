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

let dependencies = [ "packages/System.Runtime/lib/netcore50/System.Runtime.dll";
                    "packages/System.Private.Uri/lib/netcore50/System.Private.Uri.dll";
                    "packages/System.Diagnostics.Debug/lib/netcore50/System.Diagnostics.Debug.dll";
                    "packages/System.Reflection/lib/netcore50/System.Reflection.dll";
                    "packages/System.Threading.Tasks/lib/netcore50/System.Threading.Tasks.dll";
                    "packages/System.Collections/lib/netcore50/System.Collections.dll";
                    "packages/System.Linq/ref/netcore50/System.Linq.dll";
                    "packages/System.Reflection.Extensions/lib/netcore50/System.Reflection.Extensions.dll";
                    "packages/System.Globalization/lib/netcore50/System.Globalization.dll";
                    "packages/System.Runtime.Extensions/lib/netcore50/System.Runtime.Extensions.dll";
                    "packages/xunit.runner.console/tools/xunit.abstractions.dll";
                    "packages/xunit.extensibility.execution/lib/net45/xunit.execution.desktop.dll";
                    "packages/xunit.extensibility.core/lib/dotnet/xunit.core.dll";
                    "packages/Unquote/lib/net40/Unquote.dll";
                    "build/Source.dll" ]

Target "Tests.dll" (fun _ ->
  ["src/spec/dayOneSpec.fs"] //!! "src/spec/*.fs"
  |> Fsc (fun p ->
            { p with Output = "./build/test/Tests.dll"
                     FscTarget = Library
                     OtherParams = ["--standalone"]
                     References = dependencies })
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
