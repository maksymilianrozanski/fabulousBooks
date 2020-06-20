namespace FabBooks

open FabBooks
open MainModel
open MainMessages
open Models
open Utils
open SearchResponseModule

module Update =
    let modelWithoutKey =
        { GoodreadsApiKey = None
          SearchPageModel = initSearchPageModel
          BookDetailsPageModel = None }

    let onPerformSearch (text, pageNum) model =
        if (text = MainMessages.deleteApiKeyCommand) then
            modelWithoutKey, [ DeleteApiKeyCmd ]
        else
            { model with
                  SearchPageModel =
                      { model.SearchPageModel with
                            Status = Status.Loading
                            EnteredText = text } }, [ (model.GoodreadsApiKey.Value, text, pageNum) |> PerformSearchCmd ]

    let onMoreBooksRequested (searchText, endBook) model =
        { model with SearchPageModel = { model.SearchPageModel with Status = Status.Loading } },
        [ (model.GoodreadsApiKey.Value, searchText, endBook) |> MoreBooksRequestedCmd ]

    let onSearchResultReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse = Some(result)
                        Status = statusFromBool (result.IsSuccessful) } }, []

    let onMoreBooksReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse =
                            match model.SearchPageModel.SearchResponse with
                            | Some x -> (combineModels x result)
                            | None -> result
                            |> Some
                        Status = statusFromBool result.IsSuccessful } }, []

    let onChangeDisplayedPage page model =
        match page with
        | SearchPage -> { model with BookDetailsPageModel = None }, []
        | DetailsPage book ->
            { model with BookDetailsPageModel = Some(initBookDetailsFromBook (Some(book))) },
            [ (model.GoodreadsApiKey.Value, book) |> UpdateBookDetailsCmd ]

    let onBookResultReceived result model =
        { model with
              BookDetailsPageModel =
                  Some
                      ({ model.BookDetailsPageModel.Value with
                             BookDetails = Some(result)
                             Status = statusFromBool (result.IsSuccessful) }) }, []

    let onSavingGoodreadsKey (key, savingFunc) model =
        { model with GoodreadsApiKey = Some(savingFunc (key)) }, []

    let onRemoveGoodreadsKey (deletingFunc: unit -> bool) model =
        deletingFunc () |> ignore
        { model with GoodreadsApiKey = None }, []

    let onUpdateBookDetails book model =
        { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = Status.Loading }) },
        [ (model.GoodreadsApiKey.Value, book) |> UpdateBookDetailsCmd ]

    let onBookSortingRequested model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse =
                            match model.SearchPageModel.SearchResponse with
                            | Some x -> Some({ x with BookItems = Utils.bookItemsSortedByRatingDesc x.BookItems })
                            | None -> None } }, []

    let onOpenInBrowser book model =
        model, [ book |> OpenBrowserCmd ]

    let onBrowserOpened model = model, []
