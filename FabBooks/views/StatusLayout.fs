namespace FabBooks

open Fabulous.XamarinForms
open Xamarin.Forms
open MainMessages

module StatusLayout =
    let statusLayout status =
        match status with
        | Success -> View.Label(text = "Success", textColor = Colors.success)
        | Failure -> View.Label(text = "Failed", textColor = Colors.failure)
        | Loading -> View.Label(text = "Loading...", textColor = Colors.loading)
