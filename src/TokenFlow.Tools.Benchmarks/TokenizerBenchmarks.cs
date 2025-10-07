using BenchmarkDotNet.Attributes;
using TokenFlow.Tokenizers.Shared;
using TokenFlow.Tokenizers.OpenAI;
using TokenFlow.Tokenizers.Claude;

namespace TokenFlow.Tools.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    public class TokenizerBenchmarks
    {
        private readonly ApproxTokenizer _approx = new ApproxTokenizer();
        private readonly OpenAITikTokenizer _openai = new OpenAITikTokenizer("gpt-4o-mini");
        private readonly ClaudeTokenizer _claude = new ClaudeTokenizer("claude-3-opus");

        [Params(
            "Hello world!",
            "TokenFlow.AI is a toolkit for accurate tokenization and cost estimation across LLMs.",
            "This is a much longer string designed to simulate realistic prompt text being tokenized in bulk for benchmark purposes."
        )]
        public string Input { get; set; }

        [Benchmark(Baseline = true)]
        public int Approx_CountTokens() => _approx.CountTokens(Input);

        [Benchmark]
        public int OpenAI_CountTokens() => _openai.CountTokens(Input);

        [Benchmark]
        public int Claude_CountTokens() => _claude.CountTokens(Input);

        [Benchmark]
        public void Approx_BatchTokenize()
        {
            for (int i = 0; i < 100; i++)
                _approx.CountTokens(Input);
        }

        [Benchmark]
        public void OpenAI_BatchTokenize()
        {
            for (int i = 0; i < 100; i++)
                _openai.CountTokens(Input);
        }

        [Benchmark]
        public void Claude_BatchTokenize()
        {
            for (int i = 0; i < 100; i++)
                _claude.CountTokens(Input);
        }
    }
}


