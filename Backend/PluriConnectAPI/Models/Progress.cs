using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

[Table("Progress")]
[Index("ChildId", Name = "IX_Progress_ChildId")]
[Index("GoalId", Name = "IX_Progress_GoalId")]
public partial class Progress
{
    [Key]
    public int Id { get; set; }

    public int ChildId { get; set; }

    public int GoalId { get; set; }

    public double Value { get; set; }

    public string? Notes { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey("ChildId")]
    [InverseProperty("Progresses")]
    public virtual Child Child { get; set; } = null!;

    [ForeignKey("GoalId")]
    [InverseProperty("Progresses")]
    public virtual Goal Goal { get; set; } = null!;
}
