# 📜 TokenFlow.AI — Changelog

All notable changes to this project will be documented in this file.  
The format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)  
and adheres to [Semantic Versioning](https://semver.org/).

---

## [Unreleased]
### 🚧 In Progress
- Adding `TokenFlowClient` unified API
- Introducing `TokenUsageTracker` for cumulative cost tracking
- JSON-based model configuration in `ModelRegistry`
- CLI tooling via `TokenFlow.Tools`
- Benchmarking suite for tokenizer and chunker performance

---

## [0.2.0-dev] — 2025-10-05
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
- Established modular folder structure:
  ```
  src/
    TokenFlow.Core/
    TokenFlow.AI/
  tests/
    TokenFlow.Core.Tests/
    TokenFlow.AI.Tests/
  ```
- All components reference shared interfaces and models from `TokenFlow.Core`.

---

## [0.1.0] — Initial Commit
### 🎉 First Release
- Introduced project foundation and CI/CD skeleton
- Implemented `ApproxTokenizer`, `CostEstimator`, and `ModelRegistry`
- Published initial NuGet metadata and README

---

> _Generated automatically as part of the Flow.AI development roadmap._  
> For full commit history, see the [GitHub repository](https://github.com/AndrewClements84/TokenFlow.AI/commits/master).
