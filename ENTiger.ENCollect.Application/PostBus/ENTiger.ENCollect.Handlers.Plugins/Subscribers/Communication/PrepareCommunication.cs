using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class PrepareCommunication : IPrepareCommunication
    {
        protected readonly ILogger<PrepareCommunication> _logger;
        private readonly ICommunicationTriggerRepository _triggerRepo;
        private readonly ITemplateParser _parser;
        private readonly ITemplateProcessor _processor;
        private readonly ILoanAccountProjectionService _projectionService;
        private readonly IRecipientDataService _recipientService;

        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected string? templateBody;
        protected string? templateSubject;
        protected string? templateLanguage;
        protected CommunicationTemplateDetail? templateData;

        public PrepareCommunication(
            ILogger<PrepareCommunication> logger, 
            IRepoFactory repoFactory,
            ICommunicationTriggerRepository triggerRepo,
            ITemplateParser parser,
            ITemplateProcessor processor,
            ILoanAccountProjectionService projectionService,
            IRecipientDataService recipientService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _triggerRepo = triggerRepo;
            _parser = parser;
            _processor = processor;
            _projectionService = projectionService;
            _recipientService = recipientService;
        }

        public virtual async Task Execute(AccountsIdentifiedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            _logger.LogInformation("Preparing communication for trigger: {TriggerId}", @event.TriggerId);

            // 1. Load delivery specs for this trigger from ICommunicationRepository
            var deliverySpecs = await _triggerRepo.GetDeliverySpecsAsync(@event.TriggerId, _flexAppContext);

            if (deliverySpecs == null || deliverySpecs.Count == 0)
            {
                _logger.LogWarning("No delivery specs found for trigger: {TriggerId}", @event.TriggerId);
                return;
            }

            // 2. Prepare communication for each delivery spec
            foreach (var deliverySpec in deliverySpecs)
            {
                _logger.LogInformation("Preparing communication for delivery spec: {DeliverySpecId}", deliverySpec.Id);

                // a. Parse template to determine required fields
                GetTemplateData(deliverySpec);

                if (string.IsNullOrEmpty(templateBody))
                {
                    _logger.LogWarning("Template body is empty for delivery spec: {DeliverySpecId}", deliverySpec.Id);
                    continue;
                }
                var requiredFields = _parser.ExtractPlaceholders(templateBody);

                //b. Determine Recipient Field dynamically
                var recipientField = await _recipientService.DetermineRecipientField(deliverySpec.CommunicationTrigger.RecipientType);
                if (string.IsNullOrEmpty(recipientField))
                {
                    _logger.LogWarning("No recipient data found for delivery spec: {DeliverySpecId}", deliverySpec.Id);
                    continue;
                }

                // Add recipientField to projection fields
                if (!requiredFields.Contains(recipientField))
                    requiredFields.Add(recipientField);

                // c. Build projections from accounts data
                var accountProjections = await _projectionService.GetAccountProjectionsAsync(
                    @event.TriggerId,
                    @event.RunId,
                    requiredFields, _flexAppContext);

                // d. Loop through projections to compose messages
                foreach (var projection in accountProjections)
                {
                    var actorId = projection.GetValueOrDefault("Id")?.ToString() ?? string.Empty;
                    //Compose message
                    var templateMessage = _processor.FillTemplate(templateBody, projection);

                    // Prepare recipient data (email or phone)                  
                    projection.TryGetValue(recipientField, out var recipientData);

                    if (recipientData == null || string.IsNullOrEmpty(recipientData.ToString()))
                    {
                        _logger.LogWarning("No recipient data found for projection");
                        continue;
                    }

                    // e. Create communication entity via constructor
                    var communication = new Communication(
                        actorId: actorId,
                        channel: deliverySpec.CommunicationTemplate.TemplateType,
                        messageBody: templateMessage,
                        messageSubject: templateSubject,
                        recipient: recipientData.ToString() ?? string.Empty,
                        recipientType: deliverySpec.CommunicationTrigger.RecipientType,
                        language: templateLanguage,
                        status: CommunicationStatusEnum.AwaitingDispatch.Value
                        );

                    // Save communication entity
                    _repoFactory.GetRepo().InsertOrUpdate(communication);
                }
            }
            // 3. Save all communications in the repository
            await _repoFactory.GetRepo().SaveAsync();
        }

        private void GetTemplateData(TriggerDeliverySpec deliverySpec)
        {
            templateData = deliverySpec.CommunicationTemplate.CommunicationTemplateDetails.FirstOrDefault(); //TODO :: Need to check on templates per language.
            templateBody = templateData?.Body ?? string.Empty;
            templateSubject = templateData?.Subject ?? string.Empty;
            templateLanguage = templateData?.Language ?? "en"; // Default to English if not specified
        }
    }
}
