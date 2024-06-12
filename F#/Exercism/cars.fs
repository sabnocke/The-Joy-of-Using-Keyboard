module cars

let successRate (speed: int): float =
    match speed with
    | x when x = 0 -> 0.0
    | x when x > 0 && x <= 4 -> 1.0
    | x when x > 4 && x <= 8 -> 0.9
    | x when x = 9 -> 0.8
    | x when x = 10 -> 0.77
    | _ -> 0.5

let productionRatePerHour (speed: int): float =
    float speed * (successRate speed) * 221.0

let workingItemsPerMinute (speed: int): int =
    int (productionRatePerHour speed / float 60)