using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🎯 Guess My Number (1–100)");

        // Created Random once so we get good randomness across games
        Random rng = new Random();

        // Play again loop
        do
        {
            PlayOneGame(rng);

            Console.Write("\nPlay again? (yes/no): ");
        }
        while (Console.ReadLine()?.Trim().ToLower() == "yes");
    }

    static void PlayOneGame(Random rng)
    {
        // CORE: generate magic number (1..100)
        int magic = rng.Next(1, 101);

        // If you want to do the earlier step where the user chooses the magic number, uncomment:
        // Console.Write("What is the magic number? ");
        // while (!int.TryParse(Console.ReadLine(), out magic)) Console.Write("Please enter a valid whole number: ");

        int guesses = 0;
        int guess;

        // Loop until correct
        while (true)
        {
            Console.Write("What is your guess? ");

            // Validate input
            if (!int.TryParse(Console.ReadLine(), out guess))
            {
                Console.WriteLine("Please enter a whole number.");
                continue;
            }

            guesses++;

            if (guess < magic)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magic)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine($"You guessed it in {guesses} guess{(guesses == 1 ? "" : "es")}! ✅");
                break;
            }
        }
    }
}
