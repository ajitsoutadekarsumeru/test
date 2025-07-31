namespace ENTiger.ENCollect
{
    public static class MasterDesignationExtensionMethods
    {
        public static IQueryable<T> ByDesignationName<T>(this IQueryable<T> designations, string name, string DesignationAcronym) where T : Designation
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(DesignationAcronym))
            {
                designations = designations.Where(a => (a.Name ?? "") == name || (a.Acronym ?? "") == DesignationAcronym);
            }
            return designations;
        }


        public static IQueryable<T> ByDesignationNameSearch<T>(this IQueryable<T> designations, string searchParam) where T : Designation
        {
            if (!String.IsNullOrWhiteSpace(searchParam))
            {
                return designations.Where(c => c.Name.StartsWith(searchParam) || c.DesignationTypeId.StartsWith(searchParam));
            }
            return designations;
        }

        public static IQueryable<T> ByDesignationIdNotEquals<T>(this IQueryable<T> designations, string id) where T : Designation
        {
            if (!String.IsNullOrEmpty(id))
            {
                designations = designations.Where(a => a.Id != id);
            }
            return designations;
        }

        public static IQueryable<T> ByDeleteDesignation<T>(this IQueryable<T> designations) where T : Designation
        {
            designations = designations.Where(c => c.IsDeleted == false);

            return designations;
        }

        public static IQueryable<T> ByDeleteDesignationType<T>(this IQueryable<T> designationsType) where T : DesignationType
        {
            designationsType = designationsType.Where(c => c.IsDeleted == false);

            return designationsType;
        }

        public static IQueryable<T> ByDesignationLevel<T>(this IQueryable<T> designations, int level) where T : Designation
        {
            designations = designations.Where(c => c.Level == level);

            return designations;
        }
    }
}