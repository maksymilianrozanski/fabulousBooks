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
          Status = Status.Success
          Idx = 0 }

    let result = isSuccessStatus data

    match result with
    | Right x -> Assert.AreEqual(data, x)
    | Left -> failwith ("should match to the Right")

[<Test>]
let ``should return Left if Status other than success`` () =
    let data =
        { LastLoadedItem = 0
          TotalItems = 0
          Status = Status.Failure
          Idx = 0 }

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
          Status = Status.Success
          Idx = 0 }

    match (areItemsRemaining data) with
    | Right x -> Assert.AreEqual(data, x)
    | Left _ -> failwith ("should match to the Right if LastLoadedItem is less than TotalItems")

[<Test>]
let ``should return Left if fetched all items`` () =
    let data =
        { LastLoadedItem = 0
          TotalItems = 0
          Status = Status.Success
          Idx = 0 }

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
let ``should return Right if close to the end of the list`` () =
    let trigger = 2

    let data =
        { LastLoadedItem = 0
          TotalItems = 20
          Status = Status.Success
          Idx = 0 }

    match (isNearEnd trigger data) with
    | Right x -> ()
    | Left y ->
        failwith (sprintf "should match to the right, items remaining: %i" (data.LastLoadedItem - (data.Idx + 1)))

    let data2 =
        { data with
              LastLoadedItem = 10
              Idx = 9 }
    match (isNearEnd trigger data2) with
    | Right x -> ()
    | Left y ->
        failwith (sprintf "should match to the right, items remaining: %i" (data2.LastLoadedItem - (data2.Idx + 1)))

    let data3 =
        { data with
              LastLoadedItem = 10
              Idx = 7 }
    match (isNearEnd trigger data3) with
    | Right x -> ()
    | Left y ->
        failwith (sprintf "should match to the right, items remaining: %i" (data3.LastLoadedItem - (data3.Idx + 1)))

[<Test>]
let ``should return Left if not close to the end of the list`` () =
    let trigger = 2

    let data =
        { LastLoadedItem = 10
          TotalItems = 20
          Status = Status.Success
          Idx = 6 }

    match (isNearEnd trigger data) with
    | Right x ->
        failwith (sprintf "should match to the left, items remaining: %i" (data.LastLoadedItem - (data.Idx + 1)))
    | Left y -> Assert.AreEqual(ShouldStop, y)
