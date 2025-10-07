# 📜 TokenFlow.AI — Changelog

All notable changes to this project are documented here.  
This project follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)  
and adheres to [Semantic Versioning](https://semver.org/).

---

## [Unreleased]
### 🚧 In Progress
- Developer documentation site (API + usage guides)
- Expand CLI command options and argument handling
- Add advanced tokenizer providers (OpenAI `tiktoken`, Anthropic `Claude`)

---

## [0.3.5] — 2025-10-07
### ✨ New Features
- **Extended `ModelRegistry` to support JSON configuration loading** including:
  - Loading from embedded resources, local JSON files, and remote URLs
  - Direct in-memory JSON string parsing via `LoadFromJsonString`
  - Safe exception handling and early-return guards for malformed or empty JSON
- Added **ModelRegistryJsonLoader** and **ModelRegistryRemoteLoader** with full test coverage

### 🧹 Improvements
- Achieved **100% line and branch coverage** across all projects (verified via Codecov)
- Simplified loader exception handling to ensure consistent fault tolerance
- Updated unit tests for `ModelRegistry`, `ModelRegistryJsonLoader`, and `ModelRegistryRemoteLoader` to cover all defensive branches
- Added `[ExcludeFromCodeCoverage]` attribute to untestable embedded resource guards for stability

### ⚙️ DevOps & CI/CD
- Updated GitHub Actions workflow for deterministic Codecov reports
- Added `IncludePrivate=true` configuration in Coverlet to capture private method instrumentation
- Improved CI summary logs and badge reliability

### 🧱 Architecture
- `ModelRegistry` is now fully modular, supporting JSON-based model definitions
- Maintained `.NET Standard 2.0` compatibility with `.NET 8.0` test suite
- Consolidated Registry tests for clarity and maintainability

### 🧾 Documentation
- Updated **README.md** to mark JSON configuration loading as completed in roadmap
- Minor style and formatting consistency improvements

---

## [0.3.4] — 2025-10-06
### ✨ New Features
- Added **TokenFlow.Tools** — developer CLI utilities for command-line token, chunk, and cost analysis  
- Added **TokenFlow.Tools.Benchmarks** — BenchmarkDotNet performance suite for profiling tokenization and cost estimation  
- Introduced `TokenUsageTracker` for cumulative token and cost tracking across analyses  
- Implemented `ITokenizerFactory` for dynamic tokenizer resolution  

### 🧹 Improvements
- Maintained **100% code coverage** across all projects  
- Improved **ModelRegistry** with safer `TryGet` and `Add` handling  
- Refactored **CLI commands** (`count`, `chunk`, `cost`) for clearer output and error handling  
- Enhanced **TokenFlowClient** for consistent API responses  

### ⚙️ DevOps & CI/CD
- Updated `.github/workflows/dotnet.yml` with:
  - Benchmark build smoke test (compile-only verification)
  - Isolated publish steps for `TokenFlow.AI` and `TokenFlow.Core`
  - Improved job naming and clarity
- Ensured benchmark projects and CLI utilities are excluded from NuGet publishing  
- Validated benchmark compilation within CI builds  

### 🧱 Architecture
- Added new subprojects:  
  ```
  src/
    TokenFlow.Core/
    TokenFlow.AI/
    TokenFlow.Tools/
    TokenFlow.Tools.Benchmarks/
  tests/
    TokenFlow.Core.Tests/
    TokenFlow.AI.Tests/
    TokenFlow.Tools.Tests/
  ```
- Unified structure across build, test, and benchmark pipelines  
- Solidified CLI as developer utility layer (non-public NuGet target)

---

## [0.3.0] — 2025-10-06
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
