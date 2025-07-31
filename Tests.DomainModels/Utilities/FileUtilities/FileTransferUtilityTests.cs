using ENTiger.ENCollect;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace ENCollect.Tests.DomainModels.Utilities
{
    public class FileTransferUtilityTests
    {
        private readonly Mock<ILogger<FileTransferUtility>> _mockLogger;
        private IFileSystem _fileSystem;
        private IOptions<FilePathSettings> _settingsOptions;

        public FileTransferUtilityTests()
        {
            _mockLogger = new Mock<ILogger<FileTransferUtility>>();

            // Default FileTransferSettings; can be overridden in specific tests.
            _settingsOptions = Options.Create(new FilePathSettings
            {
                IncomingPath = Path.Combine(Path.GetTempPath(), "upload")
            });
        }

        #region DownloadFileDefaultPath Tests

        [Fact]
        public void DownloadFileDefaultPath_WhenFilenameIsNull_ThrowsArgumentException()
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => utility.DownloadFileDefaultPath(null));
        }

        [Fact]
        public void DownloadFileDefaultPath_WhenFileDoesNotExist_ThrowsFileNotFound()
        {
            // Arrange
            _fileSystem = new MockFileSystem(); // empty => no files
            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
                utility.DownloadFileDefaultPath("missingfile.txt"));
        }

        [Fact]
        public void DownloadFileDefaultPath_WhenFileExists_ReturnsFileContentResult()
        {
            // Arrange
            var baseDir = _fileSystem.Path.Combine(_settingsOptions.Value.BasePath, _settingsOptions.Value.IncomingPath);
            var fileName = "existing.txt";
            var fullPath = Path.Combine(baseDir, fileName);

            var mockFiles = new Dictionary<string, MockFileData>
            {
                { fullPath, new MockFileData("File content") }
            };

            _fileSystem = new MockFileSystem(mockFiles);

            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // Act
            var result = utility.DownloadFileDefaultPath(fileName);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal(fileName, fileResult.FileDownloadName);
            Assert.Equal("File content", System.Text.Encoding.UTF8.GetString(fileResult.FileContents));
        }

        #endregion DownloadFileDefaultPath Tests

        #region DownloadFile Tests

        [Fact]
        public void DownloadFile_WhenFilenameIsNull_ThrowsArgumentException()
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => utility.DownloadFile(null));
        }

        [Fact]
        public void DownloadFile_WhenCustomPathIsInvalid_ThrowsUnauthorizedAccess()
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // A path that tries to backtrack outside the expected directory
            var invalidPath = Path.Combine(Path.GetTempPath(), "outer", "..", "hacky");

            Assert.Throws<UnauthorizedAccessException>(() =>
                utility.DownloadFile("myfile.txt", invalidPath));
        }

        [Fact]
        public void DownloadFile_WhenFileExists_ReturnsFileContentResult()
        {
            // Arrange
            var customBase = Path.Combine(Path.GetTempPath(), "CustomDownload");
            var fileName = "validFile.txt";
            var fullPath = Path.Combine(customBase, fileName);

            var mockFiles = new Dictionary<string, MockFileData>
            {
                { fullPath, new MockFileData("Hello World") }
            };
            _fileSystem = new MockFileSystem(mockFiles);

            // Overriding the default DownloadPath for this test
            var customSettings = new FilePathSettings
            {
                IncomingPath = Path.Combine(Path.GetTempPath(), "upload")
            };
            var options = Options.Create(customSettings);

            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, options);

            // Act
            var result = utility.DownloadFile(fileName, customBase);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal(fileName, fileResult.FileDownloadName);
            Assert.Equal("Hello World", System.Text.Encoding.UTF8.GetString(fileResult.FileContents));
        }

        #endregion DownloadFile Tests

        #region SaveUploadFile Tests

        [Fact]
        public void SaveUploadFile_WhenFileIsNull_ThrowsException()
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => utility.SaveUploadFile(null));
        }

        [Fact]
        public void SaveUploadFile_WhenValidFile_SavesFileAndReturnsDto()
        {
            // Arrange
            var baseUploadDir = _fileSystem.Path.Combine(_settingsOptions.Value.BasePath, _settingsOptions.Value.IncomingPath);
            var mockFiles = new Dictionary<string, MockFileData>();
            _fileSystem = new MockFileSystem(mockFiles);

            // Ensure the directory exists in the mock FS
            _fileSystem.Directory.CreateDirectory(baseUploadDir); var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, _settingsOptions);

            // Mock an IFormFile
            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.Setup(f => f.FileName).Returns("testupload.txt");
            mockFormFile.Setup(f => f.CopyTo(It.IsAny<Stream>()))
                        .Callback<Stream>(stream =>
                        {
                            // Write some content to the stream
                            var content = System.Text.Encoding.UTF8.GetBytes("Content of uploaded file");
                            stream.Write(content, 0, content.Length);
                        });

            // Act
            var fileDto = utility.SaveUploadFile(mockFormFile.Object);

            // Assert
            Assert.NotNull(fileDto);
            Assert.Contains("testupload_", fileDto.Name);
            Assert.Equal(fileDto.Name, fileDto.Path);

            var pathInMockFs = _fileSystem.Path.Combine(baseUploadDir, fileDto.Name);
            Assert.True(_fileSystem.File.Exists(pathInMockFs));

            var writtenContent = _fileSystem.File.ReadAllBytes(pathInMockFs);
            Assert.Equal("Content of uploaded file", System.Text.Encoding.UTF8.GetString(writtenContent));
        }

        #endregion SaveUploadFile Tests

        #region DownloadAttendancefilesAsZip Tests
        [Fact]
        public void DownloadAttendancefilesAsZip_WhenFileMissing_ThrowsFileNotFoundException()
        {
            var baseDir = Path.Combine(Path.GetTempPath(), "DownloadFolder");
            _fileSystem = new MockFileSystem(); // no files

            var customSettings = new FilePathSettings
            {
                IncomingPath = Path.Combine(Path.GetTempPath(), "upload")
            };
            var customOptions = Options.Create(customSettings);

            var utility = new FileTransferUtility(_mockLogger.Object, _fileSystem, customOptions);

            Assert.Throws<FileNotFoundException>(() =>
                utility.DownloadAttendancefilesAsZip("missing.jpg"));
        }

        #endregion DownloadAttendancefilesAsZip Tests
    }
}