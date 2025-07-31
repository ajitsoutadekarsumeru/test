
namespace ENTiger.ENCollect
{
    public static class UserExtensionMethods
    {

        public static IQueryable<T> ByApplicationUserType<T>(this IQueryable<T> model, string userType) where T : ApplicationUser
        {
            if (userType != null)
            {
                model = model.Where(c => c.UserType == userType);
            }
            return model;
        }

        public static IQueryable<T> ByUserIds<T>(this IQueryable<T> user, List<string> Ids) where T : UsersUpdateFile
        {
            return user.Where(i => Ids.Contains(i.CreatedBy));
        }

        public static IQueryable<T> ByUsersUpdateFileName<T>(this IQueryable<T> model, string value) where T : UsersUpdateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FileName.StartsWith(value));
            }
            return model;
        }

        public static IQueryable<T> ByUsersUpdateTransactionId<T>(this IQueryable<T> model, string value) where T : UsersUpdateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByUsersUpdateUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : UsersUpdateFile
        {
            if (value != null && value.Value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.UploadedDate >= startDate && c.UploadedDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByUsersUpdateFileStatus<T>(this IQueryable<T> model, string value) where T : UsersUpdateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Status == value);
            }
            return model;
        }

        public static IQueryable<T> ByUsersUpdateFileUser<T>(this IQueryable<T> model, string value) where T : UsersUpdateFile
        {
            return model = model.Where(a => a.CreatedBy == value);
        }

        public static IQueryable<T> ByUsersCreateFileName<T>(this IQueryable<T> model, string value) where T : UsersCreateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FileName.StartsWith(value));
            }
            return model;
        }

        public static IQueryable<T> ByUsersCreateTransactionId<T>(this IQueryable<T> model, string value) where T : UsersCreateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByUsersCreateUserType<T>(this IQueryable<T> model, string value) where T : UsersCreateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.UploadType == value);
            }
            return model;
        }

        public static IQueryable<T> ByUsersCreateUploadedDate<T>(this IQueryable<T> model, DateTime? value) where T : UsersCreateFile
        {
            if (value.HasValue && value.Value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.UploadedDate >= startDate && c.UploadedDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByUsersCreateFileStatus<T>(this IQueryable<T> model, string value) where T : UsersCreateFile
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Status == value);
            }
            return model;
        }

        public static IQueryable<T> ByUsersCreateFileUser<T>(this IQueryable<T> model, string value) where T : UsersCreateFile
        {
            return model = model.Where(a => a.CreatedBy == value);
        }

        public static IQueryable<T> ByCreatedDate<T>(this IQueryable<T> model) where T : UsersCreateFile
        {
            return model = model.OrderByDescending(a => a.CreatedDate);
        }

    }
}