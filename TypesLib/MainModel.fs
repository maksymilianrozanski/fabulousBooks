namespace FabBooks

open FabBooks
open Models

module MainModel =
    type Model =
        { SearchPageModel: SearchPageModel
          BookDetailsPageModel: Option<BookDetailsPageModel> }

    let init () =
        { SearchPageModel = initSearchPageModel
          BookDetailsPageModel = None }, []
