using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Registry
{
    public static class ModelRegistryJsonLoader
    {
        public static IList<ModelSpec> LoadFromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return new List<ModelSpec>();

            try
            {
                string json = File.ReadAllText(path);
                var models = JsonSerializer.Deserialize<List<ModelSpec>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return models ?? new List<ModelSpec>();
            }
            catch (Exception)
            {
                // Gracefully fail — return an empty list if invalid JSON
                return new List<ModelSpec>();
            }
        }
    }
}


