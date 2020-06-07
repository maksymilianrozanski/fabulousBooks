// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

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

    let updateBookDetailsCmd (bookItem: BookItem) =
        bookWithKey bookItem.Id
        |> Async.map Msg.BookResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let private onMsgPerformSearch text model =
        { model with
              EnteredText = text
              Status = Status.Loading }, [ text |> PerformSearchCmd ]

    let private onSearchResultReceived result model =
        { model with
              ResponseModel = result
              Status = statusFromBool (result.IsSuccessful) }, []

    let private onChangeDisplayedPage page model =
        match page with
        | SearchPage -> { model with BookDetailsPageModel = None }, []
        | DetailsPage book ->
            { model with BookDetailsPageModel = Some(BookDetailsPage.initFromBook (Some(book))) },
            [ book |> UpdateBookDetailsCmd ]

    let private onBookResultReceived result model =
        { model with
              BookDetailsPageModel =
                  Some
                      ({ model.BookDetailsPageModel.Value with
                             BookDetails = Some(result)
                             Status = statusFromBool (result.IsSuccessful) }) }, []

    let private onUpdateBookDetails book model =
        { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = Status.Loading }) },
        [ book |> UpdateBookDetailsCmd ]

    let update msg (model: Model) =
        match msg with
        | Msg.PerformSearch text ->
            onMsgPerformSearch text model
        | Msg.SearchResultReceived result ->
            onSearchResultReceived result model
        | Msg.ChangeDisplayedPage page ->
            onChangeDisplayedPage page model
        | Msg.BookResultReceived result ->
            onBookResultReceived result model
        | Msg.UpdateBookDetails book ->
            onUpdateBookDetails book model

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
        | PerformSearchCmd searchText -> performSearchCmd searchText
        | UpdateBookDetailsCmd bookItem -> updateBookDetailsCmd bookItem

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgramWithCmdMsg init update view mapCommands
