module yacht

type Category = 
    | Ones
    | Twos
    | Threes
    | Fours
    | Fives
    | Sixes
    | FullHouse
    | FourOfAKind
    | LittleStraight
    | BigStraight
    | Choice
    | Yacht

type Die =
    | One
    | Two
    | Three
    | Four
    | Five
    | Six


let getDie (d: Die): int = 
    match d with 
    | One -> 1
    | Two -> 2
    | Three -> 3
    | Four -> 4
    | Five -> 5
    | Six -> 6 

let remap dice = List.map getDie dice

let countFilteredBy (f : ('a -> bool)) (array : 'a[]) : int =
    Array.fold
        (fun acc item -> if f item
                         then acc + 1
                         else acc)
        0 array


let count (needle : 'a) (haystack : List<'a>) : int =
    List.fold
        (fun eax itr -> if itr = needle
                        then eax + 1
                        else eax)
        0 haystack

let ones (dice): int = remap dice |> List.filter(fun x -> x = 1) |> List.sum
let twos (dice): int = remap dice |> List.filter(fun x -> x = 2) |> List.sum
let threes (dice): int = remap dice |> List.filter(fun x -> x = 3) |> List.sum
let fours (dice): int = remap dice |> List.filter(fun x -> x = 4) |> List.sum
let fives (dice): int = remap dice |> List.filter(fun x -> x = 5) |> List.sum
let sixes (dice): int = remap dice |> List.filter(fun x -> x = 6) |> List.sum
let yacht dice : int = remap dice |> fun x -> if List.length(List.distinct x) = 1 then 50 else 0
let four_of_a_kind dice : int = remap dice |> List.filter(fun y -> (count y (remap dice)) >= 4) |> fun x -> if (List.length x) > 0 then (List.take 4 x |> List.sum) else List.sum x
let little_straight dice : int = remap dice |> List.sort |> List.compareWith (fun x y -> if x = y then 0 else 1) [1;2;3;4;5] |> fun x -> if x = 0 then 30 else 0
let big_straight dice : int = remap dice |> List.sort |> List.compareWith (fun x y -> if x = y then 0 else 1) [2;3;4;5;6] |> fun x -> if x = 0 then 30 else 0
let choice dice : int = remap dice |> List.sum

let distinct_dice dice = remap dice |> List.distinct
let first_in_dice dice = distinct_dice dice |> List.head |> fun num -> count num (remap dice)  |> fun n -> n &&& 2 <> 2

let full_house dice :int =  distinct_dice dice |> fun x -> if (List.tail x |> List.length) > 1 || (first_in_dice dice) then 0 else List.sum (remap dice)

let score (category: Category) (coll) : int =
    match category with
    | Ones -> ones coll
    | Twos -> twos coll
    | Threes -> threes coll
    | Fours -> fours coll
    | Fives -> fives coll
    | Sixes -> sixes coll
    | Yacht -> yacht coll
    | FourOfAKind -> four_of_a_kind coll
    | LittleStraight -> little_straight coll
    | BigStraight -> big_straight coll
    | Choice -> choice coll
    | FullHouse -> full_house coll