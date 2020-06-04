namespace FabBooks

module BookItemLayoutModule =

    open FabBooks.BookItemModule
    open Fabulous.XamarinForms

    let bookItemLayout (b: BookItem, action) =
        View.StackLayout
            (children =
                [ View.Label(text = b.Title)
                  View.Label(text = b.Author)
                  View.Image(source = ImagePath b.SmallImageUrl, width = 100.0, height = 100.0)
                  View.Button(text = "Open Details", command = action) ])
