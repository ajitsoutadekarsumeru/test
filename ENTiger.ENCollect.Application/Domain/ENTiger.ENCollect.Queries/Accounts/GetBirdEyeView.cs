using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetBirdEyeView : FlexiQueryBridgeAsync<GetBirdEyeViewDto>
    {
        protected readonly ILogger<GetBirdEyeView> _logger;
        protected GetBirdEyeViewParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private List<BirdEyeViewAccountDetailDto> Encollectloanaccount;
        private List<string> dispcodeconnects;
        private List<string> dispcodecontacts;
        private List<string> dispcodeWorkableAccounts;
        private List<string> dispcodePTPGroup;
        private string userId = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetBirdEyeView(ILogger<GetBirdEyeView> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetBirdEyeView AssignParameters(GetBirdEyeViewParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetBirdEyeViewDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            _repoFactory.Init(_params);
            userId = _flexAppContext.UserId;

            await dispcodeconnectfetch();
            await  dispcodeWorkableAccountsfetch();
            await dispcodePTPGroupfetch();
            await dispcodecontactfetch();

            await FetchFeedback();

            GetBirdEyeViewDto result = BindRecords();

            return result;
        }

        private GetBirdEyeViewDto BindRecords()
        {
            GetBirdEyeViewDto outputmodel = new GetBirdEyeViewDto();
            outputmodel.ptpnotdue = new PTPoutputmodel();
            outputmodel.ptpnotdue = fetchptpnotdue();
            outputmodel.PaidQueue = new PaidTodaysQueue();
            outputmodel.PaidQueue = fetchpaidtodaysqueue();
            outputmodel.contactsUniqueQueue = new contactsUniqueTodaysQueue();
            outputmodel.contactsUniqueQueue = fetchcontactsUnique();
            outputmodel.ConnectsUniqueQueue = new ConnectsUniqueTodaysQueue();
            outputmodel.ConnectsUniqueQueue = fetchConnectsUniqueTodaysQueue();
            outputmodel.assigned = new Assignedviewoutputmodel();
            outputmodel.assigned = Fetchassigned();
            outputmodel.ptp = new PTPoutputmodel();
            outputmodel.ptp = FetchPTPConfirmedToday();
            outputmodel.brokenptp = new BrokenTodaysQueue();
            outputmodel.brokenptp = fetchbrokentodaysqueue();
            outputmodel.WorkableAccounts = new WorkableAccountsTodaysQueue();
            outputmodel.WorkableAccounts = fetchWorkableAccountsTodaysQueue();
            return outputmodel;
        }

        private PTPoutputmodel fetchptpnotdue()
        {
            PTPoutputmodel paidTodaysQueue = new PTPoutputmodel();

            DateTime currentDate = DateTime.Now.Date;
            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0);
            DateTime endDate = startDate.AddMonths(1); // First day of next month

            ICollection<BirdEyeViewAccountDetailDto> brokenfeedback = Encollectloanaccount.Where(x => x.DispCode != null && x.DispCode.ToLower() == DispCodeEnum.PTP.Value
                && x.LatestPTPDate.HasValue && x.LatestPTPDate.Value.Date > currentDate && x.LatestFeedbackDate >= startDate && x.LatestFeedbackDate < endDate).ToList();
            
            paidTodaysQueue.count = brokenfeedback.Count;
            if (brokenfeedback != null)
            {
                paidTodaysQueue.accountdetails = FeedbacksGroupbyaccount(brokenfeedback);
                paidTodaysQueue.pos = Sumpos(paidTodaysQueue.accountdetails);
            }
            return paidTodaysQueue;
        }

        private PaidTodaysQueue fetchpaidtodaysqueue()
        {
            PaidTodaysQueue paidTodaysQueue = new PaidTodaysQueue();
            ICollection<BirdEyeViewAccountDetailDto> todayfeedback;

            try
            {
                todayfeedback = Encollectloanaccount.Where(x => x.LatestPaymentDate.HasValue && x.LatestPaymentDate.Value.Month == 
                                                            DateTime.Now.Month && x.LatestPaymentDate.Value.Year == DateTime.Now.Year).ToList();

                paidTodaysQueue.count = todayfeedback.Count;
                if (Encollectloanaccount != null)
                {
                    paidTodaysQueue.accountdetails = FeedbacksGroupbyaccount(todayfeedback);
                    paidTodaysQueue.pos = Sumpos(paidTodaysQueue.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return paidTodaysQueue;
        }

        private contactsUniqueTodaysQueue fetchcontactsUnique()
        {
            contactsUniqueTodaysQueue TodaysQueue = new contactsUniqueTodaysQueue();
            ICollection<BirdEyeViewAccountDetailDto> todayfeedback;
            try
            {
                //todayfeedback = Encollectloanaccout.Where(x => dispcodecontacts.Contains(x.DispCode) && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Month == DateTime.Now.Month && x.LatestFeedbackDate.Value.Year == DateTime.Now.Year).ToListAsync();

                todayfeedback = Encollectloanaccount.Where(x => x.GroupName == "Contact" && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Month == 
                                        DateTime.Now.Month && x.LatestFeedbackDate.Value.Year == DateTime.Now.Year).ToList();

                TodaysQueue.count = todayfeedback.Count;
                if (Encollectloanaccount != null)
                {
                    TodaysQueue.accountdetails = FeedbacksGroupbyaccount(todayfeedback);
                    TodaysQueue.pos = Sumpos(TodaysQueue.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return TodaysQueue;
        }

        private ConnectsUniqueTodaysQueue fetchConnectsUniqueTodaysQueue()
        {
            ConnectsUniqueTodaysQueue TodaysQueue = new ConnectsUniqueTodaysQueue();
            ICollection<BirdEyeViewAccountDetailDto> todayfeedback;

            try
            {
                todayfeedback = Encollectloanaccount.Where(x => x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Month == 
                                    DateTime.Now.Month && x.LatestFeedbackDate.Value.Year == DateTime.Now.Year).ToList();

                TodaysQueue.count = todayfeedback.Count;
                if (Encollectloanaccount != null)
                {
                    TodaysQueue.accountdetails = FeedbacksGroupbyaccount(todayfeedback);
                    TodaysQueue.pos = Sumpos(TodaysQueue.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return TodaysQueue;
        }

        private Assignedviewoutputmodel Fetchassigned()
        {
            Assignedviewoutputmodel obj = new Assignedviewoutputmodel();
            ICollection<BirdEyeViewAccountDetailDto> TotalAssigned;
            try
            {
                TotalAssigned = Encollectloanaccount.ToList();

                obj.count = TotalAssigned.Count;
                if (Encollectloanaccount != null)
                {
                    obj.accountdetails = FeedbacksGroupbyaccount(TotalAssigned);
                    obj.pos = Sumpos(obj.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return obj;
        }

        private PTPoutputmodel FetchPTPConfirmedToday()
        {
            PTPoutputmodel pTPConfirmedToday = new PTPoutputmodel();
            ICollection<BirdEyeViewAccountDetailDto> todayfeedback;
            try
            {
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                DateTime endDate = startDate.AddMonths(1); // First day of next month

                todayfeedback = Encollectloanaccount.Where(x => x.DispCode != null && x.DispCode.ToLower() == DispCodeEnum.PTP.Value
                    && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate >= startDate && x.LatestFeedbackDate < endDate).ToList();


                pTPConfirmedToday.count = todayfeedback.Count;
                if (Encollectloanaccount != null)
                {
                    pTPConfirmedToday.accountdetails = FeedbacksGroupbyaccount(todayfeedback);
                    pTPConfirmedToday.pos = Sumpos(pTPConfirmedToday.accountdetails);
                }
            }
            catch (Exception ex)
            { }

            return pTPConfirmedToday;
        }

        private BrokenTodaysQueue fetchbrokentodaysqueue()
        {
            BrokenTodaysQueue paidTodaysQueue = new BrokenTodaysQueue();
            DateTime currentDate = DateTime.Now.Date;
            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0);
            DateTime endDate = startDate.AddMonths(1); // First day of next month

            ICollection<BirdEyeViewAccountDetailDto> brokenfeedback = Encollectloanaccount.Where(x => x.DispCode != null && x.DispCode.ToLower() == DispCodeEnum.PTP.Value
                    && x.LatestPTPDate.HasValue && x.LatestPTPDate.Value.Date < currentDate
                    && x.LatestFeedbackDate >= startDate && x.LatestFeedbackDate < endDate).ToList();

            paidTodaysQueue.count = brokenfeedback.Count;
            if (brokenfeedback != null)
            {
                paidTodaysQueue.accountdetails = FeedbacksGroupbyaccount(brokenfeedback);
                paidTodaysQueue.pos = Sumpos(paidTodaysQueue.accountdetails);
            }
            return paidTodaysQueue;
        }

        private WorkableAccountsTodaysQueue fetchWorkableAccountsTodaysQueue()
        {
            WorkableAccountsTodaysQueue WorkableAccountsTodaysQueue = new WorkableAccountsTodaysQueue();
            ICollection<BirdEyeViewAccountDetailDto> TotalWorkableAssigned;
            ICollection<BirdEyeViewAccountDetailDto> WorkableAccountsfeedback;
            try
            {
                DateTime currentDate = DateTime.Now.Date;
                DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0);
                DateTime endDate = startDate.AddMonths(1); // First day of next month

                WorkableAccountsfeedback = Encollectloanaccount.Where(x => 
                    (x.LatestPaymentDate.HasValue && x.LatestPaymentDate >= startDate && x.LatestPaymentDate < endDate)
                || (x.DispCode != null && dispcodeWorkableAccounts.Contains(x.DispCode) 
                        && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate >= startDate && x.LatestFeedbackDate < endDate)
                || (x.DispCode != null && dispcodePTPGroup.Contains(x.DispCode) 
                    && x.LatestPTPDate.HasValue && x.LatestPTPDate.Value.Date >= currentDate
                    && x.LatestFeedbackDate >= startDate && x.LatestFeedbackDate < endDate)
                ).ToList();

                TotalWorkableAssigned = Encollectloanaccount.ToList();
                WorkableAccountsTodaysQueue.count = TotalWorkableAssigned.Count - WorkableAccountsfeedback.Count;
                ICollection<BirdEyeViewAccountDetailDto> accountList = TotalWorkableAssigned.Except(WorkableAccountsfeedback).ToList();
                if (WorkableAccountsfeedback != null)
                {
                    WorkableAccountsTodaysQueue.accountdetails = FeedbacksGroupbyaccount(accountList);
                    WorkableAccountsTodaysQueue.pos = Sumpos(WorkableAccountsTodaysQueue.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return WorkableAccountsTodaysQueue;
        }

        private async Task FetchFeedback()
        {
            Encollectloanaccount = new List<BirdEyeViewAccountDetailDto>();

            try
            {
                Encollectloanaccount = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                        .Where(x => x.YEAR == DateTime.Now.Year && x.MONTH == DateTime.Now.Month
                                                && (x.TeleCallerId == userId || x.CollectorId == userId))
                                        .FlexInclude(x => x.Agency)
                                        .FlexInclude(x => x.Collector)
                                        .FlexInclude(x => x.TeleCallingAgency)
                                        .FlexInclude(x => x.TeleCaller)
                                        .SelectTo<BirdEyeViewAccountDetailDto>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ICollection<BirdEyeViewAccountSearchDto> FeedbacksGroupbyaccount(ICollection<BirdEyeViewAccountDetailDto> loanAccounts)
        {
            ICollection<BirdEyeViewAccountSearchDto> obj = new List<BirdEyeViewAccountSearchDto>();

            obj = loanAccounts
                   .Select(p => new BirdEyeViewAccountSearchDto
                   {
                       Id = p.Id,
                       CustomerID = p.CUSTOMERID,
                       accountno = p.AGREEMENTID,
                       CUSTOMERNAME = p.CUSTOMERNAME,
                       EMIAMT = p.EMIAMT,//p.EMIAMT.ToString(),
                       CURRENT_BUCKET = p.CURRENT_BUCKET,
                       Product = p.PRODUCT,
                       agencyCode = p.CustomId,
                       MonthOpeningPOS = p.BOM_POS != null ? p.BOM_POS.ToString() : "",
                       CURRENT_POS = !string.IsNullOrEmpty(p.CURRENT_POS) ? Convert.ToDecimal(p.CURRENT_POS) : 0,
                       TeleCallingAgencyCode = p.TeleCallingAgency != null ? p.TeleCallingAgency : string.Empty,
                       TeleCallerName = p.TeleCaller != null ? p.TeleCaller : string.Empty,
                       CollectorName = p.Collector != null ? p.Collector : string.Empty,
                       EMI_OD_AMT = p.EMI_OD_AMT,
                       LatePaymentcharges = p.PENAL_PENDING,
                   }).ToList();

            return obj;
        }

        private decimal? Sumpos(ICollection<BirdEyeViewAccountSearchDto> accountdetails)
        {
            decimal? amount = accountdetails.Sum(x => x.CURRENT_POS);
            return amount;
        }

        private async Task dispcodeconnectfetch()
        {
            dispcodeconnects = new List<string>();

            dispcodeconnects = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.CategoryMasterId.Equals(CategoryMasterEnum.TelecallerConnect.Value)).Select(x => x.Code).ToListAsync();
        }

        private async Task dispcodecontactfetch()
        {
            dispcodecontacts = new List<string>();

            dispcodecontacts = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.CategoryMasterId.Equals(CategoryMasterEnum.TelecallerContact.Value)).Select(x => x.Code).ToListAsync();
        }

        private async Task dispcodePTPGroupfetch()
        {
            dispcodePTPGroup = new List<string>();

            dispcodePTPGroup = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.CategoryMasterId.Equals(CategoryMasterEnum.TelecallerPTPGroup.Value)).Select(x => x.Code).ToListAsync();
        }

        private async Task dispcodeWorkableAccountsfetch()
        {
            dispcodeWorkableAccounts = new List<string>();

            dispcodeWorkableAccounts = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.CategoryMasterId.Equals(CategoryMasterEnum.TelecallerWorkableAccounts.Value)).Select(x => x.Code).ToListAsync();
        }
    }

    public class GetBirdEyeViewParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string Id { get; set; }
    }
}