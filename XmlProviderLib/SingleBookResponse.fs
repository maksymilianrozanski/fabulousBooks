namespace FabBooks

open FabBooks.Responses
open XmlProviderLib

module SingleBookResponseModelModule =

    let singleBookFromXml xmlString =
        let response = XmlParser.GoodreadsBookResponse.Parse(xmlString)
        SingleBookResponse(true, response.Book.Description, response.Book.Url)
