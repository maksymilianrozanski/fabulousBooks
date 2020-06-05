namespace FabBooks

open FSharp.Data

module XmlParser =
    type GoodreadsSearchResponse = XmlProvider<"searchresponse.xml">

    type GoodreadsBookResponse = XmlProvider<"bookdetailsresponse.xml">
