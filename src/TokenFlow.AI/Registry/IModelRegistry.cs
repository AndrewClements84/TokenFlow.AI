using System.Collections.Generic;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Registry
{
    /// <summary>
    /// Defines a registry for managing and retrieving model specifications.
    /// </summary>
    public interface IModelRegistry
    {
        /// <summary>
        /// Registers a model in the registry or replaces an existing one with the same ID.
        /// </summary>
        void Register(ModelSpec model);

        /// <summary>
        /// Attempts to retrieve a model by its ID.
        /// </summary>
        bool TryGet(string id, out ModelSpec model);

        /// <summary>
        /// Retrieves a model specification by its unique ID.
        /// </summary>
        ModelSpec GetById(string id);

        /// <summary>
        /// Retrieves all registered model specifications.
        /// </summary>
        IReadOnlyList<ModelSpec> GetAll();

        /// <summary>
        /// Loads model definitions from a JSON file.
        /// </summary>
        void LoadFromJsonFile(string filePath);

        /// <summary>
        /// Loads model definitions from a JSON string.
        /// </summary>
        void LoadFromJsonString(string json);
    }
}
