using Microsoft.EntityFrameworkCore;
using PluriConnectAPI.Models;

namespace PluriConnectAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Child> Children => Set<Child>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Progress> Progress => Set<Progress>();
    public DbSet<GoalActivities> GoalActivities => Set<GoalActivities>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Child - Comments 1:N
        modelBuilder.Entity<Child>()
            .HasMany(c => c.Comments)
            .WithOne()
            .HasForeignKey(c => c.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        // Goal - Child (N:1)
        modelBuilder.Entity<Goal>()
            .HasOne<Child>()
            .WithMany()
            .HasForeignKey(g => g.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        // Progress -> Child, Goal
        modelBuilder.Entity<Progress>()
            .HasOne<Child>()
            .WithMany()
            .HasForeignKey(p => p.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne<Goal>()
            .WithMany()
            .HasForeignKey(p => p.GoalId)
            .OnDelete(DeleteBehavior.Cascade);

        // GoalActivities - composite uniqueness example (optional)
        modelBuilder.Entity<GoalActivities>()
            .HasIndex(ga => new { ga.GoalId, ga.ActivityId })
            .IsUnique(false);
    }
}
