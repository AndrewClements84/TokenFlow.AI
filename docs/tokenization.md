# 🔢 Tokenization

TokenFlow.AI provides accurate token counting for GPT-style models.

## Example

```csharp
using TokenFlow.AI.Tokenization;

var tokenizer = new ApproxTokenizer();
int count = tokenizer.CountTokens("TokenFlow.AI makes prompt engineering easier!");
Console.WriteLine($"Token count: {count}");
```

Tokenizers are pluggable; use `ITokenizerFactory` to select between OpenAI, Anthropic, or custom tokenizers.
