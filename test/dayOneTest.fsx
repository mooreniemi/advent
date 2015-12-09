#r "../packages/System.Runtime/lib/netcore50/System.Runtime.dll"
#r "../packages/xunit.runner.console/tools/xunit.abstractions.dll"
#r "../packages/xunit.extensibility.execution/lib/net45/xunit.execution.desktop.dll"
#r "../packages/xunit.extensibility.core/lib/portable-net45+win8+wp8+wpa81/xunit.core.dll"
#r "../packages/Unquote/lib/net40/Unquote.dll"

#load "../lib/dayOne.fsx"

open Xunit
open Swensen.Unquote

open DayOne

module DayOneTests =
  do
    printfn "running tests..."

  [<Fact>]
  let ``matches on one`` () =
    test <@ DayOne.someFunky 1 = "one" @>

  [<Fact>]
  let ``should be able to add two numbers from stack`` () =
    test <@ DayOne.someFunky 2 = "one" @>
