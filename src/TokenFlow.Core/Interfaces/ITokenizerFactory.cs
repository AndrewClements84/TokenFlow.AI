using TokenFlow.Core.Interfaces;

namespace TokenFlow.Tokenizers.Factory
{
    /// <summary>
    /// Defines a factory responsible for creating and managing tokenizer instances.
    /// </summary>
    public interface ITokenizerFactory
    {
        /// <summary>
        /// Creates or retrieves a tokenizer instance for the specified model or name.
        /// </summary>
        ITokenizer Create(string name);

        /// <summary>
        /// Registers a tokenizer instance under the given name.
        /// </summary>
        void Register(string name, ITokenizer tokenizer);

        /// <summary>
        /// Determines whether a tokenizer is registered under the given name.
        /// </summary>
        bool IsRegistered(string name);
    }
}

