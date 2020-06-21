module FabBooksTests.UpdateTests

open FabBooks
open FabBooks.BookDetailsPage
open FabBooks.BookItemModule
open FabBooks.SearchResponseModule
open FabBooks.MainMessages
open Models
open FabBooks.SingleBookResponseModelModule
open NUnit.Framework
open MainModel
open NUnit.Framework
open Responses

let exampleApiKey = Some("ExampleApiKey")

[<Test>]
let shouldUpdateTextAndStatus () =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let queryText = "New text"

    let expected =
        { SearchPageModel =
              { Status = Loading
                EnteredText = queryText
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }, [ PerformSearchCmd(exampleApiKey.Value, queryText, 1) ]

    let result = App.update (Msg.PerformSearch(queryText, 1)) (initialModel)
    Assert.AreEqual(expected, result)

[<Test>]
let shouldUpdateResponseModelAndStatus () =
    let initialModel =
        { SearchPageModel =
              { Status = Loading
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let receivedResponseModel =
        { IsSuccessful = true
          Start = 0
          End = 1
          Total = 1
          BookItems = [ BookItem("Author", "Title", "https://example.com", "https://example.com", 42, 4.3) ] }

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = Some receivedResponseModel }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }, []

    let result = App.update (Msg.SearchResultReceived receivedResponseModel) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let shouldChangeDisplayedPageToSearchPage () =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = Some(initBookDetailsFromBook (None))
          GoodreadsApiKey = exampleApiKey }

    let pageMsg = Msg.ChangeDisplayedPage SearchPage

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }, []

    let result = App.update pageMsg initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should change displayed page to DetailsPage`` () =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42, 4.3)
    let pageMsg = Msg.ChangeDisplayedPage(DetailsPage(book))

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = Some(initBookDetailsFromBook (Some(book)))
          GoodreadsApiKey = exampleApiKey }, [ book |> UpdateBookDetailsCmd ]

    let result = App.update pageMsg initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update BookDetailsPageModel on BookResultReceived`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42, 4.3)

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading })
          GoodreadsApiKey = exampleApiKey }

    let response = SingleBookResponse(true, "book description", "https://example.com")

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = Some(response)
                     Status = Status.Success })
          GoodreadsApiKey = exampleApiKey }, []

    let result = App.update (Msg.BookResultReceived response) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update BookDetailsPageModel on BookResultReceived - expected Failure status`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42, 4.3)

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading })
          GoodreadsApiKey = exampleApiKey }

    let response = SingleBookResponse(false, "response was not successful", "https://example.com")

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = Some(response)
                     Status = Status.Failure })
          GoodreadsApiKey = exampleApiKey }, []

    let result = App.update (Msg.BookResultReceived response) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should set Loading status in BookDetailsPageModel and call UpdateBookDetailsCmd`` () =
    let book = BookItem("Author", "Title", "https://example.com", "https://example.com", 42, 4.3)

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Success })
          GoodreadsApiKey = exampleApiKey }

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel =
              Some
                  ({ DisplayedBook = Some(book)
                     BookDetails = None
                     Status = Status.Loading })
          GoodreadsApiKey = exampleApiKey }, [ UpdateBookDetailsCmd book ]

    let result = App.update (Msg.UpdateBookDetails book) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should update model on MoreBooksReceived`` () =
    let searchText = "Init text"

    let initBookItems =
        [ for i in 1 .. 20 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30, 4.3) ]

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = searchText
                SearchResponse =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = 21
                          Total = 125
                          BookItems = initBookItems } }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let moreBookItems =
        [ for i in 21 .. 41 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30, 4.3) ]

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
                SearchResponse =
                    Some
                        { IsSuccessful = true
                          Start = 21
                          End = 41
                          Total = 125
                          BookItems = initBookItems @ moreBookItems } }

          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }, []

    let result = App.update (Msg.MoreBooksReceived receivedResponseModel) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should call MoreBooksRequestedCmd`` () =
    let searchText = "Init text"
    let lastLoadedBook = 20

    let bookItems =
        [ for i in 1 .. 20 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30, 4.3) ]

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = searchText
                SearchResponse =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = lastLoadedBook
                          Total = 125
                          BookItems = bookItems } }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let expected =
        { SearchPageModel =
              { Status = Loading
                EnteredText = searchText
                SearchResponse =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = lastLoadedBook
                          Total = 125
                          BookItems = bookItems } }

          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }, [ (searchText, lastLoadedBook) |> MoreBooksRequestedCmd ]

    let result = App.update (Msg.MoreBooksRequested(searchText, lastLoadedBook)) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should return items sorted by rating`` () =
    let initialBookItems =
        [ BookItem("Author1", "Title1", "https://example.com", "https://example.com", 42, 4.2)
          BookItem("Author2", "Title2", "https://example.com", "https://example.com", 45, 4.3)
          BookItem("Author3", "Title3", "https://example.com", "https://example.com", 44, 4.4)
          BookItem("Author4", "Title4", "https://example.com", "https://example.com", 46, 4.0) ]

    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Search-text"
                SearchResponse =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = 4
                          Total = 125
                          BookItems = initialBookItems } }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let expectedBooks =
        [ initialBookItems.[2]
          initialBookItems.[1]
          initialBookItems.[0]
          initialBookItems.[3] ]

    let expected =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Search-text"
                SearchResponse =
                    Some
                        { IsSuccessful = true
                          Start = 1
                          End = 4
                          Total = 125
                          BookItems = expectedBooks } }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }, []

    let result = App.update (Msg.BookSortingRequested) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should return 'None' when empty items`` () =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Search-text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = exampleApiKey }

    let expected = initialModel, []
    let result = App.update (Msg.BookSortingRequested) initialModel
    Assert.AreEqual(expected, result)
    
[<Test>]
let ``should call function saving api key when key is empty``() =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = None }
        
    let savingFunc key=
        sprintf ("%s World!") key
    
    let expected =
        {initialModel with GoodreadsApiKey = Some("Hello World!")}, []
    let result = App.update ((Msg.SaveGoodreadsKey) ("Hello", savingFunc)) initialModel
    Assert.AreEqual(expected, result)
    
[<Test>]
let ``should call function saving api key when key is not empty``() =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = Some("old key") }
        
    let savingFunc key=
        sprintf ("%s World!") key
    
    let expected =
        {initialModel with GoodreadsApiKey = Some("Hello World!")}, []
    let result = App.update ((Msg.SaveGoodreadsKey) ("Hello", savingFunc)) initialModel
    Assert.AreEqual(expected, result)
    
[<Test>]
let ``should call function deleting api key when key is not empty``() =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = Some("old key") }
        
    let deletingFunc () =
        true
        
    let expected =
        {initialModel with GoodreadsApiKey = None}, []
        
    let result = App.update (Msg.RemoveGoodreadsKey deletingFunc) initialModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should call function deleting api key when key is empty``() =
    let initialModel =
        { SearchPageModel =
              { Status = Success
                EnteredText = "Init text"
                SearchResponse = None }
          BookDetailsPageModel = None
          GoodreadsApiKey = None }
        
    let deletingFunc () =
        false
        
    let expected =
        {initialModel with GoodreadsApiKey = None}, []
        
    let result = App.update (Msg.RemoveGoodreadsKey deletingFunc) initialModel
    Assert.AreEqual(expected, result)