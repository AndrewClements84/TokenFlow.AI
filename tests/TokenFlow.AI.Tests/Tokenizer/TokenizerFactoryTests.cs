using TokenFlow.AI.Tokenizer;

namespace TokenFlow.AI.Tests.Tokenizer
{
    public class TokenizerFactoryTests
    {
        [Fact]
        public void Default_ShouldContain_ApproxTokenizer()
        {
            var factory = new TokenizerFactory();
            Assert.True(factory.IsRegistered("approx"));
        }

        [Fact]
        public void Create_ShouldReturn_ApproxTokenizer_WhenUnrecognized()
        {
            var factory = new TokenizerFactory();
            var tokenizer = factory.Create("unknown");

            Assert.NotNull(tokenizer);
            Assert.Equal("approx", tokenizer.Name);
        }

        [Fact]
        public void Register_ShouldAdd_CustomTokenizer()
        {
            var factory = new TokenizerFactory();
            var custom = new ApproxTokenizer();

            factory.Register("custom", custom);

            Assert.True(factory.IsRegistered("custom"));
        }

        [Fact]
        public void Create_ShouldReturn_CustomTokenizer()
        {
            var factory = new TokenizerFactory();
            var custom = new ApproxTokenizer();

            factory.Register("custom", custom);
            var tokenizer = factory.Create("custom");

            Assert.Same(custom, tokenizer);
        }

        [Fact]
        public void Create_ShouldHandle_NullOrEmptyName()
        {
            var factory = new TokenizerFactory();
            var tokenizer = factory.Create(null);

            Assert.Equal("approx", tokenizer.Name);
        }

        [Fact]
        public void Register_ShouldIgnore_NullValues()
        {
            var factory = new TokenizerFactory();
            factory.Register("nulltest", null);

            Assert.False(factory.IsRegistered("nulltest"));
        }
    }
}

