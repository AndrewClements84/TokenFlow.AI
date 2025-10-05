namespace TokenFlow.Core.Models
{
    /// <summary>
    /// Describes a language model, its tokenizer, limits, and pricing.
    /// </summary>
    public class ModelSpec
    {
        public string Id { get; }
        public string Family { get; }
        public string TokenizerName { get; }
        public int MaxInputTokens { get; }
        public int? MaxOutputTokens { get; }
        public decimal InputPricePer1K { get; }
        public decimal OutputPricePer1K { get; }

        public ModelSpec(
            string id,
            string family,
            string tokenizerName,
            int maxInputTokens,
            int? maxOutputTokens,
            decimal inputPricePer1K,
            decimal outputPricePer1K)
        {
            Id = id;
            Family = family;
            TokenizerName = tokenizerName;
            MaxInputTokens = maxInputTokens;
            MaxOutputTokens = maxOutputTokens;
            InputPricePer1K = inputPricePer1K;
            OutputPricePer1K = outputPricePer1K;
        }
    }
}

