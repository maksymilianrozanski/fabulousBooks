namespace FabBooks

open FabBooks.MainMessages
open Xamarin.Forms
open Utils
open Xamarin.Essentials

module BookItemLayoutModule =

    open FabBooks.BookItemModule
    open Fabulous.XamarinForms

    let textInfo (b: BookItem) =
        [ Label.label b.Title
          Label.label b.Author
          Label.label (sprintf "Avg rating: %.2f*" b.Rating) ]

    let bookImage (b: BookItem) = View.Image(source = ImagePath b.SmallImageUrl, width = 240.0, height = 240.0)

    let openBrowserButton (b: BookItem) dispatch =
        Button.button "open goodreads" (fun () -> Msg.OpenBrowserRequested b |> dispatch)

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

    let bookItemLayout (b: BookItem) dispatch =
        View.Frame
            (padding = Thickness 6.0, backgroundColor = Color.Transparent,
             content =
                 View.Frame
                     (cornerRadius = 10.0, backgroundColor = Colors.backgroundSecondary,
                      content =
                          View.StackLayout
                              (children =
                                  [ chooseBookLayout b
                                    openBrowserButton b dispatch ],
                               gestureRecognizers =
                                   [ View.TapGestureRecognizer
                                       (command = fun () -> Msg.ChangeDisplayedPage(DetailsPage b) |> dispatch) ])))
