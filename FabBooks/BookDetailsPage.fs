namespace FabBooks

open FabBooks.BookItemModule
open FabBooks.SingleBookResponseModelModule
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

    let initFromBook id =
        { DisplayedBook = id
          BookDetails = None
          Status = Loading }

    let bookDetailsPageView (model: BookDetailsPageModel) dispatch =
        let descriptionView =
            match model.BookDetails with
            | Some x -> View.Label(text = model.BookDetails.Value.Description, textType = TextType.Html)
            | None -> View.Label("no description...")

        let imageView =
            if (Utils.hasImage model.DisplayedBook.Value.ImageUrl)
            then [ BookItemLayoutModule.bookImage model.DisplayedBook.Value ]
            else []

        View.ContentPage
            (View.ScrollView
                (content =
                    View.StackLayout
                        (children =
                            [ StatusLayout.statusLayout (model.Status) ]
                            @ BookItemLayoutModule.textInfo (model.DisplayedBook.Value)
                              @ imageView @ [ descriptionView ], padding = Thickness 8.0)))
