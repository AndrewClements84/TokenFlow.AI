# ⚙️ TokenFlow.Tools CLI Usage

The `TokenFlow.Tools` CLI enables quick token analysis directly from your terminal.

## 🔢 Count Tokens

```bash
dotnet run --project src/TokenFlow.Tools -- count "Hello TokenFlow!"
```

## 🧱 Chunk Text

```bash
dotnet run --project src/TokenFlow.Tools -- chunk "Long text body here..."
```

## 💰 Estimate Cost

```bash
dotnet run --project src/TokenFlow.Tools -- cost "Estimate this text's price."
```

For advanced arguments, see `dotnet run -- --help`.
