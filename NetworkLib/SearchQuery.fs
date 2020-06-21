namespace FabBooks

open System
open System.Collections.Specialized
open System.Net
open System.Web
open FSharp.Data
open FabBooks.MainMessages
open FabBooks.Responses
open SearchResponseModule
open GoodreadsApiKey

module SearchQuery =
    
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

    let private emptySearchResponse: SearchResponse =
        { IsSuccessful = false
          Start = 0
          End = 0
          Total = 0
          BookItems = [] }    
    
    let searchGet requestBuilder queryBuilder (searchedValue:SearchText) (page:PageNum)=
        (queryBuilder searchedValue page|> requestBuilder).ToString()
        |> Http.AsyncRequestString
        |> Async.Catch
        |> Async.map (function
            | Choice1Of2 x -> x |> goodreadsFromXml
            | Choice2Of2 _ -> emptySearchResponse)
   
    let searchWithPage = searchGet goodreadsSearchRequestBuilder (queryWithPage (goodreadsApiKey))
    
    let searchWithPage2 apiKey =  searchGet goodreadsSearchRequestBuilder (queryWithPage (apiKey))