namespace FabBooks

module SingleBookResponseModelModule =

    type SingleBookResponseModel(isSuccessful: bool, description: string, link: string) =
        member this.IsSuccessful = isSuccessful
        member this.Description = description
        member this.Link = link

    let emptySingleBookModel = SingleBookResponseModel(false, "", "")

    let singleBookFromXml xmlString =
        let response = FabBooks.XmlParser.GoodreadsBookResponse.Parse(xmlString)
        SingleBookResponseModel(true, response.Book.Description, response.Book.Url)
