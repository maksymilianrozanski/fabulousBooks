namespace FabBooks

open FSharp.Data
open System.Xml.Linq

module XmlParser =
    type GoodreadsResponse = XmlProvider<"""<?xml version="1.0" encoding="UTF-8"?>
<GoodreadsResponse>
    <Request>
        <authentication>true</authentication>
        <key><![CDATA[m9Sc8EET5xVtAGfiruCGg]]></key>
        <method><![CDATA[search_index]]></method>
    </Request>
    <search>
        <query><![CDATA[Ender's Game]]></query>
        <results-start>1</results-start>
        <results-end>20</results-end>
        <total-results>752</total-results>
        <source>Goodreads</source>
        <query-time-seconds>0.06</query-time-seconds>
        <results>
            <work>
                <id type="integer">2422333</id>
                <books_count type="integer">254</books_count>
                <ratings_count type="integer">1091580</ratings_count>
                <text_reviews_count type="integer">42631</text_reviews_count>
                <original_publication_year type="integer">1985</original_publication_year>
                <original_publication_month type="integer" nil="true" />
                <original_publication_day type="integer" nil="true" />
                <average_rating>4.30</average_rating>
                <best_book type="Book">
                    <id type="integer">375802</id>
                    <title>Ender’s Game (Ender’s Saga, #1)</title>
                    <author>
                        <id type="integer">589</id>
                        <name>Orson Scott Card</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1408303130l/375802._SY160_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1408303130l/375802._SY75_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">938064</id>
                <books_count type="integer">65</books_count>
                <ratings_count type="integer">83212</ratings_count>
                <text_reviews_count type="integer">884</text_reviews_count>
                <original_publication_year type="integer">1984</original_publication_year>
                <original_publication_month type="integer">12</original_publication_month>
                <original_publication_day type="integer" nil="true" />
                <average_rating>4.18</average_rating>
                <best_book type="Book">
                    <id type="integer">44687</id>
                    <title>Enchanters' End Game (The Belgariad, #5)</title>
                    <author>
                        <id type="integer">8732</id>
                        <name>David Eddings</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1217735909l/44687._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1217735909l/44687._SY75_.jpg
                    </small_image_url>
                </best_book>
            </work>

        </results>
    </search>

</GoodreadsResponse>""">
