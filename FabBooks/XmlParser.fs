module FabBooks.XmlParser

open System.Xml.Serialization

//#r "../../../bin/lib/net45/FSharp.Data.dll"
//#r "System.Xml.Linq.dll"

open FSharp.Data

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

            <work>
                <id type="integer">6581562</id>
                <books_count type="integer">5</books_count>
                <ratings_count type="integer">35922</ratings_count>
                <text_reviews_count type="integer">238</text_reviews_count>
                <original_publication_year type="integer">2009</original_publication_year>
                <original_publication_month type="integer" nil="true" />
                <original_publication_day type="integer" nil="true" />
                <average_rating>4.39</average_rating>
                <best_book type="Book">
                    <id type="integer">6393082</id>
                    <title>Ender's Game, Volume 1: Battle School (Ender's Saga)</title>
                    <author>
                        <id type="integer">38491</id>
                        <name>Christopher Yost</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">55447683</id>
                <books_count type="integer">9</books_count>
                <ratings_count type="integer">32899</ratings_count>
                <text_reviews_count type="integer">2094</text_reviews_count>
                <original_publication_year type="integer">2017</original_publication_year>
                <original_publication_month type="integer">11</original_publication_month>
                <original_publication_day type="integer">14</original_publication_day>
                <average_rating>4.15</average_rating>
                <best_book type="Book">
                    <id type="integer">34368113</id>
                    <title>End Game (Will Robie, #5)</title>
                    <author>
                        <id type="integer">9291</id>
                        <name>David Baldacci</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1495976812l/34368113._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1495976812l/34368113._SY75_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">43992</id>
                <books_count type="integer">7</books_count>
                <ratings_count type="integer">14136</ratings_count>
                <text_reviews_count type="integer">245</text_reviews_count>
                <original_publication_year type="integer">1984</original_publication_year>
                <original_publication_month type="integer" nil="true" />
                <original_publication_day type="integer" nil="true" />
                <average_rating>4.27</average_rating>
                <best_book type="Book">
                    <id type="integer">44660</id>
                    <title>The Belgariad Boxed Set: Pawn of Prophecy / Queen of Sorcery / Magician's Gambit / Castle of
                        Wizardry / Enchanters' End Game (The Belgariad, #1-5)
                    </title>
                    <author>
                        <id type="integer">8732</id>
                        <name>David Eddings</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1391347386l/44660._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1391347386l/44660._SX50_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">7272274</id>
                <books_count type="integer">3</books_count>
                <ratings_count type="integer">15203</ratings_count>
                <text_reviews_count type="integer">56</text_reviews_count>
                <original_publication_year type="integer">2010</original_publication_year>
                <original_publication_month type="integer">3</original_publication_month>
                <original_publication_day type="integer">24</original_publication_day>
                <average_rating>4.60</average_rating>
                <best_book type="Book">
                    <id type="integer">7025086</id>
                    <title>Ender's Game, Volume 2: Command School</title>
                    <author>
                        <id type="integer">38491</id>
                        <name>Christopher Yost</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">56291562</id>
                <books_count type="integer">7</books_count>
                <ratings_count type="integer">7052</ratings_count>
                <text_reviews_count type="integer">369</text_reviews_count>
                <original_publication_year type="integer">2017</original_publication_year>
                <original_publication_month type="integer">7</original_publication_month>
                <original_publication_day type="integer">9</original_publication_day>
                <average_rating>4.16</average_rating>
                <best_book type="Book">
                    <id type="integer">35010791</id>
                    <title>The Gender End (The Gender Game, #7)</title>
                    <author>
                        <id type="integer">6860531</id>
                        <name>Bella Forrest</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">42437887</id>
                <books_count type="integer">7</books_count>
                <ratings_count type="integer">5483</ratings_count>
                <text_reviews_count type="integer">671</text_reviews_count>
                <original_publication_year type="integer">2015</original_publication_year>
                <original_publication_month type="integer">9</original_publication_month>
                <original_publication_day type="integer">8</original_publication_day>
                <average_rating>4.11</average_rating>
                <best_book type="Book">
                    <id type="integer">22874150</id>
                    <title>The End Game</title>
                    <author>
                        <id type="integer">6876994</id>
                        <name>Kate McCarthy</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1423089153l/22874150._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1423089153l/22874150._SY75_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">938065</id>
                <books_count type="integer">10</books_count>
                <ratings_count type="integer">7335</ratings_count>
                <text_reviews_count type="integer">146</text_reviews_count>
                <original_publication_year type="integer">1980</original_publication_year>
                <original_publication_month type="integer">1</original_publication_month>
                <original_publication_day type="integer">1</original_publication_day>
                <average_rating>4.39</average_rating>
                <best_book type="Book">
                    <id type="integer">18879</id>
                    <title>The Belgariad, Vol. Two: Castle of Wizardry / Enchanters' End Game (The Belgariad, #4-5)
                    </title>
                    <author>
                        <id type="integer">8732</id>
                        <name>David Eddings</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">44223038</id>
                <books_count type="integer">20</books_count>
                <ratings_count type="integer">6236</ratings_count>
                <text_reviews_count type="integer">474</text_reviews_count>
                <original_publication_year type="integer">2015</original_publication_year>
                <original_publication_month type="integer">9</original_publication_month>
                <original_publication_day type="integer">15</original_publication_day>
                <average_rating>4.27</average_rating>
                <best_book type="Book">
                    <id type="integer">24611928</id>
                    <title>The End Game (A Brit in the FBI, #3)</title>
                    <author>
                        <id type="integer">1239</id>
                        <name>Catherine Coulter</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1425319746l/24611928._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1425319746l/24611928._SX50_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">56238306</id>
                <books_count type="integer">5</books_count>
                <ratings_count type="integer">2489</ratings_count>
                <text_reviews_count type="integer">91</text_reviews_count>
                <original_publication_year type="integer">2017</original_publication_year>
                <original_publication_month type="integer">4</original_publication_month>
                <original_publication_day type="integer">24</original_publication_day>
                <average_rating>4.32</average_rating>
                <best_book type="Book">
                    <id type="integer">34963329</id>
                    <title>End Game (Jack Noble #12)</title>
                    <author>
                        <id type="integer">6151659</id>
                        <name>L.T. Ryan</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">66029652</id>
                <books_count type="integer">3</books_count>
                <ratings_count type="integer">1398</ratings_count>
                <text_reviews_count type="integer">151</text_reviews_count>
                <original_publication_year type="integer" nil="true" />
                <original_publication_month type="integer" nil="true" />
                <original_publication_day type="integer" nil="true" />
                <average_rating>4.03</average_rating>
                <best_book type="Book">
                    <id type="integer">42372731</id>
                    <title>The End Game (Love Games #2)</title>
                    <author>
                        <id type="integer">15996299</id>
                        <name>Mickey Miller</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">51609213</id>
                <books_count type="integer">8</books_count>
                <ratings_count type="integer">1231</ratings_count>
                <text_reviews_count type="integer">277</text_reviews_count>
                <original_publication_year type="integer">2018</original_publication_year>
                <original_publication_month type="integer">1</original_publication_month>
                <original_publication_day type="integer">2</original_publication_day>
                <average_rating>4.21</average_rating>
                <best_book type="Book">
                    <id type="integer">30985221</id>
                    <title>End Game (Dirty Money, #4)</title>
                    <author>
                        <id type="integer">73977</id>
                        <name>Lisa Renee Jones</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1497740135l/30985221._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1497740135l/30985221._SX50_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">53830455</id>
                <books_count type="integer">5</books_count>
                <ratings_count type="integer">2294</ratings_count>
                <text_reviews_count type="integer">165</text_reviews_count>
                <original_publication_year type="integer">2016</original_publication_year>
                <original_publication_month type="integer">11</original_publication_month>
                <original_publication_day type="integer">27</original_publication_day>
                <average_rating>4.42</average_rating>
                <best_book type="Book">
                    <id type="integer">33144572</id>
                    <title>End Game (Fallen Empire, #8)</title>
                    <author>
                        <id type="integer">4512224</id>
                        <name>Lindsay Buroker</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">61846440</id>
                <books_count type="integer">2</books_count>
                <ratings_count type="integer">1281</ratings_count>
                <text_reviews_count type="integer">236</text_reviews_count>
                <original_publication_year type="integer">2018</original_publication_year>
                <original_publication_month type="integer">5</original_publication_month>
                <original_publication_day type="integer">23</original_publication_day>
                <average_rating>4.40</average_rating>
                <best_book type="Book">
                    <id type="integer">39947220</id>
                    <title>End Game (Bellevue Bullies, #4)</title>
                    <author>
                        <id type="integer">5255580</id>
                        <name>Toni Aleo</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1526291653l/39947220._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1526291653l/39947220._SY75_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">19174329</id>
                <books_count type="integer">10</books_count>
                <ratings_count type="integer">601</ratings_count>
                <text_reviews_count type="integer">111</text_reviews_count>
                <original_publication_year type="integer">2013</original_publication_year>
                <original_publication_month type="integer">1</original_publication_month>
                <original_publication_day type="integer">1</original_publication_day>
                <average_rating>3.74</average_rating>
                <best_book type="Book">
                    <id type="integer">13586977</id>
                    <title>Ender's World: Fresh Perspectives on the SF Classic Ender's Game</title>
                    <author>
                        <id type="integer">589</id>
                        <name>Orson Scott Card</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1344608510l/13586977._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1344608510l/13586977._SX50_.jpg
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">12531</id>
                <books_count type="integer">1</books_count>
                <ratings_count type="integer">1890</ratings_count>
                <text_reviews_count type="integer">87</text_reviews_count>
                <original_publication_year type="integer">2001</original_publication_year>
                <original_publication_month type="integer">10</original_publication_month>
                <original_publication_day type="integer">14</original_publication_day>
                <average_rating>4.22</average_rating>
                <best_book type="Book">
                    <id type="integer">9736</id>
                    <title>Beyond Ender's Game: Speaker for the Dead, Xenocide, Children of the Mind (Ender's Saga,
                        #2-4)
                    </title>
                    <author>
                        <id type="integer">589</id>
                        <name>Orson Scott Card</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">12530</id>
                <books_count type="integer">3</books_count>
                <ratings_count type="integer">1906</ratings_count>
                <text_reviews_count type="integer">60</text_reviews_count>
                <original_publication_year type="integer">2002</original_publication_year>
                <original_publication_month type="integer">9</original_publication_month>
                <original_publication_day type="integer">16</original_publication_day>
                <average_rating>4.49</average_rating>
                <best_book type="Book">
                    <id type="integer">9735</id>
                    <title>Ender's Game Boxed Set: Ender's Game, Ender's Shadow, Shadow of the Hegemon</title>
                    <author>
                        <id type="integer">589</id>
                        <name>Orson Scott Card</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">56991591</id>
                <books_count type="integer">5</books_count>
                <ratings_count type="integer">1330</ratings_count>
                <text_reviews_count type="integer">66</text_reviews_count>
                <original_publication_year type="integer">2017</original_publication_year>
                <original_publication_month type="integer">7</original_publication_month>
                <original_publication_day type="integer">7</original_publication_day>
                <average_rating>4.27</average_rating>
                <best_book type="Book">
                    <id type="integer">35623296</id>
                    <title>End Game</title>
                    <author>
                        <id type="integer">6422267</id>
                        <name>Charlie Gallagher</name>
                    </author>
                    <image_url>
                        https://s.gr-assets.com/assets/nophoto/book/111x148-bcc042a9c91a29c1d680899eff700a03.png
                    </image_url>
                    <small_image_url>
                        https://s.gr-assets.com/assets/nophoto/book/50x75-a91bf249278a81aabab721ef782c4a74.png
                    </small_image_url>
                </best_book>
            </work>

            <work>
                <id type="integer">71051396</id>
                <books_count type="integer">4</books_count>
                <ratings_count type="integer">334</ratings_count>
                <text_reviews_count type="integer">193</text_reviews_count>
                <original_publication_year type="integer">2020</original_publication_year>
                <original_publication_month type="integer">1</original_publication_month>
                <original_publication_day type="integer" nil="true" />
                <average_rating>4.24</average_rating>
                <best_book type="Book">
                    <id type="integer">46125032</id>
                    <title>End Game (Capital Intrigue, #1)</title>
                    <author>
                        <id type="integer">7913140</id>
                        <name>Rachel Dylan</name>
                    </author>
                    <image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1563377096l/46125032._SX98_.jpg
                    </image_url>
                    <small_image_url>
                        https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1563377096l/46125032._SY75_.jpg
                    </small_image_url>
                </best_book>
            </work>

        </results>
    </search>

</GoodreadsResponse>""">
