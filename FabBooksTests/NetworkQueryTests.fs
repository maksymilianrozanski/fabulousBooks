module FabBooksTests.NetworkQueryTests

open System
open System.Web
open NUnit.Framework
open FabBooks.GoodreadsQuery

[<Test>]
let shouldBuildSearchQuery () =
    let apiKey = "123456abc"
    let searchedValue = "functional programming"

    let result = searchQuery apiKey searchedValue

    Assert.AreEqual(searchedValue, result.Get("q"))
    Assert.AreEqual(apiKey, result.Get("key"))

[<Test>]
let shouldBuildRequest () =
    let query = HttpUtility.ParseQueryString(String.Empty)
    query.["Hello"] <- "World"
    let result = goodreadsRequestBuilder query
    Assert.AreEqual("https",result.Scheme)
    Assert.AreEqual("goodreads.com", result.Host)
    Assert.AreEqual("/search/index.xml", result.LocalPath)
    Assert.AreEqual("?Hello=World", result.Query)
    Assert.AreEqual("https://goodreads.com/search/index.xml?Hello=World", result.ToString())
