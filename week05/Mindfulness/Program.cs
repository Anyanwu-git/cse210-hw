using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
    // Base Activity class (Inheritance base)
    abstract class Activity
    {
        private string _name;
        private string _description;
        protected int _durationSeconds;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        // Template method called by Program
        public void Start()
        {
            Console.Clear();
            DisplayStartingMessage();
            _durationSeconds = GetDurationFromUser();
            Console.WriteLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);

            Console.Clear();
            RunActivity();  // Polymorphic call to derived class implementation

            DisplayEndingMessage();
            ActivityLog.AddEntry(_name, _durationSeconds);
        }

        // Each derived activity implements its own core behavior here
        protected abstract void RunActivity();

        protected void DisplayStartingMessage()
        {
            Console.WriteLine($"Welcome to the {_name}.");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
        }

        protected void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(2);
            Console.WriteLine();
            Console.WriteLine(
                $"You have completed the {_name} for {_durationSeconds} seconds.");
            ShowSpinner(3);
        }

        private int GetDurationFromUser()
        {
            while (true)
            {
                Console.Write("How long, in seconds, would you like your session? ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    return seconds;
                }

                Console.WriteLine("Please enter a positive whole number.");
            }
        }

        // Simple spinner animation
        protected void ShowSpinner(int seconds)
        {
            char[] sequence = { '|', '/', '-', '\\' };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int index = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write(sequence[index]);
                Thread.Sleep(150);
                Console.Write('\b');
                index = (index + 1) % sequence.Length;
            }
        }

        // Countdown animation (e.g., 3...2...1)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine();
        }
    }

    // -------------------------------------------------------------
    // Breathing Activity
    // -------------------------------------------------------------
    class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base(
                "Breathing Activity",
                "This activity will help you relax by guiding you through slow, " +
                "deep breathing. Clear your mind and focus on your breath.")
        {
        }

        protected override void RunActivity()
        {
            DateTime endTime = DateTime.Now.AddSeconds(_durationSeconds);

            while (DateTime.Now < endTime)
            {
                Console.Write("Breathe in... ");
                ShowCountdown(4);

                Console.Write("Now breathe out... ");
                ShowCountdown(6);

                Console.WriteLine();
            }
        }
    }

    // -------------------------------------------------------------
    // Reflection Activity
    // -------------------------------------------------------------
    class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "What did you learn about yourself from this experience?",
            "How did you feel when it was complete?",
            "What made this time different than other times?",
            "What is your favorite thing about this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private Random _random = new Random();

        public ReflectionActivity()
            : base(
                "Reflection Activity",
                "This activity will help you reflect on times when you have shown " +
                "strength and resilience. This will help you recognize the power " +
                "you have and how you can use it in other situations.")
        {
        }

        protected override void RunActivity()
        {
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine();

            string prompt = GetRandomPrompt();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();
            Console.Write("When you have something in mind, press Enter to continue. ");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions.");
            Console.WriteLine("You may begin in: ");
            ShowCountdown(5);
            Console.Clear();

            DateTime endTime = DateTime.Now.AddSeconds(_durationSeconds);

            while (DateTime.Now < endTime)
            {
                string question = GetRandomQuestion();
                Console.Write($"> {question} ");
                ShowSpinner(6);   // give them thinking time
                Console.WriteLine();
            }
        }

        private string GetRandomPrompt()
        {
            int index = _random.Next(_prompts.Count);
            return _prompts[index];
        }

        private string GetRandomQuestion()
        {
            int index = _random.Next(_questions.Count);
            return _questions[index];
        }
    }

    // -------------------------------------------------------------
    // Listing Activity
    // -------------------------------------------------------------
    class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private Random _random = new Random();

        public ListingActivity()
            : base(
                "Listing Activity",
                "This activity will help you reflect on the good things in your " +
                "life by having you list as many things as you can in a certain area.")
        {
        }

        protected override void RunActivity()
        {
            Console.WriteLine("List as many responses as you can to the following prompt:");
            Console.WriteLine();

            string prompt = GetRandomPrompt();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();

            Console.Write("You may begin in: ");
            ShowCountdown(5);

            int count = 0;
            DateTime endTime = DateTime.Now.AddSeconds(_durationSeconds);

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                // If user takes longer than remaining time, that's okay;
                // we'll count the response if they press Enter before the end.
                if (Console.KeyAvailable)
                {
                    // small trick to not block if nothing typed
                }

                string response = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(response))
                {
                    count++;
                }

                if (DateTime.Now >= endTime)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {count} item(s).");
        }

        private string GetRandomPrompt()
        {
            int index = _random.Next(_prompts.Count);
            return _prompts[index];
        }
    }

    // -------------------------------------------------------------
    // Creativity Extra: ActivityLog
    // Keeps a log of what the user has done during this run
    // -------------------------------------------------------------
    static class ActivityLog
    {
        private static List<string> _entries = new List<string>();

        public static void AddEntry(string activityName, int durationSeconds)
        {
            string entry =
                $"{DateTime.Now:HH:mm:ss} - {activityName} for {durationSeconds} seconds";
            _entries.Add(entry);
        }

        public static void ShowLog()
        {
            Console.Clear();
            Console.WriteLine("Activity Log");
            Console.WriteLine("------------");

            if (_entries.Count == 0)
            {
                Console.WriteLine("No activities have been completed yet.");
            }
            else
            {
                foreach (string entry in _entries)
                {
                    Console.WriteLine(entry);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }
    }

    // -------------------------------------------------------------
    // Program (Main Menu)
    // -------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Start Breathing Activity");
                Console.WriteLine("2. Start Reflection Activity");
                Console.WriteLine("3. Start Listing Activity");
                Console.WriteLine("4. View Activity Log (extra creativity)");
                Console.WriteLine("5. Quit");
                Console.WriteLine();

                Console.Write("Select a choice from the menu: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new BreathingActivity().Start();
                        break;

                    case "2":
                        new ReflectionActivity().Start();
                        break;

                    case "3":
                        new ListingActivity().Start();
                        break;

                    case "4":
                        ActivityLog.ShowLog();
                        break;

                    case "5":
                        Console.WriteLine("Goodbye! Thanks for using the Mindfulness Program.");
                        Thread.Sleep(1500);
                        return;

                    default:
                        Console.WriteLine("Please select a valid option (1–5).");
                        Thread.Sleep(1200);
                        break;
                }
            }
        }
    }
}
