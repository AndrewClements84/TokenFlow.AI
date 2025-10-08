import sys
import json
from statistics import mean

# -----------------------------
# TokenFlow.AI Benchmark Comparator
# -----------------------------
# Usage:
#   python3 .github/scripts/compare_benchmarks.py baseline.json latest.json
# Fails the build if mean runtime exceeds baseline by >10%.

THRESHOLD = 1.10  # 10% slower allowed

def get_mean_time(entry):
    """Extract mean time from BenchmarkDotNet JSON entry."""
    stats = entry.get("Statistics", {})
    mean_val = stats.get("Mean")
    if isinstance(mean_val, list):
        return mean(mean_val)
    return float(mean_val) if mean_val else None

def compare_benchmarks(baseline_data, latest_data):
    """Compare each benchmark entry and report regressions."""
    regressions = []

    # Both files are lists of benchmark results
    for base_entry in baseline_data:
        bench_name = base_entry.get("FullName") or base_entry.get("DisplayInfo")
        base_mean = get_mean_time(base_entry)
        if base_mean is None:
            continue

        # Try to find matching benchmark in latest results
        match = next((b for b in latest_data if b.get("FullName") == bench_name), None)
        if not match:
            print(f"⚠️  Missing benchmark in latest results: {bench_name}")
            continue

        new_mean = get_mean_time(match)
        if new_mean is None:
            continue

        ratio = new_mean / base_mean
        if ratio > THRESHOLD:
            regressions.append((bench_name, ratio))

    if regressions:
        print("\n❌ Performance regressions detected:")
        for name, ratio in regressions:
            print(f"   {name}: {ratio:.2f}x slower than baseline")
        sys.exit(1)
    else:
        print("✅ No significant regressions detected.")
        sys.exit(0)

def main():
    if len(sys.argv) != 3:
        print("Usage: compare_benchmarks.py baseline.json latest.json")
        sys.exit(1)

    baseline_path, latest_path = sys.argv[1], sys.argv[2]

    with open(baseline_path, "r", encoding="utf-8") as f:
        baseline_data = json.load(f)
    with open(latest_path, "r", encoding="utf-8") as f:
        latest_data = json.load(f)

    compare_benchmarks(baseline_data, latest_data)

if __name__ == "__main__":
    main()

