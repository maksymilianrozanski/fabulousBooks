namespace FabBooks

open FabBooks.GoodreadsResponseModelModule

module Messages =

    type DisplayedPage =
        | SearchPage
        | DetailsPage

    type Status =
        | Success
        | Failure
        | Loading

    type NavigateToDetailsPageMsg = NavigateToDetailsPageMsg of int

    type Msg =
        | UpdateEnteredText of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateStatus of Status
        | ChangeDisplayedPage of DisplayedPage
