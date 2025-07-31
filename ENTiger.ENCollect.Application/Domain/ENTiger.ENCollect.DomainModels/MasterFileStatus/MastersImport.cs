using ENTiger.ENCollect.CommonModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class MasterFileStatus : DomainModelBridge
    {
        #region "Public Methods"
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        public MasterFileStatus(FilePathSettings fileSettings, IFileSystem fileSystem)
        {
            _fileSettings = fileSettings;
            _fileSystem = fileSystem;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual MasterFileStatus MastersImport(MastersImportCommand cmd, string filepath)
        {
            Guard.AgainstNull("MasterFileStatus command cannot be empty", cmd);

            //string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.CustomId = cmd.Dto.CustomId;
            this.Status = FileStatusEnum.Uploaded.Value;
            this.FileName = cmd.Dto.FileName;
            this.FilePath = filepath;
            this.UploadType = cmd.Dto.FileType;
            this.Description = FileStatusEnum.Uploaded.Value;
            this.FileUploadedDate = DateTime.Now;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}