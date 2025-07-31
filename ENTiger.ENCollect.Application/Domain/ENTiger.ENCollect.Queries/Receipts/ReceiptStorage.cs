using ENTiger.ENCollect.CollectionsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ReceiptStorage
    {
        protected readonly IRepoFactory _repoFactory;

        public ReceiptStorage()
        {
        }

        public ReceiptStorage(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        public async Task AllocateNewReceipt(Receipt receipt, AddCollectionDto input)
        {
            receipt.SetAdded();
            await Save(receipt, input);
        }

        private async Task Save(Receipt receipt, AddCollectionDto input)
        {
            _repoFactory.Init(input);
            try
            {
                _repoFactory.GetRepo().InsertOrUpdate(receipt);
                int records = await _repoFactory.GetRepo().SaveAsync();
                //_RepoFlex.SaveAsync().GetAwaiter().GetResult();
            }
            catch (FlexRepoException ex)
            {
                //Log critical error
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}