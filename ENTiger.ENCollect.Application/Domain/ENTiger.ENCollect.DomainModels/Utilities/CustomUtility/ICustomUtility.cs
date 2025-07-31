using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    public interface ICustomUtility
    {
        string JsonToQuery(string jsonQuery);
        Task<string> GetNextCustomIdAsync(FlexAppContextBridge context, string name);
        string GenerateRandomCode();
        TValue GetValueByKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey searchKey);

        string GetValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey searchKey);

        List<dynamic> FormatAccountsData<T>(List<T> accounts, ILogger logger) where T : class;

        string FullMaskValueFunc(string MaskValue);
        string GenerateShortSecureToken(int length = 16, bool lowerCase = true);
        string ReturnExpiryColorBasedOnExpiryDays(int expiryDays);
    }
}