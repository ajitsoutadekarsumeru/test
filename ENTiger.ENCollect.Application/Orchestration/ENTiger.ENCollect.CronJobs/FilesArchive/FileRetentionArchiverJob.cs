using Cronos;
using ENTiger.ENCollect.AccountsModule;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.IO.Compression;

namespace ENTiger.ENCollect.CronJobs.FilesArchive
{
    /// <summary>
    /// Background cron job to archive files based on retention settings.
    /// </summary>
    public class FileArchiverJob : BackgroundService, IFlexCronJob
    {
        private readonly ILogger<FileArchiverJob> _logger;
        private readonly IFlexHost _flexHost;
        private readonly CronExpression _cron;
        private readonly CronJobSettings _cronSettings;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;

        public FileArchiverJob(
            ILogger<FileArchiverJob> logger,
            ProcessAccountsService processAccountsService,
            IOptions<CronJobSettings> cronSettings,
            IFlexHost flexHost,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _cronSettings = cronSettings.Value;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;

            if (!string.IsNullOrWhiteSpace(_cronSettings.ArchiveScheduleCron))
            {
                _cron = CronExpression.Parse(_cronSettings.ArchiveScheduleCron);
            }
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var now = DateTimeOffset.Now;
                    var nextUtc = _cron.GetNextOccurrence(now, TimeZoneInfo.Local);

                    if (!nextUtc.HasValue)
                    {
                        _logger.LogError("Next occurrence is null. Check cron configuration.");
                        break;
                    }

                    var delay = nextUtc.Value - now;
                    _logger.LogInformation($"Next execution scheduled at {nextUtc.Value}");

                    await Task.Delay(delay, stoppingToken);

                    using var cts = new CancellationTokenSource();
                    try
                    {
                        var success = await TaskWithTimeoutAndException(ArchiveExpiredFilesAsync(cts.Token), TimeSpan.FromSeconds(_cronSettings.CronTimeout));
                        if (success)
                        {
                            _logger.LogInformation("File archiving process completed successfully.");
                            CreateZipFromBackupFolder();
                        }
                    }
                    catch (TimeoutException)
                    {
                        cts.Cancel();
                        _logger.LogError("File archiving process timed out.");
                    }
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning("File archiving job has been cancelled.");
            }
        }

        /// <summary>
        /// Orchestrates the archiving process for expired files based on retention policy.
        /// </summary>
        /// <param name="token">Cancellation token to handle job cancellation.</param>
        /// <returns>Returns true after processing all folder pairs.</returns>
        private async Task<bool> ArchiveExpiredFilesAsync(CancellationToken token)
        {
            foreach (var (mainFolder, childFolder) in GetAllFolderPairs())
            {
                await ArchiveFilesInFolderPairAsync(mainFolder, childFolder, token);
            }

            return true;
        }

        /// <summary>
        /// Retrieves all valid combinations of main and child folders to be checked for file retention.
        /// </summary>
        /// <returns>Collection of main-child folder path pairs.</returns>
        private IEnumerable<(string mainFolder, string childFolder)> GetAllFolderPairs()
        {
            var mainFolders = new[]
            {
               _fileSettings.UnAllocationProcessedFilePath,
               _fileSettings.AllocationProcessedFilePath,
               _fileSettings.UserProcessedFilePath,
               _fileSettings.BulkTrailProcessedFilePath,
               _fileSettings.BulkCollectionProcessedFilePath
            };

            var childFolders = new[]
            {
              _fileSettings.SuccessProcessedFilePath,
              _fileSettings.FailedProcessedFilePath,
              _fileSettings.PartialProcessedFilePath,
              _fileSettings.InvalidProcessedFilePath
            };

            foreach (var main in mainFolders)
            {
                foreach (var child in childFolders)
                {
                    yield return (main, child);

                }
            }
        }

        /// <summary>
        /// Archives expired files within a specific combination of main and child folders.
        /// </summary>
        /// <param name="mainFolder">Name of the main folder (e.g., AllocationProcessed).</param>
        /// <param name="childFolder">Name of the child folder (e.g., Success, Failed).</param>
        /// <param name="token">Cancellation token for graceful shutdown support.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task ArchiveFilesInFolderPairAsync(string mainFolder, string childFolder, CancellationToken token)
        {
            var sourceFolderPath = _fileSystem.Path.Combine(
                _fileSettings.BasePath,
                _fileSettings.IncomingPath,
                mainFolder,
                childFolder);

            if (!_fileSystem.Directory.Exists(sourceFolderPath))
            {
                _logger.LogWarning($"Source folder does not exist: {sourceFolderPath}");
                return;
            }

            await Task.Run(() => MoveFilesPastRetentionPeriod(sourceFolderPath, mainFolder, childFolder), token);
        }

        /// <summary>
        /// Moves all files from the source folder to the backup location if they exceed the configured retention period.
        /// </summary>
        /// <param name="sourceFolderPath">Full path to the source folder.</param>
        /// <param name="mainFolder">Main folder name for backup folder structure.</param>
        /// <param name="childFolder">Child folder name for backup folder structure.</param>
        private void MoveFilesPastRetentionPeriod(string sourceFolderPath, string mainFolder, string childFolder)
        {
            var files = _fileSystem.Directory.GetFiles(sourceFolderPath);

            foreach (var file in files)
            {
                var creationTime = _fileSystem.File.GetLastWriteTime(file);
                if (IsFileCrossRetentionPeriod(creationTime))
                {
                    var fileName = _fileSystem.Path.GetFileName(file);
                    var destinationFolder = _fileSystem.Path.Combine(
                       _fileSettings.BasePath,
                        _fileSettings.BackupFilePath);

                    MoveFileToBackup(sourceFolderPath, destinationFolder, fileName);
                }
            }
        }

        /// <summary>
        /// Determines whether the file exceeds the retention period based on its creation time.
        /// </summary>
        /// <param name="creationTime">The creation date of the file.</param>
        /// <returns>True if the file should be moved; otherwise, false.</returns>
        private bool IsFileCrossRetentionPeriod(DateTime creationTime)
        {
            return (DateTime.Today - creationTime).TotalDays >= _cronSettings.FileRetentionDays;
        }

        /// <summary>
        /// Moves a file to the designated backup directory and logs the operation.
        /// </summary>
        /// <param name="sourcePath">Full path of the file to move.</param>
        /// <param name="destinationFolder">Destination folder path under the backup structure.</param>
        /// <param name="fileName">Name of the file to be moved.</param>
        private void MoveFileToBackup(string sourceFolder, string destinationFolder, string fileName)
        {
            _fileSystem.Directory.CreateDirectory(destinationFolder);
            var sourcePath = _fileSystem.Path.Combine(sourceFolder, fileName);
            var destinationPath = _fileSystem.Path.Combine(destinationFolder, fileName);
            _fileSystem.File.Move(sourcePath, destinationPath, overwrite: true);
            _logger.LogInformation($"Moved: {fileName} → {destinationFolder}");
        }


        #region Timeout Helpers

        private static async Task<T> DelayedTimeoutExceptionTask<T>(TimeSpan delay)
        {
            await Task.Delay(delay);
            throw new TimeoutException();
        }

        private static async Task<T> TaskWithTimeoutAndException<T>(Task<T> task, TimeSpan timeout)
        {
            return await await Task.WhenAny(task, DelayedTimeoutExceptionTask<T>(timeout));
        }
        /// <summary>
        /// Creates a ZIP archive from all non-ZIP files in the backup folder, including all subdirectories.
        /// </summary>
        /// <remarks>
        /// - The archive is created in the same backup folder with a timestamped name.
        /// - Files ending in .zip are excluded to avoid redundant archiving.
        /// - the original files are deleted after successful zipping.
        /// </remarks>
        private void CreateZipFromBackupFolder()
        {
            try
            {
                var backupRoot = Path.Combine(_fileSettings.BasePath, _fileSettings.BackupFilePath);

                // Ensure the backup directory exists
                if (!_fileSystem.Directory.Exists(backupRoot))
                {
                    _logger.LogWarning("Backup folder does not exist: {BackupRoot}", backupRoot);
                    return;
                }

                // Enumerate all non-zip files recursively
                var allFiles = _fileSystem.Directory
                    .EnumerateFiles(backupRoot, "*", SearchOption.AllDirectories)
                    .Where(file => !file.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!allFiles.Any())
                {
                    _logger.LogInformation("No non-zip files found in backup folder to archive.");
                    return;
                }

                // Create the zip file with timestamp in the name
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var zipFileName = $"Backup_{timestamp}.zip";
                var zipFilePath = _fileSystem.Path.Combine(backupRoot, zipFileName);

                // Create and write entries to the zip archive
                using var zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);

                foreach (var fullPath in allFiles)
                {
                    var relativePath = Path.GetRelativePath(backupRoot, fullPath);
                    zip.CreateEntryFromFile(fullPath, relativePath, CompressionLevel.Optimal);
                }

                // Optionally delete the original files after successful archiving
                foreach (var file in allFiles)
                {
                    _fileSystem.File.Delete(file);
                }
                _logger.LogInformation("Deleted original files after zipping.");


                _logger.LogInformation("Created archive: {ZipFile} with {FileCount} files.", zipFilePath, allFiles.Count);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create zip archive from backup folder.");
            }
        }


        #endregion
    }
}
