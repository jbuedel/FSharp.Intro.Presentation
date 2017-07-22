let multiply x y = x * y
multiply 7.0 9.0
multiply 7 9

7.0 * 9.0
o


type Direction =
| ClockWise
| CounterClockwise


type Feature =
| Segment of float
| Angle of float
| Hem of Direction

let h :: rst = [7; 9; 8]