using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CreateUsersByBatchDto : DtoBridge
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
    }

    public class BulkUserErrorList
    {
        public int sequence { get; set; }
        public string errormessage { get; set; }
    }

    public class BulkUserDateCheck
    {
        public bool validate { get; set; }
        public DateTime Date { get; set; }
    }
}