namespace FabBooks

open System.Data
open FabBooks
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
        //search messages
        | PerformSearch of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateSearchStatus of Status
        | ChangeDisplayedPage of DisplayedPage
        //details messages
        | NavigateToDetailsPageMsg of BookItem
        | BookResultReceived of SingleBookResponseModel
        | UpdateBookDetails of BookItem
        | UpdateDetailsStatus of Status

    type CmdMsg =
        //search messages
        | PerformSearch of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateSearchStatus of Status
        | ChangeDisplayedPage of DisplayedPage
        //details messages
        | NavigateToDetailsPageMsg of BookItem
        | BookResultReceived of SingleBookResponseModel
        | UpdateBookDetails of BookItem
        | UpdateDetailsStatus of Status