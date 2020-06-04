namespace FabBooks

open System.IO
open System.Web
open System

module GoodreadsBookQuery =

    let bookQuery key =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query

    let goodreadsBookRequestBuilder goodreadsBookId query =
        (UriBuilder
            (Scheme = "https", Host = "goodreads.com", Path = sprintf "book/show/%i.xml" goodreadsBookId,
             Query = query.ToString())).Uri
