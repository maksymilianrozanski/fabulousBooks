namespace FabBooks

open FabBooks.BookItemModule
open FabBooks.SingleBookResponseModelModule
open Fabulous
open Fabulous.XamarinForms
open MainMessages
open DetailsMessages

module BookDetailsPage =

    type BookDetailsPageModel =
        { DisplayedBook: Option<BookItem>
          BookDetails: Option<SingleBookResponseModel> }

    let initModel =
        { DisplayedBook = None
          BookDetails = None }

    let init () = initModel, Cmd.none

    let initFromId id =
        { DisplayedBook = id
          BookDetails = None }

    let update msg model =
        match msg with
        | NavigateToDetailsPageMsg book ->
            { model with DisplayedBook = Some(book) }, Cmd.none
        | BookResultReceived details ->
            {model with BookDetails = Some(details)}, Cmd.none

    let view (model: BookDetailsPageModel) dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (children =
                        [ View.Label(text = "details page.")
                          View.Label(text = "current title = " + model.DisplayedBook.Value.Title.ToString()) ]))
