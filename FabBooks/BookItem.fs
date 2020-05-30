module FabBooks.BookItem

  type BookItem(author: string, title: string, imageUrl: string, smallImageUrl: string) =
        member this.Author = author
        member this.Title = title
        member this.ImageUrl = imageUrl
        member this.SmallImageUrl = smallImageUrl