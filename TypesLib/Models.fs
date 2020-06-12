namespace FabBooks

open FabBooks.BookItemModule
open FabBooks.MainMessages
open FabBooks.Responses

module Models =
    type SearchPageModel =
        { Status: Status
          EnteredText: string
          SearchResponse: Option<SearchResponse> }

    let initSearchPageModel =
        { Status = Status.Success
          EnteredText = ""
          SearchResponse = None }

    type BookDetailsPageModel =
        { DisplayedBook: Option<BookItem>
          BookDetails: Option<SingleBookResponse>
          Status: Status }

    let initBookDetailsFromBook book =
        { DisplayedBook = book
          BookDetails = None
          Status = Loading }
