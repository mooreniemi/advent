namespace Advent

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
