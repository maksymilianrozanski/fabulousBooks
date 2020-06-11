namespace FabBooks

open FabBooks.GoodreadsResponseModelModule
open FabBooks.MainMessages


module SearchPageModelModule =
    type SearchPageModel =
        { Status: Status
          EnteredText: string
          ResponseModel: Option<GoodreadsResponseModel> }

    let initModel =
        {
            Status = Status.Success
            EnteredText = ""
            ResponseModel = None
        }