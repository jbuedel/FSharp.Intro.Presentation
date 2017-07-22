#### Introduction To F\#
##### Functional Programming Series

---

### Why functional programming?

Learning about functional programming will make us better C family programmers. 

---

### Why F#?

F# is a pretty great language on it's own merits.

But I picked it for this series because:

* Many of the features overlap with other languages.
* The core syntax, called ml, is easy to follow.
* We're already using F# in one project.

---

### The `let` statement

```
let <name> <parms> = <expr> 
```

---

Example

```
let square x = x * x
```

---
We can put it on one line but we usually don't.

```
let square x = 
   x * x
```
---
To call `square`...

```
square 7
```

---
Or to capture the result...

``` 
let s = square 7
```
---
F# can infer types

```
let square x = // the type of x is int
   x * x
square 7 
```

---
F# can infer types

```
let square x = 
   x * x
square "abc"  // compiler error 
```
---
Functions are implied generic when possible

```
let square x = 
   x * x
square 7.6 // works
```
---

Build up a feature list to polyline function as we go.

Next put in 
* function signatures syntax (int -> int)
* Tuples (and show them in function syntax (int * int -> int))
        Note (3,8) looks a lot like a parameter list doesn't it
* Discriminated unions 
    make a profile feature example, with tuples
* Match expressions
* Add in unit of measure
    * can't multiply an angle by a length anymore
* And if I can, make the function accept a list with a Segment head (and tail?)

