namespace FabBooks

[<AutoOpen>]
module Either =

    type Either<'a, 'b> =
        | Left of 'a
        | Right of 'b

    let isLeft =
        function
        | Left _ -> true
        | _ -> false

    let isRight =
        function
        | Right _ -> true
        | _ -> false

    let bind f (either: Either<'a, 'b>) =
        match either with
        | Right x -> f (x)
        | Left -> either
