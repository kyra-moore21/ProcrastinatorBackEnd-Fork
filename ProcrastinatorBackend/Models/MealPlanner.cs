using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcrastinatorBackend.Models;

public partial class Mealplanner
{
    [Column("mealid")]
    public int Mealid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }
    [Column("title")]
    public string? Title { get; set; }
    [Column("url")]
    public string? Url { get; set; }
    [Column("like")]
    public bool? Like { get; set; }
    [Column("iscompleted")]
    public bool? Iscompleted { get; set; }

    public virtual User User { get; set; } = null!;
}
