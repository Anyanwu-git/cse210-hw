using System;

public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    protected override string GetActivityType()
    {
        return "Swimming";
    }

    public override double GetDistance()
    {
        // Each lap = 50 meters
        // Convert to km, then to miles
        const double metersPerLap = 50.0;
        const double metersPerKilometer = 1000.0;
        const double kilometersToMiles = 0.62;

        double distanceKm = _laps * metersPerLap / metersPerKilometer;
        double distanceMiles = distanceKm * kilometersToMiles;

        return distanceMiles;
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
