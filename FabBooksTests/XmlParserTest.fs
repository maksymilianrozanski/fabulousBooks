module FabBooksTests.XmlParserTest
//
//open NUnit.Framework
//open FabBooks.XmlParser
//
//[<Test>]
//let shouldPass () = Assert.AreEqual(2, 2)
//
//[<Test>]
//let ShouldParseXmlTest () =
//
//    let resultsStart = 10
//    let resultsEnd = 20
//    let totalResults = 1222
//
//    let originalPublicationYear1 = 1999
//    let averageRating1 = 4.20
//    let title1 = "Harry Potter"
//    let author1 = "JK Rowling"
//    let imageUrl1 = "https://example.com"
//    let smallImageUrl1 = "https://example2.com"
//
//    let originalPublicationYear2 = 2000
//    let averageRating2 = 4.30
//    let title2 = "Harry Potter2"
//    let author2 = "JK Rowling"
//    let imageUrl2 = "https://example3.com"
//    let smallImageUrl2 = "https://example4.com"
//
//    let input =
//        sprintf """<?xml version="1.0" encoding="UTF-8"?>
//<GoodreadsResponse>
//    <Request>
//        <authentication>true</authentication>
//        <key><![CDATA[m9Sc8EET5xVtAGfiruCGg]]></key>
//        <method><![CDATA[search_index]]></method>
//    </Request>
//    <search>
//        <query><![CDATA[Ender's Game]]></query>
//        <results-start>%i</results-start>
//        <results-end>%i</results-end>
//        <total-results>%i</total-results>
//        <source>Goodreads</source>
//        <query-time-seconds>0.06</query-time-seconds>
//        <results>
//            <work>
//                <id type="integer">2422333</id>
//                <books_count type="integer">254</books_count>
//                <ratings_count type="integer">1091580</ratings_count>
//                <text_reviews_count type="integer">42631</text_reviews_count>
//                <original_publication_year type="integer">%i</original_publication_year>
//                <original_publication_month type="integer" nil="true" />
//                <original_publication_day type="integer" nil="true" />
//                <average_rating>%f</average_rating>
//                <best_book type="Book">
//                    <id type="integer">375802</id>
//                    <title>%s</title>
//                    <author>
//                        <id type="integer">589</id>
//                        <name>%s</name>
//                    </author>
//                    <image_url>
//                        %s
//                    </image_url>
//                    <small_image_url>
//                        %s
//                    </small_image_url>
//                </best_book>
//            </work>
//
//            <work>
//                <id type="integer">938064</id>
//                <books_count type="integer">65</books_count>
//                <ratings_count type="integer">83212</ratings_count>
//                <text_reviews_count type="integer">884</text_reviews_count>
//                <original_publication_year type="integer">%i</original_publication_year>
//                <original_publication_month type="integer">12</original_publication_month>
//                <original_publication_day type="integer" nil="true" />
//                <average_rating>%f</average_rating>
//                <best_book type="Book">
//                    <id type="integer">44687</id>
//                    <title>%s</title>
//                    <author>
//                        <id type="integer">8732</id>
//                        <name>%s</name>
//                    </author>
//                    <image_url>
//                        %s
//                    </image_url>
//                    <small_image_url>
//                        %s
//                    </small_image_url>
//                </best_book>
//            </work>
//
//        </results>
//    </search>
//
//</GoodreadsResponse>""" resultsStart resultsEnd totalResults originalPublicationYear1 averageRating1 title1 author1
//            imageUrl1 smallImageUrl1 originalPublicationYear2 averageRating2 title2 author2 imageUrl2 smallImageUrl2
//
//    let result = GoodreadsSearchResponse.Parse(input)
//    Assert.AreEqual(resultsStart, result.Search.ResultsStart)
//    Assert.AreEqual(resultsEnd, result.Search.ResultsEnd)
//    Assert.AreEqual(totalResults, result.Search.TotalResults)
//    
//    Assert.AreEqual(originalPublicationYear1, result.Search.Results.Works.[0].OriginalPublicationYear.Value)
//    Assert.AreEqual(averageRating1, result.Search.Results.Works.[0].AverageRating)
//    Assert.AreEqual(title1, result.Search.Results.Works.[0].BestBook.Title)
//    Assert.AreEqual(author1, result.Search.Results.Works.[0].BestBook.Author.Name)
//    Assert.AreEqual(imageUrl1, result.Search.Results.Works.[0].BestBook.ImageUrl.Trim().TrimEnd())
//    Assert.AreEqual(smallImageUrl1, result.Search.Results.Works.[0].BestBook.SmallImageUrl.Trim().TrimEnd())
//    
//    Assert.AreEqual(originalPublicationYear2, result.Search.Results.Works.[1].OriginalPublicationYear.Value)
//    Assert.AreEqual(averageRating2, result.Search.Results.Works.[1].AverageRating)
//    Assert.AreEqual(title2, result.Search.Results.Works.[1].BestBook.Title)
//    Assert.AreEqual(author2, result.Search.Results.Works.[1].BestBook.Author.Name)
//    Assert.AreEqual(imageUrl2, result.Search.Results.Works.[1].BestBook.ImageUrl.Trim().TrimEnd())
//    Assert.AreEqual(smallImageUrl2, result.Search.Results.Works.[1].BestBook.SmallImageUrl.Trim().TrimEnd())
//    
