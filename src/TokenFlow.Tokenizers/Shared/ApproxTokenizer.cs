using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TokenFlow.Core.Interfaces;

namespace TokenFlow.Tokenizers.Shared
{
    /// <summary>
    /// A lightweight, language-agnostic tokenizer approximation that splits
    /// on whitespace and punctuation. Acts as a fallback when a model-specific
    /// tokenizer (e.g., OpenAI or Claude) is not available.
    /// </summary>
    public class ApproxTokenizer : ITokenizer
    {
        private static readonly Regex _splitter = new Regex(@"\w+|[^\s\w]", RegexOptions.Compiled);

        /// <summary>
        /// Gets the display name for this tokenizer.
        /// </summary>
        public string Name => "approx";

        /// <inheritdoc />
        public int CountTokens(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            return _splitter.Matches(text).Count;
        }

        /// <inheritdoc />
        public int CountTokens(IEnumerable<(string role, string content)> messages)
        {
            if (messages == null)
                return 0;

            int total = 0;
            foreach (var msg in messages)
                total += CountTokens(msg.content);

            return total;
        }

        /// <inheritdoc />
        public IReadOnlyList<string> Encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            return _splitter.Matches(text)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
        }

        /// <inheritdoc />
        public string Decode(IEnumerable<string> tokens)
        {
            if (tokens == null)
                return string.Empty;

            return string.Join(" ", tokens);
        }
    }
}
