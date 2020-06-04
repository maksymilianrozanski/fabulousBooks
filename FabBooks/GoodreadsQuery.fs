namespace FabBooks

open System
open System.Web
open FSharp.Data
open GoodreadsResponseModelModule
open GoodreadsApiKey

module GoodreadsQuery =

    let searchQuery key search =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query.["q"] <- search
        query

    let goodreadsSearchRequestBuilder query =
        UriBuilder(Scheme = "https", Host = "goodreads.com", Path = "search/index.xml", Query = query.ToString()).Uri

    let private searchGet key search =
        let getRequest = goodreadsSearchRequestBuilder (searchQuery key search)
        getRequest.ToString()
        |> Http.AsyncRequest
        |> Async.Catch
        |> Async.map (function
            | Choice1Of2 x ->
                match x.Body with
                | Text text -> text |> goodreadsFromXml
                | _ -> emptyGoodreadsModel
            | Choice2Of2 _ -> emptyGoodreadsModel)

    let searchWithKey = searchGet goodreadsApiKey
