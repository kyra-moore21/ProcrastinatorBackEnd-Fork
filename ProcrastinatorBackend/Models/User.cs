using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProcrastinatorBackend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhotoUrl { get; set; }

    public string? Display { get; set; }

    [JsonIgnore]
    public virtual ICollection<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>();

    [JsonIgnore]

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
