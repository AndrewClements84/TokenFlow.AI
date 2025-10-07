using System;
using System.IO;
using TokenFlow.AI.Client;
using TokenFlow.AI.Registry;
using TokenFlow.Tools.Utilities;

namespace TokenFlow.Tools.Commands
{
    /// <summary>
    /// Performs full text analysis — token count, cost, and model info.
    /// Supports multiple output formats and optional file redirection.
    /// </summary>
    public static class AnalyzeCommand
    {
        public static int Run(
            string text,
            IModelRegistry registry = null,
            string format = "table",
            string modelId = "gpt-4o",
            string outputPath = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("Please provide text to analyze.");
                    return 1;
                }

                // 🧩 Quiet mode toggle — suppress registry logs
                bool wasSilent = false;
                if (format.Equals("quiet", StringComparison.OrdinalIgnoreCase))
                {
                    wasSilent = true;
                    Environment.SetEnvironmentVariable("TOKENFLOW_SILENT", "1");
                }

                registry ??= new ModelRegistry();
                var client = new TokenFlowClient(modelId);
                var result = client.AnalyzeText(text);

                // Ensure JSON output is clean for pipelines (no preamble)
                if (format.Equals("json", StringComparison.OrdinalIgnoreCase))
                    Console.SetOut(TextWriter.Synchronized(Console.Out));

                if (format.Equals("table", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Model: {result.ModelId}");
                    Console.WriteLine($"Tokens: {result.TokenCount}");
                    Console.WriteLine($"Estimated cost: £{result.EstimatedCost:F4}");
                }
                else
                {
                    OutputFormatter.Write(result, format, outputPath);
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
            finally
            {
                // 🧩 Always clear the silent flag
                Environment.SetEnvironmentVariable("TOKENFLOW_SILENT", null);
            }
        }
    }
}
