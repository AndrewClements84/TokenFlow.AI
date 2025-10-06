using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TokenFlow.Core.Models;
using Newtonsoft.Json;

namespace TokenFlow.AI.Registry
{
    public class ModelRegistry : IModelRegistry
    {
        private readonly List<ModelSpec> _models;

        public ModelRegistry()
        {
            _models = new List<ModelSpec>
            {
                new ModelSpec("gpt-4o", "openai", "approx", 128000, 4096, 0.01m, 0.03m),
                new ModelSpec("claude-3", "anthropic", "approx", 200000, 4096, 0.008m, 0.024m)
            };
        }

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
            return _models.FirstOrDefault(m => string.Equals(m.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public IReadOnlyList<ModelSpec> GetAll()
        {
            return _models.AsReadOnly();
        }

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
    }
}
