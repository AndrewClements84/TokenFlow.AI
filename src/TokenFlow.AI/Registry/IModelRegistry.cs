using System.Collections.Generic;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Registry
{
    /// <summary>
    /// Defines a contract for accessing and managing model metadata
    /// within the TokenFlow.AI registry system.
    /// </summary>
    public interface IModelRegistry
    {
        /// <summary>
        /// Registers a new model or updates an existing one in the registry.
        /// </summary>
        /// <param name="model">The model to register.</param>
        void Register(ModelSpec model);

        /// <summary>
        /// Attempts to retrieve a model by its unique identifier.
        /// </summary>
        /// <param name="id">The model identifier.</param>
        /// <param name="model">The resulting model, if found.</param>
        /// <returns><c>true</c> if the model exists; otherwise, <c>false</c>.</returns>
        bool TryGet(string id, out ModelSpec model);

        /// <summary>
        /// Retrieves a model by its unique identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="id">The model identifier.</param>
        /// <returns>The model if found; otherwise, <c>null</c>.</returns>
        ModelSpec GetById(string id);

        /// <summary>
        /// Returns all registered models as a read-only list.
        /// </summary>
        IReadOnlyList<ModelSpec> GetAll();

        /// <summary>
        /// Describes the source from which the models were loaded.
        /// Can be "Remote", "Local", "Embedded", or "Unknown".
        /// </summary>
        string LoadSource { get; }
    }
}

