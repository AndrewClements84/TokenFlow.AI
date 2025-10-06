using BenchmarkDotNet.Attributes;
using TokenFlow.AI.Tokenizer;

namespace TokenFlow.Tools.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    public class TokenizerBenchmarks
    {
        private readonly ApproxTokenizer _tokenizer = new ApproxTokenizer();

        [Params(
            "Hello world!",
            "TokenFlow.AI is a toolkit for accurate tokenization and cost estimation across LLMs.",
            "This is a much longer string designed to simulate realistic prompt text being tokenized in bulk for benchmark purposes."
        )]
        public string Input { get; set; }

        [Benchmark]
        public int CountTokens() => _tokenizer.CountTokens(Input);

        [Benchmark]
        public void TokenizeMany()
        {
            for (int i = 0; i < 100; i++)
                _tokenizer.CountTokens(Input);
        }
    }
}

