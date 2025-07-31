using ENTiger.ENCollect.CommonModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DomainModels.Utilities;

public interface IFileTransferUtility
{
    Task<FileResult> DownloadFileDefaultPathAsync(string filename);
    Task<FileResult> DownloadFileAsync(string filename, string? path = null);

    Task<FileDto> SaveUploadFileAsync(IFormFile file);

    //FileDto SaveUploadFile(IFormFile file);

    //FileResult DownloadAttendancefilesAsZip(string fileName);

    FileResult DownloadFileAsZip(string filename, string? path = null);
    Task<FileResult> DownloadAttendanceFilesAsZipAsync(string fileName);
    Task RenameAndMoveFileAsync(string rootFolderPath, string destinationPath, string fileName);
}