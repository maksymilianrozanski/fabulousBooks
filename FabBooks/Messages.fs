namespace FabBooks

open System.Data
open FabBooks.BookItemModule
open FabBooks.GoodreadsResponseModelModule
open SingleBookResponseModelModule

module MainMessages =
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
        | UpdateBookDetails of BookItem
        | UpdateStatus of MainMessages.Status
