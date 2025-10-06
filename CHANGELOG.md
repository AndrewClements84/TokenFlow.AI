# 📜 TokenFlow.AI — Changelog

All notable changes to this project are documented here.  
This project follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)  
and adheres to [Semantic Versioning](https://semver.org/).

---

## [Unreleased]
### 🚧 In Progress
- Introduce `TokenUsageTracker` for cumulative cost tracking
- Implement `ITokenizerFactory` for dynamic tokenizer resolution
- Extend `ModelRegistry` to support JSON configuration loading
- CLI utilities via `TokenFlow.Tools`
- Benchmark suite using BenchmarkDotNet

---

## [0.3.1] — 2025-10-06
### 🧩 Packaging & CI/CD Fixes
- Fixed NuGet workflow to **only publish intended packages** (`TokenFlow.AI` and `TokenFlow.Core`)
- Updated GitHub Actions workflow to pack projects explicitly instead of the full solution
- Marked internal libraries (`TokenFlow.Tools`, `TokenFlow.Tokenizers`) as `<IsPackable>false>`
- Ensured version metadata (`<Version>0.3.0</Version>`) is respected across builds
- Verified release pipeline builds cleanly for **.NET Standard 2.0** and **.NET 8.0** targets

### 🧹 Cleanup
- Unlisted unintended packages from NuGet.org
- Improved artifact output organization under `/artifacts`
- Retained 100% test coverage and Codecov integration after pipeline update

---

## [0.3.0] — 2025-10-05
### ✨ New Features
- Introduced **TokenFlowClient** — unified high-level API for token counting, chunking, and cost estimation
- Added **TokenAnalysisResult** model to standardize analysis outputs
- Added full **xUnit test suite** for TokenFlowClient and supporting models
- Extended **TokenChunker** with overlap handling and improved safety checks
- Achieved **dual targeting**: `.NET Standard 2.0` and `.NET 8.0`

### 🧹 Improvements
- Maintained **100% test coverage** across all projects
- Enhanced **ApproxTokenizer** with null-safe and edge-case handling
- Refactored **CostEstimator** and **ModelRegistry** for consistency
- Expanded **README.md** with architecture examples and roadmap
- Improved **NuGet packaging** metadata and icon embedding

### ⚙️ DevOps & CI/CD
- Added GitHub Actions pipeline for automated builds, tests, and Codecov reporting
- Automated NuGet publishing on tagged releases
- Scoped API key and namespace configuration for secure publishing
- Artifacts organized under `/artifacts` output folder

### 🧱 Architecture
- Established clean modular folder structure:
  ```
  src/
    TokenFlow.Core/
    TokenFlow.AI/
  tests/
    TokenFlow.Core.Tests/
    TokenFlow.AI.Tests/
  ```
- All core components reference shared interfaces and models from `TokenFlow.Core`

---

## [0.2.0-dev] — 2025-10-04
### ✨ New Features
- Implemented `TokenChunker` for token-aware text segmentation
- Added `Chunk` model with token count and position tracking
- Integrated overlap handling between chunks
- Added comprehensive xUnit test suite with full coverage

### 🧹 Improvements
- Achieved **100% line and branch test coverage**
- Enhanced `ApproxTokenizer` with null-safe handling
- Improved defensive logic in `TokenChunker`
- Expanded README and roadmap

### ⚙️ DevOps
- Added GitHub Actions CI/CD workflow with Codecov integration
- Automated NuGet publishing on tagged releases
- Scoped API key and namespace claim guidance included

### 🧱 Architecture
- Established modular folder structure and test isolation
- All components reference shared interfaces from `TokenFlow.Core`

---

## [0.1.0] — Initial Commit
### 🎉 First Release
- Introduced project foundation and CI/CD skeleton
- Implemented `ApproxTokenizer`, `CostEstimator`, and `ModelRegistry`
- Published initial NuGet metadata and README

---

> _Generated automatically as part of the Flow.AI development roadmap._  
> For full commit history, see the [GitHub repository](https://github.com/AndrewClements84/TokenFlow.AI/commits/master).
