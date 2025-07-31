using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.DomainModels; // <-- File-scoped namespace (C# 10+)

/// <summary>
/// Provides methods for validating file names, file existence, and file upload/download constraints.
/// Uses IFileSystem for file I/O and FileValidationSettings for configuration.
/// </summary>
public class FileValidationUtility : IFileValidationUtility
{
    private readonly ILogger<FileValidationUtility> _logger = FlexContainer.ServiceProvider.GetService<ILogger<FileValidationUtility>>();
    private readonly IFileSystem _fileSystem;
    private readonly FileValidationSettings _settings;
    private readonly FilePathSettings _fileSettings;
    // Precompile the regex once (C# 9+ target-typed new allows a bit more brevity if you like).
    private static readonly Regex FileNameRegex = new(
        pattern: @"^[a-zA-Z0-9_ ]+\.[a-zA-Z]+$",
        options: RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    /// <summary>
    /// Constructs an instance of FileValidationUtility with injected logging, file system, and settings.
    /// </summary>
    public FileValidationUtility(
        ILogger<FileValidationUtility> logger,
        IFileSystem fileSystem,
        IOptions<FileValidationSettings> settings,
        IOptions<FilePathSettings> fileSettings)
    {
        _logger = logger;
        _fileSystem = fileSystem;
        _settings = settings.Value;
        _fileSettings = fileSettings.Value;
    }

    /// <summary>
    /// Validates that the given filename matches the expected pattern (alphanumeric, underscore, space, plus an extension).
    /// </summary>
    /// <param name="fileName">The filename to validate.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <returns>True if valid; otherwise false.</returns>
    public bool ValidateFileName(string fileName, out string message)
    {
        if (fileName is null)
            throw new ArgumentNullException(nameof(fileName));

        if (!FileNameRegex.IsMatch(fileName))
        {
            _logger.LogError(
                "ValidateFileName: Invalid filename '{FileName}'. Must not contain special characters.",
                fileName);

            message = "Filename should not contain special characters";
            return false;
        }

        message = string.Empty;
        return true;
    }

    /// <summary>
    /// Validates whether a file can be downloaded, checking for path presence, existence, and size constraints.
    /// </summary>
    /// <param name="fileName">The filename to validate.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <param name="path">Optional custom path to check. If null, uses _fileSettings.DefaultFolders.UploadFolder.</param>
    /// <returns>True if valid; otherwise false.</returns>
    public bool ValidateDownloadFile(string fileName, out string message, string? path = null)
    {
        path ??= _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

        var fullFilePath = _fileSystem.Path.Combine(path, fileName);        
          
        var maxSizeInBytes = _settings.MaxDownloadFileSizeMb * 1024 * 1024;

        var fileSize = _fileSystem.FileInfo.New(fullFilePath).Length;

        message = ValidateFilePath(fileName, path);

        if(!string.IsNullOrEmpty(message))
        {
            return false;
        }

        if (fileSize > maxSizeInBytes)
        {
            _logger.LogError(
                "ValidateDownloadFile: File '{FullFilePath}' size ({FileSize} bytes) exceeds max limit ({MaxSizeBytes} bytes).",
                fullFilePath,
                fileSize,
                maxSizeInBytes);

            message = $"File size is more than {_settings.MaxDownloadFileSizeMb}MB";
            return false;
        }

        message = string.Empty;
        return true;
    }

    /// <summary>
    /// Validates an uploaded file (e.g., from an HTTP request), checking for null file, default folder presence, and size constraints.
    /// </summary>
    /// <param name="file">The IFormFile representing the uploaded file.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <returns>True if valid; otherwise false.</returns>
    public bool ValidateUploadFile(IFormFile file, out string message)
    {
        var maxSizeInBytes = _settings.MaximumFileSizeMb * 1024 * 1024;

        if (file is null)
        {
            _logger.LogError(
                "ValidateUploadFile: file is null. Default folder is '{DefaultFolder}'.",
                _fileSettings.IncomingPath);

            message = "Invalid File";
            return false;
        }

        if (string.IsNullOrEmpty(_fileSettings.IncomingPath))
        {
            var attemptedFileName = file.FileName ?? "[unknown filename]";
            _logger.LogError(
                "ValidateUploadFile: DefaultFolder is null/empty. Cannot upload '{FileName}'.",
                attemptedFileName);

            message = "Internal Error, Please contact administrator";
            return false;
        }

        var fileExtension = _fileSystem.Path.GetExtension(file.FileName)?.ToLower();

        if (!GetValidFileExtensions().Contains(fileExtension))
        {
            _logger.LogError($"ValidateUploadFile: File extension not supported : '{fileExtension}'");
            message = "Invalid file";
            return false;
        }

        if (file.Length > maxSizeInBytes)
        {
            _logger.LogError(
                "ValidateUploadFile: File '{FileName}' size ({FileLength} bytes) exceeds maximum allowed size ({MaxSizeBytes} bytes).",
                file.FileName,
                file.Length,
                maxSizeInBytes);

            message = $"File size is more than {_settings.MaximumFileSizeMb}MB";
            return false;
        }

        message = string.Empty;
        return true;
    }

    /// <summary>
    /// Checks whether a file exists at the specified path and filename, using the injected file system abstraction.
    /// </summary>
    /// <param name="filePath">The directory or path to check.</param>
    /// <param name="fileName">The filename to combine with filePath.</param>
    /// <returns>True if the file exists; otherwise false.</returns>
    public bool CheckIfFileExists(string filePath, string fileName)
    {
        var fullPath = _fileSystem.Path.Combine(filePath, fileName);
        var fileExists = _fileSystem.File.Exists(fullPath);

        if (!fileExists)
        {
            // Possibly log at Debug or Warning if a missing file is not always an error
            _logger.LogDebug("CheckIfFileExists: File '{FullPath}' does not exist.", fullPath);
        }

        return fileExists;
    }

    private List<string> GetValidFileExtensions()
    {
        return new List<string>
        {
            FileTypeEnum.CSV.Value,
            FileTypeEnum.XLS.Value,
            FileTypeEnum.XLSX.Value,
            FileTypeEnum.PDF.Value,
            FileTypeEnum.DOC.Value,
            FileTypeEnum.DOCX.Value,
            FileTypeEnum.JPG.Value,
            FileTypeEnum.PNG.Value
        };
    }

    private string ValidateFilePath(string fileName, string path)
    {
        string message = string.Empty;
        if (string.IsNullOrEmpty(path))
        {
            _logger.LogError(
                "ValidateDownloadFile: path is null/empty. Cannot download file '{FileName}'. Settings default = '{DefaultFolder}'.",
                fileName,
                _fileSettings.IncomingPath);

            message = "Internal Error, Please contact administrator";
            return message;
        }

        var fullFilePath = _fileSystem.Path.Combine(path, fileName);

        if (!_fileSystem.File.Exists(fullFilePath))
        {
            _logger.LogError(
                "ValidateDownloadFile: File '{FullFilePath}' not found.",
                fullFilePath);

            message = "Invalid File";
            return message;
        }
        return message;

    }

    public bool ValidateReportDownloadFile(string fileName, out string message, string? path = null)
    {
        var fullFilePath = _fileSystem.Path.Combine(path, fileName);

        path ??= _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

        var maxSizeInBytes = _settings.MaxInsightReportDownloadFileSizeInMb * 1024 * 1024;

        var fileSize = _fileSystem.FileInfo.New(fullFilePath).Length;

        message = ValidateFilePath(fileName, path);

        if (!string.IsNullOrEmpty(message))
        {
            return false;
        }

        if (fileSize > maxSizeInBytes)
        {
            _logger.LogError(
                "ValidateDownloadFile: File '{FullFilePath}' size ({FileSize} bytes) exceeds max limit ({MaxSizeBytes} bytes).",
                fullFilePath,
                fileSize,
                maxSizeInBytes);

            message = $"File size is more than {_settings.MaxDownloadFileSizeMb}MB";
            return false;
        }

        message = string.Empty;
        return true;
    }

}