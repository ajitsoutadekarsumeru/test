namespace ENTiger.ENCollect
{
    public static class CategoryItemExtensionMethods
    {
        public static IQueryable<T> ByCategoryNameandCodeandMasterID<T>(this IQueryable<T> model, string Name, string Code, string MasterId) where T : CategoryItem
        {
            if (!string.IsNullOrEmpty(Name))
            {
                model = model.Where(a =>
                    (a.Name == Name && a.CategoryMasterId == MasterId) ||
                    (a.Code == Code && a.CategoryMasterId == MasterId));
            }
            return model;
        }


        public static IQueryable<T> ByCategoryNameAndMasterId<T>(this IQueryable<T> model, string value, string MasterId) where T : CategoryItem
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Name.Trim() == value.Trim() && a.CategoryMasterId.Trim() == MasterId.Trim());
            }
            return model;
        }


        public static IQueryable<T> ByCategoryNameandCodeSearch<T>(this IQueryable<T> model, string value, string MasterId) where T : CategoryItem
        {
            model = model.Where(a => a.CategoryMasterId == MasterId);

            if (!string.IsNullOrEmpty(value))
            {
                return model.Where(a => a.Code == value || a.Name.StartsWith(value));
            }
            return model;
        }

        public static IQueryable<T> ByCategoryItemIdNotEquals<T>(this IQueryable<T> model, string value) where T : CategoryItem
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Id != value);
            }
            return model;
        }

        public static IQueryable<T> ByNotDeleteCategoryItem<T>(this IQueryable<T> model) where T : CategoryItem
        {
            return model.Where(c => !c.IsDeleted);
        }

        public static IQueryable<T> ByCode<T>(this IQueryable<T> model, string code, string MasterId) where T : CategoryItem
        {
            return model.Where(p => p.Code == code && p.CategoryMasterId == MasterId);
        }

        public static IQueryable<T> OnlyActiveItems<T>(this IQueryable<T> model) where T : CategoryItem
        {
            return model.Where(c => !c.IsDeleted && !c.IsDisabled.GetValueOrDefault());
        }

        public static IQueryable<T> ByCategoryMaster<T>(this IQueryable<T> model, string value) where T : CategoryItem
        {
            return model.Where(p => p.CategoryMasterId == value);
        }

        public static IQueryable<T> ByParent<T>(this IQueryable<T> model, string value) where T : CategoryItem
        {
            return model.Where(p => p.ParentId == value);
        }
    }
}