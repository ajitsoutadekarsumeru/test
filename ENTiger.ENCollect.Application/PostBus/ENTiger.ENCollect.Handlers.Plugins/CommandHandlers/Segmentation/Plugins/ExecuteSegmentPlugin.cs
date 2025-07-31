using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ExecuteSegmentPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteSegmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a145c69c5b23d3a04ad5b3f0e0571dc";
        public override string FriendlyName { get; set; } = "ExecuteSegmentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteSegmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        private readonly IELKUtility _elasticUtility;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ExecuteSegmentPlugin(ILogger<ExecuteSegmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IELKUtility elasticUtility)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _elasticUtility = elasticUtility;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(ExecuteSegmentPostBusDataPacket packet)
        {
            //Write your code here:
            _repoFactory.Init(packet.Cmd.Dto);
            var client = _elasticUtility.GetElasticConnection();

            List<Segmentation> segmentations = null;
            segmentations = await _repoFactory.GetRepo().FindAll<Segmentation>()
                                    .FlexInclude(x => x.SegmentAdvanceFilter)
                                    .Where(a => a.Id == packet.Cmd.Dto.Id).ToListAsync();
            Segmentation segment = segmentations[0];

            var segmentadvancefilter = segment.SegmentAdvanceFilter;

            string productgroup = _elasticUtility.GetFilterTextForElasticSearch(segment.ProductGroup);
            string product = _elasticUtility.GetFilterTextForElasticSearch(segment.Product);
            string subproduct = _elasticUtility.GetFilterTextForElasticSearch(segment.SubProduct);
            string bucket = _elasticUtility.GetFilterTextForElasticSearch(segment.BOM_Bucket);
            string currentbucket = _elasticUtility.GetFilterTextForElasticSearch(segment.CurrentBucket);
            string currentdpd = _elasticUtility.GetFilterTextForElasticSearch(segment.Current_DPD);
            string region = _elasticUtility.GetFilterTextForElasticSearch(segment.Region);
            string state = _elasticUtility.GetFilterTextForElasticSearch(segment.State);
            string city = _elasticUtility.GetFilterTextForElasticSearch(segment.City);
            string branch = _elasticUtility.GetFilterTextForElasticSearch(segment.Branch);
            string clustername = _elasticUtility.GetFilterTextForElasticSearch(segment.ClusterName);

            string customerpersona = string.Empty;
            string lastdispositioncode = string.Empty;
            string npa_stageid = string.Empty;
            string preferredmodeofpayment = string.Empty;
            string currentdpdfrom = string.Empty;
            string currentdpdto = string.Empty;
            string creditbureauscorefrom = string.Empty;
            string creditbureauscoreto = string.Empty;
            string principal_odfrom = string.Empty;
            string principal_odto = string.Empty;
            string tosfrom = string.Empty;
            string tosto = string.Empty;
            string customerbehaviourscore1from = string.Empty;
            string customerbehaviourscore1to = string.Empty;
            string customerbehaviourscore2from = string.Empty;
            string customerbehaviourscore2to = string.Empty;
            string earlywarningscorefrom = string.Empty;
            string earlywarningscoreto = string.Empty;
            string legalstagefrom = string.Empty;
            string legalstageto = string.Empty;
            string repostagefrom = string.Empty;
            string repostageto = string.Empty;
            string settlementstagefrom = string.Empty;
            string settlementstageto = string.Empty;
            string customerbehaviorscoretokeephiswordfrom = string.Empty;
            string customerbehaviorscoretokeephiswordto = string.Empty;
            string propensitytopayonlinefrom = string.Empty;
            string propensitytopayonlineto = string.Empty;
            string digitalcontactabilityscorefrom = string.Empty;
            string digitalcontactabilityscoreto = string.Empty;
            string callcontactabilityscorefrom = string.Empty;
            string callcontactabilityscoreto = string.Empty;
            string fieldcontactabilityscorefrom = string.Empty;
            string fieldcontactabilityscoreto = string.Empty;
            string statementdatefrom = string.Empty;
            string statementdateto = string.Empty;
            string duedatefrom = string.Empty;
            string duedateto = string.Empty;

            string TotalOverdueAmountadvancefilter = string.Empty;
            string DNDFlagadvancefilter = string.Empty;
            string minimumAmountDue = string.Empty;
            string Month = string.Empty;
            string Year = string.Empty;
            string loan_status = string.Empty;
            string emi_od_amt_from = string.Empty;
            string emi_od_amt_to = string.Empty;

            bool todayplus = false;

            if (segmentadvancefilter != null)
            {
                customerpersona = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.CustomerPersona);
                lastdispositioncode = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.LastDispositionCode);
                npa_stageid = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.NPA_STAGEID);
                preferredmodeofpayment = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.PreferredModeOfPayment);
                TotalOverdueAmountadvancefilter = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.TotalOverdueAmount);
                DNDFlagadvancefilter = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.DNDFlag);
                minimumAmountDue = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.MinimumAmountDue);
                Month = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.Month);
                Year = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.Year);
                loan_status = _elasticUtility.GetFilterTextForElasticSearch(segmentadvancefilter.LOAN_STATUS);

                var currentdpdadvancefilter = segmentadvancefilter.CurrentDPD;
                var creditbureauscoreadvancefilter = segmentadvancefilter.CreditBureauScore;
                var principalodadvancefilter = segmentadvancefilter.PRINCIPAL_OD;
                var tosadvancefilter = segmentadvancefilter.TOS;
                var customerbehaviourscore1advancefilter = segmentadvancefilter.CustomerBehaviourScore1;
                var customerbehaviourscore2advancefilter = segmentadvancefilter.CustomerBehaviourScore2;
                var earlywarningscoreadvancefilter = segmentadvancefilter.EarlyWarningScore;
                var legalstageadvancefilter = segmentadvancefilter.LegalStage;
                var repostageadvancefilter = segmentadvancefilter.RepoStage;
                var settlementstageadvancefilter = segmentadvancefilter.SettlementStage;
                var customerbehaviorscoretokeephiswordadvancefilter = segmentadvancefilter.CustomerBehaviorScoreToKeepHisWord;
                var propensitytopayonlineadvancefilter = segmentadvancefilter.PropensityToPayOnline;
                var digitalcontactabilityscoreadvancefilter = segmentadvancefilter.DigitalContactabilityScore;
                var callcontactabilityscoreadvancefilter = segmentadvancefilter.CallContactabilityScore;
                var fieldcontactabilityscoreadvancefilter = segmentadvancefilter.FieldContactabilityScore;
                string settlementdateadvancefilter = segmentadvancefilter.StatementDate;
                string duedateadvancefilter = segmentadvancefilter.DueDate;
                var emi_od_amt = segmentadvancefilter.EMI_OD_AMT;

                if (!string.IsNullOrEmpty(emi_od_amt))
                {
                    string[] emi_od_amt_arr = emi_od_amt.Split('-');
                    emi_od_amt_from = _elasticUtility.GetFilterTextForElasticSearch(emi_od_amt_arr[0]);
                    emi_od_amt_to = _elasticUtility.GetFilterTextForElasticSearch(emi_od_amt_arr[1]);
                }

                if (!string.IsNullOrEmpty(currentdpdadvancefilter))
                {
                    string[] currentdpdarr = currentdpdadvancefilter.Split('-');
                    currentdpdfrom = _elasticUtility.GetFilterTextForElasticSearch(currentdpdarr[0]);
                    currentdpdto = _elasticUtility.GetFilterTextForElasticSearch(currentdpdarr[1]);
                }
                if (!string.IsNullOrEmpty(principalodadvancefilter))
                {
                    string[] principalodarr = principalodadvancefilter.Split('-');
                    principal_odfrom = _elasticUtility.GetFilterTextForElasticSearch(principalodarr[0]);
                    principal_odto = _elasticUtility.GetFilterTextForElasticSearch(principalodarr[1]);
                }
                if (!string.IsNullOrEmpty(tosadvancefilter))
                {
                    string[] tosarr = tosadvancefilter.Split('-');
                    tosfrom = _elasticUtility.GetFilterTextForElasticSearch(tosarr[0]);
                    tosto = _elasticUtility.GetFilterTextForElasticSearch(tosarr[1]);
                }
                if (!string.IsNullOrEmpty(creditbureauscoreadvancefilter))
                {
                    string[] creditbureauscoreadvancefilterarr = creditbureauscoreadvancefilter.Split('-');
                    creditbureauscorefrom = _elasticUtility.GetFilterTextForElasticSearch(creditbureauscoreadvancefilterarr[0]);
                    creditbureauscoreto = _elasticUtility.GetFilterTextForElasticSearch(creditbureauscoreadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(customerbehaviourscore1advancefilter))
                {
                    string[] customerbehaviourscore1advancefilterarr = customerbehaviourscore1advancefilter.Split('-');
                    customerbehaviourscore1from = _elasticUtility.GetFilterTextForElasticSearch(customerbehaviourscore1advancefilterarr[0]);
                    customerbehaviourscore1to = _elasticUtility.GetFilterTextForElasticSearch(customerbehaviourscore1advancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(customerbehaviourscore2advancefilter))
                {
                    string[] customerbehaviourscore2advancefilterarr = customerbehaviourscore2advancefilter.Split('-');
                    customerbehaviourscore2from = _elasticUtility.GetFilterTextForElasticSearch(customerbehaviourscore2advancefilterarr[0]);
                    customerbehaviourscore2to = _elasticUtility.GetFilterTextForElasticSearch(customerbehaviourscore2advancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(earlywarningscoreadvancefilter))
                {
                    string[] earlywarningscoreadvancefilterarr = earlywarningscoreadvancefilter.Split('-');
                    earlywarningscorefrom = _elasticUtility.GetFilterTextForElasticSearch(earlywarningscoreadvancefilterarr[0]);
                    earlywarningscoreto = _elasticUtility.GetFilterTextForElasticSearch(earlywarningscoreadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(legalstageadvancefilter))
                {
                    string[] legalstageadvancefilterarr = legalstageadvancefilter.Split('-');
                    legalstagefrom = _elasticUtility.GetFilterTextForElasticSearch(legalstageadvancefilterarr[0]);
                    legalstageto = _elasticUtility.GetFilterTextForElasticSearch(legalstageadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(repostageadvancefilter))
                {
                    string[] repostageadvancefilterarr = repostageadvancefilter.Split('-');
                    repostagefrom = _elasticUtility.GetFilterTextForElasticSearch(repostageadvancefilterarr[0]);
                    repostageto = _elasticUtility.GetFilterTextForElasticSearch(repostageadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(settlementstageadvancefilter))
                {
                    string[] settlementstageadvancefilterarr = settlementstageadvancefilter.Split('-');
                    settlementstagefrom = _elasticUtility.GetFilterTextForElasticSearch(settlementstageadvancefilterarr[0]);
                    settlementstageto = _elasticUtility.GetFilterTextForElasticSearch(settlementstageadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(customerbehaviorscoretokeephiswordadvancefilter))
                {
                    string[] customerbehaviorscoretokeephiswordadvancefilterarr = customerbehaviorscoretokeephiswordadvancefilter.Split('-');
                    customerbehaviorscoretokeephiswordfrom = _elasticUtility.GetFilterTextForElasticSearch(customerbehaviorscoretokeephiswordadvancefilterarr[0]);
                    customerbehaviorscoretokeephiswordto = _elasticUtility.GetFilterTextForElasticSearch(customerbehaviorscoretokeephiswordadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(propensitytopayonlineadvancefilter))
                {
                    string[] propensitytopayonlineadvancefilterarr = propensitytopayonlineadvancefilter.Split('-');
                    propensitytopayonlinefrom = _elasticUtility.GetFilterTextForElasticSearch(propensitytopayonlineadvancefilterarr[0]);
                    propensitytopayonlineto = _elasticUtility.GetFilterTextForElasticSearch(propensitytopayonlineadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(digitalcontactabilityscoreadvancefilter))
                {
                    string[] digitalcontactabilityscoreadvancefilterarr = digitalcontactabilityscoreadvancefilter.Split('-');
                    digitalcontactabilityscorefrom = _elasticUtility.GetFilterTextForElasticSearch(digitalcontactabilityscoreadvancefilterarr[0]);
                    digitalcontactabilityscoreto = _elasticUtility.GetFilterTextForElasticSearch(digitalcontactabilityscoreadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(callcontactabilityscoreadvancefilter))
                {
                    string[] callcontactabilityscoreadvancefilterarr = callcontactabilityscoreadvancefilter.Split('-');
                    callcontactabilityscorefrom = _elasticUtility.GetFilterTextForElasticSearch(callcontactabilityscoreadvancefilterarr[0]);
                    callcontactabilityscoreto = _elasticUtility.GetFilterTextForElasticSearch(callcontactabilityscoreadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(fieldcontactabilityscoreadvancefilter))
                {
                    string[] fieldcontactabilityscoreadvancefilterarr = fieldcontactabilityscoreadvancefilter.Split('-');
                    fieldcontactabilityscorefrom = _elasticUtility.GetFilterTextForElasticSearch(fieldcontactabilityscoreadvancefilterarr[0]);
                    fieldcontactabilityscoreto = _elasticUtility.GetFilterTextForElasticSearch(fieldcontactabilityscoreadvancefilterarr[1]);
                }
                if (!string.IsNullOrEmpty(settlementdateadvancefilter))
                {
                    if (string.Equals(settlementdateadvancefilter, "today"))
                    {
                        statementdatefrom = "now/d";
                        statementdateto = "now/d";
                    }
                    else
                    {
                        string todaysdate = GetValueBasedOnTodayDate(settlementdateadvancefilter);

                        if (settlementdateadvancefilter.Contains("+"))
                        {
                            statementdatefrom = "now/d-" + todaysdate;
                            statementdateto = statementdatefrom;//"now/d";
                            todayplus = true;
                        }
                        else if (settlementdateadvancefilter.Contains("-"))
                        {
                            statementdatefrom = "now/d+" + todaysdate;
                            statementdateto = statementdatefrom;//"now/d";
                            todayplus = true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(duedateadvancefilter))
                {
                    if (string.Equals(duedateadvancefilter, "today"))
                    {
                        duedatefrom = "now/d";
                        duedateto = "now/d";
                    }
                    else
                    {
                        string todaysdate = GetValueBasedOnTodayDate(duedateadvancefilter);

                        if (duedateadvancefilter.Contains("+"))
                        {
                            duedatefrom = "now/d-" + todaysdate;
                            duedateto = duedatefrom;//"now/d";
                            todayplus = true;
                        }
                        else if (duedateadvancefilter.Contains("-"))
                        {
                            _logger.LogInformation("todaysdate " + todaysdate);
                            duedatefrom = "now/d+" + todaysdate;
                            duedateto = duedatefrom;//"now/d";
                            todayplus = true;
                        }
                    }
                }
            }

            var fetchindexname = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => a.Parameter == "SegmentationIndexName").FirstOrDefaultAsync();

            string loanaccountsIndex = fetchindexname?.Value;//InitFlexEF.GetElasticSearchConnection(packet.TenantId).LoanAccountIndexName;
            _logger.LogInformation("ExecuteSegmentFFPlugin : Index - " + loanaccountsIndex);

            int commacounter = 0;
            string DSLQueryForAllDocs = @"
                {
                    ""query"": {
                    ""bool"": {
                    ""must"": [
                ";

            if (productgroup != MagickString.NoFilterPresent)
            {
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""productgroup"",
                        ""query"": ""{productgroup}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (product != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }

                DSLQueryForAllDocs += $@"
                        {{
                          ""query_string"": {{
                            ""default_field"": ""product"",
                            ""query"": ""{product}""
                          }}
                        }}
                        ";
            }
            if (subproduct != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""subproduct"",
                        ""query"": ""{subproduct}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (bucket != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""bucket"",
                        ""query"": ""{bucket}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (currentbucket != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""current_bucket"",
                        ""query"": ""{currentbucket}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (currentdpd != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""current_dpd"",
                        ""query"": ""{currentdpd}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (region != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""region"",
                        ""query"": ""{region}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (state != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""state"",
                        ""query"": ""{state}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (city != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""city"",
                        ""query"": ""{city}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (clustername != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""cluster_name"",
                        ""query"": ""{clustername}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (branch != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""branch"",
                        ""query"": ""{branch}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (customerpersona != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""customerpersona"",
                        ""query"": ""{customerpersona}""
                      }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(currentdpdfrom) && !string.IsNullOrEmpty(currentdpdto) && currentdpdfrom != MagickString.NoFilterPresent && currentdpdto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""current_dpd"": {{
                            ""gte"":""{currentdpdfrom}"",
                            ""lte"":""{currentdpdto}""
                        }}
                    }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(creditbureauscorefrom) && !string.IsNullOrEmpty(creditbureauscoreto) && creditbureauscorefrom != MagickString.NoFilterPresent && creditbureauscoreto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""creditbureauscore"": {{
                            ""gte"":""{creditbureauscorefrom}"",
                            ""lte"":""{creditbureauscoreto}""
                        }}
                    }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(customerbehaviourscore1from) && !string.IsNullOrEmpty(customerbehaviourscore1to) && customerbehaviourscore1from != MagickString.NoFilterPresent && customerbehaviourscore1to != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""customerbehaviourscore1"": {{
                            ""gte"":""{customerbehaviourscore1from}"",
                            ""lte"":""{customerbehaviourscore1to}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(customerbehaviourscore2from) && !string.IsNullOrEmpty(customerbehaviourscore2to) && customerbehaviourscore2from != MagickString.NoFilterPresent && customerbehaviourscore2to != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""customerbehaviourscore2"": {{
                            ""gte"":""{customerbehaviourscore2from}"",
                            ""lte"":""{customerbehaviourscore2to}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(earlywarningscorefrom) && !string.IsNullOrEmpty(earlywarningscoreto) && earlywarningscorefrom != MagickString.NoFilterPresent && earlywarningscoreto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""earlywarningscore"": {{
                            ""gte"":""{earlywarningscorefrom}"",
                            ""lte"":""{earlywarningscoreto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(legalstagefrom) && !string.IsNullOrEmpty(legalstageto) && legalstagefrom != MagickString.NoFilterPresent && legalstageto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""legalstage"": {{
                            ""gte"":""{legalstagefrom}"",
                            ""lte"":""{legalstageto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(repostagefrom) && !string.IsNullOrEmpty(repostageto) && repostagefrom != MagickString.NoFilterPresent && repostageto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""repostage"": {{
                            ""gte"":""{repostagefrom}"",
                            ""lte"":""{repostageto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(settlementstagefrom) && !string.IsNullOrEmpty(settlementstageto) && settlementstagefrom != MagickString.NoFilterPresent && settlementstageto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""settlementstage"": {{
                            ""gte"":""{settlementstagefrom}"",
                            ""lte"":""{settlementstageto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(customerbehaviorscoretokeephiswordfrom) && !string.IsNullOrEmpty(customerbehaviorscoretokeephiswordto) && customerbehaviorscoretokeephiswordfrom != MagickString.NoFilterPresent && customerbehaviorscoretokeephiswordto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""customerbehaviorscoretokeephisword"": {{
                            ""gte"":""{customerbehaviorscoretokeephiswordfrom}"",
                            ""lte"":""{customerbehaviorscoretokeephiswordto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (preferredmodeofpayment != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""preferredmodeofpayment"",
                        ""query"": ""{preferredmodeofpayment}""
                      }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(propensitytopayonlinefrom) && !string.IsNullOrEmpty(propensitytopayonlineto) && propensitytopayonlinefrom != MagickString.NoFilterPresent && propensitytopayonlineto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""propensitytopayonline"": {{
                            ""gte"":""{propensitytopayonlinefrom}"",
                            ""lte"":""{propensitytopayonlineto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(digitalcontactabilityscorefrom) && !string.IsNullOrEmpty(digitalcontactabilityscoreto) && digitalcontactabilityscorefrom != MagickString.NoFilterPresent && digitalcontactabilityscoreto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""digitalcontactabilityscore"": {{
                            ""gte"":""{digitalcontactabilityscorefrom}"",
                            ""lte"":""{digitalcontactabilityscoreto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(callcontactabilityscorefrom) && !string.IsNullOrEmpty(callcontactabilityscoreto) && callcontactabilityscorefrom != MagickString.NoFilterPresent && callcontactabilityscoreto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""callcontactabilityscore"": {{
                            ""gte"":""{callcontactabilityscorefrom}"",
                            ""lte"":""{callcontactabilityscoreto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(fieldcontactabilityscorefrom) && !string.IsNullOrEmpty(fieldcontactabilityscoreto) && fieldcontactabilityscorefrom != MagickString.NoFilterPresent && fieldcontactabilityscoreto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""fieldcontactabilityscore"": {{
                            ""gte"":""{fieldcontactabilityscorefrom}"",
                            ""lte"":""{fieldcontactabilityscoreto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }

            if (lastdispositioncode != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""dispcode"",
                        ""query"": ""{lastdispositioncode}""
                      }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (npa_stageid != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""npa_stageid"",
                        ""query"": ""{npa_stageid}""
                      }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (!string.IsNullOrEmpty(principal_odfrom) && !string.IsNullOrEmpty(principal_odto) && principal_odfrom != MagickString.NoFilterPresent && principal_odto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""principal_od"": {{
                            ""gte"":""{principal_odfrom}"",
                            ""lte"":""{principal_odto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }

            if (!string.IsNullOrEmpty(tosfrom) && !string.IsNullOrEmpty(tosto) && tosfrom != MagickString.NoFilterPresent && tosto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""tos"": {{
                            ""gte"":""{tosfrom}"",
                            ""lte"":""{tosto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            _logger.LogInformation("statementdatefrom statementdateto " + statementdatefrom + " " + statementdateto);
            if ((todayplus == false) && !string.IsNullOrEmpty(statementdatefrom) && !string.IsNullOrEmpty(statementdateto) && statementdatefrom != MagickString.NoFilterPresent && statementdateto != MagickString.NoFilterPresent)
            {
                _logger.LogInformation("Statemented date");
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""last_statement_date"": {{
                            ""time_zone"": ""+05:30"",
                            ""gte"":""{statementdatefrom}"",
                            ""lte"":""{statementdateto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if ((todayplus == true) && !string.IsNullOrEmpty(statementdatefrom) && !string.IsNullOrEmpty(statementdateto) && statementdatefrom != MagickString.NoFilterPresent && statementdateto != MagickString.NoFilterPresent)
            {
                _logger.LogInformation("Statemented date");
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""last_statement_date"": {{
                            ""time_zone"": ""+05:30"",
                            ""gte"":""{statementdatefrom}"",
                            ""lte"":""{statementdateto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if ((todayplus == false) && !string.IsNullOrEmpty(duedatefrom) && !string.IsNullOrEmpty(duedateto) && duedatefrom != MagickString.NoFilterPresent && duedateto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""statemented_due_date_system"": {{
                            ""time_zone"": ""+05:30"",
                            ""gte"":""{duedatefrom}"",
                            ""lte"":""{duedateto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if ((todayplus == true) && !string.IsNullOrEmpty(duedatefrom) && !string.IsNullOrEmpty(duedateto) && duedatefrom != MagickString.NoFilterPresent && duedateto != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""statemented_due_date_system"": {{
                            ""time_zone"": ""+05:30"",
                            ""gte"":""{duedatefrom}"",
                            ""lte"":""{duedateto}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (TotalOverdueAmountadvancefilter != MagickString.NoFilterPresent)
            {
                TotalOverdueAmountadvancefilter = TotalOverdueAmountadvancefilter.Replace(">", "").Replace("greaterthan-", "").Replace("GREATERTHAN-", "");
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""current_total_amount_due"": {{
                            ""gt"":""{TotalOverdueAmountadvancefilter}""
                        }}
                    }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (DNDFlagadvancefilter != MagickString.NoFilterPresent)
            {
                string dndvalue = string.Empty;
                if (string.Equals(DNDFlagadvancefilter, "yes"))
                {
                    dndvalue = "true";
                }
                else
                {
                    dndvalue = "false";
                }

                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""isdndenabled"",
                        ""query"": ""{dndvalue}""
                      }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (Month != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""month"",
                        ""query"": ""{Month}""
                      }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (Year != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""query_string"": {{
                        ""default_field"": ""year"",
                        ""query"": ""{Year}""
                      }}
                    }}
                    ";

                commacounter = commacounter + 1;
            }
            if (loan_status != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                        {{
                          ""query_string"": {{
                            ""default_field"": ""loan_status"",
                            ""query"": ""{loan_status}""
                          }}
                        }}
                        ";
            }
            if (!string.IsNullOrEmpty(emi_od_amt_from) && !string.IsNullOrEmpty(emi_od_amt_to) && emi_od_amt_from != MagickString.NoFilterPresent && emi_od_amt_to != MagickString.NoFilterPresent)
            {
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""emi_od_amt"": {{
                            ""gte"":""{emi_od_amt_from}"",
                            ""lte"":""{emi_od_amt_to}""
                        }}
                    }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }
            if (minimumAmountDue != MagickString.NoFilterPresent)
            {
                minimumAmountDue = minimumAmountDue.Replace(">", "").Replace("greaterthan-", "").Replace("GREATERTHAN-", "");
                if (commacounter > 0)
                {
                    DSLQueryForAllDocs += $@",";
                }
                DSLQueryForAllDocs += $@"
                    {{
                      ""range"": {{
                        ""current_minimum_amount_due"": {{
                            ""gt"":""{minimumAmountDue}""
                        }}
                    }}
                    }}
                    ";
                commacounter = commacounter + 1;
            }

            DSLQueryForAllDocs += $@"

                ]
                }}
                }}
                 ,""script"": ""ctx._source.segmentationid = '{packet.Cmd.Dto.Id}'""
             }}
                ";

            string elasticsearchapipath = loanaccountsIndex + "/_update_by_query";

            var ElkResp = await client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs));
            string response = ElkResp.Body;
            _logger.LogInformation("Execute Segment Update query response " + response);

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        public string GetValueBasedOnTodayDate(string advancefiltervalue)
        {
            string days = string.Empty;

            if (!string.IsNullOrEmpty(advancefiltervalue))
            {
                // Extract the number from the string
                var prefix = "today";
                var numberString = advancefiltervalue.Substring(prefix.Length);

                // Try to parse the number from the extracted string
                if (int.TryParse(numberString, out int number))
                {
                    // Check if it's a valid "today+X" or "today-X"
                    if (advancefiltervalue.StartsWith("today") && (number >= 1 && number <= 50))
                    {
                        days = $"{Math.Abs(number)}d";
                    }
                }
            }

            return days;
        }
    }
}