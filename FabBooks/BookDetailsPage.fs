namespace FabBooks

open Fabulous
open Fabulous.XamarinForms
open Messages

module BookDetailsPage =
    type BookDetailsPageModel =
        { DisplayedBook: int }

    let initModel = { DisplayedBook = 0 }

    let init () = initModel, Cmd.none

    let initFromId id = { DisplayedBook = id }

    let update msg model =
        match msg with
        | NavigateToDetailsPageMsg ->
            { model with DisplayedBook = 2 }, Cmd.none

    let view (model: BookDetailsPageModel) dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (children =
                        [ View.Label(text = "details page.")
                          View.Label(text = "current id = " + model.DisplayedBook.ToString()) ]))
