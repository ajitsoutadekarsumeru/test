using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class PersistenceModelBridge : PersistenceModel
    {
        public void SetAsDeleted(bool value)
        {
            this.IsDeleted = value;
        }
    }
}