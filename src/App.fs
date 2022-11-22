module App

module Model =

    type Model =
        { Value: int }

    type Msg =
        | Increment
        | Decrement

module State =

    open Elmish
    open Model

    let init () =
        let model = { Value = 0 }
        (model, Cmd.none)

    let update (msg: Msg) (model: Model) =
        match msg with
        | Increment ->
            let model' = { model with Value = model.Value + 1}
            (model', Cmd.none)
        | Decrement ->
            let model' = { model with Value = model.Value - 1}
            (model', Cmd.none)

module View =

    open Feliz
    open Feliz.Bulma
    open Feliz.UseElmish

    open Model

    [<ReactComponent>]
    let ViewComp () =

        let state, dispatch = React.useElmish (State.init, State.update, [||])

        [ Bulma.title "Example"
          Html.p (string state.Value)
          Bulma.button.button [ prop.text "+"; prop.onClick (fun _ -> Increment |> dispatch) ]
          Bulma.button.button [ prop.text "-"; prop.onClick (fun _ -> Decrement |> dispatch) ]
           ]
        |> Html.div

open Browser.Dom
open View

Feliz.ReactDOM.render (ViewComp, document.getElementById "elmish-app")
