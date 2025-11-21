using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

[Index("GoalId", "ActivityId", Name = "IX_GoalActivities_GoalId_ActivityId")]
public partial class GoalActivity
{
    [Key]
    public int Id { get; set; }

    public int GoalId { get; set; }

    public int ActivityId { get; set; }
}
