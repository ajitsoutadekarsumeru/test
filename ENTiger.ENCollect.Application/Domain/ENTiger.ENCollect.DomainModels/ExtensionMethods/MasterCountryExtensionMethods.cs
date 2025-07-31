namespace ENTiger.ENCollect
{
    public static class MasterCountryExtensionMethods
    {
        public static IQueryable<T> ByCountryNickNameOrName<T>(this IQueryable<T> countries, string name, string nickName) where T : Countries
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(nickName))
            {
                countries = countries.Where(a => (a.Name ?? "") == name || (a.NickName ?? "") == nickName);
            }

            return countries;
        }


        public static IQueryable<T> ByCountryNickNameOrNameSearch<T>(this IQueryable<T> countries, string searchParam) where T : Countries
        {
            if (!String.IsNullOrEmpty(searchParam))
            {
                return countries.Where(c => c.Name.StartsWith(searchParam) || c.NickName.StartsWith(searchParam));
            }

            return countries;
        }

        public static IQueryable<T> ByCountryIdNotEquals<T>(this IQueryable<T> countries, string id) where T : Countries
        {
            if (!String.IsNullOrEmpty(id))
            {
                countries = countries.Where(a => a.Id != id);
            }
            return countries;
        }

        public static IQueryable<T> ByDeleteCountry<T>(this IQueryable<T> countries) where T : Countries
        {
            countries = countries.Where(c => c.IsDeleted == false);

            return countries;
        }
    }
}