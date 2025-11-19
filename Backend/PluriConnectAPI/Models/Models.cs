namespace PluriConnectAPI.Models;

public class Child
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime Dob { get; set; }
}

public class Goal
{
    public int Id { get; set; }
    public int ChildId { get; set; }
    public string Description { get; set; } = "";
    public double TargetPercentage { get; set; }
}

public class Activity
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public string Description { get; set; } = "";
}

public class Progress
{
    public int Id { get; set; }
    public int ChildId { get; set; }
    public int GoalId { get; set; }
    public double Value { get; set; }
    public string? Notes { get; set; }   // Puede ser null
    public DateTime Date { get; set; }
}

public class GoalActivities
{
    public int Id { get; set; }
    public int GoalId { get; set; }
    public int ActivityId { get; set; }
}

