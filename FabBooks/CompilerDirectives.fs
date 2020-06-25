module FabBooks.CompilerDirectives
open System
open FabBooks
open FabBooks.MainModel
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms
   
type App() as app =
    inherit Application()
    
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
    
    let removeBookDetailsPageModel (model:Model) =
        {model with BookDetailsPageModel =  None}
    
    let restoreModel modelTransformation  =
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) ->

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<MainModel.Model>(json)
                let newModel = modelTransformation model
                
                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                Console.WriteLine("OnResume: transformedModel, newModel = {0}", (sprintf "%0A" newModel))                
                runner.SetCurrentModel (newModel, Cmd.none)

            | _ -> ()
        with ex ->
            App.program.onError("Error while restoring model found in app.Properties", ex)
            
    override __.OnSleep() =

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() =
        restoreModel (fun x -> x)

    override this.OnStart() =
        restoreModel removeBookDetailsPageModel     