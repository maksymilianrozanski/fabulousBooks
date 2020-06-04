namespace FabBooks

open FabBooks.BookItemModule
open Fabulous
open Fabulous.XamarinForms
open Messages

module BookDetailsPage =
    type BookDetailsPageModel =
        { DisplayedBook: Option<BookItem> }

    let initModel = { DisplayedBook = None }

    let init () = initModel, Cmd.none

    let initFromId id = { DisplayedBook = id }

    let update msg model =
        match msg with
        | NavigateToDetailsPageMsg book ->
            { model with DisplayedBook = Some(book) }, Cmd.none

    let view (model: BookDetailsPageModel) dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (children =
                        [ View.Label(text = "details page.")
                          View.Label(text = "current title = " + model.DisplayedBook.Value.Title.ToString()) ]))
