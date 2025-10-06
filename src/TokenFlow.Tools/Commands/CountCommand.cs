using System;
using TokenFlow.AI.Tokenizer;

namespace TokenFlow.Tools.Commands
{
    public static class CountCommand
    {
        public static int Run(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to count tokens for.");
                return 1;
            }

            var tokenizer = new ApproxTokenizer();
            int count = tokenizer.CountTokens(text);

            Console.WriteLine($"Token count: {count}");
            return 0;
        }
    }
}

