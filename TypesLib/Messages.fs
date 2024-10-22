namespace FabBooks

open System
open FabBooks
open FabBooks.BookItemModule
open FabBooks.Responses

module MainMessages =
    type DisplayedPage =
        | SearchPage
        | DetailsPage of BookItem

    type LastLoadedBook = int

    type PageNum = int

    type SearchText = string
    type ApiKey = string
    let deleteApiKeyCommand = "-deleteapikey"

    type Status =
        | Success
        | Failure
        | Loading

    type Msg =
        //search messages
        | SearchTextEntered of SearchText * PageNum
        | SearchResultReceived of SearchResponse
        | MoreBooksReceived of SearchResponse
        | ChangeDisplayedPage of DisplayedPage
        | MoreBooksRequested of SearchText * LastLoadedBook
        | BookSortingRequested
        | OpenBrowserRequested of BookItem
        | BrowserOpened of Boolean
        //api key
        | SaveGoodreadsKey of (string * (string -> string))
        | RemoveGoodreadsKey of (unit -> bool)
        //details messages
        | BookResultReceived of SingleBookResponse
        | UpdateBookDetails of BookItem

    type CmdMsg =
        | PerformSearchCmd of ApiKey * SearchText * PageNum
        | UpdateBookDetailsCmd of ApiKey * BookItem
        | MoreBooksRequestedCmd of ApiKey * SearchText * LastLoadedBook
        | OpenBrowserCmd of BookItem
        | DeleteApiKeyCmd
