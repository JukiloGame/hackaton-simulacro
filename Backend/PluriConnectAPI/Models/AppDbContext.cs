using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PluriConnectAPI.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity>? Activities { get; set; }

    public virtual DbSet<Child>? Children { get; set; }

    public virtual DbSet<Comment>? Comments { get; set; }

    public virtual DbSet<Goal>? Goals { get; set; }

    public virtual DbSet<GoalActivity>? GoalActivities { get; set; }

    public virtual DbSet<Progress>? Progresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlite("Data Source=Data/pluriconnect.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
