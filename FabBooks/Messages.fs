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
        | ChangeDisplayedPage of DisplayedPage
        //details messages
        | BookResultReceived of SingleBookResponseModel
        | UpdateBookDetails of BookItem

    type CmdMsg =
        //search messages
        | PerformSearch of string
        | SearchResultReceived of GoodreadsResponseModel
        | ChangeDisplayedPage of DisplayedPage
        //details messages
        | BookResultReceived of SingleBookResponseModel
        | UpdateBookDetails of BookItem
