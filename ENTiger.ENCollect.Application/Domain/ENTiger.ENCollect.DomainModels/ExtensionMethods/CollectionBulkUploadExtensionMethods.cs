namespace ENTiger.ENCollect
{
    public static class CollectionBulkUploadExtensionMethods
    {
        public static IQueryable<T> ByCustomId<T>(this IQueryable<T> model, string value) where T : CollectionUploadFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByFileName<T>(this IQueryable<T> model, string value) where T : CollectionUploadFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FileName == value);
            }
            return model;
        }

        public static IQueryable<T> ByUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : CollectionUploadFile
        {
            if (value != null && value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.FileUploadedDate >= startDate && c.FileUploadedDate < endDate);
            }
            return model;
            
        }

        public static IQueryable<T> ByFileUploadedStatus<T>(this IQueryable<T> model, string value) where T : CollectionUploadFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Status == value);
            }
            return model;
        }
    }
}