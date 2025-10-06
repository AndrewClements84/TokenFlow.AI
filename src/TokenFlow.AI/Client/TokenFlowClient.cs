using System;
using System.Collections.Generic;
using TokenFlow.AI.Chunking;
using TokenFlow.AI.Costing;
using TokenFlow.AI.Registry;
using TokenFlow.AI.Tokenizer;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Client
{
    /// <summary>
    /// High-level API that wraps token counting, chunking, and cost estimation.
    /// </summary>
    public class TokenFlowClient : ITokenFlowClient
    {
        private readonly ModelSpec _model;
        private readonly ApproxTokenizer _tokenizer;
        private readonly CostEstimator _estimator;
        private readonly TokenChunker _chunker;

        public TokenFlowClient(string modelId = "gpt-4o")
        {
            var registry = new ModelRegistry();
            if (!registry.TryGet(modelId, out _model))
                throw new ArgumentException($"Model '{modelId}' not found in registry.");

            _tokenizer = new ApproxTokenizer();
            _estimator = new CostEstimator();
            _chunker = new TokenChunker(_tokenizer);
        }

        public TokenAnalysisResult AnalyzeText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new TokenAnalysisResult(0, 0m, _model.Id);

            int tokens = _tokenizer.CountTokens(text);
            var result = new TokenCountResult(tokens, 0, tokens);
            decimal cost = _estimator.EstimateTotalCost(result, _model);

            return new TokenAnalysisResult(tokens, cost, _model.Id);
        }

        public IReadOnlyList<string> ChunkText(string text, int maxTokens, int overlapTokens = 0)
        {
            var chunks = _chunker.ChunkByTokens(text, maxTokens, overlapTokens);
            var result = new List<string>();
            foreach (var c in chunks)
                result.Add(c.Text);
            return result;
        }

        public ModelSpec GetModel() => _model;
    }
}

