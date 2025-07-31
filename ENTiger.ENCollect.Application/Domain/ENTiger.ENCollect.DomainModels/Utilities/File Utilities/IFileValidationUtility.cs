using Microsoft.AspNetCore.Http;

namespace ENTiger.ENCollect.DomainModels;

public interface IFileValidationUtility
{
    /// <summary>
    /// Validates that the given filename matches the expected pattern (alphanumeric, underscore, space, plus an extension).
    /// </summary>
    /// <param name="fileName">The filename to validate.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <returns>True if valid; otherwise false.</returns>
    bool ValidateFileName(string fileName, out string message);

    /// <summary>
    /// Validates whether a file can be downloaded, checking for path presence, existence, and size constraints.
    /// </summary>
    /// <param name="fileName">The filename to validate.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <param name="path">Optional custom path to check. If null, uses _settings.DefaultFolder.</param>
    /// <returns>True if valid; otherwise false.</returns>
    bool ValidateDownloadFile(string fileName, out string message, string? path = null);

    /// <summary>
    /// Validates an uploaded file (e.g., from an HTTP request), checking for null file, default folder presence, and size constraints.
    /// </summary>
    /// <param name="file">The IFormFile representing the uploaded file.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <returns>True if valid; otherwise false.</returns>
    bool ValidateUploadFile(IFormFile file, out string message);

    /// <summary>
    /// Checks whether a file exists at the specified path and filename, using the injected file system abstraction.
    /// </summary>
    /// <param name="filePath">The directory or path to check.</param>
    /// <param name="fileName">The filename to combine with filePath.</param>
    /// <returns>True if the file exists; otherwise false.</returns>
    bool CheckIfFileExists(string filePath, string fileName);

    /// <summary>
    /// Validates whether a insight report file can be downloaded, checking for path presence, existence, and size constraints.
    /// </summary>
    /// <param name="fileName">The filename to validate.</param>
    /// <param name="message">An error message if invalid; otherwise empty.</param>
    /// <param name="path">Optional custom path to check. If null, uses _settings.DefaultFolder.</param>
    /// <returns>True if valid; otherwise false.</returns>
    bool ValidateReportDownloadFile(string fileName, out string message, string? path = null);


}