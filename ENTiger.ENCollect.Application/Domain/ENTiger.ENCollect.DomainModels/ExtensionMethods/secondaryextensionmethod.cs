
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class secondaryextensionmethod
    {
        public static IQueryable<T> ByFileName<T>(this IQueryable<T> model, string value) where T : SecondaryAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FileName.StartsWith(value));
            }
            return model;
        }


        public static IQueryable<T> ByTransactionId<T>(this IQueryable<T> model, string value) where T : SecondaryAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                return model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : SecondaryAllocationFile
        {
            if (value.HasValue && value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.FileUploadedDate >= startDate && c.FileUploadedDate < endDate);
            }            
            return model;            
        }

        public static IQueryable<T> ByFileUploadedStatus<T>(this IQueryable<T> model, string status) where T : SecondaryAllocationFile
        {
            if (!string.IsNullOrEmpty(status))
            {
                model = model.Where(c => c.Status == status);
            }
            return model;
        }
        public static IQueryable<T> ByAllocationMethod<T>(this IQueryable<T> model, string value) where T : SecondaryAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Description == value);
            }
            return model;
        }

        public static IQueryable<T> BySecondaryUnAllocationFileUser<T>(this IQueryable<T> model, string value) where T : SecondaryUnAllocationFile
        {
            return model = model.Where((T a) => a.CreatedBy == value);
        }

        public static IQueryable<T> BySecondaryTransactionId<T>(this IQueryable<T> model, string value) where T : SecondaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> BySecondaryUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : SecondaryUnAllocationFile
        {
            if (value.HasValue && value.Value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.UploadedDate >= startDate && c.UploadedDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> BySecondaryFileStatus<T>(this IQueryable<T> model, string value) where T : SecondaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.Status == value);
            }
            return model;
        }

        public static IQueryable<T> BySecondaryFileName<T>(this IQueryable<T> model, string value) where T : SecondaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.FileName.StartsWith(value));
            }
            return model;
        }
        public static IQueryable<T> BySecondaryUnAllocationType<T>(this IQueryable<T> model, string value) where T : SecondaryUnAllocationFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where((T c) => c.Description == value);
            }

            return model;
        }

    }
}