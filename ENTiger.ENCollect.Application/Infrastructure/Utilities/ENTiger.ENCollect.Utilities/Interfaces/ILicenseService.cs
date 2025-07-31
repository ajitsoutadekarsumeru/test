using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.DomainModels.Enum;

namespace ENTiger.ENCollect;

public interface ILicenseService
{
    LicenseInfo License { get; }
    bool IsLicenseValid();
    Task<int> GetFieldLicenseConsumptionAsync(dynamic dto);
    Task<int> GetTeleLicenseConsumptionAsync(dynamic dto);
    Task<int> GetFreeLicenseConsumptionAsync(dynamic dto);
    Task<int> GetFreeCollectionsConsumptionAsync(string userId, dynamic dto);
    Task<int> GetFreeTrailsConsumptionAsync(string userId, dynamic dto);
    int GetFieldLicenseLimit();
    int GetTeleLicenseLimit();
    int GetFreeLicenseLimit();
    int GetFreeCollectionsLicenseLimit();
    int GetFreeTrailsLicenseLimit();
    Task LogLicenseViolationAsync(LicenseFeatureEnum feature, int limit, int consumption, FlexAppContextBridge hostContextInfo);
    Task<LicenseValidationResult> ValidateUserLicenseLimitAsync(UserTypeEnum userType, dynamic Dto);
    Task<LicenseValidationTransactionsResult> ValidateTransactionLicenseLimitAsync(LicenseTransactionType transactionType, string userId, dynamic Dto);
    Task<GetUserTransactionLimitsDto> GetTransactionTypeDetailAsync(LicenseTransactionType transactionType, string userId, dynamic dto);
    Task<GetUserTypeDetailsDto> GetUserTypeDetailAsync(UserTypeEnum userType, dynamic dto);
    Task<decimal> GetUserTypeLimitPercentageDetailAsync(UserTypeEnum userType, dynamic dto);
    Task<string> GetUtilizationColor(decimal utilizedPercentage);
}
