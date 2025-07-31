using ENTiger.ENCollect.CommonModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.X509;
using Sumeru.Flex;
using System.Drawing;
using System.IO;
using System.IO.Abstractions;
using System.IO.Compression;

namespace ENTiger.ENCollect.DomainModels.Utilities;

public class FileTransferUtility : IFileTransferUtility
{
    private readonly ILogger<FileTransferUtility> _logger;

    private readonly IFileSystem _fileSystem;
    private readonly FilePathSettings _fileSettings;

    public FileTransferUtility(
        ILogger<FileTransferUtility> logger,
        IFileSystem fileSystem,
        IOptions<FilePathSettings> fileSettings)
    {
        _logger = logger;
        _fileSystem = fileSystem;
        _fileSettings = fileSettings.Value;
    }

    /// <summary>
    /// Asynchronously downloads a file from the default configured path.
    /// </summary>
    /// <param name="filename">The name of the file to download.</param>
    /// <returns>A <see cref="FileResult"/> containing the requested file's content and MIME type.</returns>
    /// <exception cref="ArgumentException">Thrown if the filename is invalid or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    public async Task<FileResult> DownloadFileDefaultPathAsync(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentException("Filename cannot be null or empty.", nameof(filename));

        var safeFilename = _fileSystem.Path.GetFileName(filename);
        if (string.IsNullOrEmpty(safeFilename))
            throw new ArgumentException("Invalid filename provided after sanitization.", nameof(filename));

        var baseDirectory = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
        var fullFilePath = _fileSystem.Path.Combine(baseDirectory, safeFilename);

        if (!_fileSystem.File.Exists(fullFilePath))
            throw new FileNotFoundException($"The file '{safeFilename}' was not found.", fullFilePath);

        await using var stream = _fileSystem.File.OpenRead(fullFilePath);
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var mimeType = MimeTypes.GetMimeType(fullFilePath);

        return new FileContentResult(memoryStream.ToArray(), mimeType)
        {
            FileDownloadName = safeFilename
        };
    }


    /// <summary>
    /// Asynchronously downloads a specified file from a given or default path.
    /// </summary>
    /// <param name="filename">The name of the file to download.</param>
    /// <param name="path">Optional base directory path. If null, uses default configured path.</param>
    /// <returns>A <see cref="FileResult"/> containing the file's content and MIME type.</returns>
    /// <exception cref="ArgumentException">Thrown if the filename is invalid.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if the resolved path is outside the allowed base directory.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    public async Task<FileResult> DownloadFileAsync(string filename, string? path = null)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentException("Filename must not be null or empty.", nameof(filename));

        var safeFilename = _fileSystem.Path.GetFileName(filename);
        if (string.IsNullOrEmpty(safeFilename))
            throw new ArgumentException("Invalid filename after sanitization.", nameof(filename));

        var defaultPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
        var basePath = path ?? defaultPath;

        // Get absolute full path
        var fullBasePath = _fileSystem.Path.GetFullPath(basePath);
        var fullFilePath = _fileSystem.Path.GetFullPath(_fileSystem.Path.Combine(fullBasePath, safeFilename));

        // Check that the full file path starts with the base directory (to prevent path traversal)
        if (!fullFilePath.StartsWith(fullBasePath, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Invalid file path access attempt detected.");

        if (!_fileSystem.File.Exists(fullFilePath))
            throw new FileNotFoundException($"The file '{safeFilename}' was not found.", fullFilePath);

        await using var fileStream = _fileSystem.File.OpenRead(fullFilePath);
        var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var mimeType = MimeTypes.GetMimeType(fullFilePath);

        return new FileContentResult(memoryStream.ToArray(), mimeType)
        {
            FileDownloadName = safeFilename
        };
    }

    /// <summary>
    /// Asynchronously saves an uploaded file to the configured file system and returns its details.
    /// </summary>
    /// <param name="file">The uploaded file to save.</param>
    /// <returns>A <see cref="FileDto"/> containing the saved file's metadata.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the input file is null.</exception>
    public async Task<FileDto> SaveUploadFileAsync(IFormFile file)
    {
        var trimmedFilename = file.FileName.Trim('"');
        var extension = _fileSystem.Path.GetExtension(trimmedFilename);
        var generatedFilename = GenerateFilename(trimmedFilename, extension);
        var filePath = BuildFilePath(generatedFilename);

        try
        {
            await SaveFileAsync(file, filePath,extension);
            _logger.LogInformation($"File '{generatedFilename}' uploaded successfully to '{filePath}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error while saving file '{generatedFilename}' to path '{filePath}'.");
            throw;
        }

        return CreateFileDto(generatedFilename, trimmedFilename.Length);
    }

    /// <summary>
    /// Generates a unique filename by appending a timestamp to the original name.
    /// </summary>
    /// <param name="sourceFilename">The original uploaded file name.</param>
    /// <returns>A new unique filename with timestamp and original extension.</returns>
    private string GenerateFilename(string sourceFilename, string extension)
    {
        var nameWithoutExtension = _fileSystem.Path.GetFileNameWithoutExtension(sourceFilename);
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        return $"{nameWithoutExtension}_{timestamp}{extension}";
    }

    /// <summary>
    /// Builds the full path where the uploaded file will be saved.
    /// </summary>
    /// <param name="filename">The generated filename.</param>
    /// <returns>The combined full path to save the file.</returns>
    private string BuildFilePath(string filename)
    {
        return _fileSystem.Path.Combine(
            _fileSettings.BasePath, _fileSettings.IncomingPath,
            filename);
    }

    /// <summary>
    /// Asynchronously saves the uploaded file to the specified file system path.
    /// </summary>
    /// <param name="file">The uploaded file.</param>
    /// <param name="path">The destination path where the file will be stored.</param>   
    private async Task SaveFileAsync(IFormFile file, string path, string extension)
    {
        if (extension == FileTypeEnum.JPG.Value || extension == FileTypeEnum.JPEG.Value
            || extension == FileTypeEnum.PNG.Value)
        {
            using var originalStream = file.OpenReadStream();
            using var originalImage = Image.FromStream(originalStream);

            using var cleanImage = new Bitmap(originalImage.Width, originalImage.Height);
            using (var graphics = Graphics.FromImage(cleanImage))
            {
                graphics.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
            }

            cleanImage.Save(path);
        }
        else
        {
            await using var stream = File.Create(path);
            await file.CopyToAsync(stream);
        }
    }


    /// <summary>
    /// Creates a <see cref="FileDto"/> object containing file metadata.
    /// </summary>
    /// <param name="filename">The saved file's name.</param>
    /// <param name="sourceFileNameLength">The length of the original file name (used to calculate size).</param>
    /// <returns>A populated <see cref="FileDto"/> with name, path, and size (in KB).</returns>
    private FileDto CreateFileDto(string filename, int sourceFileNameLength)
    {
        return new FileDto
        {
            Name = filename,
            Path = _fileSystem.Path.Combine(
            _fileSettings.BasePath, _fileSettings.IncomingPath),
            Size = sourceFileNameLength / 1024
        };
    }


    /// <summary>
    /// Asynchronously downloads the specified attendance file as a ZIP archive.
    /// </summary>
    /// <param name="fileName">The name of the file to zip and download.</param>
    /// <returns>A <see cref="FileResult"/> containing the zipped file content.</returns>
    /// <exception cref="ArgumentException">Thrown if the filename is invalid.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    public async Task<FileResult> DownloadAttendanceFilesAsZipAsync(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Filename must not be null or empty.", nameof(fileName));

        var safeFilename = _fileSystem.Path.GetFileName(fileName); // Sanitize filename
        if (string.IsNullOrEmpty(safeFilename))
            throw new ArgumentException("Invalid filename provided.", nameof(fileName));

        var destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TrailGapReportPath);
        var fullFilePath = _fileSystem.Path.Combine(destPath, safeFilename);

        if (!_fileSystem.File.Exists(fullFilePath))
            throw new FileNotFoundException($"The file '{safeFilename}' was not found.", fullFilePath);

        await using var memoryStream = new MemoryStream();
        using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            zipArchive.CreateEntryFromFile(fullFilePath, safeFilename);
        }

        memoryStream.Position = 0;
        var mimeType = "application/zip"; // Since we are sending a zip now

        return new FileContentResult(memoryStream.ToArray(), mimeType)
        {
            FileDownloadName = Path.GetFileNameWithoutExtension(safeFilename) + ".zip"
        };
    }


    /// <summary>
    /// Asynchronously renames a file by prefixing it with "Processed_" and moves it from the source directory to the destination directory.
    /// </summary>
    /// <param name="sourceDirectory">The directory where the original file is located.</param>
    /// <param name="destinationDirectory">The directory where the renamed file should be moved to.</param>
    /// <param name="fileName">The name of the file to be renamed and moved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// If the file does not exist at the source location, an error is logged and the method exits gracefully.
    /// If any exception occurs during the move operation, it is caught and logged.
    /// </remarks>
    /// <exception cref="IOException">Thrown internally by file operations if move fails due to IO reasons (logged but not rethrown).</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown internally if permissions are insufficient (logged but not rethrown).</exception>
    public async Task RenameAndMoveFileAsync(string sourceDirectory, string destinationDirectory, string fileName)
    {
        // Combine the source file path
        string sourceFilePath = _fileSystem.Path.Combine(sourceDirectory, fileName);

        // Check if the source file exists
        bool fileExists = _fileSystem.File.Exists(sourceFilePath);
        if (!fileExists)
        {
            _logger.LogError($"File '{fileName}' not found at the source path: '{sourceFilePath}'. Please verify the file exists before attempting to process it.");
            return; // Exit the method after logging the error
        }

        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }

        string destinationFilePath = _fileSystem.Path.Combine(destinationDirectory, fileName);

        try
        {
            // Move and rename the file (overwriting if exists)
            _fileSystem.File.Move(sourceFilePath, destinationFilePath, overwrite: true);

            _logger.LogInformation($"Successfully moved it from '{sourceFilePath}' to '{destinationFilePath}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while moving the file '{fileName}' from '{sourceFilePath}' to '{destinationFilePath}'. Error: {ex.Message}. Please check the source and destination paths, or try again.");
        }
    }


    public FileResult DownloadFileAsZip(string fileName,string? path = null)
    {
        // Ensure the provided path or use the default path if null
        path ??= _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

        // Build the full file path
        string filepath = _fileSystem.Path.Combine(path, fileName);

        // Get the filename and MIME type for the file
        string filename = _fileSystem.Path.GetFileName(filepath);
        string mimeType = "application/zip"; // MIME type for zip files is usually "application/zip"

        // Create a memory stream to hold the zip data
        using (var memoryStream = new MemoryStream())
        {
            // Create a ZipArchive in write mode
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                // Create the entry in the zip file from the original file
                var zipEntry = zipArchive.CreateEntry(Path.GetFileName(filepath));

                // Open the file to read its content
                using (var fileStream = _fileSystem.File.OpenRead(filepath))
                using (var entryStream = zipEntry.Open())
                {
                    // Copy the file contents to the zip entry stream
                    fileStream.CopyTo(entryStream);
                }
            }

            // Set the position of the memory stream back to the beginning before returning
            memoryStream.Position = 0;

            // Copy the memory stream to a new byte array to avoid disposal issues
            var memoryStreamArray = memoryStream.ToArray();

            string filenamewithoutextension = Path.GetExtension(filename).Replace(".csv","");
            // Return the file as a downloadable file with the correct MIME type
            return new FileContentResult(memoryStreamArray, mimeType)
            {
                FileDownloadName = filenamewithoutextension + ".zip" // Ensure the downloaded file has a .zip extension
            };
        }

    }


}