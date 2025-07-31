using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTreatmentById : FlexiQueryBridgeAsync<Treatment, GetTreatmentByIdDto>
    {
        protected readonly ILogger<GetTreatmentById> _logger;
        protected GetTreatmentByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetTreatmentById(ILogger<GetTreatmentById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetTreatmentById AssignParameters(GetTreatmentByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetTreatmentByIdDto> Fetch()
        {
            List<ViewSubTreatmentOutputDto> subtreatmentlist = new List<ViewSubTreatmentOutputDto>();
            var result = await Build<Treatment>().SelectTo<GetTreatmentByIdDto>().FirstOrDefaultAsync();

            var subtreatments = result.subTreatment.Where(a => a.IsDeleted == false).ToList();

            foreach (var sub in subtreatments)
            {
                ViewSubTreatmentOutputDto res = new ViewSubTreatmentOutputDto();
                res.Id = sub.Id;
                res.TreatmentType = sub.TreatmentType;
                res.AllocationType = sub.AllocationType;
                res.StartDay = sub.StartDay;
                res.EndDay = sub.EndDay;
                res.Order = sub.Order;
                res.POSCriteria = sub?.POSCriteria?.Where(a => a.IsDeleted == false).OrderByDescending(a => a.Id).ToList();
                res.AccountCriteria = sub?.AccountCriteria?.Where(a => a.IsDeleted == false).OrderByDescending(a => a.Id).ToList();
                res.RoundRobinCriteria = sub?.RoundRobinCriteria?.Where(a => a.IsDeleted == false).OrderByDescending(a => a.Id).ToList();
                res.TreatmentByRule = sub.TreatmentByRule?.Where(a => a.IsDeleted == false).ToList();
                res.ScriptToPersueCustomer = sub.ScriptToPersueCustomer;
                res.PreSubtreatmentOrder = sub.PreSubtreatmentOrder;
                res.QualifyingCondition = sub.QualifyingCondition;
                res.QualifyingStatus = sub.QualifyingStatus;
                res.Communication = sub?.Communication;
                res.UpdateTrail = sub?.UpdateTrail;
                res.Designation = sub?.Designation;
                //}

                if (res.TreatmentByRule != null && res.TreatmentByRule.Count() > 0)
                {
                    ViewTreatmentByRuleDto outputrule = res.TreatmentByRule.FirstOrDefault();
                    outputrule.TreatmentRules = new List<ViewTreatmentRules>();

                    if (outputrule != null)
                    {
                        //logger.LogInformation("ViewTreatment started 5");
                        string rulevalue = outputrule.Rule;

                        string[] stringSeparators = new string[] { "and", "or" };
                        string[] stringruleSeparators = new string[] { "1", "2", "6", "9", "11", "12", "13", "14" };

                        string[] rulenames = rulevalue.Split(stringSeparators, StringSplitOptions.None);
                        string[] operatorname = rulevalue.Split(stringruleSeparators, StringSplitOptions.None);
                        operatorname = operatorname.Where(a => a != "").ToArray();
                        for (int i = 0; i < rulenames.Count(); i++)
                        {
                            int value = Convert.ToInt32(rulenames[i]);
                            char index = (char)value;
                            var enumrulename = (TreatmentRules)value;

                            ViewTreatmentRules rule = new ViewTreatmentRules();
                            rule.RuleId = rulenames[i];
                            rule.RuleName = enumrulename.ToString();
                            if (i >= 0 && i < operatorname.Length)
                            {
                                if (operatorname[i].Contains("and"))
                                {
                                    rule.RuleOperator = "and";
                                }
                                else if (operatorname[i].Contains("or"))
                                {
                                    rule.RuleOperator = "or";
                                }
                                else
                                {
                                    rule.RuleOperator = "";
                                }
                            }
                            outputrule.TreatmentRules.Add(rule);
                        }
                    }
                }

                subtreatmentlist.Add(res);
            }
            result.subTreatment = subtreatmentlist.OrderBy(a => a.Order).ToList();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                  .FlexInclude("subTreatment.TreatmentOnCommunication")
                                  .FlexInclude("subTreatment.TreatmentOnUpdateTrail")
                                  .Where(t => t.Id == _params.Id);
            return query;
        }
    }

    public class GetTreatmentByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}