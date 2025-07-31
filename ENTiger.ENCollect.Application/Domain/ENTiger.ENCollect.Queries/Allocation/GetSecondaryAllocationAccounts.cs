using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSecondaryAllocationAccounts : FlexiQueryEnumerableBridgeAsync<LoanAccount, GetSecondaryAllocationAccountsDto>
    {
        protected readonly ILogger<GetSecondaryAllocationAccounts> _logger;
        protected GetSecondaryAllocationAccountsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId = string.Empty;

        private ApplicationUser? user;
        private Accountability? accountability;


        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetSecondaryAllocationAccounts(ILogger<GetSecondaryAllocationAccounts> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetSecondaryAllocationAccounts AssignParameters(GetSecondaryAllocationAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetSecondaryAllocationAccountsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;

            _repoFactory.Init(_params);

            user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == userId).FirstOrDefaultAsync();

            accountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(x => x.ResponsibleId == userId).FirstOrDefaultAsync();

            var result = await Build<LoanAccount>().SelectTo<GetSecondaryAllocationAccountsDto>().ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {            
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .byAccountUploadedDate(_params.FromDate, _params.ToDate)
                                    .byProductGroup(_params.ProductGroup)
                                    .byproduct(_params.Product)
                                    .bysubProduct(_params.SubProduct)
                                    .bydownloadBucket(_params.Bucket)
                                    .ByAccountAgencyId(_params.AgencyId)
                                    .ByAccountTeleCallingAgencyId(_params.TeleCallingAgencyId)
                                    .byZone(_params.Zone)
                                    .byRegion(_params.Region)
                                    .byBranch(_params.Branch)
                                    .bycity(_params.City)
                                    .BySecondAllocation(_params.IsAllocated, _params.IsUnAllocated, user, accountability, accountability.CommisionerId, userId);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetSecondaryAllocationAccountsParams : DtoBridge
    {
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Bucket")]
        public string? Bucket { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid City")]
        public string? City { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid State")]
        public string? State { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Invalid Region")]
        public string? Region { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Zone")]
        public string? Zone { get; set; }

        //[RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Branch")]
        public string? Branch { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid TeleCallingAgencyId")]
        public string? TeleCallingAgencyId { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid AgencyId")]
        public string? AgencyId { get; set; }

        public bool IsAllocated { get; set; }
        public bool IsUnAllocated { get; set; }

        //[Required]
        public DateTime? FromDate { get; set; }

        //[Required]
        public DateTime? ToDate { get; set; }
    }
}