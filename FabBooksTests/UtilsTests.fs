module FabBooksTests.UtilsTests

open FabBooks
open FabBooks.BookItemModule
open FabBooks.MainMessages
open NUnit.Framework
open Utils

[<Test>]
let ``should return next page number`` () =
    Assert.AreEqual(1, nextPageNumber (0))
    Assert.AreEqual(2, nextPageNumber (20))
    Assert.AreEqual(2, nextPageNumber (21))
    Assert.AreEqual(2, nextPageNumber (39))
    Assert.AreEqual(3, nextPageNumber (40))

[<Test>]
let ``should return Right if Status is success`` () =
    let data =
        { LastLoadedItem = 0
          TotalItems = 0
          Status = Status.Success }

    let result = isSuccessStatus data

    match result with
    | Right x -> Assert.AreEqual(data, x)
    | Left -> failwith ("should match to the Right")

[<Test>]
let ``should return Left if Status other than success`` () =
    let data =
        { LastLoadedItem = 0
          TotalItems = 0
          Status = Status.Failure }

    let verify (result: Either<ShouldStop, ShouldFetchArgs>) =
        match result with
        | Right x -> failwith ("should match to 2 of 2 for status: " + x.Status.ToString())
        | Left y -> Assert.AreEqual(ShouldStop, y)

    verify (isSuccessStatus data)
    verify (isSuccessStatus { data with Status = Status.Loading })

[<Test>]
let ``should return Right if at least one item remaining`` () =
    let data =
        { LastLoadedItem = 19
          TotalItems = 20
          Status = Status.Success }

    match (areItemsRemaining data) with
    | Right x -> Assert.AreEqual(data, x)
    | Left _ -> failwith ("should match to the Right if LastLoadedItem is less than TotalItems")

[<Test>]
let ``should return Left if fetched all items`` () =
    let data =
        { LastLoadedItem = 0
          TotalItems = 0
          Status = Status.Success }

    let verify (result: Either<ShouldStop, ShouldFetchArgs>) =
        match result with
        | Right x ->
            failwith
                (sprintf "should return left for: LastLoadedItem %i, TotalItems %i" x.LastLoadedItem x.TotalItems)
        | Left y -> Assert.AreEqual(ShouldStop, y)

    verify (areItemsRemaining data)
    verify
        (areItemsRemaining
            { data with
                  LastLoadedItem = 10
                  TotalItems = 9 })
    verify
        (areItemsRemaining
            { data with
                  LastLoadedItem = 10
                  TotalItems = 10 })

[<Test>]
let ``should return true if url contains photo`` () =
    Assert.True
        (hasImage
            ("https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1456611934l/29354137._SX98_.jpg"))

[<Test>]
let ``should return false if url does not contain photo`` () =
    Assert.False(hasImage ("https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png"))

[<Test>]
let ``should sort books by rating from highest to lowest`` () =
    let bookItems =
        [ BookItem("Author1", "Title1", "https://example.com", "https://example.com", 42, 4.2)
          BookItem("Author2", "Title2", "https://example.com", "https://example.com", 45, 4.3)
          BookItem("Author3", "Title3", "https://example.com", "https://example.com", 44, 4.4)
          BookItem("Author4", "Title4", "https://example.com", "https://example.com", 46, 4.0) ]

    let expected =
        [ bookItems.[2]
          bookItems.[1]
          bookItems.[0]
          bookItems.[3] ]

    let result = Utils.bookItemsSortedByRatingDesc bookItems
    Assert.AreEqual(expected, result)
