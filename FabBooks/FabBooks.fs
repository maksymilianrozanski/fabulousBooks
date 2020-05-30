// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open System
open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App =

    type BookItem(author: string, title: string) =
        member this.Author = author
        member this.Title = title

    type Model =
        { EnteredText: string
          BookItems: List<BookItem> }

    let mockBooks =
        [ BookItem("author0", "title0")
          BookItem("author1", "title1")
          BookItem("author2", "title2")
          BookItem("author3", "title3")
          BookItem("author4", "title4")
          BookItem("author5", "title5")
          BookItem("author6", "title6")
          BookItem("author7", "title7") ]

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
                                               yield View.StackLayout
                                                         (children =
                                                             [ View.Label(text = b.Title)
                                                               View.Label(text = b.Author) ]) ])) ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
