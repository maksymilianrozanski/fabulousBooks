module FabBooksTests.UpdateTests

open System
open FabBooks
open FabBooks.BookDetailsPage
open FabBooks.BookItemModule
open FabBooks.MainMessages
open FabBooks.SingleBookResponseModelModule
open NUnit.Framework
open App
open GoodreadsResponseModelModule
open NUnit.Framework
open NUnit.Framework
open NUnit.Framework
open NUnit.Framework.Internal

[<Test>]
let shouldUpdateTextAndStatus () =
    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }

    let queryText = "New text"

    let expected =
        { EnteredText = queryText
          Status = Status.Loading
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }, [ PerformSearchCmd queryText ]

    let result = App.update (Msg.PerformSearch queryText) (initialModel)
    Assert.AreEqual(expected, result)

[<Test>]
let shouldUpdateResponseModelAndStatus () =
    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Loading
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }

    let receivedResponseModel =
        GoodreadsResponseModel
            (true, 0, 1, 1,
             System.Collections.Generic.List
                 [ BookItem("Author", "Title", "https://example.com", "https://example.com", 42) ])

    let expected =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = receivedResponseModel
          BookDetailsPageModel = None }, []

    let result = App.update (Msg.SearchResultReceived receivedResponseModel) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let shouldChangeDisplayedPageToSearchPage () =
    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = Some(BookDetailsPage.initFromBook (None)) }

    let pageMsg = Msg.ChangeDisplayedPage SearchPage

    let expected =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }, []

    let result = App.update pageMsg initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should change displayed page to DetailsPage`` () =
    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }

    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42)
    let pageMsg = Msg.ChangeDisplayedPage(DetailsPage(book))

    let expected =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = Some(BookDetailsPage.initFromBook (Some(book))) }, [ book |> UpdateBookDetailsCmd ]

    let result = App.update pageMsg initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update BookDetailsPageModel on BookResultReceived`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42)

    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading }) }

    let response = SingleBookResponseModel(true, "book description", "https://example.com")

    let expected =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = Some(response)
                     Status = Status.Success }) }, []

    let result = App.update (Msg.BookResultReceived response) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update BookDetailsPageModel on BookResultReceived - expected Failure status`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42)

    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading }) }

    let response = SingleBookResponseModel(false, "response was not successful", "https://example.com")

    let expected =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = Some(response)
                     Status = Status.Failure }) }, []

    let result = App.update (Msg.BookResultReceived response) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should set Loading status in BookDetailsPageModel and call UpdateBookDetailsCmd`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42)

    let initialModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Success }) }

    let expected =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading }) }, [ UpdateBookDetailsCmd book ]

    let result = App.update (Msg.UpdateBookDetails book) initialModel
    Assert.AreEqual(expected, result)
