// https://msdn.microsoft.com/en-us/library/dd233175.aspx

#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

module DayOne = 
  let someFunky =
    function
      | 1 -> "one" 
      | _ -> "not one"
