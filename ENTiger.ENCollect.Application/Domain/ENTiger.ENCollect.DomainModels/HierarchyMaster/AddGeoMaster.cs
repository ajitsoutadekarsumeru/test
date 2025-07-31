using ENTiger.ENCollect.HierarchyModule;

namespace ENTiger.ENCollect
{
    public partial class HierarchyMaster : PersistenceModelBridge
    {

        #region "Public Methods"
        public virtual HierarchyMaster AddGeoMaster(AddGeoMasterCommand cmd)
        {
            Guard.AgainstNull("HierarchyMaster command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            this.SetAdded(cmd.Dto.GetGeneratedId());
            return this;
        }
        #endregion
    }
}
