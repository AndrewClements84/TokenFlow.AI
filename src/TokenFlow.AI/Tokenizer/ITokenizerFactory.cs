using TokenFlow.Core.Interfaces;

namespace TokenFlow.AI.Tokenizer
{
    public interface ITokenizerFactory
    {
        ITokenizer Create(string name);
        void Register(string name, ITokenizer tokenizer);
        bool IsRegistered(string name);
    }
}
