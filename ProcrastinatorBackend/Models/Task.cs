using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcrastinatorBackend.Models;

public partial class Task
{
    [Column("taskid")]
    public int Taskid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("task1")]
    public string? Task1 { get; set; }

    [Column("deadline")]
    public DateOnly? Deadline { get; set; }

    [Column("details")]
    public string? Details { get; set; }

    [Column("iscomplete")]
    public bool? Iscomplete { get; set; }

    [Column("created")]
    public DateOnly? Created { get; set; }

    [ForeignKey("Userid")]
    public virtual User User { get; set; } = null!;
}
