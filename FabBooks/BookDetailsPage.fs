namespace FabBooks

open System.Data
open FabBooks.BookItemModule
open FabBooks.SingleBookResponseModelModule
open Fabulous
open Fabulous.XamarinForms
open MainMessages
open DetailsMessages
open GoodreadsBookQuery

module BookDetailsPage =

    type BookDetailsPageModel =
        { DisplayedBook: Option<BookItem>
          BookDetails: Option<SingleBookResponseModel>
          Status: Status }

    let initModel =
        { DisplayedBook = None
          BookDetails = None
          Status = Success }

    let init () = initModel, Cmd.none

    let initFromId id =
        { DisplayedBook = id
          BookDetails = None
          Status = Success }

    let update msg model =
        match msg with
        | NavigateToDetailsPageMsg book ->
            { model with DisplayedBook = Some(book) }, Cmd.none
        | BookResultReceived result ->
            { model with BookDetails = Some(result) },
            match result.IsSuccessful with
            | true -> Status.Success
            | _ -> Status.Failure
            |> UpdateStatus
            |> Cmd.ofMsg
        | UpdateBookDetails book ->
            model,
            bookWithKey book.Id
            |> Async.map BookResultReceived
            |> Async.map (fun x -> Some x)
            |> Cmd.ofAsyncMsgOption
        | UpdateStatus status ->
            { model with Status = status }, Cmd.none


    let view (model: BookDetailsPageModel) dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (children =
                        [ View.Label(text = "details page.")
                          StatusLayout.statusLayout (model.Status)
                          View.Label(text = "current title = " + model.DisplayedBook.Value.Title.ToString()) ]))
