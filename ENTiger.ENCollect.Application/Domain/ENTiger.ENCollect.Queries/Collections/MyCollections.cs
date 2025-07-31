using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class MyCollections : FlexiQueryBridgeAsync<LoanAccount, MyCollectionsDto>
    {
        protected readonly ILogger<MyCollections> _logger;
        protected MyCollectionsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public int _take;
        public int _skip;
        private string? loggedInUserId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public MyCollections(ILogger<MyCollections> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual MyCollections AssignParameters(MyCollectionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<MyCollectionsDto> Fetch()
        {
            FlexiPagedList<MyCollectionsDto> finaloutput;

            if (_params.Take < 0)
            {
                _take = 0;
            }
            if (_params.Take == 0)
            {
                _take = 50;
            }
            else
            {
                _take = _params.Take;
            }
            if (_params.Skip < 0)
            {
                _skip = 0;
            }
            else
            {
                _skip = _params.Skip;
            }

            MyCollectionsDto outputResult = new MyCollectionsDto();

            var projection = await Build<LoanAccount>().SelectTo<MyCollectionsAccountsDto>().Skip(_skip).Take(_take).ToListAsync();
            //var result = BuildPagedOutput(projection);
            //finaloutput = result;
            if (projection.Count() > 0)
            {
                outputResult.count = projection.Count();
                decimal? AllocatedPos = projection.Sum(a => a.BOM_POS);
                outputResult.MyCollectionFlow = GetAccountFlowDetails(projection, AllocatedPos, _params);
                outputResult.MyCollectionReco = await GetAccountRecoDetails(projection, AllocatedPos, _params);
            }

            return outputResult;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();
            string userid = _flexAppContext.UserId;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByCollectorId(userid)
                                    .WithMonthAndYear(_params.Month, _params.year)
                                    .ByLoanaccountProductGroup(_params.ProductGroup)
                                    .ByLoanaccountProduct(_params.Product)
                                    .ByLoanaccountSubProduct(_params.SubProduct)
                                    .ByLoanAccountBucket(_params.Bucket)
                                    .WithNpaStageid(_params.NPA)
                                    ;

            //Build Your Query With All Parameters Here

            //query = CreatePagedQuery<T>(query, _params.PageNumber, _params.PageSize);

            return query;
        }

        private ICollection<MyCollectionFlowModel> GetAccountFlowDetails(List<MyCollectionsAccountsDto> CollectorAccountList, decimal? AllocatedPos, MyCollectionsParams _params)
        {
            ICollection<MyCollectionFlowModel> MyCollectionFlowList = new List<MyCollectionFlowModel>();
            try
            {
                //TODO : PaymentStatus add in Account
                Int64? StUnit = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.Stab.Value)).Count();
                Int64? RBUnit = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.RB.Value)).Count();
                Int64? OdUnit = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.OD.Value)).Count();
                Int64? NormUnit = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.Norm.Value)).Count();
                Int64? RFUnit = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.RF.Value)).Count();

                Int64? ResUnit = StUnit + RBUnit + OdUnit + NormUnit;

                decimal? StPos = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.Stab.Value)).Sum(a => a.BOM_POS);
                decimal? RBPos = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.RB.Value)).Sum(a => a.BOM_POS);
                decimal? ODPos = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.OD.Value)).Sum(a => a.BOM_POS);
                decimal? NormPos = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.Norm.Value)).Sum(a => a.BOM_POS);
                decimal? RFPos = CollectorAccountList.Where(a => string.Equals(a.PAYMENTSTATUS, PaymentStatusEnum.RF.Value)).Sum(a => a.BOM_POS);

                decimal? ResPos = StPos + RBPos + NormPos + ODPos;

                MyCollectionFlowModel myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "Allocated";
                myCollectionAllocated.Unit = Convert.ToString(CollectorAccountList.Count());
                myCollectionAllocated.POS = Convert.ToString(AllocatedPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((AllocatedPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);

                myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "ST";
                myCollectionAllocated.Unit = Convert.ToString(StUnit);
                myCollectionAllocated.POS = Convert.ToString(StPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((StPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);

                myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "RB";
                myCollectionAllocated.Unit = Convert.ToString(RBUnit);
                myCollectionAllocated.POS = Convert.ToString(RBPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((RBPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);

                myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "OD";
                myCollectionAllocated.Unit = Convert.ToString(OdUnit);
                myCollectionAllocated.POS = Convert.ToString(ODPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((ODPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);

                myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "RF";
                myCollectionAllocated.Unit = Convert.ToString(RFUnit);
                myCollectionAllocated.POS = Convert.ToString(RFPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((RFPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);

                myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "NORM";
                myCollectionAllocated.Unit = Convert.ToString(NormUnit);
                myCollectionAllocated.POS = Convert.ToString(NormPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((NormPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);

                myCollectionAllocated = new MyCollectionFlowModel();
                myCollectionAllocated.Flows = "RES";
                myCollectionAllocated.Unit = Convert.ToString(ResUnit);
                myCollectionAllocated.POS = Convert.ToString(ResPos);
                if (AllocatedPos != 0)
                {
                    myCollectionAllocated.POSPercentage = Convert.ToString((ResPos / AllocatedPos) * 100);
                }
                MyCollectionFlowList.Add(myCollectionAllocated);
            }
            catch (Exception ex)
            {
                //packet.AddError("Error", ex.InnerException);
            }
            return MyCollectionFlowList;
        }

        private async Task<ICollection<MyCollectionRecoModel>> GetAccountRecoDetails(List<MyCollectionsAccountsDto> CollectorAccountList, decimal? AllocatedPos, MyCollectionsParams _params)
        {
            ICollection<MyCollectionRecoModel> MyCollectionRecoList = new List<MyCollectionRecoModel>();

            List<string> accountids = CollectorAccountList.Select(a => a.Id).ToList();

            var collections = await _repoFactory.GetRepo().FindAll<Collection>().FlexInclude(a => a.Account).Where(a => accountids.Contains(a.AccountId)).ToListAsync();

            try
            {
                MyCollectionRecoModel myCollectionReco = new MyCollectionRecoModel();
                myCollectionReco.Reco = "Allocated";
                myCollectionReco.Unit = Convert.ToString(CollectorAccountList.Count());
                myCollectionReco.POS = Convert.ToString(AllocatedPos);
                if (AllocatedPos != 0)
                {
                    myCollectionReco.RORPercentage = Convert.ToString((AllocatedPos / AllocatedPos) * 100);
                }
                MyCollectionRecoList.Add(myCollectionReco);

                decimal recovered = (collections.Count() > 0) ? Math.Round(Convert.ToDecimal(collections.Select(a => a.Account).Sum(a => a.BOM_POS)), 2) : 0;//Math.Round(Convert.ToDecimal(CollectorAccountList.Where(x => x.Collections.Count() > 0).Sum(a => a.BOM_POS)), 2);
                decimal TotalROR = Math.Round(Convert.ToDecimal(CollectorAccountList.Sum(a => a.BOM_POS)), 2);

                myCollectionReco = new MyCollectionRecoModel();
                myCollectionReco.Reco = "Recovered";
                myCollectionReco.Unit = (collections.Count() > 0) ? collections.Count().ToString() : "0";//Convert.ToString(CollectorAccountList.Where(x => x.Collections.Count() > 0).Count());
                myCollectionReco.POS = Convert.ToString(recovered);
                if (TotalROR != 0)
                {
                    myCollectionReco.RORPercentage = Convert.ToString(Math.Round((recovered / TotalROR), 2) * 100);
                }
                MyCollectionRecoList.Add(myCollectionReco);
            }
            catch (Exception ex)
            {
                //packet.AddError("Error", ex.InnerException);
            }
            return MyCollectionRecoList;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class MyCollectionsParams : DtoBridge
    {
        [Required]
        [RegularExpression("^[0-9]{1,2}$", ErrorMessage = "Invalid Month")]
        public Int64 Month { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4,4}$", ErrorMessage = "Invalid Year")]
        public Int64 year { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid Bucket")]
        public string? Bucket { get; set; }

        [Required]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid Product Group")]
        [RegularExpression("^[a-zA-Z0-9-_ ]*$", ErrorMessage = "You may not use special characters.Invalid ProductGroup")]
        public string? ProductGroup { get; set; }

        [Required]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid Product")]
        [RegularExpression("^[a-zA-Z0-9-_ ]*$", ErrorMessage = "You may not use special characters.Invalid Product")]
        public string? Product { get; set; }

        [Required]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid Sub Product")]
        [RegularExpression("^[a-zA-Z0-9-_ ]*$", ErrorMessage = "You may not use special characters.Invalid SubProduct")]
        public string? SubProduct { get; set; }

        public bool? NPA { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}