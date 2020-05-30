// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open System
open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App =

    type BookItem(author: string, title: string, imageUrl: string, smallImageUrl: string) =
        member this.Author = author
        member this.Title = title
        member this.ImageUrl = imageUrl
        member this.SmallImageUrl = smallImageUrl

    type Model =
        { EnteredText: string
          BookItems: List<BookItem> }

    let mockBooks =
        [ BookItem("author0", "title0", "bigImageUrl", "smallImageUrl")
          BookItem("author1", "title1", "bigImageUrl", "smallImageUrl")
          BookItem("author2", "title2", "bigImageUrl", "smallImageUrl")
          BookItem("author3", "title3", "bigImageUrl", "smallImageUrl")
          BookItem("author4", "title4", "bigImageUrl", "smallImageUrl")
          BookItem("author5", "title5", "bigImageUrl", "smallImageUrl")
          BookItem("author6", "title6", "bigImageUrl", "smallImageUrl")
          BookItem("author7", "title7", "bigImageUrl", "smallImageUrl") ]

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
