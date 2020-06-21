namespace FabBooks

open FabBooks
open MainMessages
open SearchQuery
open Fabulous
open BookItemModule
open BookDetailsQuery
open Utils
open Xamarin.Essentials


module UpdateCmd =

    let performSearchCmd apiKey text pageNum =
        searchWithPage apiKey text pageNum
        |> Async.map Msg.SearchResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let updateBookDetailsCmd apiKey (bookItem: BookItem) =
        bookGet apiKey bookItem.Id
        |> Async.map Msg.BookResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let onMoreBooksRequestedCmd (apiKey, searchText, endBook) =
        searchWithPage apiKey (searchText) (nextPageNumber (endBook))
        |> Async.map Msg.MoreBooksReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let onOpenBrowserCmd (book: BookItem) =
        Browser.OpenAsync
            (sprintf ("https://www.goodreads.com/book/show/%i") book.Id, BrowserLaunchMode.SystemPreferred)
        |> Async.AwaitIAsyncResult
        |> Async.map Msg.BrowserOpened
        |> Cmd.ofAsyncMsg

    let onDeleteApiKeyCmd () =
        PreferencesModule.deleteKey () |> ignore
        Cmd.none
