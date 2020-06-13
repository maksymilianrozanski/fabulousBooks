namespace FabBooks

open FabBooks.Models
open Fabulous.XamarinForms
open Fabulous
open BookItemLayoutModule
open Utils
open Xamarin.Forms
open StatusLayout
open MainMessages

module SearchPageViews =
    let private booksCollectionView searchPageModel openDetails sortByRating dispatch =
        match searchPageModel.SearchResponse with
        | Some x ->
            View.CollectionView
                (items =
                    [ yield View.Button
                                (text = "Sort by rating", backgroundColor = Colors.accentPrimaryLight, cornerRadius = 10,
                                 command = sortByRating)
                      for b in x.BookItems do
                          yield dependsOn b (fun m b -> bookItemLayout (b, openDetails)) ], remainingItemsThreshold = 2,
                 remainingItemsThresholdReachedCommand =
                     (fun () ->
                         if (shouldFetchMoreItems x.End x.Total searchPageModel.Status) then
                             dispatch (Msg.MoreBooksRequested(searchPageModel.EnteredText, x.End))))
        | None -> View.Label("nothing here yet")

    let searchPageView searchPageModel openDetails sortByRating dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (padding = Thickness 8.0, verticalOptions = LayoutOptions.Start,
                     children =
                         [ View.Entry
                             (width = 200.0, placeholder = "Search",
                              completed = fun textArgs -> Msg.PerformSearch(textArgs, 1) |> dispatch)
                           View.Label(text = searchPageModel.EnteredText)
                           statusLayout (searchPageModel.Status)
                           booksCollectionView searchPageModel openDetails sortByRating dispatch ]))
