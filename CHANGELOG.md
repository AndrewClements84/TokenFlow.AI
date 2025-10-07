
# 📜 TokenFlow.AI — Changelog

All notable changes to this project are documented here.  
This project follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)  
and adheres to [Semantic Versioning](https://semver.org/).

---

## [0.5.0] — 2025-10-07
### ✨ New Features
- **Advanced Tokenizers** module (`TokenFlow.Tokenizers`) introduced:
  - Added `OpenAITikTokenizer` with full [TiktokenSharp](https://www.nuget.org/packages/TiktokenSharp) support.
  - Added `ClaudeTokenizer` using [SharpToken](https://www.nuget.org/packages/SharpToken) for Anthropic models.
  - Added `ApproxTokenizer` migration into the new Tokenizers module.
  - Implemented unified `TokenizerFactory` with prefix-based model detection (`gpt-*`, `claude-*`, fallback `approx`).
- Created new test project **TokenFlow.Tokenizers.Tests** with full branch coverage.
- Integrated **TokenFlow.Tokenizers** into the main build and CI pipeline.

### 🧪 Testing & Coverage
- Achieved **100% line and branch coverage** across all tokenizer classes.
- Added edge case tests for null, empty, and malformed inputs.
- Extended coverage reporting and Codecov integration for the new test project.

### 🧱 Architecture
- Extracted tokenizer logic from `TokenFlow.AI` into standalone **TokenFlow.Tokenizers** package.
- Introduced modular structure with `Shared`, `OpenAI`, `Claude`, and `Factory` namespaces.
- Benchmarks updated to include all tokenizers for comparative analysis.

### ⚡ Performance & Benchmarks
- Updated `TokenizerBenchmarks` to include OpenAI, Claude, and Approx implementations.
- Added Markdown and HTML exporters for richer reports.

### 📘 Documentation
- Added new **[Tokenizers documentation](docs/tokenizers.md)** with examples, benchmarks, and architecture overview.
- Updated roadmap and README for milestone completion (`Advanced Tokenizers` ✅).

### 🧹 Misc Improvements
- Simplified fallback logic and default model name handling.
- Improved CI/CD metadata and documentation consistency.

---

## [0.4.0] — 2025-10-01
### ✨ New Features
- **CLI v2.1** released — expanded command-line capabilities:
  - Added `--format` options (`table`, `json`, `csv`, `quiet`)
  - Added `--input` and `--output` file redirection
  - Introduced quiet mode with full log suppression (`TOKENFLOW_SILENT`)
- Added structured output formatting via `OutputFormatter`.

### 🧪 Testing & Coverage
- Achieved **100% line and branch coverage** across all CLI projects.
- Added new tests for CSV, JSON, and table output formatting.

### 🧱 Architecture
- Unified CLI commands (`Analyze`, `Count`, `Chunk`, `Cost`).
- Simplified environment flag handling for automation pipelines.

### 🧹 Improvements
- Streamlined developer experience with enhanced CLI usage docs.
- Improved CI pipeline reliability and test reporting.

---

## [0.3.0] — 2025-09-10
### ✨ New Features
- Added **ModelRegistry** system with JSON and remote loading capabilities.
- Introduced **CostEstimator** for real-time cost calculations per token.
- Implemented **TokenFlowClient** — unified API for tokenization and costing.
- Added **TokenUsageTracker** for cumulative usage monitoring.

### 🧪 Testing & Coverage
- Full test suite for registry, costing, and client logic.
- Achieved 100% test coverage across all core projects.

### 🧱 Architecture
- Added `TokenFlow.Core` project for shared models and interfaces.
- Introduced `ModelSpec`, `TokenCountResult`, and `ICostEstimator` contracts.

---

## [0.2.0] — 2025-08-15
### ✨ New Features
- Added **TokenFlow.Tools.Benchmarks** project powered by BenchmarkDotNet.
- Added initial **CLI tools** project for developers.
- Implemented first version of `ApproxTokenizer` for approximate counting.

### 🧪 Testing
- Added initial xUnit suite for tokenizer and CLI.
- Integrated Coverlet and Codecov for automated coverage.

### 🧱 Architecture
- Introduced modular solution layout with `src/` and `tests/` directories.
- Added GitHub Actions for automated CI/CD.

---

## [0.1.0] — 2025-07-20
### 🚀 Initial Release
- Project initialized with **TokenFlow.AI** core logic.
- Implemented basic `ITokenizer` interface and prototype `ApproxTokenizer`.
- Added foundational cost estimation model.
- Established dual targeting for `.NET Standard 2.0` and `.NET 8.0`.
- Created repository structure, README, and MIT License.
