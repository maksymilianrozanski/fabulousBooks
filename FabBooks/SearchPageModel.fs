namespace FabBooks

open FabBooks.MainMessages
open FabBooks.SearchResponseModule

module SearchPageModelModule =
    type SearchPageModel =
        { Status: Status
          EnteredText: string
          SearchResponse: Option<SearchResponse> }

    let initModel =
        {
            Status = Status.Success
            EnteredText = ""
            SearchResponse = None
        }