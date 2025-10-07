using TokenFlow.AI.Integration;

namespace TokenFlow.Integration.Tests
{
    public class TokenFlowProviderTests
    {
        [Fact]
        public void Should_Return_TokenCount_From_Provider()
        {
            var provider = new TokenFlowProvider("gpt-4o-mini");
            int count = provider.CountTokens("gpt-4o-mini", "Integration readiness test");
            Assert.True(count > 0);
        }
    }
}
