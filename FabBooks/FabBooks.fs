// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.Messages
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open BookItemLayoutModule
open FabBooks.GoodreadsResponseModelModule
open GoodreadsQuery
open BookDetailsPage

module App =

    type Model =
        { EnteredText: string
          Status: Status
          IsDisplayingDetails: bool
          ResponseModel: GoodreadsResponseModel }

    let initModel =
        { EnteredText = ""
          Status = Success
          IsDisplayingDetails = false
          ResponseModel = emptyGoodreadsModel }

    let init () = initModel, Cmd.none

    let update msg model =
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
            |> UpdateStatus
            |> Cmd.ofMsg
        | UpdateStatus status -> { model with Status = status }, Cmd.none
        | DisplayDetailsPage i -> { model with IsDisplayingDetails = true }, Cmd.none

    let statusLayout status =
        match status with
        | Success -> View.Label(text = "Success", textColor = Color.Green)
        | Failure -> View.Label(text = "Failed", textColor = Color.Red)
        | Loading -> View.Label(text = "Loading...", textColor = Color.Yellow)

    let view (model: Model) dispatch =

        let detailsPage = BookDetailsPage.view (BookDetailsPage.initModel) dispatch

        let rootPage =
            View.ContentPage
                (content =
                    View.StackLayout
                        (padding = Thickness 20.0, verticalOptions = LayoutOptions.Start,
                         children =
                             [ View.Entry
                                 (width = 200.0, placeholder = "Search",
                                  completed =
                                      fun textArgs ->
                                          UpdateStatus Status.Loading |> dispatch
                                          UpdateEnteredText textArgs |> dispatch)
                               View.Label(text = model.EnteredText)
                               statusLayout (model.Status)
                               View.ScrollView
                                   (content =
                                       View.StackLayout
                                           (children =
                                               [ for b in model.ResponseModel.BookItems do
                                                   yield bookItemLayout (b) ])) ]))

        if (model.IsDisplayingDetails) then detailsPage else rootPage

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
