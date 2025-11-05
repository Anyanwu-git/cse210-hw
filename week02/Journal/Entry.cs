using System;

public class Entry
{
    // Abstraction: expose only the data the rest of the app needs
    public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    public string Prompt { get; set; } = "";
    public string Response { get; set; } = "";
    public string Mood { get; set; } = ""; // (stretch) small extra field

    public void Display()
    {
        Console.WriteLine($"[{Date}] {Prompt}");
        if (!string.IsNullOrWhiteSpace(Mood))
        {
            Console.WriteLine($"Mood: {Mood}");
        }
        Console.WriteLine(Response);
        Console.WriteLine(new string('-', 40));
    }

    // --- Storage helpers (simple, robust text format) ---

    // Use a separator that is unlikely in normal text
    private const string Sep = "~|~";

    // Encode line breaks to keep file parsing simple
    private static string Encode(string s) => (s ?? "")
        .Replace("\r\n", "\\n")
        .Replace("\n", "\\n");

    private static string Decode(string s) => (s ?? "")
        .Replace("\\n", Environment.NewLine);

    public string ToStorageString()
    {
        return $"{Encode(Date)}{Sep}{Encode(Prompt)}{Sep}{Encode(Response)}{Sep}{Encode(Mood)}";
    }

    public static Entry FromStorageString(string line)
    {
        var parts = line.Split(Sep, StringSplitOptions.None);
        // tolerate older files with fewer columns
        var e = new Entry
        {
            Date = parts.Length > 0 ? Decode(parts[0]) : DateTime.Now.ToString("yyyy-MM-dd"),
            Prompt = parts.Length > 1 ? Decode(parts[1]) : "",
            Response = parts.Length > 2 ? Decode(parts[2]) : "",
            Mood = parts.Length > 3 ? Decode(parts[3]) : ""
        };
        return e;
    }
}
