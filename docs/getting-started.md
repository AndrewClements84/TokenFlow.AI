# 🚀 Getting Started with TokenFlow.AI

TokenFlow.AI is a lightweight .NET library for tokenization, chunking, and cost estimation across modern LLMs such as OpenAI GPT-4o, Anthropic Claude, and Azure OpenAI.

## 🔧 Installation

```bash
dotnet add package TokenFlow.AI
```

Or for the shared core contracts:

```bash
dotnet add package TokenFlow.Core
```

## 🧩 Example

```csharp
using TokenFlow.AI.Client;

var client = new TokenFlowClient("gpt-4o");
var result = client.AnalyzeText("Hello TokenFlow!");

Console.WriteLine($"Tokens: {result.TokenCount}");
Console.WriteLine($"Cost: £{result.EstimatedCost:F4}");
```

👉 See also [CLI Usage](cli-usage.md) and [Cost Tracking](cost-tracking.md).
