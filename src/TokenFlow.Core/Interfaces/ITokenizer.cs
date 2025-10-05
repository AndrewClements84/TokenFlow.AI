using System.Collections.Generic;

namespace TokenFlow.Core.Interfaces
{
    /// <summary>
    /// Defines a tokenizer capable of counting and encoding text into tokens.
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// The display name of this tokenizer (e.g., "openai-bpe").
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Counts the number of tokens in a given text.
        /// </summary>
        int CountTokens(string text);

        /// <summary>
        /// Counts tokens across multiple chat messages, if applicable.
        /// </summary>
        int CountTokens(IEnumerable<(string role, string content)> messages);

        /// <summary>
        /// Optionally encodes text into a sequence of token strings or IDs.
        /// </summary>
        IReadOnlyList<string> Encode(string text);

        /// <summary>
        /// Optionally decodes tokens back into text.
        /// </summary>
        string Decode(IEnumerable<string> tokens);
    }
}
