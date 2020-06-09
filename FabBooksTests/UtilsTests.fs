module FabBooksTests.UtilsTests

open FabBooks
open FabBooks.MainMessages
open NUnit.Framework
open NUnit.Framework.Internal
open NUnit.Framework.Internal
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
