using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        // Sample data
        Activity run = new Running(new DateTime(2022, 11, 3), 30, 3.0);      // 3 miles in 30 min
        Activity bike = new Cycling(new DateTime(2022, 11, 3), 30, 6.0);      // 6 mph for 30 min
        Activity swim = new Swimming(new DateTime(2022, 11, 3), 30, 20);      // 20 laps

        activities.Add(run);
        activities.Add(bike);
        activities.Add(swim);

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
