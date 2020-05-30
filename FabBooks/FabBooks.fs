// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open System
open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App =
    type Model =
        { EnteredText: string }

    type Msg = UpdateEnteredText of string

    let initModel = { EnteredText = "" }

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
                               (verticalOptions = LayoutOptions.Center,
                                content =
                                    View.StackLayout
                                        (children =
                                            [ View.Label(text = "first item hello")
                                              View.Label(text = "second item") ]), height = 50.0) ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view
