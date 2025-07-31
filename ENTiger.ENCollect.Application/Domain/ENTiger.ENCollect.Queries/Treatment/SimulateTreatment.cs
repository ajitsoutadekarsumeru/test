using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class SimulateTreatment : FlexiQueryEnumerableBridgeAsync<SimulateTreatmentDto>
    {
        protected readonly ILogger<SimulateTreatment> _logger;
        protected SimulateTreatmentParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SimulateTreatment(ILogger<SimulateTreatment> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SimulateTreatment AssignParameters(SimulateTreatmentParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SimulateTreatmentDto>> Fetch()
        {
            decimal? TotalBOM_POS = 0;
            List<SimulateTreatmentDto> output = new List<SimulateTreatmentDto>();
            _repoFactory.Init(_params);

            TotalBOM_POS = await _repoFactory.GetRepo().FindAll<LoanAccount>().SumAsync(a => a.BOM_POS);

            var accounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a => _params.TreatmentIds.Contains(a.TreatmentId)).Select(a => new
            {
                a.TreatmentId,
                a.BOM_POS
            }).ToListAsync();

            if (accounts.Count > 0)
            {
                var groupedaccounts = accounts.GroupBy(a => a.TreatmentId);

                foreach (var x in groupedaccounts)
                {
                    SimulateTreatmentDto result = new SimulateTreatmentDto();
                    result.TreatmentId = x.Key;
                    result.Count = accounts.Where(b => b.TreatmentId == result.TreatmentId).Count();
                    result.BOMPos = Convert.ToDouble(x.Sum(b => b.BOM_POS));
                    if (TotalBOM_POS != 0)
                    {
                        string percentage = (result.BOMPos / Convert.ToDouble(TotalBOM_POS) * 100).ToString();
                        result.BOMPosPercentage = Convert.ToDouble(String.Format("{0:0.00}", percentage));
                    }
                    output.Add(result);
                }

                if (output.Count() > 0)
                {
                    var res =await _repoFactory.GetRepo().FindAll<Treatment>().Where(a => output.Select(b => b.TreatmentId).Contains(a.Id)).ToListAsync();

                    output.ForEach(a =>
                    {
                        var p = res.Where(b => b.Id == a.TreatmentId).FirstOrDefault();
                        a.TreatmentName = p != null ? p.Name : "";
                    });
                }
            }

            return output;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SimulateTreatmentParams : DtoBridge
    {
        public List<string> TreatmentIds { get; set; }
    }
}