using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTreatmentRules : FlexiQueryEnumerableBridgeAsync<GetTreatmentRulesDto>
    {
        protected readonly ILogger<GetTreatmentRules> _logger;
        protected GetTreatmentRulesParams _params;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetTreatmentRules(ILogger<GetTreatmentRules> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTreatmentRules AssignParameters(GetTreatmentRulesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTreatmentRulesDto>> Fetch()
        {
            IEnumerable<GetTreatmentRulesDto> result = new List<GetTreatmentRulesDto>();

            result = new List<GetTreatmentRulesDto>()
                    {
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.SamePBGasAccount,
                            Name = "Same PBG as Account"
                        },
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.LatestAgency,
                            Name = "Latest Agency"
                        },
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.StaffWhoHasCreatedTheLoan,
                            Name = "Staff Who Has Created The Loan"
                        },
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.SameBranchAsAccount,
                            Name = "Same Branch As Account"
                        },
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.AllocateTillLoadIsReached,
                            Name = "Allocate Till Load Is Reached"
                        },
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.SamePincodeAsAccount,
                            Name = "Same Pincode As Account"
                        },
                        new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.SamePersonaInSkillAsAccount,
                            Name = "Same Persona In Skill As Account"
                        },
                         new GetTreatmentRulesDto()
                        {
                            Id = (int)TreatmentRulesEnum.SameSubProductAsAccount,
                            Name = "Same SubProduct As Account"
                        }
                    };

            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetTreatmentRulesParams : DtoBridge
    {
        public string Type { get; set; }
    }
}