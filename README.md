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
[![Buy Me A Coffee](https://img.shields.io/badge/☕%20Buy%20me%20a%20coffee-FFDD00?style=flat&logo=buy-me-a-coffee&logoColor=black)](https://buymeacoffee.com/andrewclements84)

---

### 💡 Overview

**TokenFlow.AI** is a lightweight .NET library for **tokenization**, **chunking**, and **cost estimation** across modern large language models (LLMs) such as OpenAI GPT-4o, Anthropic Claude, and Azure OpenAI.

It provides accurate token counting, intelligent text splitting, cumulative usage tracking, and real-time cost estimation for any AI-driven application.  
Now includes CLI utilities, developer documentation, full Flow.AI ecosystem integration, and automated performance benchmarking.

---

### 🧩 Key Features

- 🔢 GPT-style **token counting** for .NET  
- 🧱 Smart **text chunking** with configurable token limits and overlap  
- 💰 Real-time **cost estimation** for prompt and completion usage  
- 🧮 **TokenUsageTracker** — track cumulative token and cost usage across analyses  
- 🧩 Unified **TokenFlowClient** — analyze, chunk, and cost in one API  
- ⚙️ **CLI utilities (TokenFlow.Tools)** — positional arguments for simplicity (`tokenflow cost "text" gpt-4o`)  
- 📘 **Developer documentation site** — API reference + usage guides via [GitHub Pages](https://andrewclements84.github.io/TokenFlow.AI/)  
- 🧾 **Benchmark suite** powered by BenchmarkDotNet and integrated with CI  
- 🔌 Pluggable **tokenizer providers** — including OpenAI `tiktoken`, Claude `cl100k_base`, and Approx fallback  
- 🔗 **Flow.AI.Core integration** — exposes `ITokenFlowProvider` for shared usage across Flow.AI ecosystem projects  
- 💬 **CLI v3.0 alignment** — enhanced cost commands, dynamic pricing, and Flow.AI registry integration  
- 🧠 Dual targeting for **.NET Standard 2.0** and **.NET 8.0**  

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

---

### 💰 CLI Usage (Positional Arguments)

#### **Estimate Token Cost**
```bash
tokenflow cost "Estimate my token cost" gpt-4o
```

#### **Analyze Prompt Text**
```bash
tokenflow analyze "Explain large language models simply." gpt-4o-mini
```

#### **Compare Multiple Models**
```bash
tokenflow compare "Summarize this text" gpt-4o gpt-3.5-turbo claude-3.5-sonnet
```

#### **Count Tokens**
```bash
tokenflow count "Estimate my token cost"
```

#### **List Available Models**
```bash
tokenflow list-models
```

> 💡 *All CLI commands support positional arguments — text first, model second.*  
> Named flags (`--model`, `--input`) will be added in a future developer-UX update.

---

### 🧪 Running Benchmarks

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

TokenFlow.AI fully implements the shared `Flow.AI.Core.Interfaces.ITokenFlowProvider` interface.  
This enables all Flow.AI components — including **PromptStream.AI**, **DataFlow.AI**, and **ChatFlow.AI** —  
to perform token counting and cost analysis through a unified provider contract.

TokenFlow.AI serves as the **engine layer** of the Flow.AI ecosystem, powering all higher-level orchestration frameworks.

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
- [x] **v0.6.2 — Enhanced Cost Estimator** using Flow.AI.Core registry ✅
- [x] **v0.7.0 — CLI Alignment & Ecosystem Integration** ✅

#### 🌟 Future Goals
- [ ] Extend CLI tooling for full Flow.AI ecosystem interoperability
- [ ] Implement enhanced Flow.AI shared configuration support
- [ ] Add named argument flags (`--model`, `--input`) for CLI commands
- [ ] Begin **PromptStream.AI** cockpit integration phase

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
