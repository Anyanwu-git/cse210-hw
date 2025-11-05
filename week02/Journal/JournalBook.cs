using System;
using System.Collections.Generic;
using System.IO;

public class JournalBook
{
    // Abstraction: internal list + methods to manipulate it
    private readonly List<Entry> _entries = new();

    public void Add(Entry e) => _entries.Add(e);

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet. Write your first one!");
            return;
        }

        foreach (var e in _entries)
        {
            e.Display();
        }
    }

    public void SaveToFile(string path)
    {
        using var writer = new StreamWriter(path);
        foreach (var e in _entries)
        {
            writer.WriteLine(e.ToStorageString());
        }
        Console.WriteLine($"Saved {_entries.Count} entries to: {path}");
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear();
        foreach (var line in File.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            _entries.Add(Entry.FromStorageString(line));
        }
        Console.WriteLine($"Loaded {_entries.Count} entries from: {path}");
    }
}
