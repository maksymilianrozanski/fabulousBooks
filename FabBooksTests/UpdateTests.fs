module FabBooksTests.UpdateTests

open System
open System.Web
open FabBooks
open FabBooks.BookDetailsPage
open FabBooks.MainMessages
open NUnit.Framework
open App
open GoodreadsResponseModelModule

[<Test>]
let shouldUpdateTextAndStatus () =
    let initModel =
        { EnteredText = "Init text"
          Status = Status.Success
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }

    let queryText = "New text"

    let expected =
        { EnteredText = queryText
          Status = Status.Loading
          ResponseModel = emptyGoodreadsModel
          BookDetailsPageModel = None }, [ PerformSearchCmd queryText ]

    let result = App.update (Msg.PerformSearch queryText) (initModel)
    Assert.AreEqual(expected, result)
