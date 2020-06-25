namespace FabBooks

open Fabulous.XamarinForms
open Xamarin.Forms

module Entry =
    let entry placeholder completed =
        View.Entry(width = 200.0, placeholder = placeholder, completed = completed)
            .ClearButtonVisibility(ClearButtonVisibility.WhileEditing)
