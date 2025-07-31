using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class GeoMasterExtensionMethods
    {
        public static IQueryable<T> IncludeBaseBranch<T>(this IQueryable<T> model) where T : GeoMaster
        {
            model = model.FlexInclude(x => x.BaseBranch);
            return model;
        }

        public static IQueryable<T> ByCountry<T>(this IQueryable<T> model, string value) where T : GeoMaster
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.Country == value);
            }
            return model;
        }


        public static IQueryable<T> ByRegion<T>(this IQueryable<T> model, string value) where T : GeoMaster
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.Region == value);
            }
            return model;
        }


        public static IQueryable<T> ByState<T>(this IQueryable<T> model, string value) where T : GeoMaster
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.State == value);
            }
            return model;
        }

        public static IQueryable<T> ByCity<T>(this IQueryable<T> model, string value) where T : GeoMaster
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.CITY == value);
            }
            return model;
        }


        public static IQueryable<T> ByArea<T>(this IQueryable<T> model, string value) where T : GeoMaster
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.Area == value);
            }
            return model;
        }

    }
}