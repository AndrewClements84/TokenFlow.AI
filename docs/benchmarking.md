# 📈 Benchmarking & CI Integration

TokenFlow.AI includes automated performance regression tracking using **BenchmarkDotNet**.

---

## 🧩 Local Usage

To run benchmarks locally and view results:

```bash
dotnet run -c Release --project src/TokenFlow.Tools.Benchmarks
```

Results will be saved to the `benchmark-results/` folder with a detailed JSON output for each benchmark.

---

## 🧮 CI Integration

Benchmark results are automatically compared against a baseline (`benchmark-results/baseline.json`) in the CI pipeline.

If any benchmark shows a **performance regression greater than 10%**, the build will fail to prevent degradations.

The pipeline also uploads all benchmark artifacts for further inspection.

---

## 🐢 Quick Mode Toggle

To disable benchmarking during regular CI builds and speed up the process, add the following environment variable:

```yaml
env:
  RUN_BENCHMARKS: false
```

When set to `false`, the build will skip benchmark execution (reducing build time from ~15 minutes to ~4 minutes).  
Set it back to `true` before publishing or major releases to re‑enable full regression testing.

---

## 📊 Example Output

| Benchmark | Mean (µs) | Error | Ratio | Allocations |
|------------|-----------|-------|--------|--------------|
| TokenizerBenchmarks.CountTokens_OpenAI | 45.2 | 0.6 | 1.00x | 0 B |
| TokenizerBenchmarks.CountTokens_Claude | 46.8 | 0.7 | 1.04x | 0 B |
| ChunkerBenchmarks.ChunkByTokens | 210.4 | 1.8 | 1.00x | 512 B |
| CostEstimatorBenchmarks.EstimateTotalCost | 7.8 | 0.2 | 1.00x | 0 B |

---

> 🧠 Benchmarks are re‑run automatically in CI with each build to ensure TokenFlow.AI remains stable and performant.
