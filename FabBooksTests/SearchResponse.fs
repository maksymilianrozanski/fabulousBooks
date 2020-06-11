module FabBooksTests.GoodreadsResponseModelTests

open FabBooks.BookItemModule
open NUnit.Framework
open FabBooks.SearchResponseModule

[<Test>]
let ``should combine two successful models`` () =
    let initBookItems =
        [ for i in 1 .. 20 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30) ]

    let moreBookItems =
        [ for i in 21 .. 41 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30) ]

    let oldModel =
        { IsSuccessful = true
          Start = 21
          End = 41
          Total = 125
          BookItems = initBookItems }

    let newModel =
        { IsSuccessful = true
          Start = 42
          End = 62
          Total = 125
          BookItems = moreBookItems }

    let expected =
        { IsSuccessful = true
          Start = 42
          End = 62
          Total = 125
          BookItems = initBookItems @ moreBookItems }

    let result = combineModels oldModel newModel
    Assert.AreEqual(expected, result)

[<Test>]
let ``should return old model with Status.Failure`` () =
    let initBookItems =
        [ for i in 1 .. 20 do
            yield BookItem("authorName", "title", "https://example.com", "https://example.com", i + 30) ]

    let oldModel =
        { IsSuccessful = true
          Start = 21
          End = 41
          Total = 125
          BookItems = initBookItems }

    let newModel =
        { IsSuccessful = false
          Start = 0
          End = 0
          Total = 0
          BookItems = [] }

    let expected = { oldModel with IsSuccessful = false }
    let result = combineModels oldModel newModel

    Assert.AreEqual(expected, result)
