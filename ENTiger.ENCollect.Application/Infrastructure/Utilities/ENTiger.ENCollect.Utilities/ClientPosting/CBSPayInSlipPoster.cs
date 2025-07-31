using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Handles posting of Pay-In Slip data to the CBS system.
    /// </summary>
    public class CBSPayInSlipPoster : IPayInSlipPoster
    {
        private readonly ILogger<CBSPayInSlipPoster> _logger;
        private readonly IFlexHost _flexHost;
        private FlexAppContextBridge? _appContext;
        private readonly IRepoFactory _repositoryFactory;
        private readonly IApiHelper _apiHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CBSPayInSlipPoster"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="repositoryFactory">The repository factory instance.</param>
        /// <param name="flexHost">The Flex host service.</param>
        public CBSPayInSlipPoster(ILogger<CBSPayInSlipPoster> logger, IRepoFactory repositoryFactory
                ,IFlexHost flexHost, IApiHelper apiHelper)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _flexHost = flexHost;
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Posts collection data to the CBS system asynchronously.
        /// </summary>
        /// <param name="payinSlip">The Pay-In Slip data.</param>
        /// <param name="paymentDetails">The payment details list.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        public async Task PostPayInSlipAsync(PayInSlipDtoWithId payinSlip)
        {
            _appContext = payinSlip?.GetAppContext();

            string? tenantId = _appContext?.TenantId;
            _logger.LogInformation("Starting PayInSlip posting | Tenant: {TenantId}, PayInSlipId: {PayInSlipId}", tenantId, payinSlip.Id);
            try
            {
                var repo = _repositoryFactory.Init(payinSlip);

                // Fetch payment configuration
                var payInSlipConfigList = repo.GetRepo() // Ensure GetRepo() returns a correct type
                    .FindAll<FeatureMaster>()  // Use GetAll() if FindAll<T>() is not available
                    .Where(p => p.Parameter.StartsWith("PayInSlipPosting"))
                    .SelectTo<FeatureMasterDtoWithId>()
                    .ToList();

                var configValues = payInSlipConfigList.ToDictionary(x => x.Parameter, x => x.Value);
                if (!configValues.TryGetValue("PayInSlipPostingUrl", out string? postingUrl) || string.IsNullOrEmpty(postingUrl))
                {
                    _logger.LogWarning("PayInSlipPostingUrl is missing for Tenant: {TenantId}", tenantId);
                    return;
                }

                configValues.TryGetValue("PayInSlipPostingLogFilePath", out string? logFilePath);
                configValues.TryGetValue("PayInSlipPostingJsonFilePath", out string? jsonFilePath);
                string filePath = GetFilePath(logFilePath, tenantId);

                var payInSlip = repo.GetRepo().FindAll<PayInSlip>().FlexInclude(a => a.CollectionBatches).FirstOrDefault(x => x.Id == payinSlip.Id);
                if (payInSlip != null)
                {
                    var tasks = payInSlip.CollectionBatches.SelectMany(batch =>
                        repo.GetRepo()
                            .FindAll<Collection>()
                            .ByCollectionBatchId(batch.Id)
                            .SelectTo<CollectionDtoWithId>()
                            .Select(collection => ProcessCollectionAsync(collection, postingUrl, tenantId, filePath, jsonFilePath, payInSlip.CustomId)));

                    await Task.WhenAll(tasks);
                }
                _logger.LogInformation("Successfully sent PayInSlip {PayInSlipId} to CBS for Tenant {TenantId}.", payinSlip.Id, tenantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting PayInSlip | Tenant: {TenantId}, PayInSlipId: {PayInSlipId}", tenantId, payinSlip.Id);
                throw;
            }
        }

        /// <summary>
        /// Processes a collection asynchronously.
        /// </summary>
        /// <param name="collection">The collection data.</param>
        /// <param name="postingUrl">The posting URL.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="filePath">The log file path.</param>
        /// <param name="jsonFilePath">The JSON file path.</param>
        /// <param name="payInSlipCustomId">The custom ID for the Pay-In Slip.</param>
        private async Task ProcessCollectionAsync(CollectionDtoWithId collection, string postingUrl, string tenantId, string filePath, string jsonFilePath, string payInSlipCustomId)
        {
            if (!string.Equals(collection?.CollectionMode, "CASH", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Skipping collection processing | CollectionId: {CollectionId} | Mode: {Mode}", collection?.Id, collection?.CollectionMode);
                return;
            }
            try
            {
                await ProcessJsonRequestAsync(collection, postingUrl, tenantId, filePath, jsonFilePath, payInSlipCustomId);
                _logger.LogInformation("Successfully processed Collection | CollectionId: {CollectionId}", collection.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing collection | CollectionId: {CollectionId}", collection.Id);
            }
        }

        /// <summary>
        /// Processes a JSON request asynchronously.
        /// </summary>
        private async Task ProcessJsonRequestAsync(CollectionDtoWithId collection, string postingUrl, string tenantId, string? filePath, string? jsonFilePath, string payInSlipCustomId)
        {
            try
            {
                _logger.LogInformation("Processing JSON request | Tenant: {TenantId}, CollectionId: {CollectionId}", tenantId, collection.Id);
                string jsonTemplate = await File.ReadAllTextAsync(jsonFilePath);
                string jsonRequest = ReplaceJsonPlaceholders(jsonTemplate, collection, payInSlipCustomId);
                await File.AppendAllTextAsync(filePath, $"[{DateTime.UtcNow}] JSON Request: {jsonRequest}{Environment.NewLine}");

                var response = await _apiHelper.SendRequestAsync(jsonRequest, postingUrl, HttpMethod.Post);
                string result = await response.Content.ReadAsStringAsync();
                
                await File.AppendAllTextAsync(filePath, $"[{DateTime.UtcNow}] Response received: {result}{Environment.NewLine}");
                _logger.LogInformation("Response received: {Response}", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing JSON request | Tenant: {TenantId}, CollectionId: {CollectionId}", tenantId, collection.Id);
                await File.AppendAllTextAsync(filePath, $"[{DateTime.UtcNow}] Response received Exception: {ex?.InnerException?.Message}{Environment.NewLine}{ex?.InnerException?.StackTrace?.ToString()}{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Replaces placeholders in the JSON template with actual collection data.
        /// </summary>
        /// <param name="jsonTemplate">The JSON template with placeholders.</param>
        /// <param name="collection">Collection data used to replace placeholders.</param>
        /// <returns>Formatted JSON string with replaced values.</returns>
        private string ReplaceJsonPlaceholders(string jsonTemplate, CollectionDtoWithId collection, string payInSlipCustomId)
        {
            var replacements = new Dictionary<string, string>
            {
                { "{CollectionId}", collection.Id.ToString() },
                { "{CollectionDate}", collection.CollectionDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "" },
                { "{TransactionNumber}", collection.TransactionNumber ?? "" },
                { "{CollectionMode}", collection.CollectionMode ?? "" },
                { "{CustomId}", collection.Account?.CustomId ?? "" },
                { "{ProductGroup}", collection.Account?.ProductGroup ?? "" },
                { "{IFSCCode}", collection.Cheque?.IFSCCode ?? "" },
                { "{MICRCode}", collection.Cheque?.MICRCode ?? "" },
                { "{Amount}", collection.Amount?.ToString() },
                { "{Branch}", collection.Account.BRANCH },
                { "{BranchName}", collection.Cheque?.BranchName ?? "" },
                { "{BankName}", collection.Cheque?.BankName ?? "" },
                { "{TxnId}", collection.CustomId ?? "" },
                { "{PayinSlipNo}", payInSlipCustomId ?? "" },
                { "{UserId}", $"{collection?.Collector?.FirstName} {collection?.Collector?.LastName}".Trim() }
            };
            return replacements.Aggregate(jsonTemplate, (current, replacement) => current.Replace(replacement.Key, replacement.Value));
        }
        /// <summary>
        /// Generates and retrieves the log file path for storing logs.
        /// </summary>
        /// <param name="logPath">The base directory path for logs.</param>
        /// <param name="tenantId">The identifier for the tenant.</param>
        /// <returns>The full file path where logs should be stored.</returns>
        private string GetFilePath(string logPath, string tenantId)
        {
            _logger.LogInformation("Generating log file path | LogPath: {LogPath}", logPath);
            string fileName = $"{tenantId}_PayInSlipUpdateLog_{DateTime.UtcNow:yyyyMMdd}.txt";
            string directoryPath = string.IsNullOrWhiteSpace(logPath) ? Directory.GetCurrentDirectory() : Path.Combine(logPath, tenantId);
            Directory.CreateDirectory(directoryPath);
            return Path.Combine(directoryPath, fileName);
        }
    }
}