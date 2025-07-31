//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Sumeru.Flex;

//namespace ENTiger.ENCollect
//{
//    public class CustomIdHelper
//    {
//        protected readonly ILogger<CustomIdHelper> _logger;
//        protected readonly IRepoFactory _repoFactory;

//        public CustomIdHelper()
//        {
//            _repoFactory = FlexContainer.ServiceProvider.GetRequiredService<IRepoFactory>();
//            _logger = FlexContainer.ServiceProvider.GetService<ILogger<CustomIdHelper>>();
//        }

//        public CustomIdHelper(IRepoFactory repoFactory, ILogger<CustomIdHelper> logger)
//        {
//            _repoFactory = repoFactory;
//            _logger = logger;
//        }

//        public async Task<string> GetNextCustomIdAsync(FlexAppContextBridge context, string name)
//        {
//            _repoFactory.Init(context);
//            int value = 0;
//            string prefix = string.Empty;
//            var model = _repoFactory.GetRepo().FindAll<IdConfigMaster>().Where(s => s.CodeType.Equals(name)).FirstOrDefault();
//            if (model != null)
//            {
//                //Update next value
//                value = model.LatestValue == 0 ? (model.BaseValue + 1) : (model.LatestValue + 1);
//                model.UpdateSequence(name, value);

//                _repoFactory.GetRepo().InsertOrUpdate(model);
//                int records = await _repoFactory.GetRepo().SaveAsync();
//                if (records > 0)
//                {
//                    _logger.LogDebug("{Entity} with {EntityName} inserted into Database: ", typeof(Sequence).Name, model.CodeType);
//                }
//                else
//                {
//                    _logger.LogWarning("No records inserted for {Entity} with {EntityName}", typeof(Sequence).Name, model.CodeType);
//                }
//                prefix = model.CreatedBy;
//            }
//            string customId = string.IsNullOrEmpty(prefix) ? value.ToString() : (prefix + value.ToString());
//            return customId;
//        }
//    }
//}