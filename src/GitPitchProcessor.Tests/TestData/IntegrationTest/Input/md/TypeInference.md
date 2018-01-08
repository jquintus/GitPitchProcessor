## Type Inference

* `var` on steroids
* Still strongly typed
* Generics are the default

+++?code=src/TypeInference/Basic.fs&lang-FSharp&title="Type Inference"

@[1-4](Equals is generic)
@[6-8](Numerics)
@[10](Sometimes you have to tell the compiler what's going on)

+++?code=src/TypeInference/Generic.fs&lang-FSharp&title="Type Inference"

@[1](Define a generic record for the example)
@[3-7](('a -> bool) -> 'a -> Result<'a>)
@[3-10](int -> Result<int>)
@[3-12](string -> Result<string>)

+++

## Type Inference Summary

* Specify types when it's ambigous
* Allow the compile to infer the rest of the types for you