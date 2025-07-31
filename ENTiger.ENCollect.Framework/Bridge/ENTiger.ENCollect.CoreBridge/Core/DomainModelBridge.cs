using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class DomainModelBridge : DomainModel
    {
        public void SetAsDeleted(bool value)
        {
            this.IsDeleted = value;
        }
    }
}