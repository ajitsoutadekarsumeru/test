using ENTiger.ENCollect.DomainModels.Utilities;
using ENTiger.ENCollect.Utilities.BulkTrailUpload;
using ENTiger.ENCollect.Utilities.Masters;
using ENTiger.ENCollect.Utilities.RegisterDevice;
using ENTiger.ENCollect.Utilities.UserManagement;
using ENTiger.ENCollect.Utilities.UserManagement.Agent;
using ENTiger.ENCollect.Utilities.UserManagement.Staff;
using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public static class UtilityExtension
    {
        public static IServiceCollection AddExtensions(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUserUtility, ApplicationUserUtility>();
            services.AddSingleton<MessageTemplateFactory>();
            services.AddSingleton<EmailProviderFactory>();
            services.AddTransient<SmsProviderFactory>();
            services.AddSingleton<PaymentGatewayFactory>();
            services.AddSingleton<PaymentGatewayStatusUpdateFactory>();
            services.AddSingleton<PackageSSISProviderFactory>();

            services.AddTransient<PaymentGatewayPaynimo>();
            services.AddTransient<PaymentGatewayRazorPay>();
            services.AddTransient<RazorPayStatusProvider>();
            services.AddTransient<PaymentGatewayPayu>();
            services.AddTransient<SmsProvider24X7>();
            services.AddTransient<SmsProviderInfoBip>();
            services.AddTransient<SmsProviderKarix>();
            services.AddTransient<EmailProviderSmtp>();
            services.AddTransient<EmailProviderNetCoreCloud>();
            services.AddTransient<ISmsUtility, SmsUtility>();
            services.AddTransient<IEmailUtility, EmailUtility>();
            services.AddTransient<ICsvExcelUtility, CsvExcelUtility>();

            
            services.AddSingleton<ADAuthProviderFactory>();
            services.AddTransient<LDAPAuthProvider>();

            services.AddTransient<FirebasePushProvider>();
            services.AddTransient<IPushNotificationProvider, FirebasePushProvider>();
            services.AddSingleton<PushNotificationProviderFactory>();

            //--------------------------------------------------------
            services.AddTransient<LoginOTPNotification>();
            services.AddTransient<ForgotPasswordNotification>();
            services.AddTransient<MobileForgotPasswordNotification>();
            services.AddTransient<CompanyUserCreatedEmailNotification>();
            services.AddTransient<CompanyUserCreatedSMSNotification>();
            services.AddTransient<CompanyUserCreatedEmailNotification>();
            services.AddTransient<CompanyUserRejectedSMSNotification>();
            services.AddTransient<CompanyUserApprovedEmailNotification>();
            services.AddTransient<CompanyUserApprovedSMSNotification>();
            services.AddTransient<AgencyApprovedEmailNotification>();
            services.AddTransient<AgencyCreatedEmailNotification>();
            services.AddTransient<AgencyUserCreatedEmailNotification>();
            services.AddTransient<AgencyUserCreatedSMSNotification>();
            services.AddTransient<AgencyUserRejectedSMSNotification>();
            services.AddTransient<AgencyUserApprovedEmailNotification>();
            services.AddTransient<AgencyUserApprovedEmailNotification>();
            services.AddTransient<UploadUsersFailedNotification>();
            services.AddTransient<UploadUsersSuccessNotification>();
            services.AddTransient<UploadUsersUploadedNotification>();
            services.AddTransient<PrimaryAllocationAgencyFailedNotification>();
            services.AddTransient<PrimaryAllocationOwnerFailedNotification>();
            services.AddTransient<PrimaryAllocationTCAgencyFailedNotification>();
            services.AddTransient<PrimaryAllocationAgencyProcessedNotification>();
            services.AddTransient<PrimaryAllocationOwnerProcessedNotification>();
            services.AddTransient<PrimaryAllocationTCAgencyProcessedNotification>();
            services.AddTransient<PrimaryAllocationAgencyUploadedNotification>();
            services.AddTransient<PrimaryAllocationOwnerUploadedNotification>();
            services.AddTransient<PrimaryAllocationTCAgencyUploadedNotification>();
            services.AddTransient<SecondaryAllocationAgentFailedNotification>();
            services.AddTransient<SecondaryAllocationStaffFailedNotification>();
            services.AddTransient<SecondaryAllocationTeleCallerFailedNotification>();
            services.AddTransient<SecondaryAllocationAgentProcessedNotification>();
            services.AddTransient<SecondaryAllocationStaffProcessedNotification>();
            services.AddTransient<SecondaryAllocationTeleCallerProcessedNotification>();
            services.AddTransient<SecondaryAllocationAgentUploadedNotification>();
            services.AddTransient<SecondaryAllocationStaffUploadedNotification>();
            services.AddTransient<SecondaryAllocationTeleCallerUploadedNotification>();
            services.AddTransient<IssueReceiptNotificationDD>();
            services.AddTransient<IssueReceiptNotificationCash>();
            services.AddTransient<IssueReceiptNotificationCheque>();
            services.AddTransient<IssueReceiptNotificationOnlineTransfer>();
            services.AddTransient<IssueReceiptNotificationNEFT>();
            services.AddTransient<IssueReceiptNotificationRTGS>();
            services.AddTransient<IssueReceiptNotificationSMS>();
            services.AddTransient<OnlinePaymentLink>();
            services.AddTransient<PaymentOtpNotification>();
            services.AddTransient<DuplicateReceiptNotification>();
            services.AddTransient<CollectionCancelledNotification>();
            services.AddTransient<CollectionCancellationRequestedNotification>();
            services.AddTransient<CollectionCancellationRejectedNotification>();
            services.AddTransient<VerifyAccountNotification>();
            services.AddTransient<AccountImportErrorNotification>();
            services.AddTransient<AccountImportFailedNotification>();
            services.AddTransient<AccountImportFileErrorNotification>();
            services.AddTransient<AccountImportFileFailedNotification>();
            services.AddTransient<AccountImportFilePartialNotification>();
            services.AddTransient<AccountImportFileReceivedNotification>();
            services.AddTransient<AccountImportFileSuccessNotification>();
            services.AddTransient<AccountImportPartialNotification>();
            services.AddTransient<AccountImportReceivedNotification>();
            services.AddTransient<AccountImportSuccessNotification>();
            services.AddTransient<ImportAccountsViaAPIFailedNotification>();
            services.AddTransient<ImportAccountsViaAPIPartialNotification>();
            services.AddTransient<ImportAccountsViaAPIReceivedNotification>();
            services.AddTransient<ImportAccountsViaAPISuccessNotification>();
            services.AddTransient<PrimaryUnAllocationFailedNotification>();
            services.AddTransient<PrimaryUnAllocationSuccessNotification>();
            services.AddTransient<PrimaryUnAllocationUploadedNotification>();
            services.AddTransient<SecondaryUnAllocationFailedNotification>();
            services.AddTransient<SecondaryUnAllocationSuccessNotification>();
            services.AddTransient<SecondaryUnAllocationUploadedNotification>();
            services.AddTransient<VideoNotification>();
            services.AddTransient<PaynimoOnlinePaymentNotification>();
            services.AddTransient<RazorPayOnlinePaymentNotification>();
            services.AddTransient<BulkTrailUploadedEmailNotification>();
            services.AddTransient<BulkTrailUploadSuccessEmailNotification>();
            services.AddTransient<BulkTrailUploadFailedEmailNotification>();
            services.AddTransient<MasterImportHeaderMismatchEmailNotification>();
            services.AddTransient<MasterImportFailedEmailNotification>();
            services.AddTransient<MasterImportProcessedEmailNotification>();
            services.AddTransient<MasterImportPartiallyProcessedEmailNotification>();
            services.AddTransient<MasterImportErrorEmailNotification>();
            services.AddTransient<MasterImportNoRecordsEmailNotification>();
            services.AddTransient<RegisterDeviceSendOTPEmailNotification>();
            services.AddTransient<RegisterDeviceSendOTPSMSNotification>();
            services.AddTransient<CustomerConsentNotification>();
            services.AddTransient<CompanyUserDormantEmailNotification>();
            services.AddTransient<AgencyUserDormantEmailNotification>();
            services.AddTransient<CompanyUserDormantManagerEmailNotification>();
            services.AddTransient<AgencyUserDormantManagerEmailNotification>();
            //--------------------------------------------------------

            services.AddTransient<MySettlementsService>();
            services.AddTransient<MySettlementsQueueService>();

            return services;
        }
    }
}