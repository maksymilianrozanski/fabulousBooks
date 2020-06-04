namespace FabBooks

open FabBooks.BookItemModule
open FabBooks.GoodreadsResponseModelModule

module Messages =

    type DisplayedPage =
        | SearchPage
        | DetailsPage of BookItem

    type Status =
        | Success
        | Failure
        | Loading

    type NavigateToDetailsPageMsg = NavigateToDetailsPageMsg of BookItem

    type Msg =
        | UpdateEnteredText of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateStatus of Status
        | ChangeDisplayedPage of DisplayedPage
