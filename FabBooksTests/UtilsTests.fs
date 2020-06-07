module FabBooksTests.UtilsTests

open FabBooks
open NUnit.Framework
open Utils

[<Test>]
let ``should return next page number`` () =
    Assert.AreEqual(1, nextPageNumber(0))
    Assert.AreEqual(2, nextPageNumber(20))
    Assert.AreEqual(2, nextPageNumber(21))
    Assert.AreEqual(2, nextPageNumber(39))
    Assert.AreEqual(3, nextPageNumber(40))
    
    