namespace ENTiger.ENCollect
{
    public static class BatchAllocationExtentionMethods
    {
        public static IQueryable<T> ByAllocationBatchUserName<T>(this IQueryable<T> fileuploads, string UserId) where T : AllocationDownload
        {
            if (!String.IsNullOrEmpty(UserId))
            {
                return fileuploads.Where(c => c.CreatedBy == UserId);
            }
            return fileuploads;
        }

        public static IQueryable<T> ByBatchAllocationType<T>(this IQueryable<T> fileuploads, string allocationtype) where T : AllocationDownload
        {
            if (!String.IsNullOrEmpty(allocationtype))
            {
                return fileuploads.Where(c => c.AllocationType == allocationtype);
            }
            return fileuploads;
        }
    }
}