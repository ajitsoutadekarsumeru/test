namespace ENTiger.ENCollect
{
    public static class TreatmentExtensionMethods
    {
        public static IQueryable<T> ByTreatmentName<T>(this IQueryable<T> treatment, string Name) where T : Treatment
        {
            if (!String.IsNullOrEmpty(Name))
            {
                treatment = treatment.Where(a => a.Name == Name);
            }
            return treatment;
        }

        public static IQueryable<T> ByTreatmentStartsWithName<T>(this IQueryable<T> treatment, string Name) where T : Treatment
        {
            if (!String.IsNullOrEmpty(Name))
            {
                treatment = treatment.Where(a => a.Name.StartsWith(Name));
            }
            return treatment;
        }


        public static IQueryable<T> ByTreatmentCreatedDate<T>(this IQueryable<T> treatment, DateTime? createdDate) where T : Treatment
        {
            if (createdDate != null && createdDate != DateTime.MinValue)
            {
                DateTime startDate = createdDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                treatment = treatment.Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDate);
            }
            return treatment;
        }

        public static IQueryable<T> ByTreatmentCreatedBy<T>(this IQueryable<T> treatment, string CreatedBy) where T : Treatment
        {
            if (!String.IsNullOrEmpty(CreatedBy))
            {
                treatment = treatment.Where(a => a.CreatedBy == CreatedBy);
            }
            return treatment;
        }

        public static IQueryable<T> ByTreatmentIsDeleted<T>(this IQueryable<T> treatment) where T : Treatment
        {
            treatment = treatment.Where(a => a.IsDeleted == false);

            return treatment;
        }

        public static IQueryable<T> ByCreatedByName<T>(this IQueryable<T> applicationUser, string CreatedByName) where T : Treatment
        {
            if (!string.IsNullOrEmpty(CreatedByName))
            {
                #region
                string FullName = CreatedByName;
                string firstName = string.Empty;
                string middleName = string.Empty;
                string lastName = string.Empty;
                string[] array = FullName.Split(' ');
                if (array[0] != null)
                {
                    firstName = array[0].ToString();
                    if (array[1] != null)
                    {
                        lastName = array[1].ToString();
                    }
                }
                string ApplicationUserId = string.Empty;
                #endregion

                //using (IFlexRepository _repoflex = InitFlex.factory.FlexRepository)
                // {
                //     ApplicationUser userId = new ApplicationUser();
                //     userId = _repoflex.FindAll<ApplicationUser>()
                //             .Where(a => a.FirstName == firstName && a.LastName == lastName)
                //             .FirstOrDefault();
                //     ApplicationUserId = Convert.ToString(userId.Id);
                //     if(ApplicationUserId != null)
                //     {
                //         applicationUser = applicationUser.Where(a => a.CreatedBy == ApplicationUserId);
                //     }
                // }
            }
            return applicationUser;
        }
    }
}