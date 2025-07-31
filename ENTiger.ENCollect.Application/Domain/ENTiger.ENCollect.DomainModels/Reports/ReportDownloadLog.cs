using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.DomainModels.Reports
{
    public class ReportDownloadLog : PersistenceModel
    {
        [StringLength(200)]
        public string? ReportType { get; set; }

        public string? ReportFilters { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

    }
}
