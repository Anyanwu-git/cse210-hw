using System;

public class Cycling : Activity
{
    private double _speedMph;

    public Cycling(DateTime date, int lengthMinutes, double speedMph)
        : base(date, lengthMinutes)
    {
        _speedMph = speedMph;
    }

    protected override string GetActivityType()
    {
        return "Cycling";
    }

    public override double GetDistance()
    {
        // Distance = speed * hours
        double hours = LengthMinutes / 60.0;
        return _speedMph * hours;
    }

    public override double GetSpeed()
    {
        return _speedMph;
    }

    public override double GetPace()
    {
        // Pace = minutes per mile
        return 60.0 / _speedMph;
    }
}
