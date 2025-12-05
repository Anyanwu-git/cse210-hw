// Eternal Quest Program - CSE 210
// Creativity / Exceeding Requirements:
// - Added a simple "Level" system based on total score (every 1000 points = new level).
// - Added motivational messages when user earns 500 or more points in a single event.
// - Clean menu structure and helper methods for readability.

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static List<Goal> _goals = new List<Goal>();
    private static int _score = 0;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Eternal Quest ===");
            Console.WriteLine();
            ShowScoreAndLevel();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateNewGoal();
                    break;

                case "2":
                    ListGoals();
                    break;

                case "3":
                    SaveGoals();
                    break;

                case "4":
                    LoadGoals();
                    break;

                case "5":
                    RecordEvent();
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("Thanks for playing Eternal Quest!");
    }

    // ----------------- Menu Actions -----------------

    private static void CreateNewGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string type = Console.ReadLine();
        Console.WriteLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (type == "3")
        {
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("What is the bonus for accomplishing it that many times? ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
        else
        {
            Console.WriteLine("Unknown goal type. No goal created.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Goal created successfully!");
    }

    private static void ListGoals()
    {
        Console.WriteLine("Your goals:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("  (No goals yet. Create one first.)");
            return;
        }

        int index = 1;
        foreach (Goal g in _goals)
        {
            Console.WriteLine($"{index}. {g.GetStatusString()}");
            index++;
        }
    }

    private static void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals yet. Create one first.");
            return;
        }

        Console.WriteLine("Which goal did you accomplish?");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].Name}");
        }

        Console.Write("Enter the number of the goal: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        Goal selected = _goals[choice - 1];
        int pointsEarned = selected.RecordEvent();

        _score += pointsEarned;

        Console.WriteLine();
        Console.WriteLine($"Congratulations! You earned {pointsEarned} points.");
        Console.WriteLine($"Your new total score is: {_score}");

        // Creativity: special motivational message for big events
        if (pointsEarned >= 500)
        {
            Console.WriteLine("🔥 Wow! That was a huge step on your Eternal Quest! 🔥");
        }
    }

    private static void SaveGoals()
    {
        Console.Write("Enter filename to save to (e.g., goals.txt): ");
        string filename = Console.ReadLine();

        using (StreamWriter output = new StreamWriter(filename))
        {
            // First line = score
            output.WriteLine(_score);

            // Then each goal as one line
            foreach (Goal g in _goals)
            {
                output.WriteLine(g.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals and score saved successfully.");
    }

    private static void LoadGoals()
    {
        Console.Write("Enter filename to load from (e.g., goals.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("File is empty.");
            return;
        }

        _goals.Clear();

        // First line is score
        _score = int.Parse(lines[0]);

        // The remaining lines are goals
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split('|');

            string type = parts[0];

            if (type == "SimpleGoal")
            {
                string name = parts[1];
                string desc = parts[2];
                int points = int.Parse(parts[3]);
                bool isComplete = bool.Parse(parts[4]);

                _goals.Add(new SimpleGoal(name, desc, points, isComplete));
            }
            else if (type == "EternalGoal")
            {
                string name = parts[1];
                string desc = parts[2];
                int points = int.Parse(parts[3]);

                _goals.Add(new EternalGoal(name, desc, points));
            }
            else if (type == "ChecklistGoal")
            {
                string name = parts[1];
                string desc = parts[2];
                int points = int.Parse(parts[3]);
                int target = int.Parse(parts[4]);
                int bonus = int.Parse(parts[5]);
                int current = int.Parse(parts[6]);

                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus, current));
            }
        }

        Console.WriteLine("Goals and score loaded successfully.");
    }

    // ----------------- Helper Methods -----------------

    private static void ShowScoreAndLevel()
    {
        Console.WriteLine($"Current Score: {_score}");

        int level = (_score / 1000) + 1; // every 1000 points = new level
        Console.WriteLine($"Current Level: {level}");
    }
}
