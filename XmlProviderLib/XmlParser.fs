namespace XmlProviderLib

open FSharp.Data
open System.Linq

module XmlParser =
    type GoodreadsSearchResponse = XmlProvider<"./searchresponse.xml", EmbeddedResource="XmlProviderLib, searchresponse.xml">

    type GoodreadsBookResponse =
        XmlProvider<"./bookdetailsresponse.xml", EmbeddedResource="XmlProviderLib, bookdetailsresponse.xml">
