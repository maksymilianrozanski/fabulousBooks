namespace FabBooks

open FabBooks.MainMessages

module Utils =
    let statusFromBool (isSuccessful: bool) =
        match isSuccessful with
        | true -> Status.Success
        | _ -> Status.Failure

    let nextPageNumber lastLoadedBook = ((lastLoadedBook / 20) + 1)

    type ShouldFetchArgs =
        { LastLoadedItem: int
          TotalItems: int
          Status: Status
          Idx: int }

    type ShouldStop = ShouldStop

    let isSuccessStatus (data: ShouldFetchArgs) =
        if data.Status = Status.Success then Right(data) else Left(ShouldStop)

    let areItemsRemaining (data: ShouldFetchArgs) =
        if data.LastLoadedItem < data.TotalItems then Right(data) else Left(ShouldStop)

    let isNearEnd margin (data: ShouldFetchArgs) =
        //idx starts from 0, item indexes starts from 1
        if (data.LastLoadedItem - (data.Idx + 1)) <= margin
        then Right(data) //Right if items below screen edge number less than or equal margin
        else Left(ShouldStop)

    let shouldFetchMoreItems margin (lastLoadedItem: int) (totalItems: int) (status: Status) (idx: int) =
        let data =
            { LastLoadedItem = lastLoadedItem
              TotalItems = totalItems
              Status = status
              Idx = idx }

        data
        |> isSuccessStatus
        |> bind (isNearEnd margin)
        |> bind areItemsRemaining
        |> function
        | Right _ -> true
        | Left _ -> false
