namespace FabBooks

open FabBooks
open Models

module MainModel =
    type Model =
        { GoodreadsApiKey: Option<string>
          SearchPageModel: SearchPageModel
          BookDetailsPageModel: Option<BookDetailsPageModel> }

    let init () =
        { GoodreadsApiKey = None
          SearchPageModel = initSearchPageModel
          BookDetailsPageModel = None }, []
