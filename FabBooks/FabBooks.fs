// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open System
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
open XmlParser

module App =

    let responseModel = goodreadsFromXml (mockGoodreadsResponse)

    type Model =
        { EnteredText: string
          ResponseModel: GoodreadsResponseModel }

    type Msg = UpdateEnteredText of string

    let initModel =
        { EnteredText = ""
          ResponseModel = responseModel }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | UpdateEnteredText text -> { model with EnteredText = text }, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (padding = Thickness 20.0, verticalOptions = LayoutOptions.Start,
                     children =
                         [ View.Entry
                             (width = 200.0, placeholder = "Search",
                              textChanged = fun textArgs -> UpdateEnteredText textArgs.NewTextValue |> dispatch)
                           View.Label(text = model.EnteredText)
                           View.Label(text = "HELLO ?! >.< :_)       :)_")
                           View.ScrollView
                               (content =
                                   View.StackLayout
                                       (children =
                                           [ for b in model.ResponseModel.BookItems do
                                               yield bookItemLayout (b) ])) ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
