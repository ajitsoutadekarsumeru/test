namespace ENTiger.ENCollect
{
    public static class ReceiptExtensionMethods
    {
        public static IQueryable<Receipt> ByReceiptNo(this IQueryable<Receipt> receipt, string customId)
        {
            return receipt.Where(i => i.CustomId == customId);
        }

        public static IQueryable<Receipt> ByCollector(this IQueryable<Receipt> receipt, string collectorId)
        {
            if (string.IsNullOrEmpty(collectorId))
            {
                return receipt;
            }
            else
            {
                return receipt.Where(i => i.CollectorId == collectorId);
            }
        }

        public static IQueryable<T> ByWorkflowState<T>(this IQueryable<T> receipt, ReceiptWorkflowState state) where T : Receipt
        {
            return receipt.Where(c => c.ReceiptWorkflowState.Name == state.Name);
        }
    }
}