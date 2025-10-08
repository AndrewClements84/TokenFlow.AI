using Flow.AI.Core.Interfaces;
using TokenFlow.AI.Client;

namespace TokenFlow.AI.Integration
{
    /// <summary>
    /// Adapter that bridges TokenFlow.AI functionality to Flow.AI.Core contracts.
    /// </summary>
    public class TokenFlowProvider : ITokenFlowProvider
    {
        private readonly TokenFlowClient _client;

        public TokenFlowProvider(string modelId = "gpt-4o-mini")
        {
            _client = new TokenFlowClient(modelId);
        }

        public int CountTokens(string modelId, string text)
        {
            var result = _client.AnalyzeText(text);
            return result.TokenCount;
        }
    }
}
