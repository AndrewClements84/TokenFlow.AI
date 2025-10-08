
<p align="center">
  <img src="https://github.com/AndrewClements84/TokenFlow.AI/blob/master/assets/logo.png?raw=true" alt="TokenFlow.AI" width="500"/>
</p>

# TokenFlow.AI

[![Build](https://github.com/AndrewClements84/TokenFlow.AI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/AndrewClements84/TokenFlow.AI/actions)
[![Docs](https://img.shields.io/badge/docs-online-brightgreen.svg?logo=githubpages)](https://andrewclements84.github.io/TokenFlow.AI/)
[![codecov](https://codecov.io/gh/AndrewClements84/TokenFlow.AI/branch/master/graph/badge.svg)](https://codecov.io/gh/AndrewClements84/TokenFlow.AI)
[![NuGet Version](https://img.shields.io/nuget/v/TokenFlow.AI.svg?logo=nuget&cacheSeconds=60)](https://www.nuget.org/packages/TokenFlow.AI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/TokenFlow.AI.svg)](https://www.nuget.org/packages/TokenFlow.AI)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

### 💡 Overview

**TokenFlow.AI** is a lightweight .NET library for **tokenization**, **chunking**, and **cost estimation** across modern large language models (LLMs) such as OpenAI GPT‑4o, Anthropic Claude, and Azure OpenAI.

It provides accurate token counting, intelligent text splitting, cumulative usage tracking, and real‑time cost estimation for any AI‑driven application.  
Now includes CLI utilities, developer documentation, full Flow.AI ecosystem integration, and automated performance benchmarking.

---

### 🧩 Key Features

- 🔢 GPT‑style **token counting** for .NET  
- 🧱 Smart **text chunking** with configurable token limits and overlap  
- 💰 Real‑time **cost estimation** for prompt and completion usage  
- 🧮 **TokenUsageTracker** — track cumulative token and cost usage across analyses  
- 🧩 Unified **TokenFlowClient** — analyze, chunk, and cost in one API  
- ⚙️ **CLI utilities (TokenFlow.Tools)** — structured automation with `--format`, `--input`, and `--output` options  
- 📘 **Developer documentation site** — API reference + usage guides via [GitHub Pages](https://andrewclements84.github.io/TokenFlow.AI/)  
- 🧾 **Benchmark suite** powered by BenchmarkDotNet and integrated with CI  
- 🔌 Pluggable **tokenizer providers** — including OpenAI `tiktoken`, Claude `cl100k_base`, and Approx fallback  
- 🔗 **Flow.AI.Core integration** — exposes `ITokenFlowProvider` for shared usage across Flow.AI ecosystem projects  
- 🧠 Dual targeting for **.NET Standard 2.0** and **.NET 8.0**  

---

### 📈 Benchmark Results (v0.6.1)

TokenFlow.AI now includes full **performance regression tracking** integrated into CI using BenchmarkDotNet.  
Results are automatically compared against a baseline to ensure no degradation beyond 10%.

| Benchmark | Mean (µs) | Error | Ratio | Allocations |
|------------|-----------|-------|--------|--------------|
| TokenizerBenchmarks.CountTokens_OpenAI | 45.2 | 0.6 | 1.00x | 0 B |
| TokenizerBenchmarks.CountTokens_Claude | 46.8 | 0.7 | 1.04x | 0 B |
| ChunkerBenchmarks.ChunkByTokens | 210.4 | 1.8 | 1.00x | 512 B |
| CostEstimatorBenchmarks.EstimateTotalCost | 7.8 | 0.2 | 1.00x | 0 B |

Benchmarks are re‑run automatically in CI with each build, and a failure is triggered if performance slows by more than **10%** relative to baseline.

---

### 🧠 Quick Examples

#### **Model-specific tokenizers:**

```csharp
using TokenFlow.Tokenizers.Factory;

var factory = new TokenizerFactory();
var gptTokenizer = factory.Create("gpt-4o");
var claudeTokenizer = factory.Create("claude-3-opus");

Console.WriteLine($"GPT tokens: {gptTokenizer.CountTokens("Hello world!")}");
Console.WriteLine($"Claude tokens: {claudeTokenizer.CountTokens("Hello world!")}");
```

#### **Flow.AI.Core Provider Integration:**

```csharp
using Flow.AI.Core.Interfaces;
using TokenFlow.AI.Integration;

ITokenFlowProvider provider = new TokenFlowProvider("gpt-4o-mini");
int tokens = provider.CountTokens("gpt-4o-mini", "Hello Flow.AI!");
Console.WriteLine($"Token count: {tokens}");
```

#### **Benchmarking tokenizers:**

```bash
dotnet run -c Release --project src/TokenFlow.Tools.Benchmarks
```

**Full benchmark documentation:**  
See [docs/tokenizers.md](docs/tokenizers.md)

---

### 🧪 Running Tests

```bash
dotnet test --no-build --verbosity normal
```

All unit tests are written in **xUnit** and run automatically through GitHub Actions.  
Code coverage is tracked with **Codecov**, and the project maintains **100% line and branch coverage** across all modules.

#### 📊 Code Coverage by Module

| Project | Coverage | Notes |
|----------|-----------|--------|
| **TokenFlow.Core** | 100% | Core models and interfaces |
| **TokenFlow.AI** | 100% | Client, costing, registry, Flow.AI integration |
| **TokenFlow.Tokenizers** | 100% | OpenAI, Claude, and Approx implementations |
| **TokenFlow.Tools** | 100% | CLI automation and output formatting |

---

### 🔗 Flow.AI.Core Integration

TokenFlow.AI now fully implements the shared `Flow.AI.Core.Interfaces.ITokenFlowProvider` interface.  
This enables all Flow.AI components — including **PromptStream.AI**, **DataFlow.AI**, and **ChatFlow.AI** —  
to perform token counting and cost analysis through a unified provider contract.

TokenFlow.AI now serves as the **engine layer** of the Flow.AI ecosystem, powering all higher-level orchestration frameworks.

---

### 🛠️ Roadmap

#### ✅ Completed
- [x] Core interfaces and models (`ITokenizer`, `ICostEstimator`, `ModelSpec`, `TokenCountResult`)
- [x] Added `TokenFlow.Tokenizers` with advanced tokenizers (`OpenAITikTokenizer`, `ClaudeTokenizer`, `ApproxTokenizer`)
- [x] Extended `TokenizerFactory` to handle OpenAI/Claude families ✅
- [x] Added **TokenFlow.Tools.Benchmarks** for tokenizer performance analysis ✅
- [x] Achieved **100% code coverage** across all projects ✅
- [x] CLI v2.1 released with structured automation ✅
- [x] Developer documentation site (API + usage guides) ✅
- [x] Integrated **Flow.AI.Core v0.1.0** and implemented `ITokenFlowProvider` ✅
- [x] Full integration tests and shared registry loading ✅
- [x] **v0.6.1 — Performance Regression Tracking** integrated with CI ✅

#### 🌟 Future Goals
- [ ] Introduce enhanced cost estimator leveraging Flow.AI.Core model registry
- [ ] Extend CLI tooling for full Flow.AI ecosystem compatibility
- [ ] Begin PromptStream.AI cockpit integration phase

---

### 💬 Contributing

Pull requests are welcome!  
If you’d like to contribute to **TokenFlow.AI**, please read the upcoming `CONTRIBUTING.md` once published.

---

### 🪪 License

Distributed under the **MIT License**.  
See [`LICENSE`](LICENSE) for details.

---

> ⭐ **If you find TokenFlow.AI useful, please give the repository a star on GitHub!**  
> It helps others discover the project and supports ongoing development.
