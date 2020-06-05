namespace FabBooks

open System.Data
open FabBooks.BookItemModule
open FabBooks.SingleBookResponseModelModule
open Fabulous
open Fabulous.XamarinForms
open MainMessages
open Xamarin.Forms
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
            | Some x -> View.Label(text = model.BookDetails.Value.Description)
            | None -> View.Label("no description...")

        View.ContentPage
            (content =
                View.StackLayout
                    (children =
                        [ View.Label(text = "details page.")
                          StatusLayout.statusLayout (model.Status)
                          View.Label(text = "current title  = " + model.DisplayedBook.Value.Title.ToString())
                          View.Label(text = "author:" + model.DisplayedBook.Value.Author.ToString())
                          View.Image
                              (source = ImagePath model.DisplayedBook.Value.ImageUrl, width = 250.0, height = 200.0,
                               aspect = Aspect.AspectFit)
                          //todo: handle html tags in description
                          descriptionView ]))
