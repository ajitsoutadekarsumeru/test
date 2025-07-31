using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class UpdateSegmentPlugin : FlexiPluginBase, IFlexiPlugin<UpdateSegmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1457536817d9826e1adf0f599f382f";
        public override string FriendlyName { get; set; } = "UpdateSegmentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateSegmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Segmentation? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateSegmentPlugin(ILogger<UpdateSegmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateSegmentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<Segmentation>().FlexInclude(a => a.SegmentAdvanceFilter).Where(m => m.Id == packet.Cmd.Dto.Id).FirstOrDefaultAsync();

            var inputModel = packet.Cmd.Dto;

            // Updating ProductGroup if it is not equal to the default enum value
            UpdateCategoryField(inputModel.ProductGroup, ProductGroupEnum.All.Value, value => packet.Cmd.Dto.ProductGroup = value);

            // Updating Product if it is not equal to the default enum value
            UpdateCategoryField(inputModel.Product, ProductCodeEnum.All.Value, value => packet.Cmd.Dto.Product = value);

            // Updating SubProduct if it is not equal to the default enum value
            UpdateCategoryField(inputModel.SubProduct, SubProductEnum.All.Value, value => packet.Cmd.Dto.SubProduct = value);

            if (_model != null)
            {
                _model.UpdateSegment(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Segmentation).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Segmentation).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Segmentation).Name, packet.Cmd.Dto.Id);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
        private async void UpdateCategoryField(string fieldValue, string enumValue, Action<string> updateAction)
        {
            if (!string.IsNullOrEmpty(fieldValue) && !fieldValue.Equals(enumValue, StringComparison.OrdinalIgnoreCase))
            {
                updateAction(await FetchCategoryItemNameAsync(fieldValue));
            }
        }
        private async Task<string> FetchCategoryItemNameAsync(string Id)
        {
            string name = string.Empty;

            var item = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (item != null)
            {
                name = item.Name;
            }

            return name;
        }
    }
}