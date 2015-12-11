namespace Advent

open NUnit.Framework
open FsUnit
open DayOne

module DayOneSpec =
  [<Test>]
    let ``counts parens`` () =
      floorNumber "(())" |> should equal 0

