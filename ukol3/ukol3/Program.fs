let rec gcd a b =
    if a = 0 then b
    else gcd (b % a) a

type fraction =
    | Fraction of int * int

let simplify (Fraction(n, d)) =
    let gcd = gcd n d
    Fraction(n / gcd, d / gcd)

let addFrac (Fraction(a, b)) (Fraction(c, d)) = 
    simplify (Fraction(a * d + c * b, b * d))

let subFrac (Fraction(a, b)) (Fraction(c, d)) = 
    simplify (Fraction(a * d - c * b, b * d))

let mulFrac (Fraction(a, b)) (Fraction(c, d)) = 
    simplify (Fraction(a * c, b * d))

let divFrac (Fraction(a, b)) (Fraction(c, d)) = 
    simplify (Fraction(a * d, b * c))

let printFrac (Fraction(a, b)) =
    printfn "%i/%i" a b

let rec comb n k =
    if k > n then 0
    elif k = 0 || k = n then 1
    else comb (n - 1) (k - 1) + comb (n - 1) k


printfn"gcd 16, 32: %i"(gcd 16 32)

let frac1 = Fraction(1, 2)
let frac2 = Fraction(3, 4)
printfn"fractions:"
printFrac(frac1)
printFrac(frac2)

printfn "add: "
printFrac (addFrac frac1 frac2)
printfn "sub: "
printFrac (subFrac frac1 frac2)
printfn "mul: "
printFrac (mulFrac frac1 frac2)
printfn "div: "
printFrac (divFrac frac1 frac2)

printfn"comb 10, 8: %i" (comb 10 8)