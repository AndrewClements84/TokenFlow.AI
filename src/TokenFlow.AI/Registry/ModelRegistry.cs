using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TokenFlow.Core.Models;
using Newtonsoft.Json;

namespace TokenFlow.AI.Registry
{
    public class ModelRegistry : IModelRegistry
    {
        private readonly List<ModelSpec> _models = new List<ModelSpec>();

        /// <summary>
        /// Describes where the models were loaded from: Remote, Local, Embedded, or Unknown.
        /// </summary>
        public string LoadSource { get; private set; } = "Unknown";

        // === Constructors ===

        /// <summary>
        /// Default constructor – loads embedded defaults.
        /// </summary>
        public ModelRegistry()
        {
            LoadEmbeddedDefaults();
        }

        /// <summary>
        /// Loads from a local JSON file path.
        /// </summary>
        public ModelRegistry(string jsonPath)
        {
            LoadFromJsonFile(jsonPath);
            if (_models.Count > 0)
            {
                LoadSource = "Local";
                LogSource(LoadSource, jsonPath);
            }
            else
            {
                LoadEmbeddedDefaults();
            }
        }

        /// <summary>
        /// Tries remote, then local, then embedded.
        /// </summary>
        public ModelRegistry(Uri remoteUrl, string localFilePath, bool useEmbeddedFallback)
        {
            bool loaded = false;

            // Try remote
            if (remoteUrl != null)
            {
                var remoteModels = ModelRegistryRemoteLoader.LoadFromUrl(remoteUrl.ToString());
                if (remoteModels != null && remoteModels.Count > 0)
                {
                    _models.AddRange(remoteModels);
                    LoadSource = "Remote";
                    LogSource(LoadSource, remoteUrl.ToString());
                    loaded = true;
                }
            }

            // Try local file
            if (!loaded && !string.IsNullOrEmpty(localFilePath))
            {
                LoadFromJsonFile(localFilePath);
                if (_models.Count > 0)
                {
                    LoadSource = "Local";
                    LogSource(LoadSource, localFilePath);
                    loaded = true;
                }
            }

            // Fallback to embedded
            if (!loaded && useEmbeddedFallback)
                LoadEmbeddedDefaults();

            if (LoadSource == "Unknown" && _models.Count > 0)
                LoadSource = "Embedded";
        }

        // === Core Methods ===

        public void Register(ModelSpec model)
        {
            if (model == null || string.IsNullOrEmpty(model.Id))
                return;

            _models.RemoveAll(m => m.Id == model.Id);
            _models.Add(model);
        }

        public bool TryGet(string id, out ModelSpec model)
        {
            model = GetById(id);
            return model != null;
        }

        public ModelSpec GetById(string id)
        {
            return _models.FirstOrDefault(m =>
                string.Equals(m.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public IReadOnlyList<ModelSpec> GetAll()
        {
            return _models.AsReadOnly();
        }

        // === JSON & Embedded Loading ===

        public void LoadFromJsonFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return;

            string json = File.ReadAllText(filePath, Encoding.UTF8);
            LoadFromJsonString(json);
        }

        public void LoadFromJsonString(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return;

            try
            {
                var models = JsonConvert.DeserializeObject<List<ModelSpec>>(json);
                if (models == null)
                    return;

                foreach (var model in models)
                    Register(model);
            }
            catch (JsonException)
            {
                // Ignore malformed input
            }
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void LoadEmbeddedDefaults()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                const string resourceName = "TokenFlow.AI.Data.models.data";
                var stream = assembly.GetManifestResourceStream(resourceName);

                if (stream == null)
                    return;

                using (var reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    LoadFromJsonString(json);
                }

                if (_models.Count > 0)
                {
                    LoadSource = "Embedded";
                    LogSource(LoadSource, "(embedded resource)");
                }
            }
            catch
            {
                LoadSource = "Unknown";
            }
        }

        private static void LogSource(string source, string detail)
        {
            Console.WriteLine($"[TokenFlow.AI] Loaded model registry from {source}: {detail}");
        }
    }
}
