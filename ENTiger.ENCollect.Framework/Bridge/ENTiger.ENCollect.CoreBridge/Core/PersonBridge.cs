using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class PersonBridge : Person
    {
        public void SetAsDeleted(bool value)
        {
            this.IsDeleted = value;
        }
    }
}