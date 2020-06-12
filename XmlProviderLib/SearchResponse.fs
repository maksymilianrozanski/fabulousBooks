namespace FabBooks

open BookItemModule
open FabBooks.Responses
open XmlProviderLib

module SearchResponseModule =

    let combineModels (oldModel: SearchResponse) (newModel: SearchResponse) =
        match newModel.IsSuccessful with
        | true -> { newModel with BookItems = List.append oldModel.BookItems newModel.BookItems }
        | false -> { oldModel with IsSuccessful = false }

    let goodreadsFromXml xmlString =
        let response = XmlParser.GoodreadsSearchResponse.Parse(xmlString)

        let bookItems =
            seq {
                for child in response.Search.Results.Works do
                    yield BookItem
                              (child.BestBook.Author.Name, child.BestBook.Title, child.BestBook.ImageUrl,
                               child.BestBook.SmallImageUrl, child.BestBook.Id.Value, child.AverageRating |> float)
            }

        { IsSuccessful = true
          Start = response.Search.ResultsStart
          End = response.Search.ResultsEnd
          Total = response.Search.TotalResults
          BookItems = Collections.List.ofSeq (bookItems) }
