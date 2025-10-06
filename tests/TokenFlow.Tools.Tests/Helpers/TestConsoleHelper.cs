namespace TokenFlow.Tools.Tests.Helpers
{
    internal static class TestConsoleHelper
    {
        /// <summary>
        /// Captures all Console.Out text during the provided action and returns it.
        /// Ensures thread safety and automatic restoration of Console state.
        /// </summary>
        public static string CaptureOutput(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (typeof(Console)) // prevent concurrent writes from other threads
            {
                var original = Console.Out;
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    action();
                    Console.Out.Flush();
                    Console.SetOut(original);
                    return sw.ToString();
                }
            }
        }
    }
}

