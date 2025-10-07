using System;
using System.Collections.Generic;
using System.Linq;
using SharpToken;
using TokenFlow.Core.Interfaces;

namespace TokenFlow.Tokenizers.Claude
{
    /// <summary>
    /// Approximate tokenizer for Anthropic Claude models using the "cl100k_base" encoding,
    /// which aligns closely with OpenAI’s GPT tokenization behavior.
    /// </summary>
    public class ClaudeTokenizer : ITokenizer
    {
        private readonly GptEncoding _encoding;

        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        /// Initializes a Claude tokenizer instance for a specific model (e.g., "claude-3-opus").
        /// </summary>
        /// <param name="modelId">The Claude model identifier.</param>
        public ClaudeTokenizer(string modelId = "claude-3-opus")
        {
            Name = modelId ?? "claude-3-opus";
            _encoding = GptEncoding.GetEncoding("cl100k_base");
        }

        /// <inheritdoc />
        public int CountTokens(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            return _encoding.Encode(text).Count;
        }

        /// <inheritdoc />
        public int CountTokens(IEnumerable<(string role, string content)> messages)
        {
            if (messages == null)
                return 0;

            return messages.Sum(m => CountTokens(m.content));
        }

        /// <inheritdoc />
        public IReadOnlyList<string> Encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<string>();

            var encoded = _encoding.Encode(text);
            return encoded.Select(id => id.ToString()).ToList();
        }

        /// <inheritdoc />
        public string Decode(IEnumerable<string> tokens)
        {
            if (tokens == null)
                return string.Empty;

            var ids = tokens
                .Select(t => int.TryParse(t, out var id) ? id : 0)
                .ToList(); // Fix: convert to List<int>

            return _encoding.Decode(ids);
        }
    }
}

