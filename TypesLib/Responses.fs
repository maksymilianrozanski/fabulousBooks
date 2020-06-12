namespace FabBooks

open FabBooks.BookItemModule

module Responses =

    type SearchResponse =
        { IsSuccessful: bool
          Start: int
          End: int
          Total: int
          BookItems: Collections.List<BookItem> }

    type SingleBookResponse(isSuccessful: bool, description: string, link: string) =
        member this.IsSuccessful = isSuccessful
        member this.Description = description
        member this.Link = link