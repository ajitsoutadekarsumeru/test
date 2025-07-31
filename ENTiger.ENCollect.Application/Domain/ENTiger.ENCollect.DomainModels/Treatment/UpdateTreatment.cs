using ENTiger.ENCollect.TreatmentModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Treatment : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Treatment UpdateTreatment(UpdateTreatmentCommand cmd)
        {
            Guard.AgainstNull("Treatment model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            this.segmentMapping.SetAddedOrModified();
            this.SetModified();
            foreach (var item in this.subTreatment)
            {
                item.SetAddedOrModified();
                foreach (var subitems in item.TreatmentByRule)
                {
                    subitems.TreatmentId = this.Id;
                    subitems.SetAddedOrModified();
                }
                foreach (var subitems in item.RoundRobinCriteria)
                {
                    subitems.TreatmentId = this.Id;
                    subitems.SetAddedOrModified();
                }
                foreach (var subitems in item.POSCriteria)
                {
                    subitems.TreatmentId = this.Id;
                    subitems.SetAddedOrModified();
                }
                foreach (var subitems in item.AccountCriteria)
                {
                    subitems.TreatmentId = this.Id;
                    subitems.SetAddedOrModified();
                }
                if (item.TreatmentOnUpdateTrail != null)
                {
                    item.TreatmentOnUpdateTrail.TreatmentId = this.Id;
                    item.TreatmentOnUpdateTrail.SetAddedOrModified();
                }
                if (item.TreatmentOnCommunication != null)
                {
                    item.TreatmentOnCommunication.TreatmentId = this.Id;
                    item.TreatmentOnCommunication.SetAddedOrModified();
                }
                foreach (var subitems in item.PerformanceBand)
                {
                    subitems.TreatmentId = this.Id;
                    subitems.SetAddedOrModified();
                }
                if (item.Designation != null)
                {
                    foreach (var subitems in item.Designation)
                    {
                        subitems.TreatmentId = this.Id;
                        subitems.SetAddedOrModified();
                    }
                }
                if (item.DeliveryStatus != null)
                {
                    foreach (var subitems in item.DeliveryStatus)
                    {
                        subitems.TreatmentId = this.Id;
                        subitems.SetAddedOrModified();
                    }
                }
            }

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}