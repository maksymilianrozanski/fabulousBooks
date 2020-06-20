namespace FabBooks

module PreferencesModule =

    open Xamarin.Essentials

    let private apiKey = "preferencesGoodreadsApiKey"

    let saveApiKey (key: string) =
        Preferences.Set(apiKey, key)
        key

    let getApiKey =
        match Preferences.Get(apiKey, "") with
        | "" -> Option.None
        | x -> Some(x)

    let deleteKey = Preferences.Remove(apiKey)
