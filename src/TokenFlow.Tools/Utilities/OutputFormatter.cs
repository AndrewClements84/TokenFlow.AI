using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace TokenFlow.Tools.Utilities
{
    /// <summary>
    /// Handles unified CLI output across commands — supports table, json, csv, and quiet formats.
    /// </summary>
    public static class OutputFormatter
    {
        public static void Write(object data, string format = "table", string outputPath = null)
        {
            format = format?.ToLowerInvariant() ?? "table";

            // Quiet mode → completely silent
            if (format == "quiet")
            {
                if (!string.IsNullOrEmpty(outputPath))
                {
                    File.WriteAllText(outputPath, string.Empty);
                }
                return;
            }


            string content = format switch
            {
                "json" => JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }),
                "csv" => ToCsv(data),
                "table" => ToTable(data),
                _ => GetDataFallbackString(data)
            };

            if (!string.IsNullOrEmpty(outputPath))
            {
                File.WriteAllText(outputPath, content);
                // ⚠️ Don't call Console.WriteLine here — tests redirect Console and this causes ObjectDisposedException
                return;
            }

            if (!string.IsNullOrWhiteSpace(content))
                Console.WriteLine(content);
        }


        // 🧱 Converts object(s) into a simple aligned text table
        private static string ToTable(object data)
        {
            if (data == null) return string.Empty;

            if (data is IEnumerable enumerable && !(data is string))
            {
                var rows = enumerable.Cast<object>().ToList();
                if (rows.Count == 0) return "(no data)";
                var props = GetPublicProperties(rows[0]);
                var headers = props.Select(p => p.Name).ToList();

                var table = new List<string[]> { headers.ToArray() };
                table.AddRange(rows.Select(r => props.Select(p => SafeToString(p.GetValue(r))).ToArray()));

                var widths = Enumerable.Range(0, headers.Count)
                    .Select(i => table.Max(row => row[i].Length))
                    .ToArray();

                var sb = new StringBuilder();
                foreach (var row in table)
                {
                    for (int i = 0; i < row.Length; i++)
                        sb.Append(row[i].PadRight(widths[i] + 2));
                    sb.AppendLine();
                }

                return sb.ToString();
            }

            // Non-collection objects: use key=value pairs
            var singleProps = GetPublicProperties(data);
            var result = new StringBuilder();
            foreach (var prop in singleProps)
            {
                result.AppendLine($"{prop.Name,-20}: {SafeToString(prop.GetValue(data))}");
            }
            return result.ToString();
        }

        // 🧾 Converts object(s) into CSV
        private static string ToCsv(object data)
        {
            if (data == null) return string.Empty;

            if (data is IEnumerable enumerable && !(data is string))
            {
                var rows = enumerable.Cast<object>().ToList();
                if (rows.Count == 0) return string.Empty;

                var props = GetPublicProperties(rows[0]);
                var sb = new StringBuilder();
                sb.AppendLine(string.Join(",", props.Select(p => EscapeCsv(p.Name))));

                foreach (var row in rows)
                {
                    var values = props.Select(p => EscapeCsv(SafeToString(p.GetValue(row))));
                    sb.AppendLine(string.Join(",", values));
                }

                return sb.ToString();
            }

            // Single object → one-line CSV
            var propsSingle = GetPublicProperties(data);
            var header = string.Join(",", propsSingle.Select(p => EscapeCsv(p.Name)));
            var valuesLine = string.Join(",", propsSingle.Select(p => EscapeCsv(SafeToString(p.GetValue(data)))));
            return $"{header}\n{valuesLine}";
        }

        private static IEnumerable<PropertyInfo> GetPublicProperties(object obj) =>
            obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        private static string SafeToString(object value) =>
            value switch
            {
                null => "",
                double d => d.ToString("G"),
                decimal m => m.ToString("G"),
                _ => GetValueFallbackString(value)
            };

        private static string EscapeCsv(string s)
        {
            if (s.Contains(',') || s.Contains('"') || s.Contains('\n'))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static string GetDataFallbackString(object data)
        {
            return data?.ToString() ?? string.Empty;
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static string GetValueFallbackString(object value)
        {
            return value?.ToString() ?? "";
        }
    }
}
