using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SequentialGuidPrimaryKeyGeneratorBridge : IFlexPrimaryKeyGeneratorBridge
    {
        public string GenerateKey()
        {
            return SequentialGuid.NewGuidString();
        }
    }
}