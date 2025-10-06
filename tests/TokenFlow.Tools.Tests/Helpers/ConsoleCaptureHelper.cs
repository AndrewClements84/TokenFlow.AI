using System;
using System.IO;

namespace TokenFlow.Tools.Tests.Helpers
{
    public static class ConsoleCaptureHelper
    {
        public static string Capture(Action action)
        {
            var original = Console.Out;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                action();
                Console.SetOut(original);
                return sw.ToString();
            }
        }
    }
}
