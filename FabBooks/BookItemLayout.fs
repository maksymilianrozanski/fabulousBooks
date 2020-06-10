namespace FabBooks

open Xamarin.Forms
open Utils

module BookItemLayoutModule =

    open FabBooks.BookItemModule
    open Fabulous.XamarinForms

    let private textInfo (b: BookItem) =
        [ View.Label(text = b.Title)
          View.Label(text = b.Author) ]

    let private bookImage (b: BookItem) = View.Image(source = ImagePath b.SmallImageUrl, width = 100.0, height = 100.0)

    let private withImage (b: BookItem) =
        View.Grid
            (rowdefs = [ Dimension.Star ], coldefs = [ Dimension.Star; Dimension.Star ],
             children =
                 [ View.StackLayout(children = textInfo b).Column(0).Row(0)
                   (bookImage b).Column(1).Row(0) ])

    let private withoutImage (b: BookItem) =
        View.StackLayout(children = textInfo b, horizontalOptions = LayoutOptions.CenterAndExpand)

    let private chooseBookLayout (b: BookItem) =
        if hasImage (b.ImageUrl) then withImage (b) else withoutImage (b)

    let bookItemLayout (b: BookItem, action) =
        View.Frame
            (padding = Thickness 6.0,
             content =
                 View.Frame
                     (cornerRadius = 10.0, backgroundColor = Color.Wheat, content = chooseBookLayout b,
                      gestureRecognizers = [ View.TapGestureRecognizer(command = action b) ]))
