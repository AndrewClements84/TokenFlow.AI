using TokenFlow.AI.Registry;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryIntegrationTests
    {
        [Fact]
        public void EmbeddedModels_ShouldLoad_AllCanonicalModels()
        {
            // Arrange
            var registry = new ModelRegistry();

            // Act
            var models = registry.GetAll();

            // Assert
            Assert.NotEmpty(models);
            Assert.Equal("Embedded", registry.LoadSource);

            // Expected canonical model IDs
            var expectedIds = new[]
            {
                "gpt-4o",
                "gpt-4o-mini",
                "gpt-5",
                "gpt-5-mini",
                "claude-opus-4",
                "claude-sonnet-4",
                "gemini-2.5-pro"
            };

            foreach (var id in expectedIds)
                Assert.Contains(models, m => m.Id == id);

            // Basic sanity checks on pricing
            Assert.All(models, m =>
            {
                Assert.True(m.InputPricePer1K >= 0, $"{m.Id} has negative input price");
                Assert.True(m.OutputPricePer1K >= 0, $"{m.Id} has negative output price");
            });

            // Ensure known providers are loaded
            var providers = models.Select(m => m.Family).Distinct().ToList();
            Assert.Contains("OpenAI", providers);
            Assert.Contains("Anthropic", providers);
            Assert.Contains("Google", providers);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectModel()
        {
            var registry = new ModelRegistry();

            var gpt5 = registry.GetById("gpt-5");

            Assert.NotNull(gpt5);
            Assert.Equal("OpenAI", gpt5.Family);
            Assert.True(gpt5.InputPricePer1K < gpt5.OutputPricePer1K);
        }
    }
}
