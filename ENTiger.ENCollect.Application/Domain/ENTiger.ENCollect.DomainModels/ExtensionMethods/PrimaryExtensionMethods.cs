
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace  ENTiger.ENCollect
{
    public static class PrimaryExtensionMethods
    {
        public static IQueryable<T> ByFileName<T>(this IQueryable<T> model, string value) where T : PrimaryAllocationFile
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return model.Where(c => c.FileName.StartsWith(value));
            }
            return model;
        }

        public static IQueryable<T> ByCustomId<T>(this IQueryable<T> model, string value) where T : PrimaryAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : PrimaryAllocationFile
        {
            if (value.HasValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.FileUploadedDate >= startDate && c.FileUploadedDate < endDate);
            }            
            return model;            
        }

        public static IQueryable<T> ByFileUploadedStatus<T>(this IQueryable<T> model, string value) where T : PrimaryAllocationFile
        {
            if (!System.String.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Status == value);
            }
            return model;
        }

        public static IQueryable<T> ByPrimaryUnAllocationFileUser<T>(this IQueryable<T> model, string value) where T : PrimaryUnAllocationFile
        {
            return model = model.Where((T a) => a.CreatedBy == value);
        }

        public static IQueryable<T> ByPrimaryTransactionId<T>(this IQueryable<T> model, string value) where T : PrimaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.CustomId == value);
            }

            return model;
        }

        public static IQueryable<T> ByPrimaryUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : PrimaryUnAllocationFile
        {
            if (value.HasValue && value.Value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where((T c) => c.UploadedDate >= startDate && c.UploadedDate < endDate);
            }

            return model;
        }

        public static IQueryable<T> ByPrimaryFileStatus<T>(this IQueryable<T> model, string value) where T : PrimaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.Status == value);
            }

            return model;
        }

        public static IQueryable<T> ByPrimaryFileName<T>(this IQueryable<T> model, string value) where T : PrimaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.FileName.StartsWith(value));
            }

            return model;
        }
        public static IQueryable<T> ByPrimaryUnAllocationType<T>(this IQueryable<T> model, string value) where T : PrimaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.Description== value);
            }

            return model;
        }

        public static IQueryable<T> ByAllocationMethod<T>(this IQueryable<T> model, string value) where T : PrimaryAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Description == value);
            }
            return model;
        }

    }
}