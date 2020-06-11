namespace XmlProviderLib

open FSharp.Data

module XmlParser =
    type GoodreadsSearchResponse = XmlProvider<"./searchresponse.xml", EmbeddedResource="XmlProviderLib, searchresponse.xml">

    type GoodreadsBookResponse =
        XmlProvider<"./bookdetailsresponse.xml", EmbeddedResource="XmlProviderLib, bookdetailsresponse.xml">
