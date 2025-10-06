using TokenFlow.AI.Registry;
using Xunit;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryRemoteLoaderTests
    {
        [Fact]
        public void RemoteLoader_ShouldReturnNull_ForInvalidUrl()
        {
            var models = ModelRegistryRemoteLoader.LoadFromUrl("https://invalid.url/doesnotexist.json");
            Assert.Null(models);
        }

        [Fact]
        public void RemoteLoader_ShouldHandleEmptyUrl()
        {
            var models = ModelRegistryRemoteLoader.LoadFromUrl("");
            Assert.Null(models);
        }

        [Fact]
        public void RemoteLoader_ShouldLoad_FromLocalFileUri()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, @"[
                { ""Id"": ""test-model"", ""Family"": ""mock"", ""TokenizerName"": ""approx"", ""MaxInputTokens"": 1000, ""InputCostPer1K"": 0.001, ""OutputCostPer1K"": 0.002 }
            ]");
            var uri = new Uri(tempFile);

            // Act
            var models = ModelRegistryRemoteLoader.LoadFromUrl(uri.ToString());

            // Assert
            Assert.NotNull(models);
            Assert.Single(models);
            Assert.Equal("test-model", models[0].Id);

            // Cleanup
            File.Delete(tempFile);
        }
    }
}

