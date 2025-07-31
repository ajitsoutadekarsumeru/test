using ENTiger.ENCollect.AgencyModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyIdentificationDoc : TFlexIdentificationDoc<AgencyIdentification, AgencyIdentificationDoc, Agency>
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual AgencyIdentificationDoc AgencyUpload(AgencyUploadCommand cmd)
        {
            Guard.AgainstNull("AgencyIdentificationDoc command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.FileName = cmd.Dto.Name;
            this.FileSize = cmd.Dto.Size;
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.SetAdded();

            //Set your appropriate SetAdded for the inner object here
            return this;
        }

        #endregion "Public Methods"
    }
}