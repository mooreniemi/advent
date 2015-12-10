namespace Advent
open FSharp.Data

module DayOne =
  type LightBulb(state) =
      member x.On = state
      override x.ToString() =
          match x.On with
          | true  -> "On"
          | false -> "Off"
