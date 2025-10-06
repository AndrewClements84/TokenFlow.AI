<p align="center">
  <img src="https://github.com/AndrewClements84/TokenFlow.AI/blob/master/assets/logo.png?raw=true" alt="TokenFlow.AI" width="500"/>
</p>

# TokenFlow.AI

[![Build](https://github.com/AndrewClements84/TokenFlow.AI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/AndrewClements84/TokenFlow.AI/actions)
[![codecov](https://codecov.io/gh/AndrewClements84/TokenFlow.AI/branch/master/graph/badge.svg)](https://codecov.io/gh/AndrewClements84/TokenFlow.AI)
[![NuGet Version](https://img.shields.io/nuget/v/TokenFlow.AI.svg?logo=nuget&cacheSeconds=60)](https://www.nuget.org/packages/TokenFlow.AI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/TokenFlow.AI.svg)](https://www.nuget.org/packages/TokenFlow.AI)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

### 💡 Overview

**TokenFlow.AI** is a lightweight .NET library for **tokenization**, **chunking**, and **cost estimation** across modern large language models (LLMs) such as OpenAI GPT‑4o, Anthropic Claude, and Azure OpenAI.

It provides accurate token counting, intelligent text splitting, cumulative usage tracking, and real‑time cost estimation for any AI‑driven application.

---

### 🧩 Key Features

- 🔢 GPT‑style **token counting** for .NET  
- 🧱 Smart **text chunking** with configurable token limits and overlap  
- 💰 Real‑time **cost estimation** for prompt and completion usage  
- 🧮 **TokenUsageTracker** — track cumulative token and cost usage across analyses  
- 🧩 Unified **TokenFlowClient** for developers — analyze, chunk, and cost in one API  
- 🔌 Pluggable **tokenizer providers** (OpenAI, Anthropic, Azure AI)  
- 📦 **Zero external dependencies** — small, fast, and portable  

---

### 🚀 Installation

```bash
dotnet add package TokenFlow.AI
```

Or install the shared core contracts:

```bash
dotnet add package TokenFlow.Core
```

---

### 🧠 Quick Examples

**Token analysis and cost estimation:**

```csharp
using TokenFlow.AI.Client;

var client = new TokenFlowClient("gpt-4o");
var result = client.AnalyzeText("TokenFlow.AI brings structure to prompt engineering.");

Console.WriteLine($"Model: {result.ModelId}");
Console.WriteLine($"Tokens: {result.TokenCount}");
Console.WriteLine($"Estimated cost: £{result.EstimatedCost:F4}");
```

**Chunking long text:**

```csharp
var chunks = client.ChunkText("This is a long body of text that exceeds a given token limit...", maxTokens: 50, overlapTokens: 5);

foreach (var chunk in chunks)
    Console.WriteLine($"Chunk: {chunk.Substring(0, Math.Min(40, chunk.Length))}...");
```

**Tracking cumulative usage:**

```csharp
using TokenFlow.AI.Tracking;

var tracker = new TokenUsageTracker(client.GetModel());

tracker.Record(client.AnalyzeText("Hello TokenFlow.AI!"));
tracker.Record(client.AnalyzeText("Let's track token usage across sessions."));

var summary = tracker.GetSummary();

Console.WriteLine($"Analyses: {summary.AnalysisCount}");
Console.WriteLine($"Total Tokens: {summary.TotalTokens}");
Console.WriteLine($"Total Cost: £{summary.TotalCost:F4}");
```

---

### 🧪 Running Tests

```bash
dotnet test --no-build --verbosity normal
```

All unit tests are written in **xUnit** and run automatically through GitHub Actions.  
Code coverage is tracked with **Codecov**, and the project maintains **100% line and branch coverage**.

---

### 🛠️ Roadmap

#### ✅ Completed
- [x] Core interfaces and models (`ITokenizer`, `ICostEstimator`, `ModelSpec`, `TokenCountResult`)
- [x] Implemented `ApproxTokenizer`, `CostEstimator`, and `ModelRegistry`
- [x] Added `TokenChunker` with overlap support
- [x] Added `TokenFlowClient` — unified entry point for developers
- [x] Added `TokenUsageTracker` — cumulative cost and token tracking
- [x] Implemented `ITokenizerFactory` for dynamic tokenizer resolution 
- [x] Full xUnit test suite with **100% code coverage**
- [x] CI/CD pipeline with Codecov and automated NuGet publishing
- [x] Dual targeting for **.NET Standard 2.0** and **.NET 8.0**


#### 🚧 In Progress
- [ ] Extend `ModelRegistry` to support JSON configuration loading
- [ ] CLI utilities via **TokenFlow.Tools**
- [ ] Benchmark suite using BenchmarkDotNet

#### 🌟 Future Goals
- [ ] Advanced tokenizers (OpenAI tiktoken, Claude tokenizer)
- [ ] Developer documentation & sample apps
- [ ] Integration with other Flow.AI components once public

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
