using ENTiger.ENCollect.AllocationModule;
using ENTiger.ENCollect.DomainModels.Reports;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.Mappers.Features.Reports
{

    public partial class InsightSaveDownloadDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public InsightSaveDownloadDetailsMapperConfiguration() : base()
        {
            CreateMap<InsightDownloadFile, InsightSaveDownloadDetailsDto>()
             ;
        }
    }
}
