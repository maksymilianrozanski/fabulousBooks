﻿// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.

namespace FabBooks

open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open FabBooks.SearchPageModelModule
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open BookItemLayoutModule
open FabBooks.GoodreadsResponseModelModule
open SearchQuery
open StatusLayout
open BookDetailsQuery
open BookItemModule
open Utils
open Xamarin.Forms

module App =
    type Model =
        { SearchPageModel: SearchPageModel
          BookDetailsPageModel: Option<BookDetailsPageModel> }

    let initModel =
        { SearchPageModel = SearchPageModelModule.initModel
          BookDetailsPageModel = None }

    let init () = initModel, []

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
                        ResponseModel = Some(result)
                        Status = statusFromBool (result.IsSuccessful) } }, []

    let private onMoreBooksReceived result model =
        { model with
              SearchPageModel =
                  { model.SearchPageModel with
                        //todo: check for 'None' in combineModels
                        ResponseModel = Some(combineModels model.SearchPageModel.ResponseModel.Value result)
                        Status = statusFromBool result.IsSuccessful } }, []

    let private onChangeDisplayedPage page model =
        match page with
        | SearchPage -> { model with BookDetailsPageModel = None }, []
        | DetailsPage book ->
            { model with BookDetailsPageModel = Some(BookDetailsPage.initFromBook (Some(book))) },
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

    let private onMoreBooksRequestedCmd (searchText, endBook) =
        searchWithPage (searchText) (nextPageNumber (endBook))
        |> Async.map Msg.MoreBooksReceived
        |> Async.map (fun x -> Some x)
        |> Cmd.ofAsyncMsgOption
    //todo: check are there any not displayed books remaining

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

    let view (model: Model) dispatch =

        let openDetailsPage bookItem = fun () -> Msg.ChangeDisplayedPage(DetailsPage bookItem) |> dispatch

        let booksCollectionView searchPageModel =
            match searchPageModel.ResponseModel with
            | Some x ->
                View.CollectionView
                    (items =
                        [ for b in x.BookItems do
                            yield dependsOn b (fun m b -> bookItemLayout (b, openDetailsPage)) ],
                     remainingItemsThreshold = 2,
                     remainingItemsThresholdReachedCommand =
                         (fun () ->
                             if (shouldFetchMoreItems x.End x.Total searchPageModel.Status) then
                                 dispatch (Msg.MoreBooksRequested(searchPageModel.EnteredText, x.End))))
            | None -> View.Label("nothing here yet")

        let searchPage =
            View.ContentPage
                (content =
                    View.StackLayout
                        (padding = Thickness 8.0, verticalOptions = LayoutOptions.Start,
                         children =
                             [ View.Entry
                                 (width = 200.0, placeholder = "Search",
                                  completed = fun textArgs -> Msg.PerformSearch(textArgs, 1) |> dispatch)
                               View.Label(text = model.SearchPageModel.EnteredText)
                               statusLayout (model.SearchPageModel.Status)
                               booksCollectionView (model.SearchPageModel) ]))

        let rootView =
            View.NavigationPage
                (pages =
                    [ yield searchPage
                      match model.BookDetailsPageModel with
                      | Some x -> yield bookDetailsPageView model.BookDetailsPageModel.Value dispatch
                      | _ -> () ], popped = fun _ -> Msg.ChangeDisplayedPage SearchPage |> dispatch)

        rootView

    let mapCommands cmdMsg =
        match cmdMsg with
        | PerformSearchCmd (searchText, pageNum) -> performSearchCmd searchText pageNum
        | UpdateBookDetailsCmd bookItem -> updateBookDetailsCmd bookItem
        | MoreBooksRequestedCmd (searchText, endBook) ->
            onMoreBooksRequestedCmd (searchText, endBook)

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgramWithCmdMsg init update view mapCommands
