using System;

public class Running : Activity
{
    private double _distanceMiles;

    public Running(DateTime date, int lengthMinutes, double distanceMiles)
        : base(date, lengthMinutes)
    {
        _distanceMiles = distanceMiles;
    }

    protected override string GetActivityType()
    {
        return "Running";
    }

    public override double GetDistance()
    {
        return _distanceMiles;
    }

    public override double GetSpeed()
    {
        // Speed = distance / time * 60
        return (GetDistance() / LengthMinutes) * 60.0;
    }

    public override double GetPace()
    {
        // Pace = minutes per mile
        return LengthMinutes / GetDistance();
    }
}
