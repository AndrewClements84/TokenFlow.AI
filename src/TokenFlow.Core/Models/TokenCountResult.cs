namespace TokenFlow.Core.Models
{
    /// <summary>
    /// Represents token usage across prompt and completion.
    /// </summary>
    public class TokenCountResult
    {
        public int PromptTokens { get; }
        public int CompletionTokens { get; }
        public int TotalTokens { get; }

        public TokenCountResult(int promptTokens, int completionTokens, int totalTokens)
        {
            PromptTokens = promptTokens;
            CompletionTokens = completionTokens;
            TotalTokens = totalTokens;
        }
    }
}
