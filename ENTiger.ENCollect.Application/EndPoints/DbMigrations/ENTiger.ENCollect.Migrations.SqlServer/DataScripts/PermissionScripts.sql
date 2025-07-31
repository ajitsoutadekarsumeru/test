INSERT INTO [permissions] (
    [Id],
    [Name],
    [Description],
    [CreatedBy],
    [CreatedDate],
    [LastModifiedBy],
    [LastModifiedDate],
    [IsDeleted],
    [Section]
)
VALUES (
	'P0001'
	,'CanAccessDialer'
	,'Can Access Dialer'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Account Search and Details'
	)
	,(
	'P0002'
	,'CanAcknowledgeBatch'
	,'Can Acknowledge Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0003'
	,'CanAcknowledgePIS'
	,'Can Acknowledge PIS'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0004'
	,'CanAcknowledgeReceipt'
	,'Can Acknowledge Receipt'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0005'
	,'CanAddPTP'
	,'Can Add PTP'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0006'
	,'CanAddTrail'
	,'Can Add Trail'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0007'
	,'CanApproveAgency'
	,'Can Approve Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0008'
	,'CanApproveAgent'
	,'Can Approve Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0009'
	,'CanApproveReceiptCancellationRequest'
	,'Can Approve Receipt Cancellation Request'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0010'
	,'CanApproveStaff'
	,'Can Approve Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0011'
	,'CanChangePassword'
	,'Can Change Password'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Profile Settings'
	)
	,(
	'P0014'
	,'CanCreateAgency'
	,'Can Create Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0015'
	,'CanCreateAgent'
	,'Can Create Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0016'
	,'CanCreateBatch'
	,'Can Create Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0018'
	,'CanCreateDepositSlip'
	,'Can Create Deposit Slip'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0019'
	,'CanCreatePIS'
	,'Can Create PIS'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0020'
	,'CanCreateReceipt'
	,'Can Create Receipt'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Receipts'
	)
	,(
	'P0021'
	,'CanCreateReceiptCancellationRequest'
	,'Can Create Receipt Cancellation Request'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0023'
	,'CanCreateStaff'
	,'Can Create Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0025'
	,'CanCreateWalkinReceipt'
	,'Can Create Walkin Receipt'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Receipts'
	)
	,(
	'P0029'
	,'CanDisableAgency'
	,'Can Disable Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0030'
	,'CanDisableAgent'
	,'Can Disable Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0032'
	,'CanDisableMaster'
	,'Can Disable Master'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'System Settings'
	)
	,(
	'P0034'
	,'CanDisableStaff'
	,'Can Disable Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0036'
	,'CanDownloadAccountDashboardReport'
	,'Can Download Account Dashboard Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0037'
	,'CanDownloadAgencyAllocationGapReport'
	,'Can Download Agency Allocation Gap Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0038'
	,'CanDownloadAgentAllocationGapReport'
	,'Can Download Agent Allocation Gap Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0039'
	,'CanDownloadAllocatedvsArchievedReport'
	,'Can Download Allocatedvs Archieved Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0040'
	,'CanDownloadAttendanceReport'
	,'Can Download Attendance Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0041'
	,'CanDownloadCollectionIntensityReport'
	,'Can Download Collection Intensity Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0042'
	,'CanDownloadCollectionTrendReport'
	,'Can Download Collection Trend Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0043'
	,'CanDownloadCommunicationHistoryReport'
	,'Can Download Communication History Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0044'
	,'CanDownloadMoneyMovementReport'
	,'Can Download Money Movement Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0045'
	,'CanDownloadPaymentReport'
	,'Can Download Payment Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0046'
	,'CanDownloadPerformanceReport'
	,'Can Download Performance Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0047'
	,'CanDownloadSupervisoryReport'
	,'Can Download Supervisory Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0048'
	,'CanDownloadTrailGapReport'
	,'Can Download Trail Gap Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0049'
	,'CanDownloadTrailHistoryReport'
	,'Can Download Trail History Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0050'
	,'CanDownloadTrailIntensityReport'
	,'Can Download Trail Intensity Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0051'
	,'CanDownloadVisitIntensityReport'
	,'Can Download Visit Intensity Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0052'
	,'CanEnableAgency'
	,'Can Enable Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0053'
	,'CanEnableAgent'
	,'Can Enable Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0056'
	,'CanEnableStaff'
	,'Can Enable Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0060'
	,'CanMyTeamsAccounts'
	,'Can My Teams Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'NA'
	)
	,(
	'P0061'
	,'CanPrintBatch'
	,'Can Print Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0062'
	,'CanRejectAgency'
	,'Can Reject Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0063'
	,'CanRejectAgent'
	,'Can Reject Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0064'
	,'CanRejectReceiptCancellationRequest'
	,'Can Reject Receipt Cancellation Request'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0065'
	,'CanRejectStaff'
	,'Can Reject Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0066'
	,'CanSearchAccounts'
	,'Can Search Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Account Search and Details'
	)
	,(
	'P0067'
	,'CanSearchAgency'
	,'Can Search Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0068'
	,'CanSearchAgent'
	,'Can Search Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0069'
	,'CanSearchAllocationOwnerBatchStatus'
	,'Can Search Allocation Owner Batch Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0070'
	,'CanSearchBatch'
	,'Can Search Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0071'
	,'CanSearchBulkAccountsUploadStatus'
	,'Can Search Bulk Accounts Upload Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'System Settings'
	)
	,(
	'P0072'
	,'CanSearchBulkEnableDisableUserStatus'
	,'Can Search Bulk Enable Disable User Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0073'
	,'CanSearchBulkTrailStatus'
	,'Can Search Bulk Trail Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0075'
	,'CanSearchMasters'
	,'Can Search Masters'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'System Settings'
	)
	,(
	'P0076'
	,'CanSearchMyCollections'
	,'Can Search My Collections'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0077'
	,'CanSearchMyDepositSlips'
	,'Can Search My Deposit Slips'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0078'
	,'CanSearchMyPTP'
	,'Can Search My PTP'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0079'
	,'CanSearchMyReceipts'
	,'Can Search My Receipts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Receipts'
	)
	,(
	'P0080'
	,'CanSearchMyTrips'
	,'Can Search My Trips'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0081'
	,'CanSearchNearestBranch'
	,'Can Search Nearest Branch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Profile Settings'
	)
	,(
	'P0082'
	,'CanSearchPIS'
	,'Can Search PIS'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0083'
	,'CanSearchPrimaryAllocationBatchStatus'
	,'Can Search Primary Allocation Batch Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0084'
	,'CanSearchPrimaryDeAllocationBatchStatus'
	,'Can Search Primary De Allocation Batch Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0085'
	,'CanSearchSecondaryAllocationBatchStatus'
	,'Can Search Secondary Allocation Batch Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0086'
	,'CanSearchSecondaryDeAllocationBatchStatus'
	,'Can Search Secondary De Allocation Batch Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0088'
	,'CanSearchStaff'
	,'Can Search Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0089'
	,'CanSearchTravelReport'
	,'Can Search Travel Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0091'
	,'CanSearchUploadMastersStatus'
	,'Can Search Upload Masters Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'System Settings'
	)
	,(
	'P0092'
	,'CanSendDuplicateReceipt'
	,'Can Send Duplicate Receipt'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0093'
	,'CanSendemail'
	,'Can Sendemail'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Account Search and Details'
	)
	,(
	'P0094'
	,'CanSendPaymentLink'
	,'Can Send Payment Link'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Receipts'
	)
	,(
	'P0095'
	,'CanSendSMS'
	,'Can Send SMS'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Account Search and Details'
	)
	,(
	'P0098'
	,'CanUpdateAgency'
	,'Can Update Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0099'
	,'CanUpdateAgent'
	,'Can Update Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0100'
	,'CanUpdateBatch'
	,'Can Update Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0102'
	,'CanUpdatePrimaryAllocationByFilter'
	,'Can Update Primary Allocation By Filter'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0103'
	,'CanUpdateSecondaryAllocationByFilter'
	,'Can Update Secondary Allocation By Filter'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0105'
	,'CanUpdateStaff'
	,'Can Update Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0107'
	,'CanUploadAllocationOwnerBatch'
	,'Can Upload Allocation Owner Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0108'
	,'CanUploadBulkAccounts'
	,'Can Upload Bulk Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'System Settings'
	)
	,(
	'P0109'
	,'CanUploadBulkEnableDisableUser'
	,'Can Upload Bulk Enable Disable User'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0110'
	,'CanUploadBulkTrail'
	,'Can Upload Bulk Trail'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0111'
	,'CanUploadMasters'
	,'Can Upload Masters'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'System Settings'
	)
	,(
	'P0112'
	,'CanUploadPrimaryAllocationBatch'
	,'Can Upload Primary Allocation Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0113'
	,'CanUploadPrimaryDeAllocationBatch'
	,'Can Upload Primary De Allocation Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0114'
	,'CanUploadSecondaryAllocationBatch'
	,'Can Upload Secondary Allocation Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0115'
	,'CanUploadSecondaryDeAllocationBatch'
	,'Can Upload Secondary De Allocation Batch'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0116'
	,'CanViewAccountDashboardReport'
	,'Can View Account Dashboard Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0117'
	,'CanViewAgency'
	,'Can View Agency'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0118'
	,'CanViewAgencyAllocationGapReport'
	,'Can View Agency Allocation Gap Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0119'
	,'CanViewAgent'
	,'Can View Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0120'
	,'CanViewAgentAllocationGapReport'
	,'Can View Agent Allocation Gap Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0121'
	,'CanViewAllocatedvsArchievedReport'
	,'Can View Allocatedvs Archieved Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0122'
	,'CanViewAttemptedAccounts'
	,'Can View Attempted Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0123'
	,'CanViewAttendanceReport'
	,'Can View Attendance Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0124'
	,'CanViewBirdsEye'
	,'Can View Birds Eye'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0125'
	,'CanViewCollateralDetails'
	,'Can View Collateral Details'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'NA'
	)
	,(
	'P0126'
	,'CanViewCollectionIntensityReport'
	,'Can View Collection Intensity Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0127'
	,'CanViewCollectionTrendReport'
	,'Can View Collection Trend Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0128'
	,'CanViewCommunicationHistoryReport'
	,'Can View Communication History Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0130'
	,'CanViewCreditBureauDetails'
	,'Can View Credit Bureau Details'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'NA'
	)
	,(
	'P0131'
	,'CanViewDigitalIDCard'
	,'Can View Digital I D Card'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Profile Settings'
	)
	,(
	'P0132'
	,'CanViewFeedbacks'
	,'Can View Feedbacks'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0133'
	,'CanViewLastFivePTPs'
	,'Can View Last Five P T Ps'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0134'
	,'CanViewLastThreeFeedbacks'
	,'Can View Last Three Feedbacks'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0135'
	,'CanViewLastThreePayments'
	,'Can View Last Three Payments'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Receipts'
	)
	,(
	'P0136'
	,'CanViewMoneyMovementReport'
	,'Can View Money Movement Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0137'
	,'CanViewMyAccounts'
	,'Can View My Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0138'
	,'CanViewPaidAccounts'
	,'Can View Paid Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0139'
	,'CanViewPaymentReport'
	,'Can View Payment Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0140'
	,'CanViewPerformanceReport'
	,'Can View Performance Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0141'
	,'CanViewPIS'
	,'Can View PIS'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Payments'
	)
	,(
	'P0142'
	,'CanViewStaff'
	,'Can View Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0143'
	,'CanViewSupervisoryReport'
	,'Can View Supervisory Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0144'
	,'CanViewTodaysPTP'
	,'Can View Todays P T P'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Trails'
	)
	,(
	'P0145'
	,'CanViewTodaysQueue'
	,'Can View Todays Queue'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0146'
	,'CanViewTopTenAccounts'
	,'Can View Top Ten Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0147'
	,'CanViewTrailGapReport'
	,'Can View Trail Gap Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0148'
	,'CanViewTrailHistoryReport'
	,'Can View Trail History Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0149'
	,'CanViewTrailIntensityReport'
	,'Can View Trail Intensity Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0150'
	,'CanViewUnattemptedAccounts'
	,'Can View Unattempted Accounts'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Allocations'
	)
	,(
	'P0151'
	,'CanViewVisitIntensityReport'
	,'Can View Visit Intensity Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0152'
	,'CanUploadBulkUser'
	,'Can Upload Bulk User'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0153'
	,'CanSearchBulkUserUploadStatus'
	,'Can Search Bulk User Upload Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0156'
	,'CanViewCustomerContactReport'
	,'Can View Customer Contact Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0157'
	,'CanSetWalletLimit'
	,'Can Set Wallet Limit'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0158'
	,'CanViewWalletDetails'
	,'Can View Wallet Details'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Profile Settings'
	)
	,(
	'P0159'
	,'CanViewCashWalletLimitReport'
	,'Can View Cash Wallet  Limit Report'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Reports'
	)
	,(
	'P0160'
	,'CanActivateAgent'
	,'Can Activate Agent'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0161'
	,'CanActivateStaff'
	,'Can Activate Staff'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'User Management'
	)
	,(
	'P0162'
	,'CanViewPermissions'
	,'Can View Permissions'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0163'
	,'CanCreatePermissionScheme'
	,'Can Create Permission Scheme'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0164'
	,'CanViewPermissionSchemes'
	,'Can View Permission Schemes'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0165'
	,'CanViewPermissionScheme'
	,'Can View Permission Scheme'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0166'
	,'CanUpdatePermissionScheme'
	,'Can Update Permission Scheme'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0167'
	,'CanViewPermissionSchemeDesignations'
	,'Can View Permission Scheme Designations'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0168'
	,'CanViewDesignationSchemeDetails'
	,'Can View Designation Scheme Details'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0169'
	,'CanAssignPermissionScheme'
	,'Can Assign Permission Scheme'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0170'
	,'CanSearchPermissions'
	,'Can Search Permissions'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Permissions'
	)
	,(
	'P0171'
	,'CanFlagSettlementAsEligible'
	,'Can Flag Settlement As Eligible'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Settlement'
	)
	,(
	'P0172'
	,'CanRequestSettlement'
	,'Can Request Settlement'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Settlement'
	)
	,(
	'P0173'
	,'CanViewMySettlement'
	,'Can View My Settlement'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Settlement'
	)
	,(
	'P0174'
	,'CanViewMyQueueSettlement'
	,'Can View My Queue Settlement'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Settlement'
	)
	,(
	'P0175'
	,'CanUpdateSettlementStatus'
	,'Can Update Settlement Status'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Settlement'
	)
	,(
	'P0176'
	,'CanEditSettlement'
	,'Can Edit Settlement'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Settlement'
	),
	(
	'P0177'
	,'CanViewMyReceiptsSummary'
	,'Can View MyReceipts Summary'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Receipts'
	),
	(
	'P0178'
	,'Can View MyAccounts Summary'
	,'Can View MyAccounts Summary'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
	,'Account Search and Details'
	);