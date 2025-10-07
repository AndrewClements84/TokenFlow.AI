
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
Now includes CLI utilities, developer documentation, and performance benchmarking.

---

### 🧩 Key Features

- 🔢 GPT‑style **token counting** for .NET  
- 🧱 Smart **text chunking** with configurable token limits and overlap  
- 💰 Real‑time **cost estimation** for prompt and completion usage  
- 🧮 **TokenUsageTracker** — track cumulative token and cost usage across analyses  
- 🧩 Unified **TokenFlowClient** — analyze, chunk, and cost in one API  
- ⚙️ **CLI utilities (TokenFlow.Tools)** — structured automation with `--format`, `--input`, and `--output` options  
- 📘 **Developer documentation site** — API reference + usage guides via [GitHub Pages](https://andrewclements84.github.io/TokenFlow.AI/)  
- 🧾 **Benchmark suite** powered by BenchmarkDotNet  
- 🔌 Pluggable **tokenizer providers** — now including OpenAI `tiktoken`, Claude `cl100k_base`, and Approx fallback  
- 🧠 Dual targeting for **.NET Standard 2.0** and **.NET 8.0**  

---

### 🚀 Installation

```bash
dotnet add package TokenFlow.AI
```

Or install the shared core contracts:

```bash
dotnet add package TokenFlow.Core
```

For advanced tokenizer support:

```bash
dotnet add package TokenFlow.Tokenizers
```

---

### 🧠 Quick Examples

**Using model-specific tokenizers:**

```csharp
using TokenFlow.Tokenizers.Factory;

var factory = new TokenizerFactory();
var gptTokenizer = factory.Create("gpt-4o");
var claudeTokenizer = factory.Create("claude-3-opus");

Console.WriteLine($"GPT tokens: {gptTokenizer.CountTokens("Hello world!")}");
Console.WriteLine($"Claude tokens: {claudeTokenizer.CountTokens("Hello world!")}");
```

**Benchmarking tokenizers:**

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
| **TokenFlow.AI** | 100% | Client, costing, and registry |
| **TokenFlow.Tokenizers** | 100% | OpenAI, Claude, and Approx implementations |
| **TokenFlow.Tools** | 100% | CLI automation and output formatting |

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

#### 🌟 Future Goals
- [ ] Integration with additional Flow.AI ecosystem components
- [ ] Performance regression tracking in CI
- [ ] Public release under Flow.AI suite branding

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
