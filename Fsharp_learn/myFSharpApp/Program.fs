// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

// Define a function to construct a message to print
let from whom = sprintf "from %s" whom


module BasicFunctions =
    let sampleFunction1 x = x * x + 3
    let result1 = sampleFunction1 4573
    printfn $"The result of squaring the integer 4573 and adding 3 is %d{result1}"
    let sampleFunction2 (x: int) = 2 * x * x - x / 5 + 3
    let result2 = sampleFunction2 (7 + 4)
    printfn $"The result of applying the 2nd sample function tp (7 + 4) is %d{result2}"

    let sampleFunction3 x =
        match x < 100.0 with
        | true -> 2.0 * x * x - x / 5.0 + 3.0
        | _ -> 2.0 * x * x + x / 5.0 - 37.0

    let result3 = sampleFunction3 (6.5 + 4.5)
    printfn $"Theresult of appliying the 3rd sample function to (6.5 + 4.5) is %f{result3}"

module Immutability =
    let number = 2
    let mutable otherNumber = 2
    printfn $"`otherNumber` is {otherNumber}"
    otherNumber <- otherNumber + 1
    printfn $"`otherNumber` changed to be {otherNumber}"

module StringManipulation =
    let string1 = "Hello"
    let string2 = "world"

    let string3 = @"C:\Program Files\"

    let string4 =
        """The computer said "hello world" when I told it to!"""

    let helloWorld = string1 + " " + string2
    printfn "%s" helloWorld
    let substring = helloWorld.[0..6]
    printfn $"{substring}"

module Tuples =
    let tuple1 = (1, 2, 3)
    let swapElems (a, b) = (b, a)
    printfn $"The result of swapping (1,2) is {(swapElems (1, 2))}"
    let tuple2 = (1, "fred", 3.1415)
    printfn $"tuple1: {tuple1}\ttuple2: {tuple2}"

    let sampleStructTuple = struct (1, 2)
    let convertFromStructTuple (struct (a, b)) = (a, b)
    let convertToStructTuple (a, b) = struct (a, b)

    printfn
        $"Struct Tuple: {sampleStructTuple}\nReference tuple made from the Struct Tuple: {
                                                                                              (sampleStructTuple
                                                                                               |> convertFromStructTuple)
        }"

module PipelineAndComposition =
    let square x = x * x
    let addOne x = x + 1
    let isOdd x = x % 2 <> 0
    let numbers = [ 1; 2; 3; 4; 5 ]

    let squareOddValuesAndAddOne values =
        let odds = List.filter isOdd values
        let squares = List.map square odds
        let result = List.map addOne squares
        result

    printfn $"processing {numbers} through 'squareOddValuesAndAddOne' produces: {squareOddValuesAndAddOne numbers}"

    let squareOddValuesAndAddOneNested values =
        List.map addOne (List.map square (List.filter isOdd values))

    printfn
        $"processing {numbers} through 'squareOddValuesAndAddOneNested' produces: {
                                                                                       squareOddValuesAndAddOneNested
                                                                                           numbers
        }"

    let squareOddValuesAndAddOnePipeline values =
        values
        |> List.filter isOdd
        |> List.map square
        |> List.map addOne

    printfn
        $"processing.{numbers} through 'squareOddValuesAndAddOnePipeline' produces: {
                                                                                         squareOddValuesAndAddOnePipeline
                                                                                             numbers
        }"

    let squareOddValuesAndAddOneShorterPipeline values =
        values
        |> List.filter isOdd
        |> List.map (fun x -> x |> square |> addOne)

    printfn
        $"processing {numbers} through 'squareOddValuesAndAddOneShorterPipeline' produces: {
                                                                                                squareOddValuesAndAddOneShorterPipeline
                                                                                                    numbers
        }"

    let squareOddValuesAndAddOneComposition =
        List.filter isOdd >> List.map (square >> addOne)

    printfn
        $"processing {numbers} through 'squareOddValuesAndAddOneComposition' produces: {
                                                                                            squareOddValuesAndAddOneComposition
                                                                                                numbers
        }"

module Lists =
    let list1 = []
    let list2 = [ 1; 2; 3 ]
    let list3 = [ 1; 2; 3 ]
    let numberList = [ 1 .. 1000 ]

    let daysList =
        [ for month in 1 .. 12 do
              for day in 1 .. System.DateTime.DaysInMonth(2017, month) do
                  yield System.DateTime(2017, month, day) ]

    printfn $"The first 5 days of 2017 are {daysList |> List.take 5}"

    let blackSquares =
        [ for i in 0 .. 7 do
              for j in 0 .. 7 do
                  match (i + j) % 2 = 1 with
                  | true -> yield (i, j)
                  | _ -> () ]

    let squares = numberList |> List.map (fun x -> x * x)

    let sumOfSquares =
        numberList
        |> List.filter (fun x -> x % 3 = 0)
        |> List.sumBy (fun x -> x * x)

    printfn $"The sum of the squares of numbers up to 1000 that are divisible by 3 is %d{sumOfSquares}"

module Arrays =
    let array1 = [||]

    let array2 =
        [| "hello"
           "world"
           "and"
           "hello"
           "world"
           "again" |]

    let array3 = [| 1 .. 1000 |]

    let array4 =
        [| for word in array2 do
               match word.Contains("l") with
               | true -> yield word
               | _ -> () |]

    let evenNumbers = Array.init 1001 (fun n -> n * 2)
    let evenNumbersSlice = evenNumbers.[0..500]

    for word in array4 do
        printfn $"word: {word}"

    array2.[1] <- "WORLD!"

    let sumOfLengthsOfWords =
        array2
        |> Array.filter (fun x -> x.StartsWith "h")
        |> Array.sumBy (fun x -> x.Length)

    printfn $"The sum of the lengths of the words in Array 2 is %d{sumOfLengthsOfWords}"

module Sequences =
    let seq1 = Seq.empty

    let seq2 =
        seq {
            yield "hello"
            yield "world"
            yield "and"
            yield "hello"
            yield "world"
            yield "again"
        }

    let numbersSeq = seq { 1 .. 1000 }

    let seq3 =
        seq {
            for word in seq2 do
                match word.Contains("l") with
                | true -> yield word
                | _ -> ()
        }

    let evenNumbers = Seq.init 1001 (fun n -> n * 2)
    let rnd = System.Random()

    let rec randomWalk x =
        seq {
            yield x
            yield! randomWalk (x + rnd.NextDouble() - 0.5)
        }

    let first100ValuesOfRandomWalk =
        randomWalk 5.0 |> Seq.truncate 100 |> Seq.toList

    printfn $"First 100 elements of a random walk: {first100ValuesOfRandomWalk}"

    let rec factorial n =
        match n with
        | 0 -> 1
        | _ -> n * factorial (n - 1)

    printfn $"Fatorial of 6 is: %d{factorial 6}"

    let rec greatestCommonFactor a b =
        match (a, b) with
        | (0, _) -> b
        | _ when a < b -> greatestCommonFactor a (b - a)
        | _ -> greatestCommonFactor (a - b) b

    printfn $"The Greatest Common Factor of 300 and 620 is %d{greatestCommonFactor 300 620}"

    let rec sumList xs =
        match xs with
        | [] -> 0
        | y :: ys -> y + sumList ys

    let rec private sumListTailRecHelper accumulator xs =
        match xs with
        | [] -> accumulator
        | y :: ys -> sumListTailRecHelper (accumulator + y) ys

    let sumListTailRecursive xs = sumListTailRecHelper 0 xs
    let oneThroughTen = [ 1; 2; 3; 4; 5; 6; 7; 8; 9; 10 ]
    printfn $"The sum 1-10 is %d{sumListTailRecursive oneThroughTen}"

module RecordTypes =
    type ContactCard =
        { Name: String
          Phone: string
          Verified: bool }

    let contact1 =
        { Name = "Alf"
          Phone = "(206) 555-0157"
          Verified = false }

    let contactOnSameLine =
        { Name = "Alf"
          Phone = "(206) 555-0157"
          Verified = false }

    let contact2 =
        { contact1 with
              Phone = "(206) 555-0112"
              Verified = true }

    let showContactCard (c: ContactCard) =
        c.Name
        + "Phone"
        + c.Phone
        + match c.Verified with
          | false -> " (unverified)"
          | _ -> ""

    printfn $"Alf's Contacts Card: {showContactCard contact1}"

    type ContactCardAlternate =
        { Name: string
          Phone: string
          Address: string
          Verified: bool }
        member this.PrintedContactCard =
            this.Name
            + " Phone: "
            + this.Phone
            + match this.Verified with
              | false -> " (unverified)"
              | _ -> ""
            + this.Address

    let contactAlternate =
        { Name = "Alf"
          Phone = "(206) 555-0157"
          Verified = false
          Address = "111 Alf Street" }

    printfn $"Alf's alternate contact card is {contactAlternate.PrintedContactCard}"

[<Struct>]
type ContactCardStruct =
    { Name: string
      Phone: string
      Verified: bool }

module DiscriminatedUnions =
    type Suit =
        | Hearts
        | Clubs
        | Diamonds
        | Spades

    type Rank =
        | Value of int
        | Ace
        | King
        | Queen
        | Jack
        static member GetAllRanks() =
            [ yield Ace
              for i in 2 .. 10 do
                  yield Value i
              yield Jack
              yield Queen
              yield King ]

    type Card = { Suit: Suit; Rank: Rank }

    let fullDeck =
        [ for suit in [ Hearts; Diamonds; Clubs; Spades ] do
              for rank in Rank.GetAllRanks() do
                  yield { Suit = suit; Rank = rank } ]

    let showPlayingCard (c: Card) =
        let rankString =
            match c.Rank with
            | Ace -> "Ace"
            | King -> "King"
            | Queen -> "Queen"
            | Jack -> "Jack"
            | Value n -> string n

        let suitString =
            match c.Suit with
            | Clubs -> "clubs"
            | Diamonds -> "diamonds"
            | Spades -> "spades"
            | Hearts -> "hearts"

        rankString + " of " + suitString

    let printAllCards () =
        for card in fullDeck do
            printfn $"{showPlayingCard card}"

type Address = Address of string
type Name = Name of string
type SSN = SSN of int

let address = Address "111 Alf Way"
let name = Name "Alf"
let ssn = SSN 1234567890

let unwrapAddress (Address a) = a
let unwrapName (Name n) = n
let unwrapSSN (SSN s) = s

printfn $"Address: {address |> unwrapAddress}, Name: {name |> unwrapName}, and SSN: {ssn |> unwrapSSN}"

type BST<'T> =
    | Empty
    | Node of value: 'T * left: BST<'T> * right: BST<'T>

let rec exists item bst =
    match bst with
    | Empty -> false
    | Node (x, left, right) ->
        match item with
        | i when i = x -> true
        | i when i < x -> (exists item left)
        | _ -> (exists item right)

let rec insert item bst =
    match bst with
    | Empty -> Node(item, Empty, Empty)
    | Node (x, left, right) as node ->
        match item with
        | i when i = x -> node
        | i when i < x -> Node(x, insert item left, right)
        | _ -> Node(x, left, insert item right)

[<Struct>]
type Shape =
    | Circle of radius: float
    | Square of side: float
    | Triangle of height: float * width: float

module PatternMatching =
    type Person = { First: string; Last: string }

    type Employee =
        | Engineer of engineer: Person
        | Manager of manager: Person * reports: List<Employee>
        | Executive of executive: Person * reports: List<Employee> * assistant: Employee

    let rec countReports (emp: Employee) =
        1
        + match emp with
          | Engineer (_) -> 0
          | Manager (_, reports) -> List.sumBy countReports reports
          | Executive (_, reports, assistant) ->
              (List.sumBy countReports reports)
              + countReports assistant

    let findDaveWithOpenPosition (emps: List<Employee>) =
        emps
        |> List.filter
            (function
            | Manager ({ First = "Dave" }, []) -> true
            | Executive ({ First = "Dave" }, [], _) -> true
            | _ -> false)

    let private parseHelper (f: string -> bool * 'T) =
        f
        >> function
        | (true, item) -> Some item
        | (false, _) -> None

    let parseDateTimeOffset = parseHelper DateTimeOffset.TryParse

    let result = parseDateTimeOffset "1970-01-01"

    match result with
    | Some _ -> printfn "It parsed!"
    | None -> printfn "It didn't parse!"

    let parseInt = parseHelper Int32.TryParse
    let parseDouble = parseHelper Double.TryParse
    let parseTimeSpan = parseHelper TimeSpan.TryParse

    let (|Int|_|) = parseInt
    let (|Double|_|) = parseDouble
    let (|Date|_|) = parseDateTimeOffset
    let (|TimeSpan|_|) = parseTimeSpan

    let printParseResult =
        function
        | Int x -> printfn $"%d{x}"
        | Double x -> printfn $"%f{x}"
        | Date d -> printfn $"%O{d}"
        | TimeSpan t -> printfn $"%O{t}"
        | _ -> printfn "Nothing was parse-able!"

    printParseResult "12"
    printParseResult "12.045"
    printParseResult "12/28/2016"
    printParseResult "9:01PM"
    printParseResult "banana!"

module OptionValues =
    type ZipCode = ZipCode of string
    type Customer = { ZipCode: ZipCode option }

    type IShippingCalculator =
        abstract GetState : ZipCode -> string option
        abstract GetShippingZone : string -> int

    let CustomerShippingZone (calculator: IShippingCalculator, customer: Customer) =
        customer.ZipCode
        |> Option.bind calculator.GetState
        |> Option.map calculator.GetShippingZone

module UnitsOfMeasure =
    open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
    let sampleValue1 = 1600.0<meter>

    [<Measure>]
    type mile =
        static member asMeter = 1609.34<meter/mile>

    let sampleValue2 = 500.0<mile>
    let sampleValue3 = sampleValue2 * mile.asMeter
    printfn $"After a %f{sampleValue1} race I would walk %f{sampleValue2} miles which would be %f{sampleValue3} meters"

module DefiningClasses =
    type Vector2D(dx: double, dy: double) =
        let length = sqrt (dx * dx + dy * dy)
        member this.DX = dx
        member this.DY = dy
        member this.Length = length
        member this.Scale(k) = Vector2D(k * this.DX, k * this.DY)

    let vector1 = Vector2D(3.0, 4.0)
    let vector2 = vector1.Scale(10.0)
    printfn $"Length of vector1: %f{vector1.Length}\nLength of vector2: %f{vector2.Length}"

module DefiningGenericClasses =
    type StateTracker<'T>(initialElement: 'T) =
        let mutable states = [ initialElement ]
        member this.UpdateState newState = states <- newState :: states
        member this.History = states
        member this.Current = states.Head

    let tracker = StateTracker 10
    tracker.UpdateState 17

module ImplementingInterfaces =
    type ReadFile() =
        let file = new System.IO.StreamReader("readme;txt")
        member this.ReadLine() = file.ReadLine()

        interface System.IDisposable with
            member this.Dispose() = file.Close()

    let interfaceImplementation =
        { new System.IDisposable with
            member this.Dispose() = printfn "disposed" }


[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    0 // return an integer exit code
