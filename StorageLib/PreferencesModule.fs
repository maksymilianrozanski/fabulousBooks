namespace FabBooks

open Xamarin.Essentials

module PreferencesModule =

    let private apiKey = "preferencesGoodreadsApiKey"

    let saveApiKey (key: string) =
        Preferences.Remove(apiKey)
        Preferences.Set(apiKey, key)
        key

    let getApiKey () =
        match Preferences.Get(apiKey, "") with
        | "" -> Option.None
        | x -> Some(x)

    let deleteKey () =
        Preferences.Remove(apiKey)
        true
