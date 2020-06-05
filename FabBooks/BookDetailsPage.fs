namespace FabBooks

open System.Data
open FabBooks.BookItemModule
open FabBooks.SingleBookResponseModelModule
open Fabulous
open Fabulous.XamarinForms
open MainMessages
open Xamarin.Forms

module BookDetailsPage =

    type BookDetailsPageModel =
        { DisplayedBook: Option<BookItem>
          BookDetails: Option<SingleBookResponseModel>
          Status: Status }

    let initModel =
        { DisplayedBook = None
          BookDetails = None
          Status = Success }

    let init () = initModel, Cmd.none

    let initFromId id =
        { DisplayedBook = id
          BookDetails = None
          Status = Success }

    let bookDetailsPageView (model: BookDetailsPageModel) dispatch =
        let descriptionView =
            match model.BookDetails with
            | Some -> View.Label(text = model.BookDetails.Value.Description)
            | None -> View.Label("no description...")

        View.ContentPage
            (content =
                View.StackLayout
                    (children =
                        [ View.Label(text = "details page.")
                          StatusLayout.statusLayout (model.Status)
                          View.Label(text = "current title = " + model.DisplayedBook.Value.Title.ToString())
                          //todo: handle html tags in description
                          descriptionView ]))
