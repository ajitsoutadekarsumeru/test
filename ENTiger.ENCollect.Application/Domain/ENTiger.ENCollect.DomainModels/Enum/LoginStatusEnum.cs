using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class LoginStatusEnum : FlexEnum
    {
        public LoginStatusEnum()
        { }

        public LoginStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly LoginStatusEnum Fail = new LoginStatusEnum("fail", "fail");
        public static readonly LoginStatusEnum Success = new LoginStatusEnum("success", "success");


    }
}