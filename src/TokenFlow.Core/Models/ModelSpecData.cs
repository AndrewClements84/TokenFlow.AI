using System;

namespace TokenFlow.Core.Models
{
    /// <summary>
    /// DTO used for loading model registry data from JSON in a .NET Standard 2.0–compatible way.
    /// Converts to immutable <see cref="ModelSpec"/> instances.
    /// Backward-compatible with legacy fields:
    /// - Provider/Name/MaxTokens
    /// - PromptCostPer1K/CompletionCostPer1K
    /// </summary>
    public class ModelSpecData
    {
        // Canonical fields (preferred)
        public string Id { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;         // canonical provider label in current codebase
        public string TokenizerName { get; set; } = "default";
        public int MaxInputTokens { get; set; }
        public int? MaxOutputTokens { get; set; }
        public decimal InputPricePer1K { get; set; }
        public decimal OutputPricePer1K { get; set; }

        // ---- Legacy/alternate aliases (accepted in incoming JSON) ----

        // Provider alias for Family
        public string Provider
        {
            get => Family;
            set { if (!string.IsNullOrWhiteSpace(value)) Family = value; }
        }

        // Friendly display name (not used by ModelSpec ctor, but kept for future)
        public string Name { get; set; }  // optional; ignored in ToModelSpec()

        // MaxTokens alias for MaxInputTokens
        public int MaxTokens
        {
            get => MaxInputTokens;
            set { if (value > 0) MaxInputTokens = value; }
        }

        // Pricing aliases: map only if canonical value not already set
        public decimal PromptCostPer1K
        {
            set { if (InputPricePer1K == 0m) InputPricePer1K = value; }
        }
        public decimal CompletionCostPer1K
        {
            set { if (OutputPricePer1K == 0m) OutputPricePer1K = value; }
        }

        /// <summary>
        /// Creates the immutable <see cref="ModelSpec"/> using canonicalized values.
        /// </summary>
        public ModelSpec ToModelSpec()
        {
            // Some sensible guardrails
            var family = string.IsNullOrWhiteSpace(Family) ? "unknown" : Family;
            var tokenizer = string.IsNullOrWhiteSpace(TokenizerName) ? "default" : TokenizerName;
            var maxIn = MaxInputTokens > 0 ? MaxInputTokens : 128000;

            return new ModelSpec(
                id: Id,
                family: family,
                tokenizerName: tokenizer,
                maxInputTokens: maxIn,
                maxOutputTokens: MaxOutputTokens,
                inputPricePer1K: InputPricePer1K,
                outputPricePer1K: OutputPricePer1K
            );
        }
    }
}
