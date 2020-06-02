namespace FabBooks

open System
open System.Web
open FSharp.Data
open GoodreadsResponseModelModule

module GoodreadsQuery =

    let searchQuery key search =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query.["q"] <- search
        query

    let goodreadsRequestBuilder query =
        UriBuilder(Scheme = "https", Host = "goodreads.com", Path = "search/index.xml", Query = query.ToString()).Uri

    let searchGet key search =
        let inline map f x = async.Bind(x, f >> async.Return)
        let getRequest = goodreadsRequestBuilder (searchQuery key search)
        getRequest.ToString()
        |> Http.AsyncRequestString
        |> map (goodreadsFromXml)
