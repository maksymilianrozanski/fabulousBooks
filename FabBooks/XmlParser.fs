namespace FabBooks

open FSharp.Data

module XmlParser =
    type GoodreadsSearchResponse = XmlProvider<"./searchresponse.xml", EmbeddedResource="FabBooks, searchresponse.xml">

    type GoodreadsBookResponse =
        XmlProvider<"./bookdetailsresponse.xml", EmbeddedResource="FabBooks, bookdetailsresponse.xml">
