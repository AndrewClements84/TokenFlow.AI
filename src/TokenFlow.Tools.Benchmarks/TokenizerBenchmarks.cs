using BenchmarkDotNet.Attributes;
using TokenFlow.Tokenizers.Factory;

namespace TokenFlow.Tools.Benchmarks
{
    [MemoryDiagnoser]
    [MarkdownExporter, HtmlExporter]
    public class TokenizerBenchmarks
    {
        private readonly TokenizerFactory _factory = new();
        private string _sampleText = string.Empty;

        [GlobalSetup]
        public void Setup()
        {
            _sampleText = new string('A', 1000);
        }

        [Benchmark(Description = "OpenAI GPT-4 Tokenizer Count")]
        public int CountTokens_OpenAI()
        {
            var tokenizer = _factory.Create("gpt-4o");
            return tokenizer.CountTokens(_sampleText);
        }

        [Benchmark(Description = "Claude 3 Tokenizer Count")]
        public int CountTokens_Claude()
        {
            var tokenizer = _factory.Create("claude-3-opus");
            return tokenizer.CountTokens(_sampleText);
        }

        [Benchmark(Description = "Approx Tokenizer Count")]
        public int CountTokens_Approx()
        {
            var tokenizer = _factory.Create("approx");
            return tokenizer.CountTokens(_sampleText);
        }
    }
}
