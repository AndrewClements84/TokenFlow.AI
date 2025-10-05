# Contributing to TokenFlow.AI

Thank you for your interest in contributing to **TokenFlow.AI** — the core tokenization and cost estimation engine of the Flow.AI ecosystem.

We welcome all improvements, from code enhancements and documentation updates to bug reports and feature ideas.  
Together, we can make the Flow.AI suite a reliable foundation for AI-driven development in .NET.

---

## 🧩 How to Contribute

### 1. Fork and Clone
Fork this repository to your own GitHub account, then clone it locally:

```bash
git clone https://github.com/<your-username>/TokenFlow.AI.git
cd TokenFlow.AI
```

### 2. Create a Feature Branch
Always work from a new branch:

```bash
git checkout -b feature/my-feature-name
```

Use a clear name, e.g. `feature/add-tokenfactory`, `fix/chunking-loop`, or `docs/update-readme`.

---

## 🧪 Development Guidelines

- Use **.NET Standard 2.0** for library compatibility and **.NET 8** for tests.
- Follow consistent C# 7.3 syntax for cross-project support.
- All code **must compile with 0 warnings**.
- Maintain **100% test coverage** (run `dotnet test --collect:"XPlat Code Coverage"`).
- Keep tests small, isolated, and fast — each unit should test one behavior.
- Run all tests locally before submitting a PR.

---

## 🧱 Commit and Pull Requests

1. Write clear, descriptive commit messages.
2. Reference issues (if any) in your PR title or description.
3. Ensure your PR passes all CI checks:
   - ✅ Build
   - ✅ Test
   - ✅ Codecov coverage threshold

Once reviewed, your PR will be merged into `master` and included in the next tagged release.

---

## 💬 Reporting Issues

If you find a bug or have a feature idea:

1. Check existing [issues](../../issues) to avoid duplicates.
2. Open a new issue with:
   - A clear title
   - Expected vs. actual behavior
   - Reproduction steps (if applicable)
   - Environment details (.NET version, OS, etc.)

---

## 🪪 License

By contributing to **TokenFlow.AI**, you agree that your contributions will be licensed under the [MIT License](LICENSE).

---

> 💡 _“Flow code, not chaos.”_  
> — The Flow.AI Team
