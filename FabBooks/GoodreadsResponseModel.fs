namespace FabBooks

open System.Collections.Generic
open BookItemModule
open FabBooks.XmlParser

module GoodreadsResponseModelModule =

    type GoodreadsResponseModel(resultsStart: int, resultsEnd: int, totalResults: int, bookItems: List<BookItem>) =
        member this.Start = resultsStart
        member this.End = resultsEnd
        member this.Total = totalResults
        member this.BookItems = bookItems

    let goodreadsFromXml xmlString =
        let response = FabBooks.XmlParser.GoodreadsResponse.Parse(xmlString)

        let bookItems =
            seq {
                for child in response.Search.Results.Works do
                    yield BookItem
                              (child.BestBook.Author.Name, child.BestBook.Title, child.BestBook.ImageUrl,
                               child.BestBook.SmallImageUrl)
            }

        GoodreadsResponseModel
            (response.Search.ResultsStart, response.Search.ResultsEnd, response.Search.TotalResults, List(bookItems))
