using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTodaysView : FlexiQueryBridgeAsync<GetTodaysViewDto>
    {
        protected readonly ILogger<GetTodaysView> _logger;
        protected GetTodaysViewParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private List<TodaysViewAccountDetailDto> Encollectloanaccount;
        private List<string> dispcodeWorkableAccounts;
        private List<string> dispcodeconnects;
        private List<string> dispcodecontacts;
        private List<string> dispcodePTPGroup;
        private string userId = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetTodaysView(ILogger<GetTodaysView> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetTodaysView AssignParameters(GetTodaysViewParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetTodaysViewDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            _repoFactory.Init(_params);
            userId = _flexAppContext.UserId;

            await dispcodeconnectfetch();
            await dispcodePTPGroupfetch();
            await dispcodeWorkableAccountsfetch();
            await dispcodecontactfetch();

            await FetchFeedback();

            GetTodaysViewDto result = await BindRecords();

            return result;
        }

        private async Task<GetTodaysViewDto> BindRecords()
        {
            GetTodaysViewDto outputmodel = new GetTodaysViewDto();
            try
            {
                outputmodel.todaysptpqueue = new TodaysPTPView();
                outputmodel.todaysptpqueue = Fetchtodaysptpqueue();
                outputmodel.PTPConfirmedToday = new PTPConfirmedView();
                outputmodel.PTPConfirmedToday = FetchPTPConfirmedToday();
                outputmodel.PaidTodaysQueue = new PaidTodaysView();
                outputmodel.PaidTodaysQueue = fetchpaidtodaysqueue();

                outputmodel.contactsUniqueTodaysQueue = new contactsUniqueTodaysView();
                outputmodel.contactsUniqueTodaysQueue = fetchcontactsUnique();
                outputmodel.ConnectsUniqueTodaysQueue = new ConnectsUniqueTodaysView();
                outputmodel.ConnectsUniqueTodaysQueue = fetchConnectsUniqueTodaysQueue();
                outputmodel.brokentodaysqueue = new BrokenTodaysView();
                outputmodel.brokentodaysqueue = fetchbrokentodaysqueue();
                outputmodel.WorkableAccounts = new WorkableAccountsTodaysView();
                outputmodel.WorkableAccounts = fetchWorkableAccountsTodaysQueue();
                outputmodel.rescheduled = new RescheduledTodaysView();
                outputmodel.rescheduled = await FetchPTPRescheduledToday();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outputmodel;
        }

        private BrokenTodaysView fetchbrokentodaysqueue()
        {
            BrokenTodaysView paidTodaysQueue = new BrokenTodaysView();
            ICollection<TodaysViewAccountDetailDto> brokenfeedback;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                brokenfeedback = Encollectloanaccount.Where(x => x.DispCode != null && x.DispCode.ToLower() == "bptp"
                        && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate >= startDate && x.LatestFeedbackDate < endDate).ToList();

                paidTodaysQueue.count = brokenfeedback.Count;
                if (Encollectloanaccount != null)
                {
                    paidTodaysQueue.accountdetails = FeedbacksGroupbyaccount(brokenfeedback);
                    paidTodaysQueue.pos = Sumpos(paidTodaysQueue.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return paidTodaysQueue;
        }

        private ConnectsUniqueTodaysView fetchConnectsUniqueTodaysQueue()
        {
            //dispcodeconnectfetch();
            ConnectsUniqueTodaysView TodaysQueue = new ConnectsUniqueTodaysView();
            ICollection<TodaysViewAccountDetailDto> todayfeedback;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                todayfeedback = Encollectloanaccount.Where(x => x.LatestFeedbackDate.HasValue
                        && x.LatestFeedbackDate.Value.Date >= startDate && x.LatestFeedbackDate.Value.Date < endDate).ToList();

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

        private contactsUniqueTodaysView fetchcontactsUnique()
        {
            //dispcodecontactfetch();
            contactsUniqueTodaysView TodaysQueue = new contactsUniqueTodaysView();
            ICollection<TodaysViewAccountDetailDto> todayfeedback;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                todayfeedback = Encollectloanaccount.Where(x => x.GroupName == "Contact" && x.LatestFeedbackDate.HasValue
                        && x.LatestFeedbackDate.Value.Date >= startDate && x.LatestFeedbackDate.Value.Date < endDate).ToList();

                TodaysQueue.count = todayfeedback.Count;
                if (todayfeedback != null)
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

        private PaidTodaysView fetchpaidtodaysqueue()
        {
            PaidTodaysView paidTodaysQueue = new PaidTodaysView();
            ICollection<TodaysViewAccountDetailDto> todayfeedback;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                todayfeedback = Encollectloanaccount.Where(x => x.LatestPaymentDate.HasValue
                    && x.LatestPaymentDate.Value.Date >= startDate && x.LatestPaymentDate.Value.Date < endDate).ToList();

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

        private PTPConfirmedView FetchPTPConfirmedToday()
        {
            PTPConfirmedView pTPConfirmedToday = new PTPConfirmedView();
            ICollection<TodaysViewAccountDetailDto> todayfeedback;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                todayfeedback = Encollectloanaccount.Where(x => x.DispCode != null && x.DispCode.ToLower() == "ptp"
                    && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Date >= startDate && x.LatestFeedbackDate.Value.Date < endDate).ToList();

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

        private TodaysPTPView Fetchtodaysptpqueue()
        {
            TodaysPTPView obj = new TodaysPTPView();
            ICollection<TodaysViewAccountDetailDto> todaysptp;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                todaysptp = Encollectloanaccount.Where(x => x.DispCode != null && x.DispCode.ToLower() == "ptp"
                    && x.LatestPTPDate.HasValue && x.LatestPTPDate.Value.Date >= startDate && x.LatestPTPDate.Value.Date < endDate).ToList();

                obj.count = todaysptp.Count;
                if (Encollectloanaccount != null)
                {
                    obj.accountdetails = FeedbacksGroupbyaccount(todaysptp);
                    obj.pos = Sumpos(obj.accountdetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Send SMS Error: ", ex.Message);
                throw ex;
            }

            return obj;
        }

        private async Task<RescheduledTodaysView> FetchPTPRescheduledToday()
        {
            RescheduledTodaysView PTPRescheduledToday = new RescheduledTodaysView();
            ICollection<TodaysViewAccountDetailDto> todayfeedback;
            try
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                todayfeedback = Encollectloanaccount.Where(x => (x.DispCode != null && dispcodePTPGroup.Contains(x.DispCode))
                    && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Date >= startDate && x.LatestFeedbackDate.Value.Date < endDate).ToList();

                foreach (TodaysViewAccountDetailDto account in todayfeedback.ToList())
                {
                    Feedback loanAccountFeedback = await FetchSecondFeedbackByAccountId(account.Id);
                    if (loanAccountFeedback == null || !string.Equals(loanAccountFeedback?.DispositionCode, DispCodeEnum.PTP.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        todayfeedback.Remove(account);
                    }
                }

                PTPRescheduledToday.count = todayfeedback.Count;
                if (todayfeedback != null)
                {
                    PTPRescheduledToday.accountdetails = FeedbacksGroupbyaccount(todayfeedback);
                    PTPRescheduledToday.pos = Sumpos(PTPRescheduledToday.accountdetails);
                }
            }
            catch (Exception ex)
            { }
            return PTPRescheduledToday;
        }

        private WorkableAccountsTodaysView fetchWorkableAccountsTodaysQueue()
        {
            WorkableAccountsTodaysView WorkableAccountsTodaysQueue = new WorkableAccountsTodaysView();
            ConnectsUniqueTodaysView TodaysQueue = new ConnectsUniqueTodaysView();
            ICollection<TodaysViewAccountDetailDto> TotalWorkableAssigned;
            ICollection<TodaysViewAccountDetailDto> WorkableAccountsfeedback;
            try
            {
                DateTime currentDate = DateTime.Now.Date;
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(1);

                WorkableAccountsfeedback = Encollectloanaccount.Where(x =>
                    (x.LatestPaymentDate.HasValue && x.LatestPaymentDate.Value.Date >= startDate && x.LatestPaymentDate.Value.Date < endDate)
                    || (x.DispCode != null && dispcodeWorkableAccounts.Contains(x.DispCode) && x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Date >= startDate && x.LatestFeedbackDate.Value.Date < endDate)
                    || (x.DispCode != null && dispcodePTPGroup.Contains(x.DispCode) && x.LatestPTPDate.HasValue && x.LatestPTPDate.Value.Date >= currentDate
                            &&( x.LatestFeedbackDate.HasValue && x.LatestFeedbackDate.Value.Date >= startDate && x.LatestFeedbackDate.Value.Date < endDate))
                ).ToList();

                TotalWorkableAssigned = Encollectloanaccount.ToList(); ;
                WorkableAccountsTodaysQueue.count = TotalWorkableAssigned.Count - WorkableAccountsfeedback.Count;
                ICollection<TodaysViewAccountDetailDto> accountFeedbackList = TotalWorkableAssigned.Except(WorkableAccountsfeedback).ToList();
                if (WorkableAccountsfeedback != null)
                {
                    WorkableAccountsTodaysQueue.accountdetails = FeedbacksGroupbyaccount(accountFeedbackList);
                    WorkableAccountsTodaysQueue.pos = Sumpos(WorkableAccountsTodaysQueue.accountdetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return WorkableAccountsTodaysQueue;
        }

        private async Task dispcodeconnectfetch()
        {
            dispcodeconnects = new List<string>();

            dispcodeconnects = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                                        .Where(a => a.CategoryMasterId.Equals(CategoryMasterEnum.TelecallerConnect.Value))
                                        .Select(x => x.Code).ToListAsync();
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


        private async Task<Feedback> FetchSecondFeedbackByAccountId(string accountId)
        {
            return await _repoFactory.GetRepo().FindAll<Feedback>().Where(a => a.AccountId == accountId).OrderByDescending(x => x.CreatedDate).Skip(1).FirstOrDefaultAsync();
        }

        private ICollection<TodaysViewAccountSearchDto> FeedbacksGroupbyaccount(ICollection<TodaysViewAccountDetailDto> loanAccounts)
        {
            ICollection<TodaysViewAccountSearchDto> obj = new List<TodaysViewAccountSearchDto>();

            obj = loanAccounts
                   .Select(p => new TodaysViewAccountSearchDto
                   {
                       Id = p.Id,
                       CURRENT_POS = !string.IsNullOrEmpty(p.CURRENT_POS) ? Convert.ToDecimal(p.CURRENT_POS) : 0,
                       CUSTOMERNAME = p.CUSTOMERNAME,
                       accountno = p.AGREEMENTID,
                       EMIAMT = p.EMIAMT,
                       CURRENT_BUCKET = p.CURRENT_BUCKET,
                       CustomerID = p.CUSTOMERID,
                       // BOMBuket = Convert.ToString(p.BUCKET),
                       Product = p.PRODUCT,
                       MonthOpeningPOS = p.BOM_POS.HasValue ? Convert.ToString(p.BOM_POS) : "",
                       TeleCallingAgencyCode = p.CustomId,
                       CollectorName = p.Collector
                       //TeleCallerName = p.TeleCaller.FirstName
                   }).ToList();

            return obj;
        }

        private async Task FetchFeedback()
        {
            Encollectloanaccount = new List<TodaysViewAccountDetailDto>();

            try
            {
                Encollectloanaccount = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                        .Where(x => x.YEAR == DateTime.Now.Year && x.MONTH == DateTime.Now.Month
                                                && (x.TeleCallerId == userId || x.CollectorId == userId))
                                        .FlexInclude(x => x.Agency)
                                        .FlexInclude(x => x.Collector)
                                        .FlexInclude(x => x.TeleCallingAgency)
                                        .FlexInclude(x => x.TeleCaller)
                                        .SelectTo<TodaysViewAccountDetailDto>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private decimal? Sumpos(ICollection<TodaysViewAccountSearchDto> accountdetails)
        {
            decimal? amount = accountdetails.Sum(x => x.CURRENT_POS);
            return amount;
        }
    }

    public class GetTodaysViewParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string Id { get; set; }
    }
}