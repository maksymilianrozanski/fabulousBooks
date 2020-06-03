namespace FabBooks

open FabBooks.GoodreadsResponseModelModule

module Messages =

    type Status =
        | Success
        | Failure
        | Loading

    type Msg =
        | UpdateEnteredText of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateStatus of Status
        | DisplayDetailsPage of int
