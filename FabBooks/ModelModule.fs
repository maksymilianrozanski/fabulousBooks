namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.SearchPageModelModule

module ModelModule =
    type Model =
        { SearchPageModel: SearchPageModel
          BookDetailsPageModel: Option<BookDetailsPageModel> }

    let initModel =
        { SearchPageModel = SearchPageModelModule.initModel
          BookDetailsPageModel = None }

    let init () = initModel, []
