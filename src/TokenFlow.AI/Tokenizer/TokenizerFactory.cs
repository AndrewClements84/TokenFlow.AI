using System;
using System.Collections.Generic;
using TokenFlow.Core.Interfaces;

namespace TokenFlow.AI.Tokenizer
{
    public class TokenizerFactory : ITokenizerFactory
    {
        private readonly Dictionary<string, ITokenizer> _registry;

        public TokenizerFactory()
        {
            _registry = new Dictionary<string, ITokenizer>(StringComparer.OrdinalIgnoreCase);
            Register("approx", new ApproxTokenizer()); // default
        }

        public void Register(string name, ITokenizer tokenizer)
        {
            if (string.IsNullOrWhiteSpace(name) || tokenizer == null)
                return;

            _registry[name] = tokenizer;
        }

        public ITokenizer Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new ApproxTokenizer();

            if (_registry.ContainsKey(name))
                return _registry[name];

            return new ApproxTokenizer();
        }

        public bool IsRegistered(string name)
        {
            return _registry.ContainsKey(name);
        }
    }
}

