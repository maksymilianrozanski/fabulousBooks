namespace FabBooks

open FabBooks.BookItemModule
open FabBooks.GoodreadsResponseModelModule
open SingleBookResponseModelModule

module Messages =
    type DisplayedPage =
        | SearchPage
        | DetailsPage of BookItem

    type Status =
        | Success
        | Failure
        | Loading

    type Msg =
        | UpdateEnteredText of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateStatus of Status
        | ChangeDisplayedPage of DisplayedPage

module DetailsMessages =
    type Msg =
        | NavigateToDetailsPageMsg of BookItem
        | BookResultReceived of SingleBookResponseModel
