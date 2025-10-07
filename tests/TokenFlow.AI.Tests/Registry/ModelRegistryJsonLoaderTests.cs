using System.IO;
using TokenFlow.AI.Registry;
using Xunit;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryJsonLoaderTests
    {
        [Fact]
        public void LoadFromFile_ShouldReturnModels_WhenValidJson()
        {
            string json = "[{\"Id\":\"test-model\",\"Family\":\"mock\",\"TokenizerName\":\"approx\",\"MaxInputTokens\":100}]";
            string path = Path.GetTempFileName();
            File.WriteAllText(path, json);

            var models = ModelRegistryJsonLoader.LoadFromFile(path);
            Assert.Single(models);
            Assert.Equal("test-model", models[0].Id);

            File.Delete(path);
        }

        [Fact]
        public void LoadFromFile_ShouldReturnEmpty_WhenFileMissing()
        {
            var models = ModelRegistryJsonLoader.LoadFromFile("doesnotexist.json");
            Assert.Empty(models);
        }

        [Fact]
        public void LoadFromFile_ShouldReturnEmpty_WhenInvalidJson()
        {
            string path = Path.GetTempFileName();
            File.WriteAllText(path, "{ this is not valid json }");

            var models = ModelRegistryJsonLoader.LoadFromFile(path);
            Assert.Empty(models);

            File.Delete(path);
        }

        [Fact]
        public void LoadFromFile_ShouldReturnEmpty_WhenEmptyFile()
        {
            string path = Path.GetTempFileName();
            File.WriteAllText(path, "");

            var models = ModelRegistryJsonLoader.LoadFromFile(path);
            Assert.Empty(models);

            File.Delete(path);
        }

        [Fact]
        public void LoadFromFile_ShouldReturnNewList_WhenDeserializedNull()
        {
            // Arrange
            string path = Path.GetTempFileName();
            File.WriteAllText(path, "null");

            // Act
            var result = ModelRegistryJsonLoader.LoadFromFile(path);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // should trigger `return models ?? new List<ModelSpec>()`

            File.Delete(path);
        }

        [Fact]
        public void LoadFromStream_ShouldReturn_WhenStreamIsNull()
        {
            // Act
            ModelRegistry.LoadFromStream(null);

            // Assert: nothing thrown means coverage hit
            Assert.True(true);
        }
    }
}

