using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcrastinatorBackend.Models;

public partial class User
{
    [Column("userid")]
    public int Userid { get; set; }

    [Column("firstname")]
    public string? Firstname { get; set; }
    [Column("lastname")]
    public string? Lastname { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("photourl")]
    public string? Photourl { get; set; }
    [Column("display")]
    public string? Display { get; set; }

    public virtual ICollection<Mealplanner> Mealplanners { get; set; } = new List<Mealplanner>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
