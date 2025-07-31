using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class OrgBridge : Org
    {
        public void SetAsDeleted(bool value)
        {
            this.IsDeleted = value;
        }
    }
}