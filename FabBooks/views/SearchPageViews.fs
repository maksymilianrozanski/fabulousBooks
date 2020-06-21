namespace FabBooks

open FabBooks.MainModel
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
                          yield dependsOn b (fun m b -> bookItemLayout (b, openDetails, openBrowser)) ],
                 remainingItemsThreshold = 2,
                 remainingItemsThresholdReachedCommand =
                     (fun () ->
                         if (shouldFetchMoreItems x.End x.Total searchPageModel.Status) then
                             dispatch (Msg.MoreBooksRequested(searchPageModel.EnteredText, x.End))))
        | None -> Label.label "nothing here yet"

    let searchPageView (model: Model) openDetails openBrowser sortByRating dispatch =
        let searchPageViewContent =
            View.StackLayout
                (padding = Thickness 8.0, verticalOptions = LayoutOptions.Start,
                 children =
                     [ View.Entry
                         (width = 200.0, placeholder = "Search",
                          completed = fun textArgs -> Msg.PerformSearch(textArgs, 1) |> dispatch)
                       Label.label model.SearchPageModel.EnteredText
                       statusLayout model.SearchPageModel.Status
                       //todo: should not display sort by rating when status failed and empty results
                       booksCollectionView model.SearchPageModel openDetails openBrowser sortByRating dispatch ])

        let apiKeyInput =
            View.StackLayout
                (children =
                    [ View.Entry
                        (width = 200.0, placeholder = "enter goodreads api key",
                         completed =
                             fun textArgs -> Msg.SaveGoodreadsKey(textArgs, PreferencesModule.saveApiKey) |> dispatch) ])

        View.ContentPage
            (content =
                match model.GoodreadsApiKey with
                | None -> apiKeyInput
                | Some x -> searchPageViewContent)
