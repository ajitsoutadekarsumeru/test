using Sumeru.Flex;
using ENTiger.ENCollect.AccountsModule;
namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountScopeConfiguration : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual AccountScopeConfiguration Update(AccountScopeConfigurationDto dto)
        {
            Guard.AgainstNull("RoleAccountFetchConfig model cannot be empty", dto);
            this.Convert(dto);
            this.LastModifiedBy = dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.ScopeLevel = int.Parse(GetScopeLevel(dto.Scope));
            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }
        #endregion

        #region "Private Methods"
        private static string GetScopeLevel(string scope)
        {
            return scope switch
            {
                var u when u == AccountAccessScopeEnum.Self.DisplayName => AccountAccessScopeEnum.Self.Value,
                var u when u == AccountAccessScopeEnum.Parent.DisplayName => AccountAccessScopeEnum.Parent.Value,
                var u when u == AccountAccessScopeEnum.All.DisplayName => AccountAccessScopeEnum.All.Value,

                _ => AccountAccessScopeEnum.All.Value  // Default for invalid values (optional error handling)
            };
        }

        #endregion

    }
}
