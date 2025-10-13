using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Registry
{
    /// <summary>
    /// Supports loading model specifications from a remote URL (e.g. GitHub, CDN, or internal API).
    /// </summary>
    public static class ModelRegistryRemoteLoader
    {
        public static List<ModelSpec> LoadFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            try
            {
                using (var client = new WebClient())
                {
                    var json = client.DownloadString(url);
                    if (string.IsNullOrWhiteSpace(json))
                        return null;

                    var raw = JsonSerializer.Deserialize<List<ModelSpecData>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (raw == null) return null;

                    var models = new List<ModelSpec>(raw.Count);
                    foreach (var d in raw)
                        models.Add(d.ToModelSpec());

                    return models;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenFlow.AI] Failed to load remote models: {ex.Message}");
                return null;
            }
        }
    }
}
