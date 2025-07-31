namespace ENTiger.ENCollect;

public record AccountContactHistoryEventData(
    string ContactSource,
    string EmailId,
    string MobileNo,
    string Address,
    string AccountId,
    decimal? Latitude,
    decimal? Longitude
);