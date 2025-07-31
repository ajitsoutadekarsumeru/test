using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class RunStatus : DomainModelBridge
    {
        public RunStatus()
        {
        }

        [StringLength(200)]
        public string Status { get; private set; }

        [StringLength(200)]
        public string CustomId { get; set; }

        [StringLength(800)]
        public string ProcessType { get; private set; }

        //public void UpdateRunStatus(string status)
        //{
        //    Status = status;
        //}
        public RunStatus(string Type, string customid)
        {
            Status = "Started";
            ProcessType = Type;
            CustomId = customid;
        }

        //public void FinishRun()
        //{
        //    Status = "Completed";
        //}
    }
}