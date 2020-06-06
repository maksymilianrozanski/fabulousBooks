namespace FabBooks

open FabBooks.MainMessages

module Utils =
    let statusFromBool (isSuccessful: bool) =
        match isSuccessful with
        | true -> Status.Success
        | _ -> Status.Failure
