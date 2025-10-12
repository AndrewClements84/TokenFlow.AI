using System;

namespace TokenFlow.Core.Models
{
    /// <summary>
    /// DTO used for loading model registry data from JSON in a .NET Standard 2.0–compatible way.
    /// Converts to immutable <see cref="ModelSpec"/> instances.
    /// </summary>
    public class ModelSpecData
    {
        // Property names must match the JSON keys exactly (case-sensitive by default)
        public string Id { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string TokenizerName { get; set; } = string.Empty;
        public int MaxInputTokens { get; set; }
        public int? MaxOutputTokens { get; set; }
        public decimal InputPricePer1K { get; set; }
        public decimal OutputPricePer1K { get; set; }

        public ModelSpec ToModelSpec()
        {
            return new ModelSpec(
                Id,
                Family,
                TokenizerName,
                MaxInputTokens,
                MaxOutputTokens,
                InputPricePer1K,
                OutputPricePer1K);
        }
    }
}
