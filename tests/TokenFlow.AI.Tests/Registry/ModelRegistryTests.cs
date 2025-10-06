using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryTests
    {
        [Fact]
        public void LoadFromJsonString_ShouldRegisterModels()
        {
            var json = @"[
                { 'Id':'test-model', 'Family':'test', 'TokenizerName':'approx', 'MaxInputTokens':100, 'InputCost':0.01, 'OutputCost':0.02 }
            ]";

            var registry = new ModelRegistry();
            registry.LoadFromJsonString(json);

            var model = registry.GetById("test-model");
            Assert.NotNull(model);
            Assert.Equal("test", model.Family);
        }

        [Fact]
        public void LoadFromJsonFile_ShouldRegisterModels()
        {
            var json = @"[
                { 'Id':'file-model', 'Family':'openai', 'TokenizerName':'approx', 'MaxInputTokens':200, 'InputCost':0.02, 'OutputCost':0.03 }
            ]";

            var path = Path.GetTempFileName();
            File.WriteAllText(path, json);

            var registry = new ModelRegistry();
            registry.LoadFromJsonFile(path);

            var model = registry.GetById("file-model");
            Assert.NotNull(model);
            Assert.Equal("openai", model.Family);

            File.Delete(path);
        }

        [Fact]
        public void LoadFromJsonString_ShouldIgnoreMalformedJson()
        {
            var registry = new ModelRegistry();
            registry.LoadFromJsonString("INVALID_JSON");

            Assert.NotNull(registry.GetById("gpt-4o")); // defaults remain intact
        }

        [Fact]
        public void LoadFromJsonFile_ShouldIgnoreMissingFile()
        {
            var registry = new ModelRegistry();
            registry.LoadFromJsonFile("does_not_exist.json");

            Assert.NotNull(registry.GetById("gpt-4o"));
        }

        [Fact]
        public void Register_ShouldReplace_ExistingModel()
        {
            var registry = new ModelRegistry();
            var original = registry.GetById("gpt-4o");

            var replacement = new ModelSpec("gpt-4o", "test", "approx", 10, 5, 0.1m, 0.2m);
            registry.Register(replacement);

            var updated = registry.GetById("gpt-4o");
            Assert.Equal("test", updated.Family);
        }

        [Fact]
        public void TryGet_ShouldReturnTrue_WhenModelExists()
        {
            var registry = new ModelRegistry();
            bool found = registry.TryGet("gpt-4o", out var model);

            Assert.True(found);
            Assert.NotNull(model);
            Assert.Equal("openai", model.Family);
        }

        [Fact]
        public void TryGet_ShouldReturnFalse_WhenModelMissing()
        {
            var registry = new ModelRegistry();
            bool found = registry.TryGet("does-not-exist", out var model);

            Assert.False(found);
            Assert.Null(model);
        }

        [Fact]
        public void Register_ShouldIgnore_NullModel()
        {
            var registry = new ModelRegistry();

            // Act
            registry.Register(null);

            // Assert
            var models = registry.GetAll();
            Assert.NotEmpty(models); // defaults still exist
            Assert.Contains(models, m => m.Id == "gpt-4o");
        }

        [Fact]
        public void Register_ShouldIgnore_ModelWithEmptyId()
        {
            var registry = new ModelRegistry();
            var invalid = new ModelSpec("", "test", "approx", 10, 5, 0.1m, 0.2m);

            registry.Register(invalid);

            var found = registry.GetById("");
            Assert.Null(found);
        }

        [Fact]
        public void GetAll_ShouldReturn_ReadOnlyList()
        {
            var registry = new ModelRegistry();
            var models = registry.GetAll();

            Assert.NotNull(models);
            Assert.NotEmpty(models);

            // Verify immutability by ensuring it cannot be cast back and modified
            var asList = models as List<ModelSpec>;
            Assert.Null(asList); // Should not be an underlying List<ModelSpec>

            // Verify that adding would throw if attempted
            Assert.ThrowsAny<NotSupportedException>(() =>
            {
                var collection = (System.Collections.IList)models;
                collection.Add(new ModelSpec("test", "test", "approx", 1, 1, 0.1m, 0.2m));
            });
        }

        [Fact]
        public void LoadFromJsonString_ShouldIgnore_NullOrEmpty()
        {
            var registry = new ModelRegistry();

            registry.LoadFromJsonString(null);
            registry.LoadFromJsonString(string.Empty);
            registry.LoadFromJsonString("   ");

            // Assert: defaults remain unchanged
            var model = registry.GetById("gpt-4o");
            Assert.NotNull(model);
        }

        [Fact]
        public void LoadFromJsonString_ShouldIgnore_NullDeserializationResult()
        {
            var registry = new ModelRegistry();

            // Passing "null" literal as JSON results in null deserialization
            registry.LoadFromJsonString("null");

            var existing = registry.GetById("gpt-4o");
            Assert.NotNull(existing);
        }

        [Fact]
        public void LoadFromJsonFile_ShouldIgnore_NullOrWhitespacePath()
        {
            var registry = new ModelRegistry();

            registry.LoadFromJsonFile(null);
            registry.LoadFromJsonFile("");
            registry.LoadFromJsonFile("   ");

            // Still have default models
            Assert.NotNull(registry.GetById("gpt-4o"));
        }
    }
}
