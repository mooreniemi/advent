namespace Advent

open System.IO

module DayOne =
  let getInstructions filePath = System.IO.File.ReadAllText(filePath)

  let floorNumber = function
    | '(' -> 1
    | ')' -> -1
    | _ -> 0

  let santaClimbing (stairs : string) =
    List.ofSeq stairs |> List.map floorNumber |> List.sum

  let whatFloor =
    santaClimbing (getInstructions "data/day1.txt")

