namespace TokenFlow.AI.Integration
{
    /// <summary>
    /// Temporary placeholder for the Flow.AI.Core ITokenFlowProvider contract.
    /// </summary>
    public interface ITokenFlowProvider
    {
        int CountTokens(string modelId, string text);
    }
}
