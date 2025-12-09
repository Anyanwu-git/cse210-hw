using System;

public abstract class Activity
{
    private DateTime _date;
    private int _lengthMinutes;

    protected Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    public DateTime Date
    {
        get { return _date; }
    }

    public int LengthMinutes
    {
        get { return _lengthMinutes; }
    }

    // Abstract methods for polymorphism
    public abstract double GetDistance(); // in miles
    public abstract double GetSpeed();    // in mph
    public abstract double GetPace();     // in min per mile

    // Virtual/abstract helper for the activity label
    protected abstract string GetActivityType();

    // Uses polymorphism – calls virtual/abstract methods
    public string GetSummary()
    {
        double distance = GetDistance();
        double speed = GetSpeed();
        double pace = GetPace();

        return $"{_date:dd MMM yyyy} {GetActivityType()} ({_lengthMinutes} min) - " +
               $"Distance {distance:F1} miles, Speed {speed:F1} mph, Pace: {pace:F1} min per mile";
    }
}
