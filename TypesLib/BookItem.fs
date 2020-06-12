namespace FabBooks

module BookItemModule =

    type BookItem(author: string, title: string, imageUrl: string, smallImageUrl: string, id: int, rating: float) =
        member this.Author = author
        member this.Title = title
        member this.ImageUrl = imageUrl
        member this.SmallImageUrl = smallImageUrl
        member this.Id = id
        member this.Rating = rating
