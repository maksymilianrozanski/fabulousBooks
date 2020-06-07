module FabBooksTests.UpdateTests

open System
open System.Collections.Generic
open System.Web
open FabBooks
open FabBooks.BookDetailsPage
open FabBooks.BookItemModule
open FabBooks.MainMessages
open NUnit.Framework
open App
open GoodreadsResponseModelModule
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
