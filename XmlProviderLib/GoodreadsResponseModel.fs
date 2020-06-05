namespace FabBooks

open System.Collections.Generic
open BookItemModule
open XmlProviderLib

module GoodreadsResponseModelModule =

    type GoodreadsResponseModel(isSuccessful: bool, resultsStart: int, resultsEnd: int, totalResults: int, bookItems: List<BookItem>) =
        member this.IsSuccessful = isSuccessful
        member this.Start = resultsStart
        member this.End = resultsEnd
        member this.Total = totalResults
        member this.BookItems = bookItems

    let emptyGoodreadsModel = GoodreadsResponseModel(false, 0, 0, 0, List())

    let goodreadsFromXml xmlString =
        let response = XmlParser.GoodreadsSearchResponse.Parse(xmlString)

        let bookItems =
            seq {
                for child in response.Search.Results.Works do
                    yield BookItem
                              (child.BestBook.Author.Name, child.BestBook.Title, child.BestBook.ImageUrl,
                               child.BestBook.SmallImageUrl, child.BestBook.Id.Value)
            }

        GoodreadsResponseModel
            (true, response.Search.ResultsStart, response.Search.ResultsEnd, response.Search.TotalResults,
             List(bookItems))
