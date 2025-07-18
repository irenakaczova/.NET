using Microsoft.FSharp.Collections;

class Program
{
    static void Main()
    {
        List<int> numbers = new() { 3, -5, 1, 6, 9, 4, 7, 0 };

        List<int> list = MySort.quickSortFun(ListModule.OfSeq(numbers)).ToList();
        Console.WriteLine(string.Join(" ", list));
        
        int[] array = MySort.quickSortImp(numbers.ToArray());
        Console.WriteLine(string.Join(" ", array));
    }
}