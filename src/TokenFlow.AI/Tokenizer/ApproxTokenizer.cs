using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TokenFlow.Core.Interfaces;

namespace TokenFlow.AI.Tokenizer
{
    /// <summary>
    /// A simple tokenizer approximation that splits on whitespace and punctuation.
    /// Used as a fallback when a model-specific tokenizer is not available.
    /// </summary>
    public class ApproxTokenizer : ITokenizer
    {
        private static readonly Regex _splitter = new Regex(@"\w+|[^\s\w]", RegexOptions.Compiled);

        public string Name { get { return "approx"; } }

        public int CountTokens(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            return _splitter.Matches(text).Count;
        }

        public int CountTokens(IEnumerable<(string role, string content)> messages)
        {
            if (messages == null)
                return 0;

            int total = 0;
            foreach (var msg in messages)
            {
                total += CountTokens(msg.content);
            }
            return total;
        }

        public IReadOnlyList<string> Encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            return _splitter.Matches(text)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
        }

        public string Decode(IEnumerable<string> tokens)
        {
            if (tokens == null)
                return string.Empty;

            return string.Join(" ", tokens);
        }
    }
}

