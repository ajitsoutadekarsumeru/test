using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentGetSegmentsFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteTreatmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentGetSegmentsFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentGetSegmentsFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;
        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ExecuteTreatmentGetSegmentsFFPlugin(ILogger<ExecuteTreatmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(ExecuteTreatmentPostBusDataPacket packet)
        {
            _repoFactory.Init(packet.Cmd.Dto);
            List<string> segments = new List<string>();

            var outputModel = await _repoFactory.GetRepo().FindAll<Treatment>()
                                    .FlexInclude(a => a.subTreatment)
                                    .FlexInclude("subTreatment.POSCriteria")
                                    .FlexInclude("subTreatment.AccountCriteria")
                                    .FlexInclude("subTreatment.RoundRobinCriteria")
                                    .FlexInclude("subTreatment.TreatmentByRule")
                                    .FlexInclude("subTreatment.TreatmentOnUpdateTrail")
                                    .FlexInclude("subTreatment.TreatmentOnCommunication")
                                    .FlexInclude("subTreatment.PerformanceBand")
                                    .FlexInclude("subTreatment.Designation")
                                    .FlexInclude("subTreatment.DeliveryStatus")
                                    .FlexInclude(a => a.segmentMapping)
                                    .FlexInclude("segmentMapping.Segment")
                                    .Where(a => a.Id == packet.Cmd.Dto.TreatmentId).FirstOrDefaultAsync();

            packet.outputModel = outputModel;

            if (outputModel != null)
            {
                var segmentCollections = outputModel.segmentMapping;
                if (segmentCollections != null)
                {
                    if (segmentCollections.Count > 0)
                    {
                        segments = segmentCollections.Select(s => s.SegmentId).ToList();
                    }
                }
            }

            DateTime? treatExecutionStartdate = null;
            DateTime? treatExecutionEnddate = null;

            if (outputModel.ExecutionStartdate == null)
            {
                _logger.LogInformation("inside outputModel.ExecutionStartdate ");
                //outputModel.ExecutionStartdate = DateTime.Now;
                treatExecutionStartdate = DateTime.Now;
                _logger.LogInformation("treatExecutionStartdate " + treatExecutionStartdate);
                _logger.LogInformation("after outputModel.ExecutionStartdate " + outputModel.ExecutionStartdate);
            }
            if (outputModel.ExecutionEnddate == null)
            {
                var enddaylist = outputModel.subTreatment.Where(a => a.EndDay != null).Select(a => new { Endday = Convert.ToInt32(a.EndDay) }).Distinct().ToList();

                if (enddaylist.Count() > 0)
                {
                    //string enddatevalue = enddaylist.Take(1).OrderByDescending(a => a.Endday).Select(a=>a.Endday).FirstOrDefault();

                    int enddate = enddaylist.OrderByDescending(a => a.Endday).Select(a => a.Endday).FirstOrDefault();//!string.IsNullOrEmpty(enddatevalue) ? Convert.ToInt32(enddatevalue) : 0;

                    // outputModel.ExecutionEnddate = DateTime.Now.AddDays(enddate-1);
                    treatExecutionEnddate = DateTime.Now.AddDays(enddate - 1);
                    _logger.LogInformation("treatExecutionEnddate " + treatExecutionEnddate);
                }
                else
                {
                    treatExecutionEnddate = DateTime.Now;
                    //outputModel.ExecutionEnddate = DateTime.Now;
                }
            }
            if (outputModel.ExecutionEnddate != null && outputModel.ExecutionEnddate.Value.Date < DateTime.Now.Date)
            {
                //outputModel.ExecutionStartdate = DateTime.Now;
                treatExecutionStartdate = DateTime.Now;

                var enddaylist = outputModel.subTreatment.Where(a => a.EndDay != null).Select(a => new { Endday = Convert.ToInt32(a.EndDay) }).Distinct().ToList();

                if (enddaylist.Count() > 0)
                {
                    //string enddatevalue = enddaylist.Take(1).OrderByDescending(a => a.Endday).Select(a => a.Endday).FirstOrDefault();

                    int enddate = enddaylist.OrderByDescending(a => a.Endday).Select(a => a.Endday).FirstOrDefault();// !string.IsNullOrEmpty(enddatevalue) ? Convert.ToInt32(enddatevalue) : 0;

                    //outputModel.ExecutionEnddate = DateTime.Now.AddDays(enddate-1);
                    treatExecutionEnddate = DateTime.Now.AddDays(enddate - 1);
                }
                else
                {
                    treatExecutionEnddate = DateTime.Now;
                    //outputModel.ExecutionEnddate = DateTime.Now;
                }
            }

            var updatetreatment = await _repoFactory.GetRepo().FindAll<Treatment>().Where(a => a.Id == packet.Cmd.Dto.TreatmentId).FirstOrDefaultAsync();

            if (updatetreatment != null)
            {
                updatetreatment.ExecutionStartdate = treatExecutionStartdate == null ? outputModel.ExecutionStartdate : treatExecutionStartdate;
                updatetreatment.ExecutionEnddate = treatExecutionEnddate == null ? outputModel.ExecutionEnddate : treatExecutionEnddate;

                packet.treatExecutionStartdate = treatExecutionStartdate == null ? outputModel.ExecutionStartdate : treatExecutionStartdate;
                packet.treatExecutionEnddate = treatExecutionEnddate == null ? outputModel.ExecutionEnddate : treatExecutionEnddate;

                updatetreatment.SetModified();
            }
            _repoFactory.GetRepo().InsertOrUpdate(updatetreatment);
            var x = await _repoFactory.GetRepo().SaveAsync();
            //Thread.Sleep(2000);
            _logger.LogInformation("Start and enddate updated to " + updatetreatment.ExecutionStartdate + " " + updatetreatment.ExecutionEnddate);

            packet.outputModel = outputModel;
            packet.segments = segments;
            _logger.LogInformation("Completed in TreatmentGetSegmentsFFPlugin ");

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}