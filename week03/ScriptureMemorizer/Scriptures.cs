using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _random = new Random();
        _words = new List<Word>();

        // Split on spaces. You can improve this by handling punctuation if you want.
        string[] parts = text.Split(' ');

        foreach (string part in parts)
        {
            _words.Add(new Word(part));
        }
    }

    public string GetDisplayText()
    {
        string referenceText = _reference.GetDisplayText();
        string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{referenceText}  {wordsText}";
    }

    public void HideRandomWords(int numberToHide)
    {
        // Get a list of indices of words that are not yet hidden
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                availableIndices.Add(i);
            }
        }

        // If there are no more visible words, just return
        if (availableIndices.Count == 0)
        {
            return;
        }

        // Hide up to numberToHide words (or as many as are left)
        int count = Math.Min(numberToHide, availableIndices.Count);

        for (int i = 0; i < count; i++)
        {
            // Pick a random index from the available list
            int randomListIndex = _random.Next(availableIndices.Count);
            int wordIndex = availableIndices[randomListIndex];

            _words[wordIndex].Hide();

            // Remove that index so we don't pick it again this round
            availableIndices.RemoveAt(randomListIndex);
        }
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}
