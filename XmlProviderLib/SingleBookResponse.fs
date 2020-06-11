namespace FabBooks

open XmlProviderLib

module SingleBookResponseModelModule =

    type SingleBookResponse(isSuccessful: bool, description: string, link: string) =
        member this.IsSuccessful = isSuccessful
        member this.Description = description
        member this.Link = link

    let emptySingleBookModel = SingleBookResponse(false, "", "")

    let singleBookFromXml xmlString =
        let response = XmlParser.GoodreadsBookResponse.Parse(xmlString)
        SingleBookResponse(true, response.Book.Description, response.Book.Url)
