namespace FabBooks

open System
open System.Web

module GoodreadsQuery =

    let searchQuery key search =
        let query = HttpUtility.ParseQueryString(String.Empty)
        query.["key"] <- key
        query.["q"] <- search
        query

    let goodreadsRequestBuilder query =
        UriBuilder(Scheme = "https", Host = "goodreads.com", Path = "search/index.xml", Query = query.ToString()).Uri
