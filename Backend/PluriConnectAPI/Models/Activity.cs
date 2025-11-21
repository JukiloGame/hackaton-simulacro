using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

public partial class Activity
{
    [Key]
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;
}
