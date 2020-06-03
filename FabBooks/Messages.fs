namespace FabBooks

open FabBooks.GoodreadsResponseModelModule

module Messages =

    type Status =
        | Success
        | Failure
        | Loading
        
    type NavigateToDetailsPageMsg =
        | NavigateToDetailsPageMsg of int 
    type Msg =
        | UpdateEnteredText of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateStatus of Status
        | DisplayDetailsPage