using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class RAGColorEnum : FlexEnum
    {
        public RAGColorEnum()
        { }

        public RAGColorEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly RAGColorEnum Red = new RAGColorEnum("#E22424", "red");
        public static readonly RAGColorEnum Amber = new RAGColorEnum("#EBA118", "amber");
        public static readonly RAGColorEnum Green = new RAGColorEnum("#1DB47F", "green");


    }
}
