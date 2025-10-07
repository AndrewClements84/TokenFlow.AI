using System;
using System.Collections.Generic;
using TiktokenSharp;
using TokenFlow.Core.Interfaces;

namespace TokenFlow.Tokenizers.OpenAI
{
    /// <summary>
    /// Tokenizer implementation using TiktokenSharp for OpenAI GPT-family models.
    /// Provides accurate tokenization for GPT-3.5, GPT-4, and GPT-4o models.
    /// </summary>
    public class OpenAITikTokenizer : ITokenizer
    {
        private readonly Lazy<TikToken> _tokenizer;

        public string Name { get; }

        public OpenAITikTokenizer(string modelId = "gpt-4o-mini")
        {
            Name = modelId ?? "gpt-4o-mini";
            _tokenizer = new Lazy<TikToken>(() => TikToken.EncodingForModel(Name));
        }

        public int CountTokens(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            return _tokenizer.Value.Encode(text).Count;
        }

        public int CountTokens(IEnumerable<(string role, string content)> messages)
        {
            if (messages == null)
                return 0;

            int total = 0;
            foreach (var msg in messages)
                total += CountTokens(msg.content);
            return total;
        }

        public IReadOnlyList<string> Encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            var encoded = _tokenizer.Value.Encode(text);
            var list = new List<string>();
            foreach (var id in encoded)
                list.Add(id.ToString());
            return list;
        }

        public string Decode(IEnumerable<string> tokens)
        {
            if (tokens == null)
                return string.Empty;

            var ids = new List<int>();
            foreach (var t in tokens)
            {
                int id;
                if (int.TryParse(t, out id))
                    ids.Add(id);
            }

            return _tokenizer.Value.Decode(ids);
        }
    }
}
