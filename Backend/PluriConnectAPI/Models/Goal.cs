using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

[Index("ChildId", Name = "IX_Goals_ChildId")]
public partial class Goal
{
    [Key]
    public int Id { get; set; }

    public int ChildId { get; set; }

    public string Description { get; set; } = null!;

    public double TargetPercentage { get; set; }

    [ForeignKey("ChildId")]
    [InverseProperty("Goals")]
    public virtual Child Child { get; set; } = null!;

    [InverseProperty("Goal")]
    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
