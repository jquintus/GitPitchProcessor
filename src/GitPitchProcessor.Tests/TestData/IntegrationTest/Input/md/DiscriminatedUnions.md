## Discriminated Unions

* Nothing really like it in C#
  * Kinda like enums with data
  * Kinda like abstract classes (but better)
* _Additive_ types
* Requires pattern matching

+++?code=src/DiscriminatedUnions/Creation.fs&lang=FSharp&title="Discriminated Unions"

@[1](One liner)
@[2-3](Constructing a Result)
@[5-7](Generics)
@[9-15](Multiline with a variety of data)

+++?code=src/DiscriminatedUnions/PatternMatching.fs&lang=FSharp&title="Pattern Matching"

@[1-6](Define some types)
@[8-12](Simple pattern matching)
@[14-23](Complex patterns and the `function` keyword)

+++

### Pattern Matching

* Can be used with all types
* Can be used to check if something is a subtype (e.g., `:? int`)
* Must be exhaustive, the compiler will tell you if you've missed something
* The underscore (`_`) can be used to catch all remaining cases, but this is typically not a good idea

+++?code=src/DiscriminatedUnions/FizzBuzz.fs&lang=FSharp&title="Fizz Buzz with Pattern Matching"

@[1](Define a function to perform FizzBuzz for a list of values)
@[2-7](Define an inner function to calculate FizzBuzz for a single value)
@[3](Create a tuple in the match definition)
@[4](If x is divisible by both 3 and 5, both tuple values will be zero)
@[5](If x is only divisible by 5, then only the second value is zero, and we don't care about the first)
@[7](If we've gotten this far, then x is not divisible by 3 or 5, just return it as a string)
@[8](Create a list from 1 to max)
@[9](Loop over that list, passing each value to the getFizzBuzz function. This is like `list.Select(x => getFizzBuzz(x))`)
@[10](Loop over the results from getFizzBuzz and print them out)

+++

### Option Type

* `null` is very rare in F#
  * Interacting with the Framework
  * Using `option` incorrectly (this is easy to avoid)
* Option is a generic discriminated union with two possible values of `Some T` or `None`
* Use pattern matching instead of null checks

+++?code=src/DiscriminatedUnions/Option.fs&lang=FSharp&title=Option

@[1-2](Creating an option)
@[4-6](Pattern matching an option)
@[8-10](Options are generic)
@[12](Don't use `option.Value` it's the only way to get a `NullReferenceException` in F#)
@[14-16](`option.Map`: A safe alternative to using `option.Value`)