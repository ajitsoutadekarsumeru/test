using ENTiger.ENCollect.SegmentationModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Segmentation : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Segmentation DisableSegments(DisableSegmentsCommand cmd, string item)
        {
            Guard.AgainstNull("Segmentation model cannot be empty", cmd);
            //this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.IsDisabled = true;
            //this.LastModifiedBy = partyId;
            this.LastModifiedDate = DateTime.Now;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}