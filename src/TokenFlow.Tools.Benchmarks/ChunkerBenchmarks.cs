using BenchmarkDotNet.Attributes;
using System.Linq;
using TokenFlow.AI.Chunking;
using TokenFlow.Tokenizers.Shared;

namespace TokenFlow.Tools.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    public class ChunkerBenchmarks
    {
        private readonly TokenChunker _chunker = new TokenChunker(new ApproxTokenizer());
        private string _input;

        [GlobalSetup]
        public void Setup()
        {
            _input = string.Join(" ", Enumerable.Repeat(
                "TokenFlow.AI provides efficient tokenization and chunking utilities.", 200));
        }

        [Benchmark]
        public void ChunkByTokens() => _chunker.ChunkByTokens(_input, 100, 10);
    }
}
