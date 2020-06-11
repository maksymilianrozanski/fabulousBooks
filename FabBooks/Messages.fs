namespace FabBooks

open System.Data
open FabBooks
open FabBooks.BookItemModule
open FabBooks.GoodreadsResponseModelModule
open SingleBookResponseModelModule
open SearchQuery

module MainMessages =
    type DisplayedPage =
        | SearchPage
        | DetailsPage of BookItem

    type LastLoadedBook = int

    type TotalBooks = int
    
    type LastItemVisible = int

    type Status =
        | Success
        | Failure
        | Loading

    type Msg =
        //search messages
        | PerformSearch of SearchText * PageNum
        | SearchResultReceived of GoodreadsResponseModel
        | SearchResultReceived2 of GoodreadsResponseModel
        | MoreBooksReceived of GoodreadsResponseModel
        | MoreBooksReceived2 of GoodreadsResponseModel
        | ChangeDisplayedPage of DisplayedPage
        | MoreBooksRequested of SearchText * LastLoadedBook
        //details messages
        | BookResultReceived of SingleBookResponseModel
        | UpdateBookDetails of BookItem

    type CmdMsg =
        | PerformSearchCmd of SearchText * PageNum
        | UpdateBookDetailsCmd of BookItem
        | MoreBooksRequestedCmd of SearchText * LastLoadedBook
