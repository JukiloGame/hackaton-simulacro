using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

public partial class Child
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Dob { get; set; }

    public int? ActivitiesId { get; set; }

    [InverseProperty("Child")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Child")]
    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    [InverseProperty("Child")]
    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
