using System;
using System.Collections.Generic;
using System.Linq;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Registry
{
    /// <summary>
    /// A simple in-memory model registry for retrieving model specifications.
    /// </summary>
    public class ModelRegistry
    {
        private readonly Dictionary<string, ModelSpec> _models;

        public ModelRegistry()
        {
            _models = new Dictionary<string, ModelSpec>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "gpt-4o",
                    new ModelSpec(
                        "gpt-4o",
                        "openai",
                        "approx",
                        128000,
                        4096,
                        0.01m,
                        0.03m)
                },
                {
                    "claude-3-sonnet",
                    new ModelSpec(
                        "claude-3-sonnet",
                        "anthropic",
                        "approx",
                        200000,
                        4096,
                        0.008m,
                        0.024m)
                }
            };
        }

        public ModelSpec Get(string id)
        {
            ModelSpec model;
            if (_models.TryGetValue(id, out model))
                return model;

            throw new KeyNotFoundException("Model not found: " + id);
        }

        public bool TryGet(string id, out ModelSpec model)
        {
            return _models.TryGetValue(id, out model);
        }

        public IEnumerable<ModelSpec> All()
        {
            return _models.Values.ToList();
        }
    }
}
