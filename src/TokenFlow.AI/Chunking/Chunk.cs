namespace TokenFlow.AI.Chunking
{
    /// <summary>
    /// Represents a segment of text with its token count and range info.
    /// </summary>
    public class Chunk
    {
        public string Text { get; private set; }
        public int TokenCount { get; private set; }
        public int StartIndex { get; private set; }
        public int Length { get; private set; }

        public Chunk(string text, int tokenCount, int startIndex, int length)
        {
            Text = text;
            TokenCount = tokenCount;
            StartIndex = startIndex;
            Length = length;
        }
    }
}
