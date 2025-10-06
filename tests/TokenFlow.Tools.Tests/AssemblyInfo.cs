using Xunit;

// Disable parallel test execution *only for this assembly*.
// CLI commands all write to Console.Out, so this prevents cross-test contamination.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
