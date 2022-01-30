// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System

module working_with_types =
  type Person = { First: string; Last: string }
  let aPerson = { First = "Alex"; Last = "Adams" }
  let { First = first; Last = last } = aPerson
  let first2 = aPerson.First
  let last2 = aPerson.Last

  type OrderQuantity =
    | UnitQuantity of int
    | KiogramQuantity of decimal

  let anOrderQtyInUnits = UnitQuantity 10
  let anOrderQtyInKG = KiogramQuantity 2.5m

  let printQuantity aOrderQty =
    match aOrderQty with
    | UnitQuantity uQty -> printfn "%i units" uQty
    | KiogramQuantity kQty -> printfn "%g kg" kQty

module building_domain_model_by_types =
  type CheckNumber = | CheckNumber of int
  type CardNumber = | CardNumber of string

  type CardType =
    | Visa
    | Mastercard

  type CreditCardInfo = { CardType: CardType; CardNumber: CardNumber }

  type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CreditCardInfo

  type PaymentAmount = | PaymentAmount of decimal

  type Currency =
    | EUR
    | USD

  type Payment =
    { Amount: PaymentAmount
      Currency: Currency
      Method: PaymentMethod }

  // type PayInvoice = UnpaidInvoice -> Payment -> PaidInvoice
  type ConvertPaymentCurrency = Payment -> Currency -> Payment

module modeling_optional_values =
  open building_domain_model_by_types

  type PersonalName =
    { FirstName: string
      MiddleInitial: string option
      LastName: string }

  type UnpaidInvoice = | Unpaid
  type PaidInvoice = | Paid

  type PaymentError =
    | CardTypeNotRecognized
    | PaymentRejected
    | PaymentProviderOffline

  type PayInvoice = UnpaidInvoice -> Payment -> Result<PaidInvoice, PaymentError>


  // type SaveCustomer = Customer -> unit
  type NextRandom = unit -> int
  type OrderId = | OrderId of int
  type OrderLine = | OrderLine of string
  type Order = { OrderId: OrderId; Lines: OrderLine list }
  let aList = [ 1; 2; 3 ]
  let aNewList = 0 :: aList

  let printList1 aList =
    match aList with
    | [] -> printfn "list is empty"
    | [ x ] -> printfn "list has one element: %A" x
    | [ x; y ] -> printfn "list has two elements: %A and %A" x y
    | longerList -> printfn "list has more than two elements"

  let printList2 aList =
    match aList with
    | [] -> printfn "list is empty"
    | first :: rest -> printfn "list is non-empty with the first element being: %A first"



[<EntryPoint>]
let main argv = 0 // return an integer exit code
