# ⚙️ TokenFlow.Tools CLI — v2.1

The **TokenFlow.Tools CLI** enables quick token analysis, chunking, and cost estimation directly from your terminal.

---

### 🔢 Count Tokens
```bash
dotnet run --project src/TokenFlow.Tools -- count "Hello TokenFlow!"
```

### 🧱 Chunk Text
```bash
dotnet run --project src/TokenFlow.Tools -- chunk "Long text body here..."
```

### 💰 Estimate Cost
```bash
dotnet run --project src/TokenFlow.Tools -- cost "Estimate cost of this text."
```

### 🧩 Analyze (New in v2.1)
```bash
# Human-readable table
dotnet run --project src/TokenFlow.Tools -- analyze "TokenFlow CLI update!"

# JSON output
dotnet run --project src/TokenFlow.Tools -- analyze "Hello" --format json

# CSV output file
dotnet run --project src/TokenFlow.Tools -- analyze "Hello" --format csv --output result.csv

# Quiet mode for CI/CD
dotnet run --project src/TokenFlow.Tools -- analyze "Hello" --format quiet
```

---

### 🧰 Options

| Option | Description |
|--------|--------------|
| `--format` | Output type: `table`, `json`, `csv`, `quiet` |
| `--input` | Read text from a file instead of inline argument |
| `--output` | Write results to a file (CSV or JSON) |
| `--quiet` | Suppresses logs; suitable for scripts/CI |

---

### 🧩 Example Output (Table)

```text
Model ID        | TokenCount | EstimatedCost
---------------------------------------------
gpt-4o          | 22          | £0.00005
```

---

> 💡 **Tip:** Combine CLI commands in scripts or pipelines to analyze datasets, estimate costs, and generate reports automatically.

