using Sumeru.Flex;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class CollectionAgencyExtensionMethods
    {
        public static IQueryable<T> ByFirstName<T>(this IQueryable<T> collectionAgencies, string firstName) where T : Agency
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                return collectionAgencies.Where(c => c.FirstName.StartsWith(firstName));
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByAgencyName<T>(this IQueryable<T> collectionAgencies, string Name) where T : Agency
        {
            if (!string.IsNullOrEmpty(Name))
            {
                collectionAgencies = collectionAgencies.Where(c => (c.FirstName == Name || c.LastName == Name));
            }
            return collectionAgencies;
        }


        public static IQueryable<T> ByAgencyWorkflowState<T>(this IQueryable<T> collectionAgencies, AgencyWorkflowState state) where T : Agency
        {
            if (state != null)
            {
                collectionAgencies = collectionAgencies.Where(c => c.AgencyWorkflowState != null && c.AgencyWorkflowState.Name == state.Name);
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByLastName<T>(this IQueryable<T> collectionAgencies, string lastName) where T : Agency
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                return collectionAgencies.Where(c => c.LastName.StartsWith(lastName));
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByPhone<T>(this IQueryable<T> collectionAgencies, string phone) where T : Agency
        {
            if (!string.IsNullOrEmpty(phone))
            {
                collectionAgencies = collectionAgencies.Where(c => c.PrimaryMobileNumber == phone);
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByEmailId<T>(this IQueryable<T> collectionAgencies, string emailId) where T : Agency
        {
            if (!string.IsNullOrEmpty(emailId))
            {
                collectionAgencies = collectionAgencies.Where(c => c.PrimaryEMail == emailId);
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByAgencyWorkFlowState<T>(this IQueryable<T> model, AgencyWorkflowState value) where T : Agency
        {
            if (value != null)
            {
                model = model.Where(c => c.AgencyWorkflowState != null && c.AgencyWorkflowState.Name == value.Name);
            }
            //return model.Where(c => c.IsBlackListed == false);
            return model;
        }

        public static IQueryable<T> ByExpiryDate<T>(this IQueryable<T> collectionAgencies, DateTime? expiryDate) where T : Agency
        {
            if (expiryDate != null && expiryDate != DateTime.MinValue)
            {
                DateTime startDate = expiryDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);

                collectionAgencies = collectionAgencies.Where(a => a.ContractExpireDate >= startDate && a.ContractExpireDate  < endDate);
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByDeferredDate<T>(this IQueryable<T> collectionAgencies, DateTime? DeferredDate) where T : Agency
        {
            if (DeferredDate.HasValue && DeferredDate != DateTime.MinValue)
            {
                DateTime startDate = DeferredDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                collectionAgencies = collectionAgencies.Where(a => a.AgencyIdentifications.Any(s => s.DeferredTillDate.HasValue 
                    && s.DeferredTillDate >= startDate && s.DeferredTillDate < endDate));
            }
            return collectionAgencies;
        }

        public static IQueryable<T> IncludeAgencyScopeOfWork<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.ScopeOfWork);
        }

        public static IQueryable<T> IncludeTFlexIdentificationDefferdDate<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude("AgencyIdentifications.DeferredTillDate");
        }

        public static IQueryable<T> IncludeAgencyWorkflow<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.AgencyWorkflowState);
        }

        public static IQueryable<T> IncludeAgencyType<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.AgencyType);
        }

        public static IQueryable<T> IncludePlaceOfWork<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.PlaceOfWork);
        }

        public static IQueryable<T> IncludeAddress<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.Address);
        }

        public static IQueryable<T> IncludeCreditAccountDetails<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.CreditAccountDetails);
        }

        public static IQueryable<T> IncludeAgencyBankBranch<T>(this IQueryable<T> model) where T : Agency
        {
            return model.FlexInclude(x => x.CreditAccountDetails.BankBranch);
        }

        public static IQueryable<T> IncludeAgencyBank<T>(this IQueryable<T> model) where T : Agency
        {
            return model.FlexInclude(x => x.CreditAccountDetails.BankBranch.Bank);
        }

        public static IQueryable<T> IncludeTflexIdentifications<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude(x => x.AgencyIdentifications);
        }

        public static IQueryable<T> IncludeTFlexIdentificationDocs<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude("AgencyIdentifications.TFlexIdentificationDocs");
        }

        public static IQueryable<T> IncludeTFlexIdentificationDocType<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude("CollectionAgencyIdentifications.CollectionAgencyIdentificationDocType");
        }

        public static IQueryable<T> IncludeTFlexIdentificationType<T>(this IQueryable<T> collectionAgency) where T : Agency
        {
            return collectionAgency.FlexInclude("CollectionAgencyIdentifications.CollectionAgencyIdentificationType");
        }

        public static IQueryable<T> ByAgencyIds<T>(this IQueryable<T> agency, List<string> Ids) where T : Agency
        {
            return agency.Where(i => Ids.Contains(i.Id));
        }

        public static IQueryable<T> ByAgencyId<T>(this IQueryable<T> agency, string Id) where T : Agency
        {
            return agency.Where(x => x.Id == Id);
        }


        public static IQueryable<T> ByAgencyIdWithUserType<T>(this IQueryable<T> agency, string Id,ApplicationUser usertype) where T : Agency
        {
            if (usertype?.GetType() == typeof(AgencyUser))
            {
                return agency.Where(x => x.Id == Id);
            }
            return agency.Where(x => x.Id == Id);
        }

        public static IQueryable<T> ByCode<T>(this IQueryable<T> agency, string Code) where T : Agency
        {
            return agency.Where(x => x.CustomId == Code);
        }

        public static IQueryable<T> ByParent<T>(this IQueryable<T> model) where T : Agency
        {
            return model.Where(x => x.IsParentAgency);
        }

        public static IQueryable<T> ByParentAgencyName<T>(this IQueryable<T> model, string value) where T : Agency
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.FirstName == value);
            }
            return model;
        }

        public static IQueryable<T> ByAgencyType<T>(this IQueryable<T> collectionAgencies, string agencyTypeId) where T : Agency
        {
            if (!string.IsNullOrEmpty(agencyTypeId))
            {
                collectionAgencies = collectionAgencies.Where(c => c.AgencyTypeId == agencyTypeId);
            }
            return collectionAgencies;
        }

        public static IQueryable<T> ByApprovedAgency<T>(this IQueryable<T> model) where T : Agency
        {
            return model = model.Where(c => c.AgencyWorkflowState != null && c.AgencyWorkflowState.Name.Contains("approved"));
        }
    }
}