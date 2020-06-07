namespace FabBooks

open System
open System.Collections.Specialized
open System.Net
open System.Web
open FSharp.Data
open GoodreadsResponseModelModule
open GoodreadsApiKey

module GoodreadsQuery =

    type PageNum = int
    type SearchText = string
    
    let searchQuery (key:ApiKey) (search:SearchText) =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query.["q"] <- search
        query

    let addPageToQuery (query: NameValueCollection) (page: PageNum) =
        query.["page"] <- page.ToString()
        query

    let goodreadsSearchRequestBuilder query =
        UriBuilder(Scheme = "https", Host = "goodreads.com", Path = "search/index.xml", Query = query.ToString()).Uri

    let queryWithPage (key:ApiKey) (search:string) (page:PageNum) =  (searchQuery key search  |> addPageToQuery) page
    
    let private searchGet (key: ApiKey) (search:SearchText) =
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

    let searchGet2 (searchedValue:SearchText) (page:PageNum) =
        goodreadsSearchRequestBuilder(queryWithPage).ToString()
        |> Http.AsyncRequest
        |> Async.Catch
        |> Async.map (function
            | Choice1Of2 x ->
                match x.Body with
                | Text text -> text |> goodreadsFromXml
                | _ -> emptyGoodreadsModel
            | Choice2Of2 _ -> emptyGoodreadsModel)
        
    let searchWithKey2 = searchGet2 goodreadsApiKey