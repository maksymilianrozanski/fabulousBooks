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
          Status: Status }

    type ShouldStop = ShouldStop

    let isSuccessStatus (data: ShouldFetchArgs) =
        if data.Status = Status.Success then Right(data) else Left(ShouldStop)

    let areItemsRemaining (data: ShouldFetchArgs) =
        if data.LastLoadedItem < data.TotalItems then Right(data) else Left(ShouldStop)

    let shouldFetchMoreItems (lastLoadedItem: int) (totalItems: int) (status: Status) =
        let data =
            { LastLoadedItem = lastLoadedItem
              TotalItems = totalItems
              Status = status }

        data
        |> isSuccessStatus
        |> bind areItemsRemaining
        |> function
        | Right _ -> true
        | Left _ -> false

    let hasImage (imageUrl: string) = not (imageUrl.StartsWith("https://s.gr-assets.com/assets/nophoto"))
