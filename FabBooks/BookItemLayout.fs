namespace FabBooks

module BookItemLayoutModule =

    open FabBooks.BookItemModule
    open Fabulous.XamarinForms

    let bookItemLayout (b: BookItem, action) =
        View.Grid
            (rowdefs = [ Dimension.Star ], coldefs = [ Dimension.Star; Dimension.Star ],
             children =
                 [ View.StackLayout(children =
                                        [ View.Label(text = b.Title)
                                          View.Label(text = b.Author) ]).Column(0).Row(0)

                   View.StackLayout(children =
                                        [ View.Image(source = ImagePath b.SmallImageUrl, width = 200.0, height = 200.0)
                                          View.Button(text = "Open Details", command = action (b)) ]).Column(1).Row(0) ])
