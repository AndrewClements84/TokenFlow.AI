<p align="center">
  <img src="https://github.com/AndrewClements84/TokenFlow.AI/blob/master/assets/logo.png?raw=true" alt="TokenFlow.AI" width="500"/>
</p>

# TokenFlow.AI

[![Build](https://github.com/AndrewClements84/TokenFlow.AI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/AndrewClements84/TokenFlow.AI/actions)
[![codecov](https://codecov.io/gh/AndrewClements84/TokenFlow.AI/branch/master/graph/badge.svg)](https://codecov.io/gh/AndrewClements84/TokenFlow.AI)
[![NuGet Version](https://img.shields.io/nuget/v/TokenFlow.AI.svg?logo=nuget&cacheSeconds=3600)](https://www.nuget.org/packages/TokenFlow.AI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/TokenFlow.AI.svg)](https://www.nuget.org/packages/TokenFlow.AI)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

### 💡 Overview

**TokenFlow.AI** is a lightweight .NET library for **tokenization**, **chunking**, and **cost estimation** across modern large language models (LLMs) like OpenAI GPT-4o and Anthropic Claude.

It forms the **core engine** of the *Flow.AI* ecosystem — powering accurate token counting, text splitting, and real-time cost tracking for AI-driven applications.

---

### 🧩 Key Features

- 🔢 GPT-style **token counting** for .NET  
- 🧱 Smart **text chunking** with configurable token limits  
- 💰 Real-time **cost estimation** for input/output usage  
- 🔌 Pluggable **tokenizer providers** (OpenAI, Anthropic, Azure AI)  
- 📦 **Zero external dependencies** — small, fast, portable  
- 🧠 Designed for use in **PromptStream.AI**, **DataFlow.AI**, and **ReasonFlow.AI**

---

### 🚀 Installation

```bash
dotnet add package TokenFlow.AI
```

Or install the core contracts only:

```bash
dotnet add package TokenFlow.Core
```

---

### 🧠 Quick Example

```csharp
using TokenFlow.AI.Costing;
using TokenFlow.AI.Tokenizer;
using TokenFlow.Core.Models;

var model = new ModelSpec("gpt-4o", "openai", "approx", 128000, 4096, 0.01m, 0.03m);
var tokenizer = new ApproxTokenizer();
var estimator = new CostEstimator();

string input = "TokenFlow.AI makes cost tracking easy!";
int tokenCount = tokenizer.CountTokens(input);

var result = new TokenCountResult(tokenCount, 0, tokenCount);
decimal cost = estimator.EstimateTotalCost(result, model);

Console.WriteLine($"Tokens: {tokenCount}, Estimated cost: {cost:C4}");
```

---

### 🧪 Running Tests

```bash
dotnet test --no-build --verbosity normal
```

All unit tests are implemented using **xUnit** and run automatically via GitHub Actions.

---

### 🧭 Part of the Flow.AI Ecosystem

| Package | Purpose |
|----------|----------|
| 🧠 **TokenFlow.AI** | Core tokenization, chunking & cost estimation |
| 💬 **PromptStream.AI** | Prompt composition & validation |
| 📊 **DataFlow.AI** | Data ingestion & structured streaming pipelines |
| 🧩 **ReasonFlow.AI** | Logical reasoning & multi-step thought orchestration |
| 🧬 **ModelFlow.AI** | Unified model abstraction & configuration registry |
| 💭 **ChatFlow.AI** | Conversational orchestration & dialogue state management |

---

### 🛠️ Roadmap

#### ✅ Completed
- [x] Core interfaces and models (`ITokenizer`, `ICostEstimator`, `ModelSpec`, `TokenCountResult`)
- [x] Implemented `ApproxTokenizer`, `CostEstimator`, and `ModelRegistry`
- [x] Added `TokenChunker` and full test coverage
- [x] CI/CD workflow with Codecov and automated NuGet publishing
- [x] Achieved 100% line and branch coverage across all components

#### 🚧 In Progress
- [ ] Add `TokenFlowClient` — unified entry point for developers
- [ ] Introduce `TokenUsageTracker` for cumulative cost tracking
- [ ] Implement `ITokenizerFactory` for dynamic tokenizer resolution
- [ ] Extend `ModelRegistry` to support JSON configuration loading
- [ ] CLI utilities via **TokenFlow.Tools**
- [ ] Benchmark suite using BenchmarkDotNet

#### 🌟 Future Goals
- [ ] Integration with **PromptStream.AI** for prompt budget validation
- [ ] Integration with **DataFlow.AI** for token-based stream segmentation
- [ ] Advanced tokenizers (OpenAI tiktoken, Claude tokenizer)
- [ ] Developer documentation & sample apps
- [ ] Public release under Flow.AI brand umbrella

---

### 💬 Contributing

Pull requests are welcome!  
If you’d like to contribute to the **Flow.AI** ecosystem, please read the upcoming `CONTRIBUTING.md` once published.

---

### 🪪 License

Distributed under the **MIT License**.  
See [`LICENSE`](LICENSE) for details.

---

> ⭐ **If you find TokenFlow.AI useful, please give the repository a star on GitHub!**  
> It helps others discover the Flow.AI ecosystem and supports ongoing development.
