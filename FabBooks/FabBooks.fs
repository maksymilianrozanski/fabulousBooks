// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open Models
open Fabulous
open Fabulous.XamarinForms
open MainModel
open SearchPageViews
open Update
open UpdateCmd

module App =

    let update =
        function
        | Msg.SearchTextEntered (text, pageNum) ->
            onPerformSearch (text, pageNum)
        | Msg.SearchResultReceived result ->
            onSearchResultReceived result
        | Msg.MoreBooksReceived result ->
            onMoreBooksReceived result
        | Msg.ChangeDisplayedPage page ->
            onChangeDisplayedPage page
        | Msg.MoreBooksRequested (searchText, endBook) ->
            onMoreBooksRequested (searchText, endBook)
        | Msg.BookResultReceived result ->
            onBookResultReceived result
        | Msg.OpenBrowserRequested book -> onOpenInBrowser book
        | Msg.BrowserOpened _ -> onBrowserOpened
        | Msg.SaveGoodreadsKey (key, savingFunc) ->
            onSavingGoodreadsKey (key, savingFunc)
        | Msg.RemoveGoodreadsKey deletingFunc -> onRemoveGoodreadsKey (deletingFunc)
        | Msg.UpdateBookDetails book ->
            onUpdateBookDetails book
        | Msg.BookSortingRequested -> onBookSortingRequested

    let view (model: Model) dispatch =
        View.NavigationPage
            (backgroundColor = Colors.backgroundPrimary,
             pages =
                 [ yield searchPageView model dispatch
                   match model.BookDetailsPageModel with
                   | Some x -> yield bookDetailsPageView model.BookDetailsPageModel.Value dispatch
                   | _ -> () ], popped = fun _ -> Msg.ChangeDisplayedPage SearchPage |> dispatch)

    let mapCommands cmdMsg =
        match cmdMsg with
        | PerformSearchCmd (apiKey, searchText, pageNum) -> performSearchCmd apiKey searchText pageNum
        | UpdateBookDetailsCmd (apiKey, bookItem) -> updateBookDetailsCmd apiKey bookItem
        | MoreBooksRequestedCmd (apiKey, searchText, endBook) ->
            onMoreBooksRequestedCmd (apiKey, searchText, endBook)
        | OpenBrowserCmd book -> onOpenBrowserCmd (book)
        | DeleteApiKeyCmd -> onDeleteApiKeyCmd ()

    let init () =
        { GoodreadsApiKey = PreferencesModule.getApiKey ()
          SearchPageModel = initSearchPageModel
          BookDetailsPageModel = None }, []

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgramWithCmdMsg init update view mapCommands
