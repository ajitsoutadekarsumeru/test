using System.Text.Json.Serialization;

namespace ENTiger.ENCollect
{
   
    public class LicenseSettings
    {
        public string UserLimitThresholds { get; set; }         // Comma-separated thresholds

        [JsonIgnore]  // optional if using Newtonsoft.Json and don't want to serialize it
        public List<decimal> UserLimitThresholdList =>     //convert comma seprated into list
        string.IsNullOrWhiteSpace(UserLimitThresholds)
          ? new List<decimal>()
          : UserLimitThresholds
              .Split(',')
              .Select(s => decimal.TryParse(s.Trim(), out var val) ? val : (decimal?)null)
              .Where(val => val.HasValue)
              .Select(val => val.Value)
              .ToList();


    }
}
