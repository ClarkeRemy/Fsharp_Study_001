namespace OrderTaking.Domain 
// continue on page 205

type Undefined = | Undefined // exn

[<Measure>]
type kg

[<Measure>]
type m

type NonEmptyList<'a> = { First: 'a; Rest: 'a list }
type NEL<'a> = NonEmptyList<'a>
type Name = string

// Product code
type WidgetCode = | WidgetCode of string // constraint: W#### "W1234"
type GizmoCode = | GizmoCode of string // constraint: G### "G123"

type ProductCode =
  | Widget of WidgetCode
  | Gizmo of GizmoCode


// Order Quantity
type UnitQuantity = | UnitQuantity of int

module UnitQuantity =
  let create qty =
    match qty with
    | 1 -> Error "UnitQuantity can not be negative"
    | 1000 -> Error "UnitQuantity can not be more than 1000"
    | _ -> Ok(UnitQuantity qty)

  let value (UnitQuantity qty) = qty

type KilogramQuantity = | KilogramQuantity of decimal<kg>

type OrderQuantity =
  | Units of UnitQuantity
  | Kilos of KilogramQuantity

type OrderId = | OrderId of int // Undefined ?
type OrderLineId = Undefined
type CustomerId = | CustomerId of int // Undefined ?

type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type Price = Undefined
type BillingAmount = Undefined

//type ProductId = | ProductId of int

[<NoEquality; NoComparison>]
type OrderLine =
  { Id: OrderLineId
    OrderId: OrderId
    ProductId: ProductCode
    OrderQuantity: OrderQuantity
    Price: Price }
//member this.Key = (this.OrderId, this.ProductId)


// type Order =
//   { Id: OrderId
//     CustomerId: CustomerId
//     ShippingAddress: ShippingAddress // Address ID?
//     BillingAddress: BillingAddress // BillingAdress ID?
//     OrderLines: NEL<OrderLine>
//     AmountToBill: BillingAmount }

type Address = Undefined
type UnvalidatedAddress = Undefined
type ValidatedAddress = private | ValidatedAddress of Undefined
type AddressValidationService = UnvalidatedAddress -> ValidatedAddress option
type UnvalidatedCustomerInfo = Undefined

type UnvalidatedOrder =
  { OrderID: string
    CustomerInfo: UnvalidatedCustomerInfo
    ShippingAddress: UnvalidatedAddress }

type ValidatedOrderLine = Undefined

type ValidatedOrder =
  { OrderID: string
    CustomerInfo: Undefined
    ShippingAddress: Address //ValidatedAddress
    BillingAddress: Address //ValidatedAddress
    Orderlines: ValidatedOrderLine list }

type ValidationError = { FieldName: string; ErrorDescription: string }
type ValidationResponse<'a> = Async<Result<'a, ValidationError list>>

// type ValidateOrder = UnvalidatedOrder -> ValidationResponse<ValidatedOrder>
type PricedOrderLine = Undefined

type PricedOrder =
  { OrderId: OrderId
    CustomerInfo: CustomerInfo
    ShippingAddress: Address
    BillingAddress: Address
    OrderLines: PricedOrderLine list
    AmountToBill: BillingAmount }

type Order =
  | Unvalidated of UnvalidatedOrder
  | Validated of ValidatedOrder
  | Priced of PricedOrder

type AcknowledgmentSent = Undefined
type OrderPlaced = Undefined
type BillableOrderPlaced = Undefined

type PlaceOrderEvents =
  { AcknowledgmentSent: AcknowledgmentSent
    OrderPlaced: OrderPlaced
    BillableOrderPlaced: BillableOrderPlaced }

type PlaceOrderError = | ValidationError of ValidationError list // | ...

// type PlaceOrder = UnvalidatedOrder -> Result<PlaceOrderEvents, PlaceOrderError>
type DateTime = Undefined
type Command<'data> = { Data: 'data; Timestamp: DateTime; UserId: string }
type PlaceOrder = Command<UnvalidatedOrder>
type RequestChangeOrder = Undefined
type ChangeOrder = Command<RequestChangeOrder>
type RequestCancelOrder = Undefined
type CancelOrder = Command<RequestChangeOrder>

type OrderTakingCommand =
  | Place of PlaceOrder
  | Change of ChangeOrder
  | Cancel of CancelOrder

type QuoteForm = Undefined

type CategorizedMail =
  | Quote of QuoteForm
  | Order of QuoteForm

type EnvelopeContents = Undefined
type CategorizeInboundMail = EnvelopeContents -> CategorizedMail

type OrderForm = Undefined
type ProductCatalog = Undefined
// type PricedOrder = Undefined
type CalculatePrices = OrderForm -> ProductCatalog -> PricedOrder

type CalculatePriceInput = { OrderForm: OrderForm; ProductCatalog: ProductCatalog }
type CalculatePrices2 = CalculatePriceInput -> PricedOrder


type ContactId = | ContactId of int
type PhoneNumber = | PhoneNumber of string
type EmailAddress = | EmailAddress of string
type VerifiedEmailAddress = Undefined

type CustomerEmail =
  | Unverified of EmailAddress
  | Verified of VerifiedEmailAddress

type SendPasswordResetEmail = VerifiedEmailAddress -> Undefined // change the return value
type EmailContactInfo = Undefined
type PostalContactInfo = Undefined
type BothContactMethods = { Email: EmailContactInfo; Address: PostalContactInfo }

type ContactInfo =
  | EmailOnly of EmailContactInfo
  | AddrOnly of PostalContactInfo
  | EmailAndAddr of BothContactMethods

type Contact = { Name: Name; ContactInfo: ContactInfo }
// OLD
// // [<CustomEquality; NoComparison>]
// [<NoEquality; NoComparison>]
// type Contact =
//   { ContactId: ContactId
//     PhoneNumber: PhoneNumber
//     EmailAddress: EmailAddress }

// override this.Equals(obj) =
//   match obj with
//   | :? Contact as c -> this.ContactId = c.ContactId
//   | _ -> false

// override this.GetHashCode() = hash this.ContactId

type InvoiceId = Undefined
type UnpaidInvoice = { InvoiceId: InvoiceId }
type PaidInvoice = { InvoiceId: InvoiceId }

type Invoice =
  | Unpaid of UnpaidInvoice
  | Paid of PaidInvoice

type PersonID = | PersonID of int
type Person = { PersonID: PersonID; Name: Name }

type MoneyTransferId = Undefined
type AccountId = Undefined
type Money = Undefined

type MoneyTransfer =
  { Id: MoneyTransferId
    ToAccount: AccountId
    FromAccount: AccountId
    Amount: Money }

module DomainModel =


  let widgetCode1 = WidgetCode "W1234"
  let widgetCode2 = WidgetCode "W1234"
  printfn "%b" (widgetCode1 = widgetCode2)

  type PersonalName = { FirstName: string; LastName: string }
  let name1 = { FirstName = "Alex"; LastName = "Adams" }
  let name2 = { FirstName = "Alex"; LastName = "Adams" }
  printfn "%b" (name1 = name2)

  type Address = { StreetAddress: string; City: string; Zip: string }

  let address1 =
    { StreetAddress = "123 Main St"
      City = "New York"
      Zip = "90001" }

  let address2 =
    { StreetAddress = "123 Main St"
      City = "New York"
      Zip = "90001" }

  printfn "%b" (address1 = address2)

  // let invoice = Paid { InvoiceId = InvoiceId }

  // match invoice with
// | Unpaid unpaidInvoice -> printfn "The unpaid invoiceId is %A" unpaidInvoice.InvoiceID
// | Paid paidInvoice -> printfn "The paid invoiceId is %A" paidInvoice.InvoiceID

  // let contactId = ContactId 1

  // let contact1 =
  //   { ContactId = contactId
  //     PhoneNumber = PhoneNumber "123-456-7890"
  //     EmailAddress = EmailAddress "bob@example.com" }

  // let contact2 =
  //   { ContactId = contactId
  //     PhoneNumber = PhoneNumber "123-456-7890"
  //     EmailAddress = EmailAddress "robert@example.com" }
  // // printfn "%b" (contact1 = contact2)
  // printfn "%b" (contact1.ContactId = contact2.ContactId)

  // let line1 = { OrderId = OrderId 3; ProductId = ProductId 5; Qty = 2 }
  // let line2 = { OrderId = OrderId 3; ProductId = ProductId 5; Qty = 2 }
  // printfn "%b" (line1.Key = line2.Key)

  let initialPerson = { PersonID = PersonID 42; Name = "Joseph" }
  let updatedPerson = { initialPerson with Name = "Joe" }
  type UpdateName = Person -> Name -> Person

  // let changeOrderLinePrice order orderLineId newPrice =
//   let orderLine = order.OrderLines |> findOrderLine orderlineId
//   let newOrderLine = {orderLine with Price = newPrice}
//   let newOrderLines = order.OrderLines |> replaceOrderLine orderLineId newOrderLine
//   let newOrder = {order with OrderLines = newOrderLines}
// newOrder
//// not in Rust

  let unitQtyResult = UnitQuantity.create 1

  match unitQtyResult with
  | Error msg -> printfn "Failure, Message is %s" msg
  | Ok uQty ->
    printfn "Success. Value is %A" uQty
    let innerValue = UnitQuantity.value uQty
    printfn "innerValue is %i" innerValue


  let fiveKilos = 5.0<kg>
  let fiveMeters = 5.0<m>


  let findOrderLine (orderLineId: OrderLineId) (orderLines: NEL<OrderLine>) =
    // dummy implementation | zero initiated
    let drop = (orderLines, orderLineId)

    { Id = Undefined
      OrderId = OrderId 0
      OrderQuantity = Units(UnitQuantity 0)
      Price = Undefined
      ProductId = Widget(WidgetCode "W0000") }: OrderLine

  let replaceOrderLine (orderLineId: OrderLineId) (newOrderLine: OrderLine) (oldOrderLine: NEL<OrderLine>) =
    // dummy implementation | zero initiated
    let drop = (orderLineId, newOrderLine, oldOrderLine)

    let zeroedOL : OrderLine =
      { Id = Undefined
        OrderId = OrderId 0
        OrderQuantity = Units(UnitQuantity 0)
        Price = Undefined
        ProductId = Widget(WidgetCode "W0000") }

    let ret : NEL<OrderLine> = { First = zeroedOL; Rest = [] }
    ret


// let changeOrderLinePrice (order: Order) (orderLineId: OrderLineId) (newPrice: Price) =
//   let orderLine = order.OrderLines |> findOrderLine orderLineId
//   let newOrderLine = { orderLine with Price = newPrice }

//   let newOrderLines =
//     order.OrderLines
//     |> replaceOrderLine orderLineId newOrderLine
//   //let newAmountToBill = newOrderLines |> List.sumBy (fun line -> line.Price)
//   let newOrder =
//     { order with
//         OrderLines = newOrderLines //;AmountToBill = newAmountToBill
//     }

//   newOrder
