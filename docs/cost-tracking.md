# 💰 Cost Tracking

Track token and cost usage across multiple analyses.

```csharp
using TokenFlow.AI.Tracking;

var tracker = new TokenUsageTracker(new TokenFlowClient("gpt-4o").GetModel());

tracker.RecordText("Hello world!");
tracker.RecordText("Estimate cost of this text...");

var summary = tracker.GetSummary();
Console.WriteLine($"Total Tokens: {summary.TotalTokens}, Total Cost: £{summary.TotalCost:F4}");
```

Useful for batch processing, analytics, or dashboards.
