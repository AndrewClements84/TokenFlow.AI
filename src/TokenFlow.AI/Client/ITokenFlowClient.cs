using System.Collections.Generic;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Client
{
    /// <summary>
    /// Defines the main interface for analyzing text using TokenFlow.AI.
    /// </summary>
    public interface ITokenFlowClient
    {
        TokenAnalysisResult AnalyzeText(string text);
        IReadOnlyList<string> ChunkText(string text, int maxTokens, int overlapTokens = 0);
        ModelSpec GetModel();
    }
}

