using TokenFlow.Core.Models;

namespace TokenFlow.Core.Tests.Models
{
    public class ModelSpecDataTests
    {
        [Fact]
        public void Provider_ShouldMapToFamily()
        {
            var data = new ModelSpecData();
            data.Provider = "OpenAI";

            Assert.Equal("OpenAI", data.Family);
            Assert.Equal("OpenAI", data.Provider);
        }

        [Fact]
        public void MaxTokens_ShouldMapToMaxInputTokens_WhenPositive()
        {
            var data = new ModelSpecData { MaxTokens = 5000 };

            Assert.Equal(5000, data.MaxInputTokens);
            Assert.Equal(5000, data.MaxTokens);
        }

        [Fact]
        public void PromptCostPer1K_ShouldAssignInputPrice_WhenZero()
        {
            var data = new ModelSpecData
            {
                InputPricePer1K = 0m
            };

            data.PromptCostPer1K = 0.123m;

            Assert.Equal(0.123m, data.InputPricePer1K);
        }

        [Fact]
        public void CompletionCostPer1K_ShouldAssignOutputPrice_WhenZero()
        {
            var data = new ModelSpecData
            {
                OutputPricePer1K = 0m
            };

            data.CompletionCostPer1K = 0.456m;

            Assert.Equal(0.456m, data.OutputPricePer1K);
        }

        [Fact]
        public void ToModelSpec_ShouldDefaultUnknownFamily_AndDefaultTokenizer()
        {
            var data = new ModelSpecData
            {
                Id = "test-model",
                Family = "",
                TokenizerName = ""
            };

            var spec = data.ToModelSpec();

            Assert.Equal("unknown", spec.Family);
            Assert.Equal("default", spec.TokenizerName);
            Assert.Equal("test-model", spec.Id);
        }

        [Fact]
        public void Name_ShouldBeSettable_AndIgnoredInToModelSpec()
        {
            var data = new ModelSpecData
            {
                Name = "GPT-4o",
                Id = "gpt-4o",
                Family = "OpenAI"
            };

            // Confirm the property stores the name correctly
            Assert.Equal("GPT-4o", data.Name);

            // But ToModelSpec() ignores it
            var spec = data.ToModelSpec();
            Assert.Equal("OpenAI", spec.Family);
            Assert.Equal("gpt-4o", spec.Id);
        }
    }
}

