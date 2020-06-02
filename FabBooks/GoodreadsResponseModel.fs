namespace FabBooks

open System
open System.Collections.Generic
open System.Xml.Linq
open BookItemModule

module GoodreadsResponseModelModule =

    type GoodreadsResponseModel(resultsStart: int, resultsEnd: int, totalResults: int, bookItems: List<BookItem>) =
        member this.Start = resultsStart
        member this.End = resultsEnd
        member this.Total = totalResults
        member this.BookItems = bookItems

    let goodreadsFromXml2 xmlString =
        let xn s = XName.Get(s)
        let xd = XDocument.Parse(xmlString)
        let root = xd.Element(xn "GoodreadsResponse")
        let search = root.Element(xn "search")
        let resultsStart = search.Element(xn "results-start").Value |> int
        let resultsEnd = search.Element(xn "results-end").Value |> int
        let totalResults = search.Element(xn "total-results").Value |> int
        let results = search.Elements(xn "results")
        let works = results.Elements(xn "work")

        let bookItems =
            seq {
                for child in works do
                    let avgRating = child.Elements(xn "average_rating")
                    let bestBook = child.Element(xn "best_book")
                    yield BookItem
                              (bestBook.Element(xn "author").Element(xn "name").Value,
                               bestBook.Element(xn "title").Value, bestBook.Element(xn "image_url").Value,
                               bestBook.Element(xn "small_image_url").Value)
            }

        GoodreadsResponseModel(resultsStart, resultsEnd, totalResults, List(bookItems))
