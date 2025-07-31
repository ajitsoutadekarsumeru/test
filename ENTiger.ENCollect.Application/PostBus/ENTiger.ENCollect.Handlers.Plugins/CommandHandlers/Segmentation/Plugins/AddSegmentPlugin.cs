using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class AddSegmentPlugin : FlexiPluginBase, IFlexiPlugin<AddSegmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1457522dbdc5a661440b8182e051d4";
        public override string FriendlyName { get; set; } = "AddSegmentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddSegmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Segmentation? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddSegmentPlugin(ILogger<AddSegmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddSegmentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var inputModel = packet.Cmd.Dto;

            _model = _flexHost.GetDomainModel<Segmentation>().AddSegment(packet.Cmd);
            if (!string.Equals(inputModel.ProductGroup, ProductGroupEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                _model.ProductGroup = await FetchCategoryItemNameAsync(inputModel.ProductGroup);
            }

            if (!string.Equals(inputModel.Product, ProductCodeEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                _model.Product = await FetchCategoryItemNameAsync(inputModel.Product);
            }

            if (!string.Equals(inputModel.SubProduct, SubProductEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                _model.SubProduct =await FetchCategoryItemNameAsync(inputModel.SubProduct);
            }

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(Segmentation).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(Segmentation).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
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