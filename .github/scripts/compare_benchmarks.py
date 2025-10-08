import sys
import json
from statistics import mean

THRESHOLD = 1.10  # 10% slower allowed

def extract_entries(data):
    """Flattens JSON data to a list of benchmark entries."""
    entries = []

    if isinstance(data, list):
        for item in data:
            if isinstance(item, dict):
                entries.append(item)
            elif isinstance(item, list):
                entries.extend(extract_entries(item))
            elif isinstance(item, str):
                try:
                    parsed = json.loads(item)
                    entries.extend(extract_entries(parsed))
                except Exception:
                    continue
    elif isinstance(data, dict):
        entries.append(data)
    else:
        try:
            parsed = json.loads(data)
            entries.extend(extract_entries(parsed))
        except Exception:
            pass

    return entries


def get_mean_time(entry):
    """Extract mean time from a BenchmarkDotNet JSON entry."""
    stats = entry.get("Statistics", {})
    mean_val = stats.get("Mean")
    if isinstance(mean_val, list):
        return mean(mean_val)
    try:
        return float(mean_val)
    except (TypeError, ValueError):
        return None


def compare_benchmarks(baseline_data, latest_data):
    base_entries = extract_entries(baseline_data)
    latest_entries = extract_entries(latest_data)
    regressions = []

    for base_entry in base_entries:
        bench_name = base_entry.get("FullName") or base_entry.get("DisplayInfo") or base_entry.get("Title")
        base_mean = get_mean_time(base_entry)
        if bench_name is None or base_mean is None:
            continue

        match = next((b for b in latest_entries if (b.get("FullName") or b.get("DisplayInfo") or b.get("Title")) == bench_name), None)
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
