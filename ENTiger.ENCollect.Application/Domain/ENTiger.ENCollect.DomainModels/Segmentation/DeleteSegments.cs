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
        public virtual Segmentation DeleteSegments(string segmentId)
        {
            Guard.AgainstNull("Segmentation model cannot be empty", segmentId);

            this.Id = segmentId;
            this.SetIsDeleted(true);
            this.SetModified();
            //this.SetDeleted();

            //Set your appropriate SetDeleted for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}