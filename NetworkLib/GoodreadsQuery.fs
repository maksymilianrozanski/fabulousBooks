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

    let queryWithPage (key:ApiKey) (search:SearchText) (page:PageNum) =  (searchQuery key search  |> addPageToQuery) page

    let searchGet requestBuilder queryBuilder (searchedValue:SearchText) (page:PageNum)=
        (queryBuilder searchedValue page|> requestBuilder).ToString()
        |> Http.AsyncRequest
        |> Async.Catch
        |> Async.map (function
            | Choice1Of2 x ->
                match x.Body with
                | Text text -> text |> goodreadsFromXml
                | _ -> emptyGoodreadsModel
            | Choice2Of2 _ -> emptyGoodreadsModel)
   
    let searchWithPage = searchGet goodreadsSearchRequestBuilder (queryWithPage (goodreadsApiKey))