using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        /*
         * EXCEEDING REQUIREMENTS / CREATIVITY:
         * 1. Program works with a small library of scriptures and chooses one at random.
         * 2. User can choose difficulty level (how many words to hide per step).
         * 3. Program only hides words that are not already hidden (stretch requirement).
         */

        Console.Clear();
        Console.WriteLine("=== Scripture Memorizer ===\n");

        // Build a small scripture library (you can add more)
        List<Scripture> scriptures = BuildScriptureLibrary();

        // Pick a random scripture
        Random random = new Random();
        int index = random.Next(scriptures.Count);
        Scripture scripture = scriptures[index];

        Console.WriteLine("A scripture has been chosen for you to memorize.\n");

        // Ask for difficulty (how many words to hide each time)
        int wordsToHideEachTime = GetDifficultyFromUser();

        string userInput = "";

        while (userInput.ToLower() != "quit" && !scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine("=== Scripture Memorizer ===\n");
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide more words, or type 'quit' to end.");
            Console.Write("> ");
            userInput = Console.ReadLine();

            if (userInput.ToLower() != "quit")
            {
                scripture.HideRandomWords(wordsToHideEachTime);
            }
        }

        // Final display with everything hidden (or user quit)
        Console.Clear();
        Console.WriteLine("=== Scripture Memorizer ===\n");
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();

        if (scripture.IsCompletelyHidden())
        {
            Console.WriteLine("All words are now hidden! Great job practicing!");
        }
        else
        {
            Console.WriteLine("Thanks for using the Scripture Memorizer.");
        }

        Console.WriteLine("\nPress any key to close...");
        Console.ReadKey();
    }

    // Build a small library of scriptures for extra creativity
    static List<Scripture> BuildScriptureLibrary()
    {
        var list = new List<Scripture>();

        // Single verse example
        Reference r1 = new Reference("John", 3, 16);
        string t1 = "For God so loved the world, that he gave his only begotten Son, " +
                    "that whosoever believeth in him should not perish, but have everlasting life.";
        list.Add(new Scripture(r1, t1));

        // Multi-verse example
        Reference r2 = new Reference("Proverbs", 3, 5, 6);
        string t2 = "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
                    "In all thy ways acknowledge him, and he shall direct thy paths.";
        list.Add(new Scripture(r2, t2));

        // Another single verse
        Reference r3 = new Reference("2 Nephi", 2, 25);
        string t3 = "Adam fell that men might be; and men are, that they might have joy.";
        list.Add(new Scripture(r3, t3));

        return list;
    }

    // Let user choose difficulty level
    static int GetDifficultyFromUser()
    {
        Console.WriteLine("Choose difficulty (how many new words to hide each time):");
        Console.WriteLine("1 - Easy (1 word)");
        Console.WriteLine("2 - Medium (2 words)");
        Console.WriteLine("3 - Hard (3 words)");
        Console.Write("> ");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                return 1;
            case "3":
                return 3;
            default:
                return 2; // medium by default
        }
    }
}
