namespace FabBooks

open Fabulous.XamarinForms
open Xamarin.Forms

module Label =
    let label text =
        View.Label(text = text, textColor = Colors.textPrimary)

    let labelHtml text =
        View.Label(text = text, textColor = Colors.textPrimary, textType = TextType.Html)
