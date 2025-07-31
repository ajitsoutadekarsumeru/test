using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class UserVerificationCodeTypeEnum : FlexEnum
    {
        public UserVerificationCodeTypeEnum()
        { }

        public UserVerificationCodeTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly UserVerificationCodeTypeEnum ForgotPassword
        = new UserVerificationCodeTypeEnum("1", "ForgotPassword");

        public static readonly UserVerificationCodeTypeEnum UpdateProfile
       = new UserVerificationCodeTypeEnum("2", "UpdateProfile");

        public static readonly UserVerificationCodeTypeEnum IssueReceipt
       = new UserVerificationCodeTypeEnum("3", "issuereceipt");

        public static readonly UserVerificationCodeTypeEnum UpdateTrail
       = new UserVerificationCodeTypeEnum("4", "updatetrail");

        public static readonly UserVerificationCodeTypeEnum VerifyLeadSave
       = new UserVerificationCodeTypeEnum("5", "VerifyLeadSave");

        public static readonly UserVerificationCodeTypeEnum LoginOtp
       = new UserVerificationCodeTypeEnum("6", "loginotp");

        public static readonly UserVerificationCodeTypeEnum VerifyAccount
       = new UserVerificationCodeTypeEnum("7", "verifyaccount");
    }
}