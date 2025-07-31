using ENTiger.ENCollect.AccountsModule;
using ENTiger.ENCollect.GeoTagModule;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.Utilities.BulkTrailUpload;
using ENTiger.ENCollect.Utilities.License;
using ENTiger.ENCollect.Utilities.Masters;
using ENTiger.ENCollect.Utilities.RegisterDevice;
using ENTiger.ENCollect.Utilities.UserManagement;
using ENTiger.ENCollect.Utilities.UserManagement.Agency;
using ENTiger.ENCollect.Utilities.UserManagement.Agent;
using ENTiger.ENCollect.Utilities.UserManagement.Staff;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Data;

namespace ENTiger.ENCollect
{
    public class MessageTemplateFactory
    {
        protected IFlexHost _flexHost;
        private ILogger<MessageTemplateFactory> _logger;

        public MessageTemplateFactory(IFlexHost flexHost, ILogger<MessageTemplateFactory> logger)
        {
            _flexHost = flexHost;
            _logger = logger;
        }

        #region Login

        private string GetHostname(DtoBridge dto)
        {
            string hostName = dto.GetAppContext()?.HostName ?? string.Empty;
            _logger.LogInformation("MessageTemplateFactory : HostName - " + hostName);
            return hostName;
        }

        public IMessageTemplate LoginOTPTemplate(SendLoginOTPDto dto)
        {
            var utility = _flexHost.GetUtilityService<LoginOTPNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        #endregion Login

        #region ForgotPassword

        public IMessageTemplate ForgotPasswordTemplate(SendForgotPasswordDto dto)
        {
            var utility = _flexHost.GetUtilityService<ForgotPasswordNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate MobileForgotPasswordTemplate(SendMobileForgotPasswordDto dto)
        {
            var utility = _flexHost.GetUtilityService<MobileForgotPasswordNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        #endregion ForgotPassword

        #region License
        #region LicenseUserLimits
        public IMessageTemplate LicenseUserLimitEmailTemplate(SendLicenseUserLimitMassageDto dto)
        {
            var utility = _flexHost.GetUtilityService<LicenseUserLimitEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }
        #endregion
        #region LicenseTransactionLimits
        public IMessageTemplate LicenseTransactionLimitEmailTemplate(SendLicenseTransactionLimitMassageDto dto)
        {
            var utility = _flexHost.GetUtilityService<LicenseTransactionLimitEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }
        #endregion
        #endregion

        #region UM
        #region CompanyUser

        public IMessageTemplate CompanyUserCreatedEmailTemplate(CompanyUserDtoWithId dto)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserCreatedEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate CompanyUserCreatedSMSTemplate(CompanyUserDtoWithId dto)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserCreatedSMSNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate CompanyUserRejectedEmailTemplate(CompanyUserDtoWithId dto)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserCreatedEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate CompanyUserRejectedSMSTemplate(CompanyUserDtoWithId dto)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserRejectedSMSNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate CompanyUserApprovedEmailTemplate(CompanyUserDtoWithId dto, string callbackurl)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserApprovedEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto, callbackurl);
            return utility;
        }

        public IMessageTemplate CompanyUserApprovedSMSTemplate(CompanyUserDtoWithId dto, string tinyurl)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserApprovedSMSNotification>(GetHostname(dto));
            utility.ConstructData(dto, tinyurl);
            return utility;
        }

        public IMessageTemplate MakeDormantCompanyUserEmailTemplate(CompanyUserDto dto, int InactiveDays)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserDormantEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto, InactiveDays);
            return utility;
        }

        public IMessageTemplate MakeDormantCompanyUserManagerEmailTemplate(CompanyUserDto dto, int InactiveDays, string ManagerFirstName)
        {
            var utility = _flexHost.GetUtilityService<CompanyUserDormantManagerEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto, InactiveDays, ManagerFirstName);
            return utility;
        }

        #endregion CompanyUser

        #region Agency

        public IMessageTemplate AgencyApprovedEmailTemplate(AgencyDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyApprovedEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        public IMessageTemplate AgencyCreatedEmailTemplate(AgencyDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyCreatedEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        #endregion Agency

        #region AgencyUser_Agent

        public IMessageTemplate AgencyUserCreatedEmailTemplate(AgencyUserDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyUserCreatedEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        public IMessageTemplate AgencyUserCreatedSMSTemplate(AgencyUserDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyUserCreatedSMSNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        public IMessageTemplate AgencyUserRejectedSMSTemplate(AgencyUserDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyUserRejectedSMSNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        public IMessageTemplate AgencyUserApprovedEmailTemplate(AgencyUserDtoWithId dto, string callbackurl)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyUserApprovedEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto, callbackurl);
            return agencyusercreated;
        }

        public IMessageTemplate AgencyUserApprovedSMSTemplate(AgencyUserDtoWithId dto, string tinyUrl)
        {
            var agencyusercreated = _flexHost.GetUtilityService<AgencyUserApprovedSMSNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto, tinyUrl);
            return agencyusercreated;
        }

        public IMessageTemplate MakeDormantAgencyUserEmailTemplate(AgencyUserDto dto, int InactiveDays)
        {
            var utility = _flexHost.GetUtilityService<AgencyUserDormantEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto, InactiveDays);
            return utility;
        }

        public IMessageTemplate MakeDormantAgencyUserManagerEmailTemplate(AgencyUserDto dto, int InactiveDays, string ManagerFirstName)
        {
            var utility = _flexHost.GetUtilityService<AgencyUserDormantManagerEmailNotification>(GetHostname(dto));
            utility.ConstructData(dto, InactiveDays, ManagerFirstName);
            return utility;
        }
        #endregion AgencyUser_Agent

        #region UploadUsers

        public IMessageTemplate UploadUsersFailedTemplate(string TransactionId, string hostName)
        {
            var _hostName = hostName ?? string.Empty;
            var utility = _flexHost.GetUtilityService<UploadUsersFailedNotification>();
            utility.ConstructData(TransactionId);
            return utility;
        }

        public IMessageTemplate UploadUsersSuccessTemplate(string TransactionId, string FileName, string hostName)
        {
            var _hostName = hostName ?? string.Empty;
            var utility = _flexHost.GetUtilityService<UploadUsersSuccessNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        public IMessageTemplate UploadUsersUploadedTemplate(string TransactionId, string FileName, string UploadType, string hostName)
        {
            var _hostName = hostName ?? string.Empty;
            var utility = _flexHost.GetUtilityService<UploadUsersUploadedNotification>();
            utility.ConstructData(TransactionId, FileName, UploadType);
            return utility;
        }

        #endregion UploadUsers

        #endregion UM

        #region PrimaryAllocation

        //Failed
        public IMessageTemplate PrimaryAllocationAgencyFailedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationAgencyFailedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate PrimaryAllocationOwnerFailedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationOwnerFailedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate PrimaryAllocationTCAgencyFailedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationTCAgencyFailedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        //Success
        public IMessageTemplate PrimaryAllocationAgencyProcessedTemplate(string TransactionId, string FileName, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationAgencyProcessedNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        public IMessageTemplate PrimaryAllocationOwnerProcessedTemplate(string TransactionId, string FileName, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationOwnerProcessedNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        public IMessageTemplate PrimaryAllocationTCAgencyProcessedTemplate(string TransactionId, string FileName, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationTCAgencyProcessedNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        //Uploaded
        public IMessageTemplate PrimaryAllocationAgencyUploadedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationAgencyUploadedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate PrimaryAllocationOwnerUploadedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationOwnerUploadedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate PrimaryAllocationTCAgencyUploadedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<PrimaryAllocationTCAgencyUploadedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        #endregion PrimaryAllocation

        #region SecondaryAllocation

        //Failed
        public IMessageTemplate SecondaryAllocationAgentFailedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationAgentFailedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate SecondaryAllocationStaffFailedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationStaffFailedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate SecondaryAllocationTeleCallerFailedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationTeleCallerFailedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        //Success
        public IMessageTemplate SecondaryAllocationAgentProcessedTemplate(string TransactionId, string FileName, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationAgentProcessedNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        public IMessageTemplate SecondaryAllocationStaffProcessedTemplate(string TransactionId, string FileName, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationStaffProcessedNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        public IMessageTemplate SecondaryAllocationTeleCallerProcessedTemplate(string TransactionId, string FileName, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationTeleCallerProcessedNotification>();
            utility.ConstructData(TransactionId, FileName);
            return utility;
        }

        //Uploaded
        public IMessageTemplate SecondaryAllocationAgentUploadedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationAgentUploadedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate SecondaryAllocationStaffUploadedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationStaffUploadedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        public IMessageTemplate SecondaryAllocationTeleCallerUploadsedTemplate(string WorkRequestId, string teanatId)
        {
            var utility = _flexHost.GetUtilityService<SecondaryAllocationTeleCallerUploadedNotification>();
            utility.ConstructData(WorkRequestId);
            return utility;
        }

        #endregion SecondaryAllocation

        #region Collection

        public IMessageTemplate IssueReceiptNotificationTemplate(CollectionDtoWithId dto)
        {
            var collectionMode = dto.CollectionMode.ToLower();

            IMessageTemplate model = null;
            switch (collectionMode)
            {
                case "dd":
                    var dd = _flexHost.GetUtilityService<IssueReceiptNotificationDD>(GetHostname(dto));
                    dd.ConstructData(dto);
                    return dd;
                    break;

                case "cash":
                    var cash = _flexHost.GetUtilityService<IssueReceiptNotificationCash>(GetHostname(dto));
                    cash.ConstructData(dto);
                    return cash;
                    break;

                case "cheque":
                    var cheque = _flexHost.GetUtilityService<IssueReceiptNotificationCheque>(GetHostname(dto));
                    cheque.ConstructData(dto);
                    return cheque;
                    break;

                case "online transfer":
                    var onlineTransfer = _flexHost.GetUtilityService<IssueReceiptNotificationOnlineTransfer>(GetHostname(dto));
                    onlineTransfer.ConstructData(dto);
                    return onlineTransfer;
                    break;

                case "neft":
                    var neft = _flexHost.GetUtilityService<IssueReceiptNotificationNEFT>(GetHostname(dto));
                    neft.ConstructData(dto);
                    return neft;
                    break;

                case "rtgs":
                    var rtgs = _flexHost.GetUtilityService<IssueReceiptNotificationRTGS>(GetHostname(dto));
                    rtgs.ConstructData(dto);
                    return rtgs;
                    break;
            }
            return model;
        }

        public IMessageTemplate IssueReceiptSMSNotificationTemplate(CollectionDtoWithId dto)
        {
            var smsModel = _flexHost.GetUtilityService<IssueReceiptNotificationSMS>(GetHostname(dto));
            smsModel.ConstructData(dto);
            return smsModel;
        }

        public IMessageTemplate OnlinePaymentLinkTemplate(CollectionDtoWithId dto, string response, string? tenantId)
        {
            var OnlinePayment = _flexHost.GetUtilityService<OnlinePaymentLink>(GetHostname(dto));
            OnlinePayment.ConstructData(dto, response);
            return OnlinePayment;
        }

        public IMessageTemplate PaymentOtpTemplate(string otp, string? hostName)
        {
            _logger.LogInformation("MessageTemplateFactory : HostName - " + hostName);
            var PaymentOtp = _flexHost.GetUtilityService<PaymentOtpNotification>(hostName);
            PaymentOtp.ConstructData(otp);
            return PaymentOtp;
        }

        public IMessageTemplate DuplicateReceiptTemplate(CollectionDtoWithId dto, string tenantId)
        {
            var DuplicateReceipt = _flexHost.GetUtilityService<DuplicateReceiptNotification>(GetHostname(dto));
            DuplicateReceipt.ConstructData(dto);
            return DuplicateReceipt;
        }

        public IMessageTemplate CollectionCancelledTemplate(CollectionDtoWithId dto, string tenantId)
        {
            var CollectionCancelled = _flexHost.GetUtilityService<CollectionCancelledNotification>(GetHostname(dto));
            CollectionCancelled.ConstructData(dto);
            return CollectionCancelled;
        }

        public IMessageTemplate CollectionCancellationRequestedTemplate(CollectionDtoWithId dto, string tenantId)
        {
            var CollectionCancellationRequested = _flexHost.GetUtilityService<CollectionCancellationRequestedNotification>(GetHostname(dto));
            CollectionCancellationRequested.ConstructData(dto);
            return CollectionCancellationRequested;
        }

        public IMessageTemplate CollectionCancellationRejectedTemplate(CollectionDtoWithId dto, string tenantId)
        {
            var CollectionCancellationRejected = _flexHost.GetUtilityService<CollectionCancellationRejectedNotification>(GetHostname(dto));
            CollectionCancellationRejected.ConstructData(dto);
            return CollectionCancellationRejected;
        }

        #endregion Collection

        #region Account

        public IMessageTemplate AccountVerifyTemplate(string otp, string tenantId)
        {
            var VerifyAccount = _flexHost.GetUtilityService<VerifyAccountNotification>();
            VerifyAccount.ConstructData(otp);
            return VerifyAccount;
        }

        public IMessageTemplate AccountPTPTemplate(AccountMessageDto dto)
        {
            var utility = _flexHost.GetUtilityService<AccountPTPNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate AccountPaymentTemplate(AccountMessageDto dto)
        {
            var utility = _flexHost.GetUtilityService<AccountPaymentNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        #endregion Account

        #region AccountImport

        public IMessageTemplate AccountImportErrorTemplate(string tenantId)
        {
            var AccountImportError = _flexHost.GetUtilityService<AccountImportErrorNotification>();
            AccountImportError.ConstructData();
            return AccountImportError;
        }

        public IMessageTemplate AccountImportFailedTemplate(string remarks, string tenantId)
        {
            var AccountImportFailed = _flexHost.GetUtilityService<AccountImportFailedNotification>();
            AccountImportFailed.ConstructData(remarks);
            return AccountImportFailed;
        }

        public IMessageTemplate AccountImportFileErrorTemplate(string transactionId, string tenantId)
        {
            var AccountImportFileError = _flexHost.GetUtilityService<AccountImportFileErrorNotification>();
            AccountImportFileError.ConstructData(transactionId);
            return AccountImportFileError;
        }

        public IMessageTemplate AccountImportFileFailedTemplate(string transactionId, string remarks, string tenantId)
        {
            var AccountImportFileFailed = _flexHost.GetUtilityService<AccountImportFileFailedNotification>();
            AccountImportFileFailed.ConstructData(transactionId, remarks);
            return AccountImportFileFailed;
        }

        public IMessageTemplate AccountImportFilePartialTemplate(string transactionId, DataTable records, string tenantId)
        {
            var AccountImportFilePartial = _flexHost.GetUtilityService<AccountImportFilePartialNotification>();
            AccountImportFilePartial.ConstructData(transactionId, records);
            return AccountImportFilePartial;
        }

        public IMessageTemplate AccountImportFileReceivedTemplate(string transactionId, string filename, string tenantId)
        {
            var AccountImportFileReceived = _flexHost.GetUtilityService<AccountImportFileReceivedNotification>();
            AccountImportFileReceived.ConstructData(transactionId, filename);
            return AccountImportFileReceived;
        }

        public IMessageTemplate AccountImportFileSuccessTemplate(string transactionId, string filename, string tenantId)
        {
            var AccountImportFileSuccess = _flexHost.GetUtilityService<AccountImportFileSuccessNotification>();
            AccountImportFileSuccess.ConstructData(transactionId, filename);
            return AccountImportFileSuccess;
        }

        public IMessageTemplate AccountImportPartialTemplate(string transactionId, DataTable records, string tenantId)
        {
            var AccountImportPartial = _flexHost.GetUtilityService<AccountImportPartialNotification>();
            AccountImportPartial.ConstructData(transactionId, records);
            return AccountImportPartial;
        }

        public IMessageTemplate AccountImportReceivedTemplate(string file, string fileExtension, string tenantId)
        {
            var AccountImportReceived = _flexHost.GetUtilityService<AccountImportReceivedNotification>();
            AccountImportReceived.ConstructData(file, fileExtension);
            return AccountImportReceived;
        }

        public IMessageTemplate AccountImportSuccessTemplate(string filename, string tenantId)
        {
            var AccountImportSuccess = _flexHost.GetUtilityService<AccountImportSuccessNotification>();
            AccountImportSuccess.ConstructData(filename);
            return AccountImportSuccess;
        }

        #endregion AccountImport

        #region AccountImportViaAPI

        public IMessageTemplate ImportAccountsViaAPIFailedTemplate(string transactionId, string remarks, string tenantId)
        {
            var ImportAccountsViaAPIFailed = _flexHost.GetUtilityService<ImportAccountsViaAPIFailedNotification>();
            ImportAccountsViaAPIFailed.ConstructData(transactionId, remarks);
            return ImportAccountsViaAPIFailed;
        }

        public IMessageTemplate ImportAccountsViaAPIPartialTemplate(string transactionId, DataTable records, string tenantId)
        {
            var ImportAccountsViaAPIPartial = _flexHost.GetUtilityService<ImportAccountsViaAPIPartialNotification>();
            ImportAccountsViaAPIPartial.ConstructData(transactionId, records);
            return ImportAccountsViaAPIPartial;
        }

        public IMessageTemplate ImportAccountsViaAPIReceivedTemplate(string transactionId, string tenantId)
        {
            var ImportAccountsViaAPIReceived = _flexHost.GetUtilityService<ImportAccountsViaAPIReceivedNotification>();
            ImportAccountsViaAPIReceived.ConstructData(transactionId);
            return ImportAccountsViaAPIReceived;
        }

        public IMessageTemplate ImportAccountsViaAPISuccessTemplate(string transactionId, string tenantId)
        {
            var ImportAccountsViaAPISuccess = _flexHost.GetUtilityService<ImportAccountsViaAPISuccessNotification>();
            ImportAccountsViaAPISuccess.ConstructData(transactionId);
            return ImportAccountsViaAPISuccess;
        }

        #endregion AccountImportViaAPI

        #region UnAllocation
        public IMessageTemplate UnAllocationPrimaryFailedTemplate(string transactionId, string unAllocationType, string tenantId)
        {
            var PrimaryUnAllocationFailed = _flexHost.GetUtilityService<PrimaryUnAllocationFailedNotification>();
            PrimaryUnAllocationFailed.ConstructData(transactionId, unAllocationType);
            return PrimaryUnAllocationFailed;
        }

        public IMessageTemplate UnAllocationPrimarySuccessTemplate(string transactionId, string fileName, string tenantId)
        {
            var PrimaryUnAllocationSuccess = _flexHost.GetUtilityService<PrimaryUnAllocationSuccessNotification>();
            PrimaryUnAllocationSuccess.ConstructData(transactionId, fileName);
            return PrimaryUnAllocationSuccess;
        }

        public IMessageTemplate UnAllocationPrimaryUploadedTemplate(string transactionId, string fileName, string uploadType, string tenantId)
        {
            var PrimaryUnAllocationUploaded = _flexHost.GetUtilityService<PrimaryUnAllocationUploadedNotification>();
            PrimaryUnAllocationUploaded.ConstructData(transactionId, fileName, uploadType);
            return PrimaryUnAllocationUploaded;
        }


        public IMessageTemplate UnAllocationSecondaryFailedTemplate(string transactionId, string tenantId, string unAllocationType)
        {
            var SecondaryUnAllocationFailed = _flexHost.GetUtilityService<SecondaryUnAllocationFailedNotification>();
            SecondaryUnAllocationFailed.ConstructData(transactionId, unAllocationType);
            return SecondaryUnAllocationFailed;
        }

        public IMessageTemplate UnAllocationSecondarySuccessTemplate(string transactionId, string fileName, string tenantId)
        {
            var SecondaryUnAllocationSuccess = _flexHost.GetUtilityService<SecondaryUnAllocationSuccessNotification>();
            SecondaryUnAllocationSuccess.ConstructData(transactionId, fileName);
            return SecondaryUnAllocationSuccess;
        }

        public IMessageTemplate UnAllocationSecondaryUploadedTemplate(string transactionId, string fileName, string uploadType, string tenantId)
        {
            var SecondaryUnAllocationUploaded = _flexHost.GetUtilityService<SecondaryUnAllocationUploadedNotification>();
            SecondaryUnAllocationUploaded.ConstructData(transactionId, fileName, uploadType);
            return SecondaryUnAllocationUploaded;
        }

        #endregion UnAllocation

        #region EmailVideoSMSEMAIL

        public IMessageTemplate VideoTemplate(string name, string link, string tinylink, string tenantId)
        {
            var VideoNotification = _flexHost.GetUtilityService<VideoNotification>();
            VideoNotification.ConstructData(name, link, tinylink);
            return VideoNotification;
        }

        #endregion EmailVideoSMSEMAIL

        #region OnlinePayment

        public IMessageTemplate PaynimoOnlinePaymentTemplate(CollectionDtoWithId dto, PaymentTransactionDtoWithId result, string tenantId)
        {
            var OnlinePayment = _flexHost.GetUtilityService<PaynimoOnlinePaymentNotification>(GetHostname(dto));
            OnlinePayment.ConstructData(dto, result);
            return OnlinePayment;
        }

        public IMessageTemplate RazorPayOnlinePaymentTemplate(CollectionDtoWithId dto, PaymentTransactionDtoWithId result, string tenantId)
        {
            var RazorPayOnlinePayment = _flexHost.GetUtilityService<RazorPayOnlinePaymentNotification>(GetHostname(dto));
            RazorPayOnlinePayment.ConstructData(dto, result);
            return RazorPayOnlinePayment;
        }

        public IMessageTemplate PayuOnlinePaymentTemplate(CollectionDtoWithId dto, PaymentTransactionDtoWithId result, string tenantId)
        {
            var PayuOnlinePayment = _flexHost.GetUtilityService<PayuOnlinePaymentNotification>(GetHostname(dto));
            PayuOnlinePayment.ConstructData(dto, result);
            return PayuOnlinePayment;
        }

        #endregion OnlinePayment

        #region bulkTrailUpload

        public IMessageTemplate BulkTrailUploadedEmailTemplate(BulkTrailUploadFileDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<BulkTrailUploadedEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        public IMessageTemplate BulkTrailUploadSucceededEmailTemplate(BulkTrailUploadFileDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<BulkTrailUploadSuccessEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        public IMessageTemplate BulkTrailUploadFailedEmailTemplate(BulkTrailUploadFileDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<BulkTrailUploadFailedEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }

        #endregion bulkTrailUpload

        #region CollectionBulkUpload

        public IMessageTemplate CollectionBulkUploadedEmailTemplate(CollectionUploadFileDtoWithId dto)
        {
            var upload = _flexHost.GetUtilityService<CollectionBulklUploadedEmailNotification>(GetHostname(dto));
            upload.ConstructData(dto);
            return upload;
        }

        public IMessageTemplate CollectionBulklUploadSuccessEmailTemplate(CollectionUploadFileDtoWithId dto)
        {
            var upload = _flexHost.GetUtilityService<CollectionBulklUploadSuccessEmailNotification>(GetHostname(dto));
            upload.ConstructData(dto);
            return upload;
        }

        public IMessageTemplate CollectionBulkUploadFailedEmailTemplate(CollectionUploadFileDtoWithId dto)
        {
            var upload = _flexHost.GetUtilityService<CollectionBulkUploadFailedEmailNotification>(GetHostname(dto));
            upload.ConstructData(dto);
            return upload;
        }

        #endregion bulkTrailUpload

        #region masterImport

        public IMessageTemplate MasterImportHeaderMismatchEmailTemplate(MasterFileStatusDtoWithId dto)
        {
            var masterimportdto = _flexHost.GetUtilityService<MasterImportHeaderMismatchEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto);
            return masterimportdto;
        }

        public IMessageTemplate MasterImportFailedEmailTemplate(MasterFileStatusDtoWithId dto)
        {
            var masterimportdto = _flexHost.GetUtilityService<MasterImportFailedEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto);
            return masterimportdto;
        }

        public IMessageTemplate MasterImportProcessedEmailTemplate(MasterFileStatusDtoWithId dto, int processedrecordcount)
        {
            var masterimportdto = _flexHost.GetUtilityService<MasterImportProcessedEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto, processedrecordcount);
            return masterimportdto;
        }

        public IMessageTemplate MasterImportEmailPartiallyProcessedTemplate(MasterFileStatusDtoWithId dto, int recordsinserted, int recordsupdated, int nooferrorrecords, int totalrecords)
        {
            var masterimportdto = _flexHost.GetUtilityService<MasterImportPartiallyProcessedEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto, recordsinserted, recordsupdated, nooferrorrecords, totalrecords);
            return masterimportdto;
        }

        public IMessageTemplate MasterImporErrorEmailTemplate(MasterFileStatusDtoWithId dto, int recordsinserted, int recordsupdated, int nooferrorrecords, int totalrecords)
        {
            var masterimportdto = _flexHost.GetUtilityService<MasterImportErrorEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto, recordsinserted, recordsupdated, nooferrorrecords, totalrecords);
            return masterimportdto;
        }

        public IMessageTemplate MasterImportNoRecordsEmailTemplate(MasterFileStatusDtoWithId dto, int recordsinserted, int recordsupdated, int nooferrorrecords, int totalrecords)
        {
            var masterimportdto = _flexHost.GetUtilityService<MasterImportNoRecordsEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto, recordsinserted, recordsupdated, nooferrorrecords, totalrecords);
            return masterimportdto;
        }

        #endregion masterImport

        #region DeviceDetail

        public IMessageTemplate RegisterDeviceSendOTPEmailTemplate(DeviceDetailDtoWithId dto)
        {
            var masterimportdto = _flexHost.GetUtilityService<RegisterDeviceSendOTPEmailNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto);
            return masterimportdto;
        }

        public IMessageTemplate RegisterDeviceSendOTPSMSTemplate(DeviceDetailDtoWithId dto)
        {
            var masterimportdto = _flexHost.GetUtilityService<RegisterDeviceSendOTPSMSNotification>(GetHostname(dto));
            masterimportdto.ConstructData(dto);
            return masterimportdto;
        }

        #endregion DeviceDetail

        #region BulkCreateUserUpload

        public IMessageTemplate BulkCreateAgentSuccessEmailTemplate(UsersCreateFileDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<BulkCreateAgencyUserSuccessEmailNotification>();
            agencyusercreated.ConstructData(dto.CustomId, dto.FileName);
            return agencyusercreated;
        }

        public IMessageTemplate BulkCreateAgencySuccessEmailTemplate(UsersCreateFileDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<BulkCreateAgencySuccessEmailNotification>();
            agencyusercreated.ConstructData(dto.CustomId, dto.FileName);
            return agencyusercreated;
        }

        public IMessageTemplate BulkCreateStaffSuccessEmailTemplate(UsersCreateFileDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<BulkCreateStaffSuccessEmailNotification>();
            agencyusercreated.ConstructData(dto.CustomId, dto.FileName);
            return agencyusercreated;
        }

        public IMessageTemplate BulkCreateAgentFailedEmailTemplate(string transactionId, string tenantId)
        {
            var PrimaryUnAllocationFailed = _flexHost.GetUtilityService<BulkCreateAgencyUserFailedEmailNotification>();
            PrimaryUnAllocationFailed.ConstructData(transactionId);
            return PrimaryUnAllocationFailed;
        }

        public IMessageTemplate BulkCreateAgencyFailedEmailTemplate(string transactionId, string tenantId)
        {
            var PrimaryUnAllocationFailed = _flexHost.GetUtilityService<BulkCreateStaffFailedEmailNotification>();
            PrimaryUnAllocationFailed.ConstructData(transactionId);
            return PrimaryUnAllocationFailed;
        }

        public IMessageTemplate BulkCreateStaffFailedEmailTemplate(string transactionId, string tenantId)
        {
            var PrimaryUnAllocationFailed = _flexHost.GetUtilityService<BulkCreateStaffFailedEmailNotification>();
            PrimaryUnAllocationFailed.ConstructData(transactionId);
            return PrimaryUnAllocationFailed;
        }

        #endregion

        #region CustomerConsent
        public IMessageTemplate CustomerConsentTemplate(CustomerConsentMessageDto dto)
        {
            var utility = _flexHost.GetUtilityService<CustomerConsentNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }

        public IMessageTemplate CustomerConsentResponseTemplate(CustomerConsentResponseMessageDto dto)
        {
            var utility = _flexHost.GetUtilityService<CustomerConsentResponseNotification>(GetHostname(dto));
            utility.ConstructData(dto);
            return utility;
        }
        #endregion

        #region GeoReport
        public IMessageTemplate GeoReportGeneratedTemplate()
        {
            var utility = _flexHost.GetUtilityService<GeoReportGeneratedNotification>();
            utility.ConstructData();
            return utility;
        }

        public IMessageTemplate GeoReportFailedTemplate()
        {
            var utility = _flexHost.GetUtilityService<GeoReportFailedNotification>();
            utility.ConstructData();
            return utility;
        }
        #endregion GeoReport

        #region Settlement
        public IMessageTemplate NotifyPotentialActorEmailTemplate(SettlementDtoWithId dto)
        {
            var agencyusercreated = _flexHost.GetUtilityService<NotifyPotentialActorEmailNotification>(GetHostname(dto));
            agencyusercreated.ConstructData(dto);
            return agencyusercreated;
        }
        #endregion

    }
}