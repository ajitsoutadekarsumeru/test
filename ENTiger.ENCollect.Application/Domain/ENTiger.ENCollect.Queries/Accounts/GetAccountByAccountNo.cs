using System.Text;
using ENCollect.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAccountByAccountNo : FlexiQueryBridgeAsync<LoanAccount, GetAccountByAccountNoDto>
    {
        protected readonly ILogger<GetAccountByAccountNo> _logger;
        protected GetAccountByAccountNoParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;
        private readonly EncryptionSettings _encryptionSettings;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAccountByAccountNo(ILogger<GetAccountByAccountNo> logger, IRepoFactory repoFactory
                , ICustomUtility customUtility, IOptions<EncryptionSettings> encryptionSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
            _encryptionSettings = encryptionSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetAccountByAccountNo AssignParameters(GetAccountByAccountNoParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetAccountByAccountNoDto> Fetch()
        {
            // byte[] aesgcmkey = new byte[] { };
            AesGcmCrypto aesGcmCrypto = new AesGcmCrypto();
            string key = _encryptionSettings.StaticKeys.DecryptionKey;
            var aesGcmKey = Encoding.UTF8.GetBytes(key);
            _params.accountno = aesGcmCrypto.Decrypt(_params.accountno, aesGcmKey);

            LoanAccountJSON json;

            GetAccountByAccountNoDto model = new GetAccountByAccountNoDto();

            Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();
            var result = await Build<LoanAccount>().FirstOrDefaultAsync();

            if (result != null)
            {
                json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == result.Id).FirstOrDefaultAsync();

                accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);
                model.PropensityToPayConfidence = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayConfidence.Value);
                model.PropensityToPay = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPay.Value);
                model.BCC_PENDING = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.BCCPending.Value);
                model.GroupName = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.GroupName.Value);
                model.referencE1_ADDRS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.Reference1Address.Value);
                model.physicaL_ADDRESS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PhysicalAddress.Value);
                model.motherS_ADDRS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.MothersAddress.Value);
                model.fatherS_ADDRS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.FathersAddress.Value);
                model.guarantoR1_CITY = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.Guarantor1City.Value);
                model.newAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.NewAddress.Value);
                model.BOUNCED_CHEQUE_CHARGES = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.BouncedChequeCharges.Value);
                model.Guarantor_Mailing_Address = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.Guarantor3.Value);
                model.Partner_Name = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PartnerName.Value);
                model.BounceCharges = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value);
                model.TOTAL_OVERDUE_AMT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalOverdueAmount.Value);
                model.TOTAL_LATE_PAYMENT_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalLatePaymentCharge.Value);
                model.AssetType = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.AssetType.Value);
                model.AssetDetails = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.AssetDetails.Value);
                model.Partner_Customer_ID = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PartnerCustomerID.Value);

                
                model.Partner_Loan_ID = result.Partner_Loan_ID;
                model.IsEligibleForRepossession = result.IsEligibleForRepossession;
                model.IsEligibleForRestructure = result.IsEligibleForRestructure;
                model.IsEligibleForSettlement = result.IsEligibleForSettlement;
                model.IsEligibleForLegal = result.IsEligibleForLegal;
                model.TOS = Convert.ToDecimal(result.TOS);
                model.EMIAmount = result.EMIAMT;
                model.CurrentBucket = result.CURRENT_BUCKET;
                model.MonthStartingBucket = result.BUCKET != null ? Convert.ToString(result.BUCKET) : string.Empty;
                model.POS = result.BOM_POS;
                model.ProductName = result.PRODUCT;
                model.ProductGroup = result.ProductGroup;
                model.City = result.CITY;
                model.State = result.STATE;
                model.CurrentDPD = result.CURRENT_DPD != null ? Convert.ToString(result.CURRENT_DPD) : string.Empty;
                model.Id = result.Id;
                model.PRINCIPLE_OVERDUE = result.PRINCIPAL_OD;
                model.INTEREST_OVERDUE = result.INTEREST_OD;
                model.PTPAmount = result.LatestPTPAmount;
                model.LatestPTPAmount = result.LatestPTPAmount;
                model.PENAL_PENDING = result.PENAL_PENDING;
                model.CURRENT_POS = result.CURRENT_POS;
                model.EMI_OD_AMT = result.EMI_OD_AMT;
                model.PTPDate = result.LatestPTPDate;
                model.Branch = result.BRANCH;
                model.AccountCategory = result.SCHEME_DESC;
                model.SubProductName = result.SubProduct;
                model.BranchCode = result.BranchCode;
                model.CentreID = result.CentreID;
                model.CentreName = result.CentreName;
                model.GroupID = result.GroupId;
                model.Partner_Loan_ID = result.Partner_Loan_ID;
                model.AccountNo = !string.IsNullOrEmpty(result.CustomId)
                    ? aesGcmCrypto.Encrypt(result.CustomId, aesGcmKey)
                    : result.CustomId;
                model.CreditCardNo = !string.IsNullOrEmpty(result.CustomId)
                    ? aesGcmCrypto.Encrypt(result.CustomId, aesGcmKey)
                    : result.CustomId;
                model.CustomerName = !string.IsNullOrEmpty(result.CUSTOMERNAME)
                    ? aesGcmCrypto.Encrypt(result.CUSTOMERNAME, aesGcmKey)
                    : result.CUSTOMERNAME;
                model.EMailId = !string.IsNullOrEmpty(result.LatestEmailId)
                    ? aesGcmCrypto.Encrypt(result.LatestEmailId, aesGcmKey)
                    : result.LatestEmailId;
                model.MobileNo = !string.IsNullOrEmpty(result.LatestMobileNo)
                    ? aesGcmCrypto.Encrypt(result.LatestMobileNo, aesGcmKey)
                    : result.LatestMobileNo;
                ;
            }

            //else
            //{
            //    packet.AddError("Error", "Invalid Account");
            //}
            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByAccountNo(_params.accountno);
            return query;
        }
    }

    public class GetAccountByAccountNoParams : DtoBridge
    {
        public string Id { get; set; }

        public string accountno { get; set; }
    }
}