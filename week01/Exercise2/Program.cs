using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();

        if (!int.TryParse(answer, out int percent) || percent < 0 || percent > 100)
        {
            Console.WriteLine("Please enter a whole number from 0 to 100.");
            return;
        }

        string letter;
        if (percent >= 90) letter = "A";
        else if (percent >= 80) letter = "B";
        else if (percent >= 70) letter = "C";
        else if (percent >= 60) letter = "D";
        else letter = "F";

        // Plus/minus (no A+ > 100, and F has no sign)
        string sign = "";
        if (letter == "A")
        {
            if (percent >= 97) sign = "+";
            else if (percent < 93) sign = "-";
        }
        else if (letter != "F")
        {
            int last = percent % 10;
            if (last >= 7) sign = "+";
            else if (last <= 2) sign = "-";
        }

        Console.WriteLine($"Your grade is: {letter}{sign}");

        if (percent >= 70)
            Console.WriteLine("🎉 You passed—nice work!");
        else
            Console.WriteLine("📘 Keep going—you’ll get it next time!");
    }
}
