namespace FabBooks

open Xamarin.Forms
open Xamarin.Forms
open Xamarin.Forms
open Xamarin.Forms
open Xamarin.Forms
open Xamarin.Forms
open Xamarin.Forms
open Xamarin.Forms

module BookItemLayoutModule =

    open FabBooks.BookItemModule
    open Fabulous.XamarinForms

    let bookItemLayout (b: BookItem, action) =
        View.Frame
            (padding = Thickness 6.0,
             content =
                 View.Frame
                     (cornerRadius = 10.0, backgroundColor = Color.Wheat,
                      content =
                          View.Grid
                              (rowdefs = [ Dimension.Star ], coldefs = [ Dimension.Star; Dimension.Star ],
                               children =
                                   [ View.StackLayout(children =
                                                          [ View.Label(text = b.Title)
                                                            View.Label(text = b.Author) ]).Column(0).Row(0)
                                     View.StackLayout(children =
                                                          [ View.Image
                                                              (source = ImagePath b.SmallImageUrl, width = 100.0,
                                                               height = 100.0) ]).Column(1).Row(0) ]),
                      gestureRecognizers = [ View.TapGestureRecognizer(command = action b) ]))
