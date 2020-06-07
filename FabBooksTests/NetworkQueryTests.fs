module FabBooksTests.NetworkQueryTests

open System
open System.Web
open NUnit.Framework
open FabBooks.GoodreadsQuery
open FabBooks.GoodreadsBookQuery

[<Test>]
let shouldBuildSearchQuery () =
    let apiKey = "123456abc"
    let searchedValue = "functional programming"

    let result = searchQuery apiKey searchedValue

    Assert.AreEqual(searchedValue, result.Get("q"))
    Assert.AreEqual(apiKey, result.Get("key"))

[<Test>]
let shouldBuildSearchRequest () =
    let query = HttpUtility.ParseQueryString(String.Empty)
    query.["Hello"] <- "World"
    let result = goodreadsSearchRequestBuilder query
    Assert.AreEqual("https", result.Scheme)
    Assert.AreEqual("goodreads.com", result.Host)
    Assert.AreEqual("/search/index.xml", result.LocalPath)
    Assert.AreEqual("?Hello=World", result.Query)
    Assert.AreEqual("https://goodreads.com/search/index.xml?Hello=World", result.ToString())

[<Test>]
let ``should build search request with page number`` () =
    let apiKey = "123456abc"
    let searchedValue = "functional programming"
    let page = 3

    let result = queryWithPage apiKey searchedValue page
    Assert.AreEqual(searchedValue, result.Get("q"))
    Assert.AreEqual(apiKey, result.Get("key"))
    Assert.AreEqual(page.ToString(), result.Get("page"))

[<Test>]
let shouldBuildBookRequest () =
    let bookId = 42
    let query = HttpUtility.ParseQueryString(String.Empty)
    query.["Hello"] <- "World"
    let result = goodreadsBookRequestBuilder bookId query
    Assert.AreEqual("https", result.Scheme)
    Assert.AreEqual("goodreads.com", result.Host)
    Assert.AreEqual(sprintf "/book/show/%i.xml" bookId, result.LocalPath)
    Assert.AreEqual("?Hello=World", result.Query)
    Assert.AreEqual(sprintf "https://goodreads.com/book/show/%i.xml?Hello=World" bookId, result.ToString())
