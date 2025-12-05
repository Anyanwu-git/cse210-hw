public class ChecklistGoal : Goal
{
    private int _target;
    private int _current;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points,
                         int target, int bonus, int current = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _current = current;
    }

    public override int RecordEvent()
    {
        _current++;
        int earned = Points;

        if (_current == _target)
        {
            earned += _bonus;
        }

        return earned;
    }

    public override bool IsComplete() => _current >= _target;

    public override string GetStatusString()
    {
        string box = IsComplete() ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description}) -- Completed {_current}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{_target}|{_bonus}|{_current}";
    }
}
