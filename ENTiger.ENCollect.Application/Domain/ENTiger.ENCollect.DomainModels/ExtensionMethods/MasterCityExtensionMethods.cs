namespace ENTiger.ENCollect
{
    public static class MasterCityExtensionMethods
    {
        public static IQueryable<T> ByCityNickNameOrName<T>(this IQueryable<T> cities, string name, string nickName) where T : Cities
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(nickName))
            {
                cities = cities.Where(a => (a.NickName ?? "") == name || (a.NickName ?? "") == nickName);
            }

            return cities;
        }

        public static IQueryable<T> ByCityNickNameOrNameSearch<T>(this IQueryable<T> cities, string searchParam) where T : Cities
        {
            if (!String.IsNullOrWhiteSpace(searchParam))
            {
                return cities.Where(c => c.Name.StartsWith(searchParam) || c.NickName.StartsWith(searchParam));
            }

            return cities;
        }

        public static IQueryable<T> ByCityIdNotEquals<T>(this IQueryable<T> cities, string id) where T : Cities
        {
            if (!String.IsNullOrEmpty(id))
            {
                cities = cities.Where(a => a.Id != id);
            }
            return cities;
        }

        public static IQueryable<T> ByCityStateMap<T>(this IQueryable<T> cities, string stateId) where T : Cities
        {
            if (!String.IsNullOrEmpty(stateId))
            {
                cities = cities.Where(a => a.StateId == stateId);
            }

            return cities;
        }

        public static IQueryable<T> ByDeleteCity<T>(this IQueryable<T> cities) where T : Cities
        {
            cities = cities.Where(c => c.IsDeleted == false);

            return cities;
        }
    }
}