using AutoMapper;
using ENTiger.ENCollect.BaseBranchesModule;
using ENTiger.ENCollect.DesignationsModule;
using ENTiger.ENCollect.DispositionModule;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchMasters : FlexiQueryBridgeAsync<SearchMastersDto>
    {
        protected readonly ILogger<SearchMasters> _logger;
        protected SearchMastersParams _params;
        protected readonly IRepoFactory _repoFactory;
        private List<dynamic> dynamicList = new List<dynamic>();
        private dynamic model = "";

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SearchMasters(ILogger<SearchMasters> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual SearchMasters AssignParameters(SearchMastersParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<SearchMastersDto> Fetch()
        {
            _repoFactory.Init(_params);

            string MasterType = _params.Name.ToUpper();

            SearchMastersDto result = new SearchMastersDto();
            if (MasterType == "PRODUCTMASTER")
            {
                result.MasterTypeDetails = await FetchProductDetails();
            }
            else if (MasterType == "DEPOSITBANKMASTER")
            {
                result.MasterTypeDetails = await _repoFactory.GetRepo().FindAll<DepositBankMaster>().SelectTo<GetDepositBankListDto>().ToListAsync();
            }
            else if (MasterType == "GEOMASTER")
            {
                result.MasterTypeDetails = await FetchGeoMasterDetails();
            }
            else if (MasterType == "BANKMASTER")
            {
                result.MasterTypeDetails = await _repoFactory.GetRepo().FindAll<Bank>().SelectTo<BankListDto>().ToListAsync();
            }
            else if (MasterType == "BASEBRANCHMASTER")
            {
                result.MasterTypeDetails = await _repoFactory.GetRepo().FindAll<BaseBranch>().ByNotDeletedBaseBranch().SelectTo<BaseBranchListDto>().ToListAsync();
            }
            else if (MasterType == "BUCKETMASTER")
            {
                result.MasterTypeDetails = await _repoFactory.GetRepo().FindAll<Bucket>().SelectTo<GetBucketListDto>().ToListAsync();
            }
            else if (MasterType == "DISPOSITIONCODEMASTER")
            {
                result.MasterTypeDetails = await _repoFactory.GetRepo().FindAll<DispositionCodeMaster>().SelectTo<GetCodesByGroupIdDto>().ToListAsync();
            }
            else if (MasterType == "DEPARTMENTDESIGNATIONMASTER")
            {
                result.MasterTypeDetails = await _repoFactory.GetRepo().FindAll<Designation>().ByDeleteDesignation().SelectTo<GetDesignationsDto>().ToListAsync();
            }
            return result;
        }

        private async Task<dynamic> FetchProductDetails()
        {
            string categoryMaster = "Product";
            // Use ToLowerInvariant for culture-independent comparisons
            string categoryMasterId = await _repoFactory.GetRepo().FindAll<CategoryMaster>()
                                       .Where(i => string.Equals(i.Name, categoryMaster))
                                       .Select(x => x.Id)
                                       .FirstOrDefaultAsync();


            // Check if categoryMasterId is not null before proceeding
            if (categoryMasterId == null)
            {
                return null; // Or handle the case appropriately
            }

            // Combine queries to reduce database round trips
            var products = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.CategoryMasterId == categoryMasterId && !a.IsDeleted).ToListAsync();
            var productIds = products.Select(x => x.Id).ToList();

            // Pre-filter subproducts to avoid querying inside the loop
            var subproducts = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => productIds.Contains(a.ParentId) && !a.IsDeleted).ToListAsync();

            // Use a dictionary to group subproducts by ParentId to avoid multiple enumerations
            var subproductGroups = subproducts.GroupBy(x => x.ParentId).ToDictionary(g => g.Key, g => g.OrderByDescending(x => x.LastModifiedDate).ToList());

            List<dynamic> dynamicList = new List<dynamic>();
            foreach (var product in products)
            {
                // Retrieve pre-grouped subproducts
                if (subproductGroups.TryGetValue(product.Id, out var objSubproducts))
                {
                    foreach (var objSub in objSubproducts)
                    {
                        dynamic model = new
                        {
                            ProductGroup = product.Parent?.Name,
                            Product = product.Name,
                            SubProduct = objSub.Name,
                            SubProductId = objSub.Id,
                            //CreatedBy = CoreUtilities.GetApplicationUserName(objSub.CreatedBy, TenantId),
                            CreatedDate = objSub.CreatedDate,
                        };
                        dynamicList.Add(model);
                    }
                }
            }
            return dynamicList;
        }

        private async Task<dynamic> FetchGeoMasterDetails()
        {
            var countries = await _repoFactory.GetRepo().FindAll<Countries>().ToListAsync();

            List<dynamic> dynamicList = new List<dynamic>();
            foreach (var country in countries)
            {
                string countryId = country.Id;
                var regionlist = await _repoFactory.GetRepo().FindAll<Regions>().Where(a => a.CountryId == countryId).ToListAsync();

                foreach (var region in regionlist)
                {
                    string regionId = region.Id;
                    var statelist = await _repoFactory.GetRepo().FindAll<State>().Where(a => a.RegionId == regionId).ToListAsync();

                    foreach (var state in statelist)
                    {
                        string stateId = state.Id;
                        var citylist = await _repoFactory.GetRepo().FindAll<Cities>().Where(a => a.StateId == stateId).ToListAsync();

                        foreach (var city in citylist)
                        {
                            string cityId = city.Id;

                            var arealist = await _repoFactory.GetRepo().FindAll<Area>().Where(a => a.CityId == cityId).ToListAsync();

                            foreach (var area in arealist)
                            {
                                dynamic geoMaster = new
                                {
                                    Id = area.BaseBranchId,
                                    Country = country.Name,
                                    Region = region.Name,
                                    State = state.Name,
                                    City = city.Name,
                                    BaseBranchName = area?.BaseBranch?.FirstName,
                                    // geoMaster.baseBranch = area.BaseBranch;
                                    //geoMaster.CreatedBy = CoreUtilities.GetApplicationUserName(area.CreatedBy, tenantId),
                                    CreatedDate = area.CreatedDate
                                };
                                dynamicList.Add(geoMaster);
                            }
                        }
                    }
                }
            }
            return dynamicList;
        }
    }

    public class SearchMastersParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string Name { get; set; }
    }
}