# 🧠 Flow.AI.Core Integration

TokenFlow.AI now integrates directly with the shared **Flow.AI.Core** framework, forming the engine layer of the **Flow.AI ecosystem**.

---

## ✅ Purpose

The Flow.AI.Core integration standardizes how tokenization and cost estimation are shared across all Flow.AI ecosystem projects, including:

- **PromptStream.AI** — prompt composition and validation toolkit  
- **DataFlow.AI** — structured AI data pipelines  
- **ChatFlow.AI** — conversational orchestration framework  
- **ReasonFlow.AI** — reasoning and multi-step tool orchestration  

---

## 🔌 Example Usage

```csharp
using Flow.AI.Core.Interfaces;
using TokenFlow.AI.Integration;

ITokenFlowProvider provider = new TokenFlowProvider("gpt-4o-mini");
int tokens = provider.CountTokens("gpt-4o-mini", "Hello Flow.AI!");
Console.WriteLine($"Token count: {tokens}");
```

This enables unified token counting, cost estimation, and analytics across all Flow.AI projects.

---

## 🧩 Core Interfaces

| Interface | Description |
|------------|--------------|
| `ITokenFlowProvider` | Main integration interface for token and cost analysis |
| `IModelRegistry` | Provides centralized access to model configurations |
| `ICostEstimator` | Computes cost based on token usage and model pricing |
| `ITokenizer` | Defines tokenizer behavior per model family |

---

## 🧱 Architecture Overview

```
+---------------------+
|     Flow.AI.Core    |  ← Shared interfaces and models
+----------+----------+
           |
           v
+---------------------+
|    TokenFlow.AI     |  ← Engine layer for tokenization & costing
+----------+----------+
           |
           v
+---------------------+
|   PromptStream.AI   |  ← Cockpit layer for prompt orchestration
+---------------------+
```

---

> 🔗 TokenFlow.AI serves as the **engine** of the Flow.AI ecosystem — powering token counting, chunking, and cost tracking across all higher-level modules.
