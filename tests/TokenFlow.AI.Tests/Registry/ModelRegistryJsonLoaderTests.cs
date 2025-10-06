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
    }
}

