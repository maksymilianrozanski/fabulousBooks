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
    let private booksCollectionView searchPageModel openDetails openBrowser sortByRating dispatch =
        match searchPageModel.SearchResponse with
        | Some x ->
            View.CollectionView
                (items =
                    [ yield (Button.button "Sort by rating" sortByRating)
                      for b in x.BookItems do
                          yield dependsOn b (fun m b -> bookItemLayout (b, openDetails, openBrowser)) ], remainingItemsThreshold = 2,
                 remainingItemsThresholdReachedCommand =
                     (fun () ->
                         if (shouldFetchMoreItems x.End x.Total searchPageModel.Status) then
                             dispatch (Msg.MoreBooksRequested(searchPageModel.EnteredText, x.End))))
        | None -> Label.label "nothing here yet"

    let searchPageView searchPageModel openDetails openBrowser sortByRating dispatch =
        View.ContentPage
            (content =
                View.StackLayout
                    (padding = Thickness 8.0, verticalOptions = LayoutOptions.Start,
                     children =
                         [ View.Entry
                             (width = 200.0, placeholder = "Search",
                              completed = fun textArgs -> Msg.PerformSearch(textArgs, 1) |> dispatch)
                           Label.label searchPageModel.EnteredText
                           statusLayout (searchPageModel.Status)
                           booksCollectionView searchPageModel openDetails openBrowser sortByRating dispatch ]))
