using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProcrastinatorBackend.Models;

public partial class MealPlanner
{
    public int UserId { get; set; }

    public int MealId { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public bool? Like { get; set; }

    public bool? IsCompleted { get; set; }
   
    public virtual User User { get; set; } = null!;
}
