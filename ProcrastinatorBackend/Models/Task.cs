using System;
using System.Collections.Generic;

namespace ProcrastinatorBackend.Models;

public partial class Task
{
    public int UserId { get; set; }

    public int TaskId { get; set; }

    public string? Task1 { get; set; }

    public DateOnly? Deadline { get; set; }

    public string? Details { get; set; }

    public bool? IsComplete { get; set; }

    public DateOnly? Created { get; set; }

    public virtual User User { get; set; } = null!;
}
