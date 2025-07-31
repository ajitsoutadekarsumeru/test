using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Reflection.PortableExecutable;
using System;
using System.Text;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Handles posting collection data to the CBS system.
    /// </summary>
    public class CBSCollectionPoster : ICollectionPoster
    {
        private readonly ILogger<CBSCollectionPoster> _logger;
        private readonly IFlexHost _flexHost;
        private FlexAppContextBridge? _appContext;
        private readonly IRepoFactory _repositoryFactory;
        private readonly IApiHelper _apiHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CBSCollectionPoster"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        public CBSCollectionPoster(ILogger<CBSCollectionPoster> logger,
            IRepoFactory repositoryFactory,
            IFlexHost flexHost,
            IApiHelper apiHelper)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _flexHost = flexHost;
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Posts collection data to the CBS system.
        /// </summary>
        /// <param name="collection">The collection data to be posted.</param>
        /// <param name="paymentDetails">Configuration details for posting.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        public async Task PostCollectionAsync(CollectionDtoWithId collection)
        {
            _appContext = collection?.GetAppContext();

            string? tenantId = _appContext?.TenantId;
            _logger.LogInformation("Starting receipt posting | Tenant: {TenantId}, CollectionId: {CollectionId}", tenantId, collection?.Id);

            try
            {
                var repo = _repositoryFactory.Init(collection);

                var receiptConfigList = repo.GetRepo()
                                            .FindAll<FeatureMaster>()
                                            .Where(p => p.Parameter.StartsWith("ReceiptPosting"))
                                            .ToDictionary(x => x.Parameter, x => x.Value);

                if (!receiptConfigList.TryGetValue("ReceiptPostingUrl", out string? postingUrl) || string.IsNullOrEmpty(postingUrl))
                {
                    _logger.LogWarning("ReceiptPostingUrl is missing for Tenant: {TenantId}", tenantId);
                    return;
                }

                receiptConfigList.TryGetValue("ReceiptPostingLogFilePath", out string? logPath);
                receiptConfigList.TryGetValue("ReceiptPostingJsonFilePath", out string? jsonFilePath);

                string filePath = GetFilePath(logPath, tenantId);

                await ProcessJsonRequestAsync(collection, postingUrl, tenantId, filePath, jsonFilePath).ConfigureAwait(false);

                _logger.LogInformation("Successfully sent Collection {CollectionId} to CBS for Tenant {TenantId}.", collection.Id, tenantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting receipt | Tenant: {TenantId}, CollectionId: {CollectionId}", tenantId, collection.Id);
                throw;
            }
        }


        /// <summary>
        /// Processes and sends the JSON request to the CBS system.
        /// </summary>
        /// <param name="collection">The collection data to be sent.</param>
        /// <param name="postingUrl">The URL to which the data should be posted.</param>
        /// <param name="tenantId">Identifier for the tenant.</param>
        /// <param name="filePath">Path to the JSON template file.</param>
        private async Task ProcessJsonRequestAsync(CollectionDtoWithId collection, string postingUrl, string tenantId, string filePath, string? jsonFilePath)
        {
            try
            {
                _logger.LogInformation("Processing JSON request | Tenant: {TenantId}, CollectionId: {CollectionId}", tenantId, collection.Id);

                string jsonTemplate = await File.ReadAllTextAsync(jsonFilePath);
                string jsonRequest = ReplaceJsonPlaceholders(jsonTemplate, collection);

                await File.AppendAllTextAsync(filePath, $"[{DateTime.UtcNow}] JSON Request: {jsonRequest}{Environment.NewLine}");

                var response = await _apiHelper.SendRequestAsync(jsonRequest, postingUrl,HttpMethod.Post);
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
        /// <summary>
        /// Replaces placeholders in the JSON template with actual collection data.
        /// </summary>
        private string ReplaceJsonPlaceholders(string jsonTemplate, CollectionDtoWithId collection)
        {
            var replacements = new Dictionary<string, string>
              {
                  { "{CollectionId}", collection.Id.ToString() },
                  { "{CollectionDate}", collection.CollectionDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "" },
                  { "{TransactionNumber}", string.Equals(collection.CollectionMode, "ONLINE TRANSFER", StringComparison.OrdinalIgnoreCase)
                      ? collection.TransactionNumber ?? ""
                      : collection.Cheque?.InstrumentNo ?? "" },
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
            string fileName = $"{tenantId}_CollectionUpdateLog_{DateTime.UtcNow:yyyyMMdd}.txt";
            string directoryPath = string.IsNullOrWhiteSpace(logPath) ? Directory.GetCurrentDirectory() : Path.Combine(logPath, tenantId);

            Directory.CreateDirectory(directoryPath);
            return Path.Combine(directoryPath, fileName);
        }
    }
}
