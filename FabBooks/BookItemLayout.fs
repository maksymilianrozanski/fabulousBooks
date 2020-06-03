namespace FabBooks

module BookItemLayoutModule =

    open FabBooks
    open FabBooks.BookItemModule
    open Fabulous.XamarinForms
    open Messages

    let bookItemLayout (b: BookItem, dispatch) =
        View.StackLayout
            (children =
                [ View.Label(text = b.Title)
                  View.Label(text = b.Author)
                  View.Image(source = ImagePath b.SmallImageUrl, width = 100.0, height = 100.0)
                  View.Button(text = "details", command = (fun () -> dispatch (DisplayDetailsPage 2))) ])
