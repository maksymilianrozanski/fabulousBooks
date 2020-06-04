namespace FabBooks

open Fabulous.XamarinForms
open Xamarin.Forms
open MainMessages

module StatusLayout =
    let statusLayout status =
        match status with
        | Success -> View.Label(text = "Success", textColor = Color.Green)
        | Failure -> View.Label(text = "Failed", textColor = Color.Red)
        | Loading -> View.Label(text = "Loading...", textColor = Color.Yellow)
