# 🧩 ModelRegistry Overview

The **ModelRegistry** manages all available language models, their token limits, and pricing metadata.  
It provides a unified source of truth for tokenization, cost calculation, and configuration.

---

### 💡 Features
- Load model specifications from **embedded resources**
- Load from **local JSON files**
- Load from **remote URLs**
- Load directly from **in-memory JSON strings**
- Defensive error handling and fault-tolerant loading

---

### 🧱 Example

```csharp
using TokenFlow.AI.Registry;

var registry = new ModelRegistry();
var model = registry.TryGet("gpt-4o");

Console.WriteLine($"{model.Id} supports up to {model.MaxInputTokens} tokens.");
```

---

### 🧾 JSON Configuration Example

You can now load model metadata dynamically from a JSON string, file, or remote URL.

```csharp
using TokenFlow.AI.Registry;

var registry = new ModelRegistry();
registry.LoadFromJsonString("[{ "Id": "custom-model", "Family": "openai", "TokenizerName": "tiktoken", "MaxInputTokens": 10000, "MaxOutputTokens": 2000, "InputPricePer1K": 0.01, "OutputPricePer1K": 0.02 }]");

var model = registry.TryGet("custom-model");
Console.WriteLine($"{model.Id}: {model.Family} — {model.MaxInputTokens} tokens");
```

✅ Supports:
- Embedded resource defaults  
- Local file paths  
- Remote URLs  
- Raw in-memory JSON strings

---

### 🧪 Testing

All registry loaders (`ModelRegistryJsonLoader`, `ModelRegistryRemoteLoader`) are covered by unit tests with 100% coverage.  
Invalid or malformed JSON is safely ignored to ensure runtime stability.

```
