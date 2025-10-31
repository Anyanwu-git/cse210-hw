using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string name = PromptUserName();
        int favorite = PromptUserNumber();
        int squared = SquareNumber(favorite);

        DisplayResult(name, squared);
    }

    // 1) Welcome
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // 2) Get user's name (string)
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // 3) Get user's favorite number (int) with validation
    static int PromptUserNumber()
    {
        while (true)
        {
            Console.Write("Please enter your favorite number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Please enter a whole number (e.g., 7, 42).");
        }
    }

    // 4) Square the number (int -> int)
    static int SquareNumber(int n)
    {
        return n * n;
    }

    // 5) Show result
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}
