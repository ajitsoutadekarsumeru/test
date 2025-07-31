using Sumeru.Flex;
using ENTiger.ENCollect.PublicModule;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Collection : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual Collection ImportCollections(ImportCollectionsCommand cmd)
        {
            Guard.AgainstNull("Collection command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here



            return this;
        }
#endregion


        #region "Private Methods"
        #endregion

    }
}
