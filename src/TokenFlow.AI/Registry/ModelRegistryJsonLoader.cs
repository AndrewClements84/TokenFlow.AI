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
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelSpecData>>(json);
                var models = new List<ModelSpec>();
                if (data != null)
                {
                    foreach (var d in data)
                        models.Add(d.ToModelSpec());
                }
                return models;
            }
            catch (Exception)
            {
                // Gracefully fail — return an empty list if invalid JSON
                return new List<ModelSpec>();
            }
        }
    }
}


