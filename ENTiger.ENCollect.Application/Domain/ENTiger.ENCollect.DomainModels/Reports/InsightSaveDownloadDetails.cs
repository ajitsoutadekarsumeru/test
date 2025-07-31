using ENTiger.ENCollect.AllocationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.DomainModels.Reports
{
    public partial class InsightDownloadFile : DomainModelBridge
    {

        public virtual InsightDownloadFile InsightSaveDownloadDetails(InsightSaveDownloadDetailsDto Dto)
        {
            Guard.AgainstNull("InsightSaveDownloadDetailsDto dto cannot be empty", Dto);

            //this.Convert(Dto);
            this.CreatedBy = Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.CustomId = Dto.CustomId;
            this.FileName = Dto.FileName;
            this.Description = Dto.Description;
            this.Status = FileStatusEnum.Processed.Value;
            this.FilePath = Dto.FilePath;

            this.SetAdded(Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here
            return this;
        }


    }
}
