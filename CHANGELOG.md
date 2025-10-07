# 📜 TokenFlow.AI — Changelog

All notable changes to this project are documented here.  
This project follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)  
and adheres to [Semantic Versioning](https://semver.org/).

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
