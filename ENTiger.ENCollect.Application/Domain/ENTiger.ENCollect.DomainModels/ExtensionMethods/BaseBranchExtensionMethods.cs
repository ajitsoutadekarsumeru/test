namespace ENTiger.ENCollect
{
    public static class BaseBranchExtensionMethods
    {
        public static IQueryable<T> ByBaseBranchName<T>(this IQueryable<T> model, string value) where T : BaseBranch
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.FirstName == value);
            }
            return model;
        }

        public static IQueryable<T> ByPartialBaseBranchName<T>(this IQueryable<T> model, string value) where T : BaseBranch
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FirstName.StartsWith(value));
            }
            return model;
        }

        public static IQueryable<T> ByBaseBranchIdNotEquals<T>(this IQueryable<T> model, string value) where T : BaseBranch
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Id != value);
            }
            return model;
        }

        public static IQueryable<T> ByNotDeletedBaseBranch<T>(this IQueryable<T> model) where T : BaseBranch
        {
            return model.Where(c => !c.IsDeleted);
        }
    }
}