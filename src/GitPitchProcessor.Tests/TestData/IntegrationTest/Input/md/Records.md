## Records

* Tuples with names
* Classes without behavior
* _Multiplicative_ types

+++?code=src/Records/Creation.fs&lang-FSharp&title=Records

@[1](Defining a record in one line)
@[2](Constructing a record)
@[4-8](Defining a record on multiple lines (no more semicolons))
@[10-14](Passing a record into a function and deconstructing it)

+++?code=src/Records/With.fs&lang-FSharp&title="New Records From Old Records"

+++?code=src/Records/StructuralEquality.fs&lang-FSharp&title="Records and Equality"

@[1-4]
@[6-7](Equals operator)
@[8-9](Object.Equals)
@[10-11](Object.ReferenceEquals)

+++?code=src/Records/ConflictingTypes.fs&lang-FSharp&title="Records with Similar Fields"

@[2-3](Different types with the same fields)
@[5](Defaults to the most recent type defined)
@[7-8](Be specific to get a different type)

+++?code=src/Records/Person.cs&lang-CSharp&title="Equivelent Code in C#"

+++

## Records Summary

* Defined with `{ }`
* Uses semicolons
* Label order does not matter
* Similar to classes without methods
  * (You can add some if you _really_ want)