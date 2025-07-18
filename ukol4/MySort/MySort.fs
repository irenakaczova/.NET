module MySort

let rec quickSortFun (l:int list) =
    match l with
    | [] -> []
    | pivot::rest ->
        let smaller = List.filter (fun x -> x < pivot) rest
        let larger = List.filter (fun x -> x >= pivot) rest
        quickSortFun smaller @ [pivot] @ quickSortFun larger


let quickSortImp (arr:int array) =
    let swap (i, j) = 
        let temp = arr.[i]
        arr.[i] <- arr.[j]
        arr.[j] <- temp

    let partition (low, high) =
        let pivot = arr.[high]
        let mutable i = low - 1
        for j = low to high - 1 do
            if arr.[j] <= pivot then
                i <- i + 1
                swap (i, j)
        swap (i + 1, high)
        i + 1

    let rec quickSortImpRec (low, high) =
        if low < high then
            let pi = partition (low, high)
            quickSortImpRec (low, pi - 1)
            quickSortImpRec (pi + 1, high)

    let n = arr.Length
    quickSortImpRec ( 0, n - 1)
    arr