using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class MasterFileStatusExtensionMethods
    {
        public static IQueryable<T> ByMasterFileTransactionId<T>(this IQueryable<T> model, string value) where T : MasterFileStatus
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByMasterFileName<T>(this IQueryable<T> model, string value) where T : MasterFileStatus
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => (a.FileName ?? "") == value);
            }
            return model;
        }


        public static IQueryable<T> ByMasterFileUploadedDate<T>(this IQueryable<T> model, DateTimeOffset? value) where T : MasterFileStatus
        {
            if (value.HasValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(a => a.FileUploadedDate >= startDate && a.FileUploadedDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByMasterFileStatus<T>(this IQueryable<T> model, string value) where T : MasterFileStatus
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => (a.Status ?? "") == value);
            }
            return model;
        }


        public static IQueryable<T> ByMasterFileType<T>(this IQueryable<T> model, string value) where T : MasterFileStatus
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return model.Where(a => a.UploadType != null && a.UploadType.StartsWith(value));
            }
            return model;
        }

    }
}