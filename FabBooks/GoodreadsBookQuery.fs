namespace FabBooks

open System.IO
open System.Web
open System
open FSharp.Data
open SingleBookResponseModelModule
open GoodreadsApiKey

module GoodreadsBookQuery =

    let bookQuery key =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query

    let goodreadsBookRequestBuilder goodreadsBookId query =
        (UriBuilder
            (Scheme = "https", Host = "goodreads.com", Path = sprintf "book/show/%i.xml" goodreadsBookId,
             Query = query.ToString())).Uri

    let private bookGet (key: ApiKey) (goodreadsBookId: int) =
        let getRequest = goodreadsBookRequestBuilder goodreadsBookId (bookQuery key)
        getRequest.ToString()
        |> Http.AsyncRequest
        |> Async.Catch
        |> Async.map (function
            | Choice1Of2 x ->
                match x.Body with
                | Text text -> text |> singleBookFromXml
                | _ -> emptySingleBookModel
            | Choice2Of2 _ -> emptySingleBookModel)

    let bookWithKey = bookGet goodreadsApiKey
