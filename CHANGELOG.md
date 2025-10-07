
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
- Added comprehensive test coverage for all tokenizers and factories.
- Achieved **100% line and branch coverage** across all tokenizer classes.
- Added new `TokenFlow.Tokenizers.Tests` project for modular test isolation.
- Extended test suite with null/empty edge case coverage for `ClaudeTokenizer` and `OpenAITikTokenizer`.
- Updated GitHub Actions pipeline and Codecov reports to include the new test assembly.

### 🧱 Architecture
- Extracted tokenizer logic from `TokenFlow.AI` into a standalone **TokenFlow.Tokenizers** package.
- Introduced modular structure with `Shared`, `OpenAI`, `Claude`, and `Factory` namespaces.
- Improved DI and code reuse through a shared `ITokenizer` interface in `TokenFlow.Core`.
- Benchmarks updated to include all tokenizers for comparative analysis.

### ⚡ Performance & Benchmarks
- Updated `TokenizerBenchmarks` to benchmark `ApproxTokenizer`, `OpenAITikTokenizer`, and `ClaudeTokenizer`.
- Added GitHub and HTML exporters for richer benchmark reporting.
- Improved consistency and memory tracking via `MemoryDiagnoser`.

### 📘 Documentation
- Added new **[Tokenizers documentation](docs/tokenizers.md)** with usage, benchmarks, and architecture overview.
- Updated project roadmap and README to reflect milestone completion (`Advanced Tokenizers` ✅).

### 🧹 Misc Improvements
- Simplified fallback logic and improved default model name handling.
- Removed redundant tokenizer code from `TokenFlow.AI`.
- Refined factory registration and detection logic.
- Improved CI/CD metadata and documentation consistency.

---

## [0.3.9] — 2025-10-09
### ✨ New Features
- **CLI v2.1** released — expanded command-line capabilities:
  - Added `--format` options (`table`, `json`, `csv`, `quiet`)
  - Added `--input` and `--output` file redirection
  - Introduced quiet mode with full log suppression (`TOKENFLOW_SILENT`)
  - Unified table + CSV formatting via new `OutputFormatter`
- Enhanced `AnalyzeCommand` and `Program` with structured automation
- Implemented robust file I/O and pipeline-safe JSON output

### 🧮 Output Enhancements
- Added aligned table output for readability
- Added CSV serialization with proper escaping
- Introduced silent/quiet mode for CI/CD automation
- Graceful handling of missing or invalid inputs

### 🧪 Testing & Coverage
- Extended test suite to cover:
  - All `--format`, `--input`, and `--output` variations
  - File-write and read exception branches
  - Fallback and conversion logic in `OutputFormatter`
- Achieved **100% line and branch coverage** across all projects
- Added `[ExcludeFromCodeCoverage]` for JIT-optimized fallback paths

### 🧱 Architecture
- Centralized all CLI formatting in `OutputFormatter`
- Unified command pattern across `Analyze`, `Compare`, `ListModels`
- Improved environment flag handling (`TOKENFLOW_SILENT`) for quiet mode
- Backwards-compatible with `.NET Standard 2.0` and `.NET 8.0`

### 🧹 Improvements
- Simplified and stabilized CLI workflows
- Refined developer docs and CLI usage examples
- Updated README roadmap and examples for CLI v2.1
