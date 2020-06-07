namespace FabBooks

open System.Data
open FabBooks
open FabBooks.BookItemModule
open FabBooks.GoodreadsResponseModelModule
open SingleBookResponseModelModule
open GoodreadsQuery

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
        | PerformSearch of SearchText * PageNum
        | SearchResultReceived of GoodreadsResponseModel
        | ChangeDisplayedPage of DisplayedPage
        //details messages
        | BookResultReceived of SingleBookResponseModel
        | UpdateBookDetails of BookItem

    type CmdMsg =
        | PerformSearchCmd of SearchText * PageNum
        | UpdateBookDetailsCmd of BookItem
