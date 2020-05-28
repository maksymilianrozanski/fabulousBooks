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
      { 
        EnteredText : string
         }

    type Msg = 
        | UpdateEnteredText of string

    let initModel = {  EnteredText = "" }

    let init () = initModel, Cmd.none


    let update msg model =
        match msg with
        | UpdateEnteredText text -> {model with EnteredText = text}, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = Thickness 20.0, verticalOptions = LayoutOptions.Start,
            children = [
                View.Entry( width = 200.0, placeholder = "Search",  textChanged = fun textArgs  -> UpdateEnteredText textArgs.NewTextValue |> dispatch)
                View.Label(text = model.EnteredText)
                View.ScrollView(verticalOptions = LayoutOptions.Center,  content = View.StackLayout(children = [
                            View.Label(text = "first item")
                            View.Label(text = "second item")
                            ]),
                        height = 50.0)
            ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> XamarinFormsProgram.run app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/tools.html#live-update for further  instructions.
    //
    do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/models.html#saving-application-state for further  instructions.

    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()