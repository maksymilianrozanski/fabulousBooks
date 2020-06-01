// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open System
open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms
open BookItem
open BookItemLayout
open FabBooks.GoodreadsResponseModel
open FabBooks.MockXmlResponse
open System.Collections.Generic

module App =

    let responseModel = goodreadsFromXml(mockGoodreadsResponse)
    
    type Model =
        { EnteredText: string
          BookItems: List<BookItem> }

    let mockBooks = responseModel.BookItems

    type Msg = UpdateEnteredText of string

    let initModel =
        { EnteredText = ""
          BookItems = mockBooks }

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
                           View.ScrollView
                               (content =
                                   View.StackLayout
                                       (children =
                                           [ for b in model.BookItems do
                                               yield bookItemLayout(b) ])) ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
