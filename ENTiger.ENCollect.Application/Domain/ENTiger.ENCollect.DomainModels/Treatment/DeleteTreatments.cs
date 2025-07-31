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
        public virtual Treatment DeleteTreatments(DeleteTreatmentsCommand cmd)
        {
            Guard.AgainstNull("Treatment model cannot be empty", cmd);
            //this.Id = cmd.Dto.Id;
            this.SetSoftDeleted();
            this.IsDisabled = false;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Set your appropriate SetDeleted for the inner object here
            foreach (var segment in this.segmentMapping)
            {
                segment.SetSoftDeleted();
                segment.SetLastModifiedBy(this.LastModifiedBy);
                segment.SetModified();
            }
            foreach (var subTreatment in this.subTreatment)
            {
                subTreatment.SetSoftDeleted();
                subTreatment.SetLastModifiedBy(this.LastModifiedBy);

                foreach (var subitem in subTreatment.POSCriteria)
                {
                    subitem.SetSoftDeleted();
                    subitem.SetLastModifiedBy(this.LastModifiedBy);
                    subitem.SetModified();
                }
                foreach (var subitem in subTreatment.AccountCriteria)
                {
                    subitem.SetSoftDeleted();
                    subitem.SetLastModifiedBy(this.LastModifiedBy);
                    subitem.SetModified();
                }
                foreach (var subitem in subTreatment.RoundRobinCriteria)
                {
                    subitem.SetSoftDeleted();
                    subitem.SetLastModifiedBy(this.LastModifiedBy);
                    subitem.SetModified();
                }
                foreach (var subitem in subTreatment.TreatmentByRule)
                {
                    subitem.SetSoftDeleted();
                    subitem.SetLastModifiedBy(this.LastModifiedBy);
                    subitem.SetModified();
                }
                if (subTreatment.TreatmentOnUpdateTrail != null)
                {
                    subTreatment.TreatmentOnUpdateTrail.TreatmentId = this.Id;
                    subTreatment.TreatmentOnUpdateTrail.SetModified();
                }
                if (subTreatment.TreatmentOnCommunication != null)
                {
                    subTreatment.TreatmentOnCommunication.TreatmentId = this.Id;
                    subTreatment.TreatmentOnCommunication.SetModified();
                }
                foreach (var subitem in subTreatment.PerformanceBand)
                {
                    subitem.SetSoftDeleted();
                    subitem.TreatmentId = this.Id;
                    subitem.SetModified();
                }
                foreach (var subitem in subTreatment.Designation)
                {
                    subitem.SetSoftDeleted();
                    subitem.TreatmentId = this.Id;
                    subitem.SetModified();
                }
                subTreatment.SetModified();
            }

            this.SetModified();

            return this;
        }

        #endregion "Public Methods"
    }
}