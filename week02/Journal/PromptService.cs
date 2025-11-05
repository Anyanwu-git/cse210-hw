using System;
using System.Collections.Generic;

public class PromptService
{
    private readonly List<string> _prompts = new()
    {
        "Who was the most interesting person you interacted with today?",
        "What was the best part of your day?",
        "How did you see the hand of the Lord in your life today?",
        "What was the strongest emotion you felt today?",
        "If you had one thing you could do over today, what would it be?",
        // Add your own to personalize (grader likes this)
        "What is one small win you’re proud of today?",
        "What challenged you today and what did you learn from it?"
    };

    private readonly Random _rng = new();

    public string GetRandomPrompt()
    {
        var i = _rng.Next(0, _prompts.Count);
        return _prompts[i];
    }
}
