# F &#35; Type System

![logo](assets/logo.png)

---

## What is F &#35;

* Functional First
  * Immutable
  * Functions as a first class citizen
  * No nulls
* .Net
  * Can call into C#/VB
  * Can be called from C#/VB
  * Has access to the .Net Framework

---

## Types

* Tuples
* Records
* Discriminated Unions
* Units of Measure

---

but first...

## Type Signatures

+++

### Simple Function

```FSharp
let intToString (x:int) = x.ToString()
```

`int -> string`

+++

### Generic Function

```FSharp
let toString x = x.ToString()
```

`'a -> string`

+++

### Function with Two Parameters

```FSharp
let add x y = x + y

let sum = add 8 80
```

`int -> int -> int`

+++

### Currying

```FSharp
let add8 y = add 8 y
let add8'  = add 8

let sum = add8 80
```

`int -> int`

+++

### Functions as Parameters

```FSharp
let pred2String predicate x =
    if predicate x then
        "true"
    else
        "false"
```

`('a -> string) -> 'a -> string`

+++

### unit

```FSharp
let printDouble x =
    printf "%i" (x * 2)
```

`int -> unit`

+++

### Tuples

```FSharp
let add t =
    let (x, y) = t
    x + y

let add1 t =
    let (x, y) = t
    (x, y + "world", y.Length)
```

`int * int -> int`

`'a * string -> 'a * string * int`

---

## Tuples

* Pairs, tripples, etc of data
* _Multiplicative_ types

+++

<span class='menu-title slide-title'>Tuples</span>
```FSharp
let myTuple = ("apples", 5)
let (fruit, count) = myTuple

type FruitOrderTuple = string * float * int
let calculateTotal (order: FruitOrderTuple) =
  let (_, cost, count) = order
  cost * float count

let cost = calculateTotal ("apples", 0.35, 2)
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1">Constructing a tuple</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="2">Deconstructing the tuple</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="4">Giving a tuple a name</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="5">Using that name</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="6">Ignoring a value when deconstructing a tuple</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="5-9">Passing a tuple into a function</span>

+++

## Tuples Summary

* Commma seperated values
* Parens useful, but not required
* Order is important
* Do not need to define anything before hand

---

## Records

* Tuples with names
* Classes without behavior
* _Multiplicative_ types

+++

<span class='menu-title slide-title'>Records</span>
```
type Person = { firstName: string; lastName: string }
let mcFly = { firstName = "Marty"; lastName = "McFly" }

type FruitOrderRecord = {
  fruit: string
  price: float
  count: int
}

let calcTotal order =
  let { price = _; count = myCount } = order
  order.price * float(myCount)

let total = calcTotal { fruit = "apples"; price = 0.35; count = 2 }
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1">Defining a record in one line</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="2">Constructing a record</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="4-8">Defining a record on multiple lines (no more semicolons)</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="10-14">Passing a record into a function and deconstructing it</span>

+++

<span class='menu-title slide-title'>New Records From Old Records</span>
```
type Person = { firstName: string; lastName: string }

let marty = { firstName = "Marty"; lastName = "McFly" }
// val marty : Person = {firstName = "Marty"; lastName = "McFly";}

let george =  { marty with firstName = "George"}
// val george : Person = {firstName = "George";lastName = "McFly";}
```


+++

<span class='menu-title slide-title'>Records and Equality</span>
```
type Person = { firstName: string; lastName: string}
 
let marty = { firstName = "Marty"; lastName = "McFly" }
let marty' = { firstName = "Marty"; lastName = "McFly" } 
 
let equals = marty = marty'
// val equals : bool = true
let systemEquals = System.Object.Equals(marty, marty')
// val systemEquals : bool = true
let sameInstance = System.Object.ReferenceEquals(marty, marty')
// val sameInstance : bool = false
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1-4"></span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="6-7">Equals operator</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="8-9">Object.Equals</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="10-11">Object.ReferenceEquals</span>

+++

<span class='menu-title slide-title'>Records with Similar Fields</span>
```
open System
type Book = { title: string; pubDate: DateTime }
type Magazine = { title: string; pubDate: DateTime }

let mad = { title = "Mad"; pubDate = new DateTime(1952, 10, 1)}

let hitchiker = { Book.title = "The Hitchhiker's Guide to the Galaxy"
                  pubDate = new DateTime(1979, 10, 12) }
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="2-3">Different types with the same fields</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="5">Defaults to the most recent type defined</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="7-8">Be specific to get a different type</span>

+++

<span class='menu-title slide-title'>Equivelent Code in C#</span>
```
public sealed class Person : IEquatable<Person>, IStructuralEquatable, IComparable<Person>, IComparable, IStructuralComparable
{
	internal string _firstName;
	internal string _lastName;
	public string FirstName => _firstName;
	public string LastName => _lastName;

	public Person(string firstName, string lastName)
	{
		_firstName = FirstName;
		_lastName = LastName;
	}

	public override string ToString()
	{
		var format = new PrintfFormat<FSharpFunc<Person, string>, Unit, string, string, Person>("%+A");
		return ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Person, string>>(format).Invoke(this);
	}

	public sealed override int CompareTo(Person obj)
	{
		if (this != null)
		{
			if (obj == null) return 1;
			IComparer genericComparer = LanguagePrimitives.GenericComparer;
			int num = string.CompareOrdinal(_firstName, obj._firstName);
			if (num < 0) return num;
			if (num > 0) return num;
			IComparer genericComparer2 = LanguagePrimitives.GenericComparer;
			return string.CompareOrdinal(_lastName, obj._lastName);
		}
		else
		{
			if (obj != null)
			{
				return -1;
			}
			return 0;
		}
	}

	public sealed override int CompareTo(object obj)
	{
		return this.CompareTo((Person)obj);
	}

	public sealed override int CompareTo(object obj, IComparer comp)
	{
		return CompareTo(obj as Person);
	}

	public sealed override int GetHashCode(IEqualityComparer comp)
	{
		if (this != null)
		{
			int num = 0;
			int arg_35_0 = -1640531527;
			string text = _lastName;
			num = arg_35_0 + (((text == null) ? 0 : text.GetHashCode()) + ((num << 6) + (num >> 2)));
			int arg_65_0 = -1640531527;
			string text2 = _firstName;
			return arg_65_0 + (((text2 == null) ? 0 : text2.GetHashCode()) + ((num << 6) + (num >> 2)));
		}
		return 0;
	}

	public sealed override int GetHashCode()
	{
		return this.GetHashCode(LanguagePrimitives.GenericEqualityComparer);
	}

	public sealed override bool Equals(object obj, IEqualityComparer comp)
	{
		if (this == null) return obj == null;
		Person person = obj as Person;
		if (person != null)
		{
			return string.Equals(_firstName, person._firstName) && string.Equals(_lastName, person._lastName);
		}
		return false;
	}

	public sealed override bool Equals(Person obj)
	{
		if (this != null)
		{
			return obj != null && string.Equals(_firstName, obj._firstName) && string.Equals(_lastName, obj._lastName);
		}
		return obj == null;
	}

	public sealed override bool Equals(object obj)
	{
		Person person = obj as Person;
		return person != null && this.Equals(person);
	}
}
```


+++

## Records Summary

* Defined with `{ }`
* Uses semicolons
* Label order does not matter
* Similar to classes without methods
  * (You can add some if you _really_ want)

---

## Discriminated Unions

* Nothing really like it in C#
  * Kinda like enums with data
  * Kinda like abstract classes (but better)
* _Additive_ types
* Requires pattern matching

+++

<span class='menu-title slide-title'>Discriminated Unions</span>
```FSharp
type Shape =Circle of int| Rectangle of int * int | Square of int
let aCircle = Circle 3
let aRectangle = Rectangle (2, 3)

type Option<'a> = 
    | Some of 'a
    | None
 
type Person = { name: string; age: int}
type KitchenSink =
    | KewlDude of Person
    | Order of string * int
    | EmptyCase
    | Things of int list 
let sinkPerson = KewlDude { name= "Marty McFly"; age = 17 }
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1">One liner</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="2-3">Constructing a Result</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="5-7">Generics</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="9-15">Multiline with a variety of data</span>

+++

<span class='menu-title slide-title'>Pattern Matching</span>
```FSharp
let pi = System.Math.PI

type Shape =
    | Circle of float
    | Rectangle of float * float
    | Square of float

let area shape =
    match shape with
    | Circle radius -> radius * radius * pi
    | Rectangle (height, width) -> height * width
    | Square width -> width * width

let perimeter =
    function
    | Circle radius when radius > 0.0
                             -> 2.0 * radius * pi
    | Circle _               -> -1.0
    | Square 1.0             -> 4.0
    | Square width           -> width * 4.0
    | Rectangle (1.0, width) -> 2.0 + 2.0 * width
    | Rectangle (height, width)
                             -> (2.0 * height) + (2.0 * width)
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1-6">Define some types</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="8-12">Simple pattern matching</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="14-23">Complex patterns and the `function` keyword</span>

+++

### Pattern Matching

* Can be used with all types
* Can be used to check if something is a subtype (e.g., `:? int`)
* Must be exhaustive, the compiler will tell you if you've missed something
* The underscore (`_`) can be used to catch all remaining cases, but this is typically not a good idea

+++

<span class='menu-title slide-title'>Fizz Buzz with Pattern Matching</span>
```FSharp
let fizzBuzz max =
    let getFizzBuzz x = 
        match x % 3, x % 5 with  
        | 0, 0 -> "fizzbuzz"
        | _, 0 -> "fizz"
        | 0, _ -> "buzz"
        | _    -> x.ToString()
    [ 1 .. max ]
    |> Seq.map getFizzBuzz
    |> Seq.iter (printfn "%A")

fizzBuzz 16
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1">Define a function to perform FizzBuzz for a list of values</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="2-7">Define an inner function to calculate FizzBuzz for a single value</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="3">Create a tuple in the match definition</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="4">If x is divisible by both 3 and 5, both tuple values will be zero</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="5">If x is only divisible by 5, then only the second value is zero, and we don't care about the first</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="7">If we've gotten this far, then x is not divisible by 3 or 5, just return it as a string</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="8">Create a list from 1 to max</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="9">Loop over that list, passing each value to the getFizzBuzz function. This is like `list.Select(x => getFizzBuzz(x))`</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="10">Loop over the results from getFizzBuzz and print them out</span>

+++

### Option Type

* `null` is very rare in F#
  * Interacting with the Framework
  * Using `option` incorrectly (this is easy to avoid)
* Option is a generic discriminated union with two possible values of `Some T` or `None`
* Use pattern matching instead of null checks

+++

<span class='menu-title slide-title'>Option</span>
```FSharp
let someInt = Some 23
let noneInt = None

let printOption = function
    | Some x -> printfn "%A" x
    | None   -> printfn "Nothing"

printOption someInt
printOption noneInt
Some "Hello world" |> printOption

noneInt.Value * 2 |> printfn "%i"

noneInt 
|> Option.map (fun x -> x * 2)
|> printfn "%A"
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1-2">Creating an option</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="4-6">Pattern matching an option</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="8-10">Options are generic</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="12">Don't use `option.Value` it's the only way to get a `NullReferenceException` in F#</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="14-16">`option.Map`: A safe alternative to using `option.Value`</span>

---

## Units of Measure

In 1999 Lockhead Martin used

Imperial units of measure while NASA used metric.

A $125 million Mars orbiter crashed as a result.

You think you can do better?

+++

<span class='menu-title slide-title'>Units of Measure</span>
```FSharp
[<Measure>] type mile
[<Measure>] type km
[<Measure>] type hour
[<Measure>] type minute
[<Measure>] type second

let marathon = 26.2188<mile>
let halfMarathon = marathon / 2.0
// val halfMarathon : float<mile> = 13.1094
let marathon' = halfMarathon + halfMarathon
// val marathon' : float<mile> = 26.2188

let milesToKm m = m * 0.621371<km/mile>
let mphToMps h = h / 60.0<minute/hour> / 60.0<second/minute> 

let desiredSpeed = 88.0<mile/hour> 
                   |> mphToMps 
                   |> milesToKm
// val desiredSpeed : float<km/second> = 0.01518906889
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1-5">Define some units</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="7-11">Adding and multiplying with units</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="13-14">Define conversions</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="16-19">Applying conversions</span>

+++

## Units of Measure Summary

* Let the compiler keep track of your units for you
* [SI units are defined for you in Microsoft.FSharp.Data.UnitSystems.SI.UnitNames](https://msdn.microsoft.com/visualfsharpdocs/conceptual/si.unitnames-namespace-%5bfsharp%5d)

---

## Type Inference

* `var` on steroids
* Still strongly typed
* Generics are the default

+++

<span class='menu-title slide-title'>Type Inference</span>
```
let genericEquals x y = x = y
let intsEqual = genericEquals 1 2
let floatEqual = genericEquals 1.0 2.0
let stringEqual = genericEquals "Hello" "world"

let addInts x y = x + y
let anInt = addInts 1 2
// let aFloat = addInts 1.0 2.0 // Fails

let length (s:string) = s.Length
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1-4">Equals is generic</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="6-8">Numerics</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="10">Sometimes you have to tell the compiler what's going on</span>

+++

<span class='menu-title slide-title'>Type Inference</span>
```
type Result<'a> = Success of 'a | Failure 

let check f x =
    if f x then
        Success x
    else
        Failure

let speedPredicate speed = speed >= 88
let checkSpeed = check speedPredicate

let checkName name = check (fun n -> n <> "Biff") name
```


<span class="code-presenting-annotation fragment current-only" data-code-focus="1">Define a generic record for the example</span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="3-7">('a -> bool) -> 'a -> Result<'a></span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="3-10">int -> Result<int></span>
<span class="code-presenting-annotation fragment current-only" data-code-focus="3-12">string -> Result<string></span>

+++

## Type Inference Summary

* Specify types when it's ambigous
* Allow the compile to infer the rest of the types for you

---

## Resources

* [F# for fun and profit](https://fsharpforfunandprofit.com)
* [F# Cheat Sheet](http://dungpa.github.io/fsharp-cheatsheet/)
* [F# Organization](http://fsharp.org)
* [Don Syme's Blog](https://blogs.msdn.microsoft.com/dsyme/) (the guy who wrote F#)
