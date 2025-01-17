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
    let private booksCollectionView searchPageModel dispatch =
        let sortByRatingButton =
            Button.button "Sort by rating" (fun () -> Msg.BookSortingRequested |> dispatch)

        match searchPageModel.SearchResponse with
        | Some x ->
            View.CollectionView
                (items =
                    [ yield (sortByRatingButton)
                      for b in x.BookItems do
                          yield dependsOn b (fun m b -> bookItemLayout b dispatch) ], remainingItemsThreshold = 2,
                 remainingItemsThresholdReachedCommand =
                     (fun () ->
                         if (shouldFetchMoreItems x.End x.Total searchPageModel.Status) then
                             Msg.MoreBooksRequested(searchPageModel.EnteredText, x.End) |> dispatch))
        | None -> Label.label "nothing here yet"

    let searchPageView (model: Model) dispatch =
        let searchPageViewContent =
            View.StackLayout
                (padding = Thickness 8.0, verticalOptions = LayoutOptions.Start,
                 children =
                     [ Entry.entry "Search" (fun textArgs -> Msg.SearchTextEntered(textArgs, 1) |> dispatch)
                       Label.label model.SearchPageModel.EnteredText
                       statusLayout model.SearchPageModel.Status
                       if (model.SearchPageModel.Status <> Status.Failure) then
                           booksCollectionView model.SearchPageModel dispatch ])

        let apiKeyInput =
            View.StackLayout
                (children =
                    [ Entry.entry "enter goodreads api key" (fun textArgs ->
                          Msg.SaveGoodreadsKey(textArgs, PreferencesModule.saveApiKey) |> dispatch) ])

        View.ContentPage
            (content =
                match model.GoodreadsApiKey with
                | None -> apiKeyInput
                | Some x -> searchPageViewContent)
