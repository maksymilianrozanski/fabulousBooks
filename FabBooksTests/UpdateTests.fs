module FabBooksTests.UpdateTests

open System
open FabBooks
open FabBooks.BookDetailsPage
open FabBooks.BookItemModule
open FabBooks.GoodreadsResponseModelModule
open FabBooks.MainMessages
open FabBooks.SearchPageModelModule
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
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel = None }

    let queryText = "New text"

    let expected =
        { SearchPageModel =
              { Status = Loading
                EnteredText = queryText
                ResponseModel = None }
          BookDetailsPageModel = None }, [ PerformSearchCmd(queryText, 1) ]

    let result = App.update (Msg.PerformSearch(queryText, 1)) (initialModel)
    Assert.AreEqual(expected, result)

[<Test>]
let shouldUpdateResponseModelAndStatus () =
    let initialModel =
        { SearchPageModel =
              { Status = Loading
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel = None }

    let receivedResponseModel =
        { IsSuccessful = true
          Start = 0
          End = 1
          Total = 1
          BookItems = [ BookItem("Author", "Title", "https://example.com", "https://example.com", 42) ] }

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = Some receivedResponseModel }
          BookDetailsPageModel = None }, []

    let result = App.update (Msg.SearchResultReceived receivedResponseModel) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let shouldChangeDisplayedPageToSearchPage () =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel = Some(BookDetailsPage.initFromBook (None)) }

    let pageMsg = Msg.ChangeDisplayedPage SearchPage

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel = None }, []

    let result = App.update pageMsg initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should change displayed page to DetailsPage`` () =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel = None }

    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42)
    let pageMsg = Msg.ChangeDisplayedPage(DetailsPage(book))

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel = Some(BookDetailsPage.initFromBook (Some(book))) }, [ book |> UpdateBookDetailsCmd ]

    let result = App.update pageMsg initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update BookDetailsPageModel on BookResultReceived`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42)

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading }) }

    let response = SingleBookResponseModel(true, "book description", "https://example.com")

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
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
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading }) }

    let response = SingleBookResponseModel(false, "response was not successful", "https://example.com")

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
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
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Success }) }

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                ResponseModel = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading }) }, [ UpdateBookDetailsCmd book ]

    let result = App.update (Msg.UpdateBookDetails book) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update model on MoreBooksReceived`` () =
    let searchText = "Init text"

    let initBookItems =
        [ for i in 1 .. 20 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30) ]

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = searchText
                ResponseModel =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = 21
                          Total = 125
                          BookItems = initBookItems } }
          BookDetailsPageModel = None }

    let moreBookItems =
        [ for i in 21 .. 41 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30) ]

    let receivedResponseModel =
        { IsSuccessful = true
          Start = 21
          End = 41
          Total = 125
          BookItems = moreBookItems }

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = searchText
                ResponseModel =
                    Some
                        { IsSuccessful = true
                          Start = 21
                          End = 41
                          Total = 125
                          BookItems = initBookItems @ moreBookItems } }

          BookDetailsPageModel = None }, []

    let result = App.update (Msg.MoreBooksReceived receivedResponseModel) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should call MoreBooksRequestedCmd`` () =
    let searchText = "Init text"
    let lastLoadedBook = 20

    let bookItems =
        [ for i in 1 .. 20 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30) ]

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = searchText
                ResponseModel =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = lastLoadedBook
                          Total = 125
                          BookItems = bookItems } }
          BookDetailsPageModel = None }

    let expected =
        { SearchPageModel =
              { Status = Loading
                EnteredText = searchText
                ResponseModel =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = lastLoadedBook
                          Total = 125
                          BookItems = bookItems } }

          BookDetailsPageModel = None }, [ (searchText, lastLoadedBook) |> MoreBooksRequestedCmd ]

    let result = App.update (Msg.MoreBooksRequested(searchText, lastLoadedBook)) initialModel
    Assert.AreEqual(expected, result)
