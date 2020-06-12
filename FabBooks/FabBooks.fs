// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open Models
open Fabulous
open Fabulous.XamarinForms
open SearchQuery
open BookDetailsQuery
open BookItemModule
open Utils
open MainModel
open SearchPageViews
open FabBooks.SearchResponseModule

module App =

    let performSearchCmd text pageNum =
        searchWithPage text pageNum
        |> Async.map Msg.SearchResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let updateBookDetailsCmd (bookItem: BookItem) =
        bookWithKey bookItem.Id
        |> Async.map Msg.BookResultReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let private onMsgPerformSearch (text, pageNum) model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        Status = Status.Loading
                        EnteredText = text } }, [ (text, pageNum) |> PerformSearchCmd ]

    let private onMoreBooksRequested (searchText, endBook) model =
        { model with SearchPageModel = { model.SearchPageModel with Status = Status.Loading } },
        [ (searchText, endBook) |> MoreBooksRequestedCmd ]

    let private onSearchResultReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse = Some(result)
                        Status = statusFromBool (result.IsSuccessful) } }, []

    let private onMoreBooksReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse =
                            match model.SearchPageModel.SearchResponse with
                            | Some x -> (combineModels x result)
                            | None -> result
                            |> Some
                        Status = statusFromBool result.IsSuccessful } }, []

    let private onChangeDisplayedPage page model =
        match page with
        | SearchPage -> { model with BookDetailsPageModel = None }, []
        | DetailsPage book ->
            { model with BookDetailsPageModel = Some(initBookDetailsFromBook (Some(book))) },
            [ book |> UpdateBookDetailsCmd ]

    let private onBookResultReceived result model =
        { model with
              BookDetailsPageModel =
                  Some
                      ({ model.BookDetailsPageModel.Value with
                             BookDetails = Some(result)
                             Status = statusFromBool (result.IsSuccessful) }) }, []

    let private onUpdateBookDetails book model =
        { model with BookDetailsPageModel = Some({ model.BookDetailsPageModel.Value with Status = Status.Loading }) },
        [ book |> UpdateBookDetailsCmd ]

    let private onBookSortingRequested model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        SearchResponse =
                            match model.SearchPageModel.SearchResponse with
                            | Some x -> Some({ x with BookItems = Utils.bookItemsSortedByRatingDesc x.BookItems })
                            | None -> None } }, []

    let private onMoreBooksRequestedCmd (searchText, endBook) =
        searchWithPage (searchText) (nextPageNumber (endBook))
        |> Async.map Msg.MoreBooksReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption

    let update =
        function
        | Msg.PerformSearch (text, pageNum) ->
            onMsgPerformSearch (text, pageNum)
        | Msg.SearchResultReceived result ->
            onSearchResultReceived result
        | Msg.MoreBooksReceived result ->
            onMoreBooksReceived result
        | Msg.ChangeDisplayedPage page ->
            onChangeDisplayedPage page
        | Msg.MoreBooksRequested (searchText, endBook) ->
            onMoreBooksRequested (searchText, endBook)
        | Msg.BookResultReceived result ->
            onBookResultReceived result
        | Msg.UpdateBookDetails book ->
            onUpdateBookDetails book
        | Msg.BookSortingRequested -> onBookSortingRequested

    let view (model: Model) dispatch =

        let openDetailsPage bookItem = fun () -> Msg.ChangeDisplayedPage(DetailsPage bookItem) |> dispatch
        let sortByRating = fun () -> Msg.BookSortingRequested |> dispatch

        View.NavigationPage
            (pages =
                [ yield searchPageView model.SearchPageModel openDetailsPage sortByRating dispatch
                  match model.BookDetailsPageModel with
                  | Some x -> yield bookDetailsPageView model.BookDetailsPageModel.Value dispatch
                  | _ -> () ], popped = fun _ -> Msg.ChangeDisplayedPage SearchPage |> dispatch)

    let mapCommands cmdMsg =
        match cmdMsg with
        | PerformSearchCmd (searchText, pageNum) -> performSearchCmd searchText pageNum
        | UpdateBookDetailsCmd bookItem -> updateBookDetailsCmd bookItem
        | MoreBooksRequestedCmd (searchText, endBook) ->
            onMoreBooksRequestedCmd (searchText, endBook)

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgramWithCmdMsg init update view mapCommands
