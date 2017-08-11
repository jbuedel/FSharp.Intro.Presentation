### Introduction To F&num;
#### Functional Programming Series

---

### Why functional programming?


Learning about functional programming will make us better C family programmers. 

---

### Why F#?

F# is a pretty great language on it's own merits.

But I picked it for this series because:

* The core syntax, called ml, is easy to follow.
* Many of the features overlap with other languages.

---

### The `let` statement

```
let <name> <parms> = <expr> 
```

---
### The `let` statement

An example

```
let multiply x y = x * y
```

---
### The `let` statement
We can put it on one line but we usually don't.

```
let multiply x y = 
   x * y
```
---
### The `let` statement
Call it

```
multiply 7 3 // returns 21
```
Notice the lack of parens

---
### The `let` statement
Capturing the result

``` 
let s = multiply 7 3
```
+++

### The `let` statement
white lie

`let` statements actually look like this
```
let <name> <parms> = <expr> in <expr>
```

```
let multiply x y = x * y in multiply 7 3 
```
it's expressions all the way down

+++

### The `let` statement
we'd write that like so 

```
let multiply x y = 
  x * y 
multiply 7 3 
```
* the nesting is expressed via indentation
* `in` keyword is gone
* the last `<expr>` becomes the return value

F# lets us replace keywords with whitespace

---
### F# infers types

```
let multiply x y = // the type of x & y are both int
   x * y
multiply 7 3 
```

---
### F# infers types
Functions are implied generic when possible

```
let multiply x y = 
   x * y
multiply 7.6 8.0 // works
multiply 3 2 // and works
multiply "abc" "789" // compiler error 
```
@[3]
@[4]
@[5]
---
### Function signatures
```
let multiply x y = 
   x * y
multiply 7 3 
```

```
multiply: int -> int -> int
```
---
### Function signatures
```
let multiply x y = 
   x * y
multiply 7.2 3.0
```

```
multiply: float -> float -> float
```
---
Function calls
```
func1 arg1 arg2 ...
```
The first position is always the function.
Followed by it's arguments.
---
### Parens `()`

``` 
multiply a b   
multiply (a b) // not the same thing
``` 

Parens are used for precedence, as we'd expect

Same calls, in C
```C#
multiply(a,b);
multiply(a(b));
```
---
###Tuples

Commas declare tuples

```
(7, "bob")
```
In c#
```
new Tuple(7, "bob")
new Tuple<int,string>(7, "bob")
```

Parens are again just for precedence

---
###Tuples

Contrasting tuple construction with function calling

```
(foo, y)
```
Creating a tuple of two values.
```
(foo y)
```
A function call (with unnecessary parenthesis).

This one got me a lot.
---
###Tuples
```
(7, 7)
(10.5, "foo")
(multiply, (7, 9.9))
```

@[1](`int * int`)
@[2](`float * string`)
@[3](`(int -> int -> int) * (int * float)`)

+++
###Tuples

Contrasting tuple construction with function calling


```
(multiply, (7, 9.9))
(multiply (7, 9.9))
multiply (7, 9.9)
```
@[1](Constructing nested tuples)
@[2-3](Constructing a tuple and calling a function with it)
---
### Discriminated union

For starters you can think of these as `enum`s with data.

```
type Feature =
| Segment of float
| Angle of float
| Hem of bool
```
@[1]
@[2](the float is the length)
@[3](this float is the degrees)
@[4](`true for clockwise bend, `false` for counter clockwise)


(draw a profile here)
---
###The discriminated union

Let's improve it

```
type Direction =
| ClockWise
| CounterClockwise

type Feature =
| Segment of float
| Angle of float
| Hem of Direction
```
@[1-3]
@[8]
---
### Lists

A real work horse of functional programming

```
let l = [7; 9; 3]
> l: [7 9 3]
let m = 3 :: l
> m: [3 7 9 3]
let h :: rst = [7; 9; 8]
> h: 7
> rst: [9 8]
let z = [| a; b; c; |]
> z: [| a b c |]
d :: z
> error: ...
```
@[1-2](No commas)
@[3-4](Adding to the front of a list. Aka "cons".)
@[5-7](Taking it apart, aka "destructuring")
@[5-7](Wouldn't actually do it this way)
@[8-9](Array declaration)
@[10-11](Can't add an element to the front of an array)

---

### Match Expressions

Like a `switch`...but way better. 


```
let toPolyLine features = 
  let rec next features point direction = 
    match features with
    | []           -> []
    | Segment(len) -> ...
    | Angle(deg)   -> ...
    | Hem(dir)     -> ...

  next features (0.0, 0.0) 0.0
```
@[3](The match expression)
@[4-7](The cases)

So much better that it's not like a `switch` at all.

+++
### Match Expressions

(Go to the board: profile to polyline algorithm)

```
let toPolyLine features = 
  let rec next features point direction = 
    match features with
    | []                   -> []
    | Segment(len) :: rest -> 
        let p2 = translate point direction len
        let polyline = next rest p2 direction
        p2 :: polyline
    | Angle(deg)   :: rest -> 
        let dir' = direction + deg
        next rest point dir'
    | Hem(dir)     :: rest -> 
        let dir' = direction (if dir then (-) else (+)) 180
        next rest point dir'

  next features (0.0, 0.0) 0.0
```
<!--
@[3-11]
@[12]
@[5]
@[6]
@[7]
@[8-9]
@[10-11]
-->

<!-- `toPolyline: Feature list -> (float, float) -> float -> (float, float) list` -->

---
### Units of measure

Annotate built in types to prevent improper combination

```
[<Measure>] type inch
[<Measure>] type cm

let x = 1<inch>    // int
let y = 1.0<inch>  // float
let z = 1.0m<inch> // decimal 

let a = 3.0<cm>
let area = a * z // compiler error

```

+++
### Units of measure

Combine them 

```
[<Measure>] type m
[<Measure>] type sec
[<Measure>] type kg

let distance = 1.0<m>    
let time = 2.0<sec>    
let speed = 2.0<m/sec>    
let acceleration = 2.0<m/sec^2>    
let force = 5.0<kg m/sec^2>    
```

Note that is just convenient syntax (not expressions)

+++

### Units of measure

Derived units of measure

```
[<Measure>] type N = kg m/sec^2

let force1 = 5.0<kg m/sec^2>    
let force2 = 5.0<N>

force1 = force2 // true

```

These really are expressions

+++
### Units of measure

Adding units to our implementation

```
[<Measure>] type inch
[<Measure>] type degree
[<Measure>] type coord

type Feature =
| Segment of float<inch>
| Angle of float<degree>
| Hem of Direction

let toPolyLine features = 
  let rec next features point direction = 
    ...

  next features (0.0<coord>, 0.0<coord>) 0.0<degree>
```
@[1-3]
@[6-7]
@[8](could've used a unit of measure instead of a `Direction` type)
@[14]

---
### Begin part II

---
### Destructuring

We know `(7, "josh")` creates a tuple.

```
let foo tup =
  let num,str = tup
  str + " " + num
```
---
@[2](this is destructuring)

`t` is of type `int*string`

Call it like so
```
foo (7, "josh")
```

+++
### Destructuring

We can destructure inline, right in the parameter list.

```
let foo (num,str) =
  str + " " + num
```

The fact that we take a tuple is an implementation detail. It's still there. It just doesn't have a name anymore.

+++
### Destructuring

Destructuring a list

```
let foo lst =
  let fst :: rst = lst
  fst + " " + rst.Length.ToString()

foo ["josh"; "buedel", "f#"]
```  

With inline destructuring

```
let foo (fst :: rst) =
  fst + " " + rst.Length.ToString()
foo ["josh"; "buedel", "f#"]
```
Note this is a compliler warning. Because empty list.

Destructuring is pattern matching, and we haven't accounted for all possible patterns. 

---
### Records

Another built in data type.
```
type Person = {firstName: string; lastName: string; age: int}

let josh = {firstName = "Josh"; lastName = "Buedel"; age = 43}
```
@[3](No type specified! Compiler uses record labels to figure it out.)

Looks a lot like C# anonymous objects. 

+++
### Records

Get at the values
```
let josh = {firstName = "Josh"; lastName = "Buedel"; age = 43}

let name = josh.firstName
```

or by destructuring
```
let {firstName = name, age = age} = josh 
```
+++
### Records

Change a value
```
let josh = {firstName = "Josh"; lastName = "Buedel"; age = 43}

let gibson = { josh with firstName = "Gibson"}
```
### Records

Destructuring a record
```
let josh = {firstName = "Josh"; lastName = "Buedel"; age = 43}

let gibson = { josh with firstName = "Gibson"}
let ageDiff person1 person2 = 
  person1.age - person2.age
```

or

```
let ageDiff {age = age1;} {age = age2;} =
  age1 - age2

```

---
### F# Koans


Website: https://github.com/ChrisMarinos/FSharpKoans

* Clone this: `git@github.com:ChrisMarinos/FSharpKoans.git`
* Set `FSharpKoans` as start up project.
* `F5`

---

### Other features  

* match expressions in depth
* `|>` pipelining
* >> operator ?
* Partial application

---
