namespace FabBooks

module BookItemModule =

    type BookItem(author: string, title: string, imageUrl: string, smallImageUrl: string, id: int) =
        member this.Author = author
        member this.Title = title
        member this.ImageUrl = imageUrl
        member this.SmallImageUrl = smallImageUrl
        member this.Id = id
