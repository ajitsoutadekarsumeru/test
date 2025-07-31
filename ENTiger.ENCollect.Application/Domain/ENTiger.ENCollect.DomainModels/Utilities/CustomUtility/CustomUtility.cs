using ENTiger.ENCollect.DomainModels.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Data;
using System.Dynamic;
using System.Security.Cryptography;

namespace ENTiger.ENCollect
{
    public class CustomUtility : ICustomUtility
    {
        protected ILogger<CustomUtility> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly AccountExpiryColorSettings _accSettings;

        public CustomUtility(ILogger<CustomUtility> logger, IRepoFactory repoFactory, IOptions<AccountExpiryColorSettings> accSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _accSettings = accSettings.Value;
        }

        public string JsonToQuery(string jsonQuery)
        {
            if (string.IsNullOrWhiteSpace(jsonQuery))
                return string.Empty;

            return jsonQuery.Replace(":", "=")
                            .Replace("{", "")
                            .Replace("}", "")
                            .Replace(",", "&")
                            .Replace("\"", "");
        }

        public string GenerateRandomCode()
        {
            const string digits = "0123456789";
            return new string(Enumerable.Repeat(digits, 6)
                    .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)])
                    .ToArray());
        }
        public async Task<string> GetNextCustomIdAsync(FlexAppContextBridge context, string name)
        {
            _repoFactory.Init(context);
            int value = 0;
            string prefix = string.Empty;
            var model = await _repoFactory.GetRepo().FindAll<IdConfigMaster>().Where(s => s.CodeType.Equals(name)).FirstOrDefaultAsync();
            if (model != null)
            {
                //Update next value
                value = model.LatestValue == 0 ? (model.BaseValue + 1) : (model.LatestValue + 1);
                model.UpdateSequence(name, value);

                _repoFactory.GetRepo().InsertOrUpdate(model);
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityName} inserted into Database: ", typeof(Sequence).Name, model.CodeType);
                }
                else
                {
                    _logger.LogWarning("No records inserted for {Entity} with {EntityName}", typeof(Sequence).Name, model.CodeType);
                }
                prefix = model.CreatedBy;
            }
            string customId = string.IsNullOrEmpty(prefix) ? value.ToString() : (prefix + value.ToString());
            return customId;
        }

        public TValue GetValueByKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey searchKey)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            var key = dictionary.Keys.FirstOrDefault(k => k.ToString().Equals(searchKey.ToString(), StringComparison.OrdinalIgnoreCase));
            return key != null ? dictionary[key] : default;
        }

        public string GetValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey searchKey)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.FirstOrDefault(pair => pair.Key.ToString().Equals(searchKey.ToString(), StringComparison.OrdinalIgnoreCase)).Value?.ToString() ?? string.Empty;
        }
        public List<dynamic> FormatAccountsData<T>(List<T> accounts, ILogger logger) where T : class
        {
            logger.LogInformation("CustomHelper: FormatAccountsData - Start");

            if (accounts == null || !accounts.Any())
                return new List<dynamic>();

            var formattedAccounts = accounts
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount) // Optional tuning
                .Select(account =>
                {
                    // Use ExpandoObject for dynamic output
                    IDictionary<string, object> expando = new ExpandoObject();
                    var properties = typeof(T).GetProperties();

                    foreach (var prop in properties)
                    {
                        var value = prop.GetValue(account);
                        if (value is Dictionary<string, string> dict)
                        {
                            foreach (var item in dict)
                            {
                                expando[item.Key] = item.Value;
                            }
                        }
                        else
                        {
                            expando[prop.Name] = value?.ToString();
                        }
                    }

                    return (dynamic)expando;
                })
                .ToList();

            logger.LogInformation("CustomHelper: FormatAccountsData - End");

            return formattedAccounts;
        }

      

        public string FullMaskValueFunc(string MaskValue)
        {
            if (!string.IsNullOrEmpty(MaskValue))
            {
                var firstDigits = MaskValue.Substring(0, 0);
                var lastDigits = string.Empty;
                if (MaskValue.Length > 0)
                {
                    lastDigits = MaskValue.Substring(MaskValue.Length - 0, 0);
                }
                var requiredMask = new String('X', MaskValue.Length - firstDigits.Length - lastDigits.Length);
                var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                return maskedString;
            }
            else
            {
                return string.Empty;
            }
        }

        public string GenerateShortSecureToken(int length = 16, bool lowerCase = true)
        {
            var token = RandomNumberGenerator.GetHexString(length, lowerCase)
            .TrimEnd('=') // Remove padding
            .Replace("+", "") // Remove special characters
            .Replace("/", "");
            return token;
        }

        public string ReturnExpiryColorBasedOnExpiryDays(int expiryDays)
        {
            //check in sequence if expiry days >= green days else then >= amber days else it will be red - not satisfied any previous condition
            switch (expiryDays)
            {
                case int n when (n >= _accSettings.AllocationExpiryGreenDays):
                    return RAGColorEnum.Green.Value;

                case int n when (n >= _accSettings.AllocationExpiryAmberDays):
                    return RAGColorEnum.Amber.Value;

                default:
                    return RAGColorEnum.Red.Value;
            }
        }
    }
}