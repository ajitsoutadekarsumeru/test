namespace ENTiger.ENCollect
{
    public static class BulkTrailExtensionMethods
    {
        public static IQueryable<T> ByBulkCustId<T>(this IQueryable<T> model, string value) where T : BulkTrailUploadFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByBulkFileName<T>(this IQueryable<T> model, string value) where T : BulkTrailUploadFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FileName == value);
            }
            return model;
        }

        public static IQueryable<T> ByBulkUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : BulkTrailUploadFile
        {
            if (value != null && value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.FileUploadedDate >= startDate && c.FileUploadedDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByBulkFileUploadedStatus<T>(this IQueryable<T> model, string value) where T : BulkTrailUploadFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Status == value);
            }
            return model;
        }
    }
}