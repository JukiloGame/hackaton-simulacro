using System.Text.Json;
using PluriConnectAPI.Data;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    // Busca JSON en la carpeta 'dataFolder' por nombre del tipo: sampleChildren.json, sampleGoals.json...
    public static async Task SeedInitialAsync(AppDbContext ctx, string dataFolder)
    {
        if (!Directory.Exists(dataFolder)) return;

        // Seed Children
        if (!await ctx.Children.AnyAsync())
        {
            var children = LoadJson<List<PluriConnectAPI.Models.Child>>(Path.Combine(dataFolder, "sampleChildren.json"));
            if (children.Any()) ctx.Children.AddRange(children);
        }

        if (!await ctx.Activities.AnyAsync())
        {
            var activities = LoadJson<List<PluriConnectAPI.Models.Activity>>(Path.Combine(dataFolder, "sampleActivities.json"));
            if (activities.Any()) ctx.Activities.AddRange(activities);
        }

        if (!await ctx.Goals.AnyAsync())
        {
            var goals = LoadJson<List<PluriConnectAPI.Models.Goal>>(Path.Combine(dataFolder, "sampleGoals.json"));
            if (goals.Any()) ctx.Goals.AddRange(goals);
        }

        if (!await ctx.Progress.AnyAsync())
        {
            var progress = LoadJson<List<PluriConnectAPI.Models.Progress>>(Path.Combine(dataFolder, "sampleProgress.json"));
            if (progress.Any()) ctx.Progress.AddRange(progress);
        }

        if (!await ctx.GoalActivities.AnyAsync())
        {
            var gas = LoadJson<List<PluriConnectAPI.Models.GoalActivities>>(Path.Combine(dataFolder, "sampleGoalActivities.json"));
            if (gas.Any()) ctx.GoalActivities.AddRange(gas);
        }

        if (!await ctx.Comments.AnyAsync())
        {
            var comments = LoadJson<List<PluriConnectAPI.Models.Comment>>(Path.Combine(dataFolder, "sampleComments.json"));
            if (comments.Any()) ctx.Comments.AddRange(comments);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("Seed: done");
    }

    private static T LoadJson<T>(string path)
    {
        if (!File.Exists(path)) return Activator.CreateInstance<T>();
        var json = File.ReadAllText(path);
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(json, opts)!;
    }
}
