module FabBooks.BookItemLayout

open FabBooks
open Fabulous.XamarinForms
open BookItem

let bookItemLayout (bookItem: BookItem) =
    View.StackLayout
        (children =
            [ View.Label(text = bookItem.Title)
              View.Label(text = bookItem.Author) ])
