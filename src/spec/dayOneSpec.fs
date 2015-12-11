namespace Advent

open NUnit.Framework
open FsUnit
open DayOne

module DayOneSpec =
  [<Test>]
    let ``parses left paren as up`` () =
      floorNumber '(' |> should equal 1

  [<Test>]
    let ``parses right paren as down`` () =
      floorNumber ')' |> should equal -1 

  [<Test>]
    let ``counts parens`` () =
      santaClimbing "(())" |> should equal 0

  [<Test>]
    let ``counts more parens`` () =
      santaClimbing "()()" |> should equal 0
