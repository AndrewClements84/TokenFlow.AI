# 🧩 ModelRegistry Overview

`ModelRegistry` manages available LLM models, token limits, and pricing.

## 💡 Features

- Load models from:
  - Embedded JSON (`TokenFlow.AI.Data.models.data`)
  - Local JSON files
  - Remote URLs
  - In-memory JSON strings via `LoadFromJsonString`
- Safe exception handling and defensive fallbacks
- Fully tested and verified (100% coverage)

## 🧱 Example

```csharp
var registry = new ModelRegistry();
var model = registry.TryGet("gpt-4o");
Console.WriteLine($"{model.Id} supports up to {model.MaxInputTokens} tokens.");
```

## 🧪 JSON Configuration Example

```json
[
  {
    "Id": "gpt-4o",
    "Family": "openai",
    "TokenizerName": "tiktoken",
    "MaxInputTokens": 128000,
    "MaxOutputTokens": 4096,
    "InputPricePer1K": 0.01,
    "OutputPricePer1K": 0.03
  }
]
```
