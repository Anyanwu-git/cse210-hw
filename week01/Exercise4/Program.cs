using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        List<int> numbers = new List<int>();

        while (true)
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int value))
            {
                Console.WriteLine("Please enter a whole number.");
                continue;
            }

            if (value == 0) break;

            numbers.Add(value);
        }

        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered.");
            return;
        }

        // Sum, Average, Max (core requirements)
        int sum = 0;
        int max = numbers[0];
        foreach (int n in numbers)
        {
            sum += n;
            if (n > max) max = n;
        }

        double average = (double)sum / numbers.Count;

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch: smallest positive
        int smallestPositive = int.MaxValue;
        bool foundPositive = false;
        foreach (int n in numbers)
        {
            if (n > 0 && n < smallestPositive)
            {
                smallestPositive = n;
                foundPositive = true;
            }
        }

        if (foundPositive)
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        else
            Console.WriteLine("There are no positive numbers.");

        // Stretch: sorted list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }
    }
}
