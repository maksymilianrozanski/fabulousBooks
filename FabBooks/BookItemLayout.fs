namespace FabBooks

module BookItemLayoutModule =

    open System
    open System.IO
    open FFImageLoading.Work
    open FabBooks
    open FabBooks.BookItemModule
    open Fabulous.XamarinForms
    open BookItemModule
    open FFImageLoading.Forms
    open Xamarin.Forms

    let bookItemLayout (b: BookItem) =
        View.StackLayout
            (children =
                [ View.Label(text = b.Title)
                  View.Label(text = b.Author)
                  View.Image(source = ImagePath b.SmallImageUrl, width = 100.0, height = 100.0) ])
