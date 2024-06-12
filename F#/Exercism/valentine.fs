module valentine

type Approval = 
    | Yes
    | Maybe
    | No

type Cuisine =
    | Korean
    | Turkish

type Genre = 
    | Crime
    | Horror
    | Romance
    | Thriller

type Activity =
    | BoardGame
    | Chill
    | Movie of Genre
    | Restaurant of Cuisine
    | Walk of int

let rateActivity (category: Activity) subtype =
    match category with
    | Restaurant subtype -> (match subtype with 
                            | Korean -> Approval.Yes
                            | Turkish -> Approval.Maybe
                            )
    | BoardGame -> Approval.No
    | Chill -> Approval.No
    | Movie subtype -> (match subtype with
                        | Romance -> Approval.Yes
                        | _ -> Approval.No
                        )
    | Walk subtype -> (match subtype with
                      | x when x < 3 -> Approval.Yes
                      | y when y < 5 -> Approval.Maybe
                      | _ -> Approval.No
                      )
