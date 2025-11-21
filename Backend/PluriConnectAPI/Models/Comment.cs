using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

[Index("ChildId", Name = "IX_Comments_ChildId")]
public partial class Comment
{
    [Key]
    public int Id { get; set; }

    public int ChildId { get; set; }

    public int ActivityId { get; set; }

    public string Text { get; set; } = null!;

    public string Date { get; set; } = null!;

    [ForeignKey("ChildId")]
    [InverseProperty("Comments")]
    public virtual Child Child { get; set; } = null!;
}
