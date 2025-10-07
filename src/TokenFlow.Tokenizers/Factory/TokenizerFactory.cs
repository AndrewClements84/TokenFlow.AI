using System;
using System.Collections.Generic;
using TokenFlow.Core.Interfaces;
using TokenFlow.Tokenizers.Shared;
using TokenFlow.Tokenizers.OpenAI;
using TokenFlow.Tokenizers.Claude;

namespace TokenFlow.Tokenizers.Factory
{
    /// <summary>
    /// Central registry and creation logic for all available tokenizers.
    /// </summary>
    public class TokenizerFactory : ITokenizerFactory
    {
        private readonly Dictionary<string, ITokenizer> _registry;

        public TokenizerFactory()
        {
            _registry = new Dictionary<string, ITokenizer>(StringComparer.OrdinalIgnoreCase);

            // Default built-in tokenizers
            Register("approx", new ApproxTokenizer());
            Register("gpt", new OpenAITikTokenizer());
            Register("openai", new OpenAITikTokenizer());
            Register("claude", new ClaudeTokenizer());
        }

        /// <inheritdoc />
        public void Register(string name, ITokenizer tokenizer)
        {
            if (string.IsNullOrWhiteSpace(name) || tokenizer == null)
                return;

            _registry[name] = tokenizer;
        }

        /// <inheritdoc />
        public ITokenizer Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return _registry["approx"];

            name = name.ToLowerInvariant();

            // Prefix matching for model families
            if (name.StartsWith("gpt") || name.StartsWith("openai"))
                return new OpenAITikTokenizer(name);

            if (name.StartsWith("claude"))
                return new ClaudeTokenizer(name);

            if (_registry.ContainsKey(name))
                return _registry[name];

            return _registry["approx"];
        }

        /// <inheritdoc />
        public bool IsRegistered(string name)
        {
            return _registry.ContainsKey(name);
        }
    }
}
