using Sumeru.Flex;
using ENTiger.ENCollect.DesignationsModule;
namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Designation : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Designation AssignScheme(AssignSchemeCommand cmd)
        {
            Guard.AgainstNull("Designation model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            
            this.SetModified();


            return this;
        }
#endregion

        #region "Private Methods"
        #endregion

    }
}
