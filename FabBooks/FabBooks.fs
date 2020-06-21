﻿// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open Models
open Fabulous
open Fabulous.XamarinForms
open SearchQuery
open BookItemModule
open Utils
open MainModel
open SearchPageViews
open FabBooks.SearchResponseModule
open Xamarin.Essentials
open UpdateCmd

module App =
    let modelWithoutKey =
        { GoodreadsApiKey = None
          SearchPageModel = initSearchPageModel
          BookDetailsPageModel = None }

    let private onMsgPerformSearch (text, pageNum) model =
        if (text = MainMessages.deleteApiKeyCommand) then
            modelWithoutKey, [ DeleteApiKeyCmd ]
        else
            { model with
                  SearchPageModel =
                      { model.SearchPageModel with
                            Status = Status.Loading
                            EnteredText = text } }, [ (model.GoodreadsApiKey.Value, text, pageNum) |> PerformSearchCmd ]

    let private onMoreBooksRequested (searchText, endBook) model =
        { model with SearchPageModel = { model.SearchPageModel with Status = Status.Loading } },
        [ (model.GoodreadsApiKey.Value, searchText, endBook) |> MoreBooksRequestedCmd ]

    let private onSearchResultReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse = Some(result)
                        Status = statusFromBool (result.IsSuccessful) } }, []

    let private onMoreBooksReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse =
                            match model.SearchPageModel.SearchResponse with
                            | Some x -> (combineModels x result)
                            | None -> result
                            |> Some
                        Status = statusFromBool result.IsSuccessful } }, []

    let private onChangeDisplayedPage page model =
        match page with
        | SearchPage -> { model with BookDetailsPageModel = None }, []
        | DetailsPage book ->
            { model with BookDetailsPageModel = Some(initBookDetailsFromBook (Some(book))) },
            [ (model.GoodreadsApiKey.Value, book) |> UpdateBookDetailsCmd ]

    let private onBookResultReceived result model =
        { model with
              BookDetailsPageModel =
                  Some
                      ({ model.BookDetailsPageModel.Value with
                             BookDetails = Some(result)
                             Status = statusFromBool (result.IsSuccessful) }) }, []

    let private onSavingGoodreadsKey (key, savingFunc) model =
        { model with GoodreadsApiKey = Some(savingFunc (key)) }, []

    let private onRemoveGoodreadsKey (deletingFunc: unit -> bool) model =
        deletingFunc () |> ignore
        { model with GoodreadsApiKey = None }, []

    let private onUpdateBookDetails book model =
        //todo: add refresh button in book details view
        { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = Status.Loading }) },
        [ (model.GoodreadsApiKey.Value, book) |> UpdateBookDetailsCmd ]

    let private onBookSortingRequested model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse =
                            match model.SearchPageModel.SearchResponse with
                            | Some x -> Some({ x with BookItems = Utils.bookItemsSortedByRatingDesc x.BookItems })
                            | None -> None } }, []

    let onOpenInBrowser book model =
        model, [ book |> OpenBrowserCmd ]

    let onBrowserOpened model = model, []

    let update =
        function
        | Msg.SearchTextEntered (text, pageNum) ->
            onMsgPerformSearch (text, pageNum)
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
