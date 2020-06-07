namespace FabBooks

open System.Collections.Generic
open BookItemModule
open XmlProviderLib

module GoodreadsResponseModelModule =

    type GoodreadsResponseModel2 =
        { IsSuccessful: bool
          Start: int
          End: int
          Total: int
          BookItems: Collections.List<BookItem> }

    let combineModels (oldModel: GoodreadsResponseModel2) (newModel: GoodreadsResponseModel2) =
        { newModel with BookItems = oldModel.BookItems @ newModel.BookItems }

    let emptyGoodreadsModel2: GoodreadsResponseModel2 =
        { IsSuccessful = false
          Start = 0
          End = 0
          Total = 0
          BookItems = [] }

    let goodreadsFromXml xmlString =
        let response = XmlParser.GoodreadsSearchResponse.Parse(xmlString)

        let bookItems =
            seq {
                for child in response.Search.Results.Works do
                    yield BookItem
                              (child.BestBook.Author.Name, child.BestBook.Title, child.BestBook.ImageUrl,
                               child.BestBook.SmallImageUrl, child.BestBook.Id.Value)
            }

        let m =
            { IsSuccessful = true
              Start = response.Search.ResultsStart
              End = response.Search.ResultsEnd
              Total = response.Search.TotalResults
              BookItems = Collections.List.ofSeq (bookItems) }

        m
