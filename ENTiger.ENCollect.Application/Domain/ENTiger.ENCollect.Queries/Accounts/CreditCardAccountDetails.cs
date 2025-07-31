using System.Text;
using ENCollect.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class CreditCardAccountDetails : FlexiQueryBridgeAsync<LoanAccount, CreditCardAccountDetailsDto>
    {
        protected readonly ILogger<CreditCardAccountDetails> _logger;
        protected CreditCardAccountDetailsParams _params;
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
        public CreditCardAccountDetails(ILogger<CreditCardAccountDetails> logger, IRepoFactory repoFactory, IOptions<EncryptionSettings> encryptionSettings, ICustomUtility customUtility)
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
        public virtual CreditCardAccountDetails AssignParameters(CreditCardAccountDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<CreditCardAccountDetailsDto> Fetch()
        {
            string encryptkey = _encryptionSettings.StaticKeys.EncryptionKey;

            var result = await Build<LoanAccount>().SelectTo<CreditCardAccountDetailsDto>().FirstOrDefaultAsync();

            LoanAccountJSON json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == result.Id).FirstOrDefaultAsync();

            Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();
            if (json != null)
            {
                accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);

                result.AccountJSON = json?.AccountJSON ?? null;
                result.Credit_Limit = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.CreditLimit.Value)?.ToString();
                result.CURRENT_BALANCE_AMOUNT = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.CurrentBalanceAmount.Value)?.ToString();
                result.STATEMENTED_OPENING_BALANCE = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.StatementedOpeningBalance.Value)?.ToString();
                result.STATEMENTED_DUE_DATE_SYSTEM = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.StatementedDueDateSystem.Value)?.ToString();
                result.LAST_PAYMENT_AMOUNT = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.LastPaymentAmount.Value)?.ToString();
                result.LAST_PAYMENT_DATE = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.LastPaymentDate.Value)?.ToString();
                result.LAST_PURCHASED_AMOUNT = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.LastPurchasedAmount.Value)?.ToString();
                result.LAST_PURCHASED_DATE = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.LastPurchasedDate.Value)?.ToString();
                result.PAYMENT_CYCLE_DUE = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentCycleDue.Value)?.ToString();
                result.PAYMENT_DUE_30_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue30Days.Value)?.ToString();
                result.PAYMENT_DUE_60_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue60Days.Value)?.ToString();
                result.PAYMENT_DUE_90_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue90Days.Value)?.ToString();
                result.PAYMENT_DUE_120_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue120Days.Value)?.ToString();
                result.PAYMENT_DUE_150_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue150Days.Value)?.ToString();
                result.PAYMENT_DUE_180_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue180Days.Value)?.ToString();
                result.PAYMENT_DUE_210_DAYS = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.PaymentDue210Days.Value)?.ToString();

                result.ResidentialAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.MailingAddress.Value)?.ToString();
                result.ResidentialNumber = result.PermanentMobileNo;
                result.OfficeAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.CustomerOfficePinCode.Value)?.ToString();
                result.OfficeNumber = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.NonMailingPhone1.Value)?.ToString();
                result.CoApplicantName = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.CoApplicant1.Value)?.ToString();
                result.CoApplicantNumber = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.CoApplicant1Contact.Value)?.ToString();

                result.GuarantorMailingAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.Guarantor1City.Value)?.ToString();
                result.FathersAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.FathersAddress.Value)?.ToString();
                result.MothersAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.MothersAddress.Value)?.ToString();
                result.ReferenceAddress = _customUtility.GetValueByKey(accountData, LoanAccountJsonEnum.Reference1Address.Value)?.ToString();
            }

            var aesGcmCrypto = new AesGcmCrypto();
            string statcikey = encryptkey;
            var aesGcmKey = Encoding.UTF8.GetBytes(statcikey);
            result.AccountNo = !string.IsNullOrEmpty(result.AccountNo)
                ? aesGcmCrypto.Encrypt(result.AccountNo, aesGcmKey)
                : result.AccountNo;
            result.CreditCardNo = !string.IsNullOrEmpty(result.CreditCardNo)
                ? aesGcmCrypto.Encrypt(result.CreditCardNo, aesGcmKey)
                : result.CreditCardNo;
            result.CustomerName = !string.IsNullOrEmpty(result.CustomerName)
                ? aesGcmCrypto.Encrypt(result.CustomerName, aesGcmKey)
                : result.CustomerName;
            result.EMailId = !string.IsNullOrEmpty(result.EMailId)
                ? aesGcmCrypto.Encrypt(result.EMailId, aesGcmKey)
                : result.EMailId;
            result.MobileNo = !string.IsNullOrEmpty(result.MobileNo)
                ? aesGcmCrypto.Encrypt(result.MobileNo, aesGcmKey)
                : result.MobileNo;

            result.Latest_Address_From_Trail = !string.IsNullOrEmpty(result.Latest_Address_From_Trail)
                ? aesGcmCrypto.Encrypt(result.Latest_Address_From_Trail, aesGcmKey)
                : result.Latest_Address_From_Trail;
            result.Latest_Email_From_Trail = !string.IsNullOrEmpty(result.Latest_Email_From_Trail)
               ? aesGcmCrypto.Encrypt(result.Latest_Email_From_Trail, aesGcmKey)
               : result.Latest_Email_From_Trail;
            result.Latest_Number_From_Trail = !string.IsNullOrEmpty(result.Latest_Number_From_Trail)
               ? aesGcmCrypto.Encrypt(result.Latest_Number_From_Trail, aesGcmKey)
               : result.Latest_Number_From_Trail;
            result.Latest_Number_From_Receipt = !string.IsNullOrEmpty(result.Latest_Number_From_Receipt)
              ? aesGcmCrypto.Encrypt(result.Latest_Number_From_Receipt, aesGcmKey)
              : result.Latest_Number_From_Receipt;
            result.Latest_Number_From_Send_Payment = !string.IsNullOrEmpty(result.Latest_Number_From_Send_Payment)
              ? aesGcmCrypto.Encrypt(result.Latest_Number_From_Send_Payment, aesGcmKey)
              : result.Latest_Number_From_Send_Payment;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.AccountId);
            return query;
        }
    }

    public class CreditCardAccountDetailsParams : DtoBridge
    {
        public string? AccountId { get; set; }
    }
}