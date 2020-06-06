﻿// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open FabBooks.SingleBookResponseModelModule
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open BookItemLayoutModule
open FabBooks.GoodreadsResponseModelModule
open GoodreadsQuery
open StatusLayout
open GoodreadsBookQuery
open BookItemModule
open Utils

module App =
    type Model =
        { EnteredText: string
          Status: Status
          ResponseModel: GoodreadsResponseModel
          BookDetailsPageModel: Option<BookDetailsPageModel> }

    let initModel =
        { EnteredText = ""
          Status = Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }

    let init () = initModel, []

    let performSearchCmd text =
        searchWithKey text
        |> Async.map Msg.SearchResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let changeDisplayedPageCmd page = Cmd.none

    let navigateToDetailsPageCmd bookItem =
        bookItem
        |> Msg.UpdateBookDetails
        |> Cmd.ofMsg

    let bookResultReceivedCmd (bookItem: SingleBookResponseModel) =
        match bookItem.IsSuccessful with
        | true -> Status.Success
        | _ -> Status.Failure
        |> Msg.UpdateDetailsStatus
        |> Cmd.ofMsg

    let updateBookDetailsCmd (bookItem: BookItem) =
        bookWithKey bookItem.Id
        |> Async.map Msg.BookResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let updateDetailsStatusCmd status = Cmd.none

    let update msg (model: Model) =
        match msg with
        | Msg.PerformSearch text ->
            { model with
                  EnteredText = text
                  Status = Status.Loading }, [ text |> PerformSearch ]
        | Msg.SearchResultReceived result ->
            { model with
                  ResponseModel = result
                  Status = statusFromBool (result.IsSuccessful) }, []
        | Msg.ChangeDisplayedPage page ->
            match page with
            | SearchPage -> { model with BookDetailsPageModel = None }, []
            | DetailsPage book ->
                { model with BookDetailsPageModel = Some(BookDetailsPage.initFromId (Some(book))) },
                [ book |> UpdateBookDetails ]
        | Msg.BookResultReceived result ->
            { model with BookDetailsPageModel =
                  Some({ model.BookDetailsPageModel.Value with BookDetails = Some(result) }) },
            [ result |> BookResultReceived ]
        | Msg.UpdateBookDetails book ->
            { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = Status.Loading }) },
            [ book |> UpdateBookDetails ]
        | Msg.UpdateDetailsStatus status ->
            { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = status }) },
            [ status |> UpdateDetailsStatus ]

    let view model dispatch =

        let openDetailsPage bookItem = fun () -> Msg.ChangeDisplayedPage(DetailsPage bookItem) |> dispatch

        let searchPage =
            View.ContentPage
                (content =
                    View.StackLayout
                        (padding = Thickness 20.0, verticalOptions = LayoutOptions.Start,
                         children =
                             [ View.Entry
                                 (width = 200.0, placeholder = "Search",
                                  completed = fun textArgs -> Msg.PerformSearch textArgs |> dispatch)
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
                      match model.BookDetailsPageModel with
                      | Some x -> yield bookDetailsPageView model.BookDetailsPageModel.Value dispatch
                      | _ -> () ], popped = fun _ -> Msg.ChangeDisplayedPage SearchPage |> dispatch)

        rootView

    let mapCommands cmdMsg =
        match cmdMsg with
        | PerformSearch searchText -> performSearchCmd searchText
        | SearchResultReceived _ -> []
        | ChangeDisplayedPage page -> changeDisplayedPageCmd page
        //details messages
        | BookResultReceived responseModel -> bookResultReceivedCmd responseModel
        | UpdateBookDetails bookItem -> updateBookDetailsCmd bookItem
        | UpdateDetailsStatus status -> updateDetailsStatusCmd status

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgramWithCmdMsg init update view mapCommands
