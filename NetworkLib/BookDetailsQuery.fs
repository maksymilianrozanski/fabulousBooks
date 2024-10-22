namespace FabBooks

open System.Web
open System
open FSharp.Data
open FabBooks.MainMessages
open FabBooks.Responses
open SingleBookResponseModelModule

module BookDetailsQuery =

    let bookQuery key =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query

    let goodreadsBookRequestBuilder goodreadsBookId query =
        (UriBuilder
            (Scheme = "https", Host = "goodreads.com", Path = sprintf "book/show/%i.xml" goodreadsBookId,
             Query = query.ToString())).Uri

    let private emptySingleBookResponse = SingleBookResponse(false, "", "")

    let bookGet (key: ApiKey) (goodreadsBookId: int) =
        let getRequest = goodreadsBookRequestBuilder goodreadsBookId (bookQuery key)
        getRequest.ToString()
        |> Http.AsyncRequestString
        |> Async.Catch
        |> Async.map (function
            | Choice1Of2 x -> x |> singleBookFromXml
            | Choice2Of2 _ -> emptySingleBookResponse)
