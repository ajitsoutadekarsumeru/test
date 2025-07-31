using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class UpdateTreatmentPlugin : FlexiPluginBase, IFlexiPlugin<UpdateTreatmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1462a72047b92fb64401c4767b6b7d";
        public override string FriendlyName { get; set; } = "UpdateTreatmentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateTreatmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateTreatmentPlugin(ILogger<UpdateTreatmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateTreatmentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var obj = await _repoFactory.GetRepo().FindAll<Treatment>().FlexInclude(a => a.subTreatment)
                                    .FlexInclude("subTreatment.POSCriteria")
                                    .FlexInclude("subTreatment.AccountCriteria")
                                    .FlexInclude("subTreatment.RoundRobinCriteria")
                                    .FlexInclude("subTreatment.TreatmentByRule")
                                    .FlexInclude("subTreatment.TreatmentOnUpdateTrail")
                                    .FlexInclude("subTreatment.TreatmentOnCommunication")
                                    .FlexInclude("subTreatment.PerformanceBand")
                                    .Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

            var existingtreatment = obj.subTreatment;

            _model = await _repoFactory.GetRepo().FindAll<Treatment>().Where(m => m.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

            _model.SetCreatedDate(obj.CreatedDate);
            _model.SetCreatedBy(obj.CreatedBy);

            if (_model != null)
            {
                _model.UpdateTreatment(packet.Cmd);

                List<TreatmentOnPOS> newpos = new List<TreatmentOnPOS>();
                List<TreatmentOnAccount> newaccount = new List<TreatmentOnAccount>();
                List<RoundRobinTreatment> newroundrobin = new List<RoundRobinTreatment>();
                List<TreatmentByRule> newtreatmentrule = new List<TreatmentByRule>();
                List<TreatmentDesignation> newtreatmentdesignation = new List<TreatmentDesignation>();
                List<TreatmentOnPerformanceBand> newperformanceband = new List<TreatmentOnPerformanceBand>();
                List<SubTreatment> newsubtreatment = new List<SubTreatment>();

                foreach (var sub in _model.subTreatment)
                {
                    SubTreatment subtreatment = new SubTreatment();
                    var existingsub = existingtreatment.Where(a => a.Id == sub.Id).FirstOrDefault();
                    subtreatment = sub;

                    if (existingsub != null)
                    {
                        if (sub.POSCriteria.Count == 0)
                        {
                            foreach (var posvalue in existingsub.POSCriteria)
                            {
                                TreatmentOnPOS pos = new TreatmentOnPOS();
                                pos = posvalue;
                                pos.SetIsDeleted(true);
                                newpos.Add(pos);
                            }
                            subtreatment.POSCriteria = newpos;
                        }
                        if (sub.AccountCriteria.Count == 0)
                        {
                            foreach (var accvalue in existingsub.AccountCriteria)
                            {
                                TreatmentOnAccount acc = new TreatmentOnAccount();
                                acc = accvalue;
                                acc.SetIsDeleted(true);
                                newaccount.Add(acc);
                            }
                            subtreatment.AccountCriteria = newaccount;
                        }
                        if (sub.RoundRobinCriteria.Count == 0)
                        {
                            foreach (var roundvalue in existingsub.RoundRobinCriteria)
                            {
                                RoundRobinTreatment round = new RoundRobinTreatment();
                                round = roundvalue;
                                round.SetIsDeleted(true);
                                newroundrobin.Add(round);
                            }
                            subtreatment.RoundRobinCriteria = newroundrobin;
                        }
                        if (sub.PerformanceBand.Count == 0)
                        {
                            foreach (var roundvalue in existingsub.PerformanceBand)
                            {
                                TreatmentOnPerformanceBand round = new TreatmentOnPerformanceBand();
                                round = roundvalue;
                                round.SetIsDeleted(true);

                                newperformanceband.Add(round);
                            }
                            subtreatment.PerformanceBand = newperformanceband;
                        }
                        if (sub.TreatmentByRule.Count == 0)
                        {
                            foreach (var rulevalue in existingsub.TreatmentByRule)
                            {
                                TreatmentByRule treatmentrule = new TreatmentByRule();
                                treatmentrule = rulevalue;
                                treatmentrule.SetIsDeleted(true);
                                newtreatmentrule.Add(treatmentrule);
                            }
                            subtreatment.TreatmentByRule = newtreatmentrule;
                        }
                        if (sub.Designation.Count == 0)
                        {
                            _logger.LogError("ELSE 5");
                            if (existingsub.Designation != null)
                            {
                                foreach (var rulevalue in existingsub.Designation)
                                {
                                    _logger.LogError("ELSE 6");
                                    TreatmentDesignation treatmentdesignation = new TreatmentDesignation();
                                    treatmentdesignation = rulevalue;
                                    treatmentdesignation.SetIsDeleted(true);

                                    newtreatmentdesignation.Add(treatmentdesignation);
                                }
                            }
                            subtreatment.Designation = newtreatmentdesignation;
                        }
                    }
                    else
                    {
                        _logger.LogError("ELSE ");
                        if (sub.POSCriteria.Count > 0)
                        {
                            subtreatment.POSCriteria = sub.POSCriteria;
                        }
                        if (sub.AccountCriteria.Count > 0)
                        {
                            subtreatment.AccountCriteria = sub.AccountCriteria;
                        }
                        if (sub.RoundRobinCriteria.Count > 0)
                        {
                            subtreatment.RoundRobinCriteria = sub.RoundRobinCriteria;
                        }
                        if (sub.TreatmentByRule.Count > 0)
                        {
                            subtreatment.TreatmentByRule = sub.TreatmentByRule;
                        }
                        if (sub.PerformanceBand.Count > 0)
                        {
                            subtreatment.PerformanceBand = sub.PerformanceBand;
                        }
                        if (sub.TreatmentOnUpdateTrail != null)
                        {
                            subtreatment.TreatmentOnUpdateTrail = sub.TreatmentOnUpdateTrail;
                        }
                        if (sub.TreatmentOnCommunication != null)
                        {
                            subtreatment.TreatmentOnCommunication = sub.TreatmentOnCommunication;
                        }
                    }

                    _logger.LogError("subtreatment " + subtreatment?.Id);
                    newsubtreatment.Add(subtreatment);
                }
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Treatment).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Treatment).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Treatment).Name, packet.Cmd.Dto.Id);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}