using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using LiteDB;

public static class DbSeeder
{
	private static string GetCollectionName<T>() => typeof(T).Name.ToLower() + "s";
    public static void Seed<T>(LiteDatabase db, string jsonPath)
    {
		var collectionName = GetCollectionName<T>();
        if(!File.Exists(jsonPath))
        {
            Console.WriteLine($"Seed: JSON not found at {jsonPath}");
            return;
        }

        try
        {
            var json = File.ReadAllText(jsonPath);
            var items = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);

            if (items == null || items.Count == 0)
            {
                Console.WriteLine($"Seed: JSON empty or invalid: {jsonPath}");
                return;
            }

            var col = db.GetCollection<T>(collectionName);
            col.DeleteAll();
            col.InsertBulk(items);

            Console.WriteLine($"Seed: Inserted {items.Count} items into {collectionName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed ERROR: {ex.Message}");
        }
    }
}

