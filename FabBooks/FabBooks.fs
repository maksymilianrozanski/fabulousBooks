﻿// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open BookItemLayoutModule
open FabBooks.GoodreadsResponseModelModule
open GoodreadsQuery
open StatusLayout
open GoodreadsBookQuery

module App =
    type Model =
        { EnteredText: string
          Status: Status
          DisplayedPage: DisplayedPage
          ResponseModel: GoodreadsResponseModel
          BookDetailsPageModel: Option<BookDetailsPageModel> }

    let initModel =
        { EnteredText = ""
          Status = Success
          DisplayedPage = SearchPage
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }

    let init () = initModel, Cmd.none

    let update (msg: Msg) (model: Model) =
        match msg with
        | UpdateEnteredText text ->
            { model with EnteredText = text },
            searchWithKey text
            |> Async.map SearchResultReceived
            |> Async.map (fun x -> Some x)
            |> Cmd.ofAsyncMsgOption
        | SearchResultReceived result ->
            { model with ResponseModel = result },
            match result.IsSuccessful with
            | true -> Status.Success
            | _ -> Status.Failure
            |> UpdateSearchStatus
            |> Cmd.ofMsg
        | UpdateSearchStatus status -> { model with Status = status }, Cmd.none
        | ChangeDisplayedPage page ->
            match page with
            | SearchPage -> { model with DisplayedPage = SearchPage }, Cmd.none
            | DetailsPage x -> { model with DisplayedPage = DetailsPage x }, Cmd.none
        | NavigateToDetailsPageMsg book ->
            { model with
                  BookDetailsPageModel = Some(BookDetailsPage.initFromId (Some(book)))
                  DisplayedPage = DetailsPage book },
            book
            |> UpdateBookDetails
            |> Cmd.ofMsg
        | BookResultReceived result ->
            { model with BookDetailsPageModel =
                  Some({ model.BookDetailsPageModel.Value with BookDetails = Some(result) }) },
            match result.IsSuccessful with
            | true -> Status.Success
            | _ -> Status.Failure
            |> UpdateDetailsStatus
            |> Cmd.ofMsg
        | UpdateBookDetails book ->
            { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = Status.Loading }) },
            bookWithKey book.Id
            |> Async.map BookResultReceived
            |> Async.map (fun x -> Some x)
            |> Cmd.ofAsyncMsgOption
        | UpdateDetailsStatus status ->
            { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = status }) },
            Cmd.none

    let view (model: Model) dispatch =

        let openDetailsPage x = fun () -> NavigateToDetailsPageMsg x |> dispatch

        let searchPage =
            View.ContentPage
                (content =
                    View.StackLayout
                        (padding = Thickness 20.0, verticalOptions = LayoutOptions.Start,
                         children =
                             [ View.Entry
                                 (width = 200.0, placeholder = "Search",
                                  completed =
                                      fun textArgs ->
                                          UpdateSearchStatus Status.Loading |> dispatch
                                          UpdateEnteredText textArgs |> dispatch)
                               View.Label(text = model.EnteredText)
                               statusLayout (model.Status)
                               View.ScrollView
                                   (content =
                                       View.StackLayout
                                           (children =
                                               [ for b in model.ResponseModel.BookItems do
                                                   yield bookItemLayout (b, openDetailsPage) ])) ]))

        let rootView =
            View.NavigationPage
                (pages =
                    [ yield searchPage
                      match model.DisplayedPage with
                      | DetailsPage x -> yield bookDetailsPageView model.BookDetailsPageModel.Value dispatch
                      | _ -> () ], popped = fun _ -> ChangeDisplayedPage SearchPage |> dispatch)

        rootView

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
