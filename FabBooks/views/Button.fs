namespace FabBooks

open Fabulous.XamarinForms

module Button =
    let button text command =
        View.Button
            (text = text, command = command, backgroundColor = Colors.accentPrimaryLight, cornerRadius = 10,
             textColor = Colors.textPrimary)
