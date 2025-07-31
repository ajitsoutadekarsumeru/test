using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.DomainModels;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUser : ApplicationUser
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual AgencyUser UpdateAgent(UpdateAgentCommand cmd)
        {
            Guard.AgainstNull("AgencyUser model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.SetWalletLimit(cmd);

            this.SetModified();
           

            return this;
        }

        private void SetWalletLimit(UpdateAgentCommand cmd)
        {
            if (this.Wallet == null)
            {
                // Wallet does not exist — create one
                var wallet = new Wallet(this.Id, cmd.Dto.WalletLimit);
                wallet.SetAddedOrModified();
                wallet.SetCreatedBy(this.LastModifiedBy);
                wallet.SetLastModifiedBy(this.LastModifiedBy);
                this.Wallet = wallet;
            }
            else if (this.Wallet.WalletLimit != cmd.Dto.WalletLimit)
            {
                // Update existing wallet limit
                this.Wallet.SetWalletLimit(cmd.Dto.WalletLimit);
                this.Wallet.SetAddedOrModified();
                this.Wallet.SetLastModifiedBy(this.LastModifiedBy);
            }
        }

        #endregion "Public Methods"
    }
}