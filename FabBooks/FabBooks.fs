// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open System
open System.Data
open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms
open BookItemModule
open BookItemLayoutModule
open FabBooks.GoodreadsResponseModelModule
open FabBooks.MockXmlResponse
open System.Collections.Generic
open Xamarin.Forms
open Xamarin.Forms
open XmlParser
open FabBooks.GoodreadsQuery
open FabBooks.GoodreadsApiKey


module App =

    //    let responseModel = goodreadsFromXml (mockGoodreadsResponse)

    type Status =
        | Success
        | Failure
        | Loading

    type Model =
        { EnteredText: string
          Status: Status
          ResponseModel: GoodreadsResponseModel }

    type Msg =
        | UpdateEnteredText of string
        | SearchResultReceived of GoodreadsResponseModel
        | UpdateStatus of Status

    let initModel =
        { EnteredText = ""
          Status = Success
          ResponseModel = emptyGoodreadsModel }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | UpdateEnteredText text ->
            { model with EnteredText = text },
            searchGet goodreadsApiKey text
            |> Async.map SearchResultReceived
            |> Async.Catch
            |> Async.map (function
                | Choice1Of2 x -> Some x
                //todo: add error handling
                | Choice2Of2 _ -> None)
            |> Cmd.ofAsyncMsgOption
        | SearchResultReceived result ->
            { model with ResponseModel = result },
            match result.Total with
            | 0 -> Status.Failure
            | _ -> Status.Success
            |> UpdateStatus
            |> Cmd.ofMsg
        | UpdateStatus status -> { model with Status = status }, Cmd.none

    let statusLayout status =
        match status with
        | Success -> View.Label(text = "Success", textColor = Color.Green)
        | Failure -> View.Label(text = "Failed", textColor = Color.Red)
        | Loading -> View.Label(text = "Loading...", textColor = Color.Yellow)

    let view (model: Model) dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (padding = Thickness 20.0, verticalOptions = LayoutOptions.Start,
                     children =
                         [ View.Entry
                             (width = 200.0, placeholder = "Search",
                              textChanged =
                                  fun textArgs ->
                                      UpdateStatus Status.Loading |> dispatch
                                      UpdateEnteredText textArgs.NewTextValue |> dispatch)
                           View.Label(text = model.EnteredText)
                           View.Label(text = "HELLO ?! >.< :_)       :)_")
                           statusLayout (model.Status)
                           View.ScrollView
                               (content =
                                   View.StackLayout
                                       (children =
                                           [ for b in model.ResponseModel.BookItems do
                                               yield bookItemLayout (b) ])) ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
