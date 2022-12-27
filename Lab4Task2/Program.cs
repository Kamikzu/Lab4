using DynamicArrayClass;
using System;

internal class Program
{
    private static void Main()
    {
        
        int[] array = { 1, 2, 3, 4, 5 };
        DynamicArray<int> obj = new(array);
        DynamicArray<int> temp = new(array);
        obj.AddRange(temp);

        if (obj.Equals(temp))


        {
            Console.WriteLine("Вау");
        }
        else Console.WriteLine("Нет");


        


    }
}