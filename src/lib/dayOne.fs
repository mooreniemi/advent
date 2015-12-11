namespace Advent

module DayOne =
  let floorNumber = function
    | '(' -> 1
    | ')' -> -1
    | _ -> 0

  let santaClimbing (stairs : string) =
    List.ofSeq stairs |> List.map floorNumber |> List.sum
