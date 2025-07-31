using ENTiger.ENCollect.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using ENTiger.ENCollect;

namespace ENCollect.Tests.DomainModels
{
    public class FileValidationUtilityTests
    {
        private readonly Mock<ILogger<FileValidationUtility>> _mockLogger;
        private IFileSystem _fileSystem;
        private IOptions<FileValidationSettings> _settingsOptions;
        private readonly IOptions<FilePathSettings> _fileSettings;
        public FileValidationUtilityTests()
        {
            _mockLogger = new Mock<ILogger<FileValidationUtility>>();

            _fileSettings = Options.Create(new FilePathSettings
            {
                BasePath = Path.GetTempPath(),
                IncomingPath = "Uploads",
                TrailGapReportPath = "TrailGapReports",
                TemporaryPath = "Temp",
                UnAllocationProcessedFilePath = "UnAllocated",
                AllocationProcessedFilePath = "Allocated",
                UserProcessedFilePath = "UserFiles",
                BulkTrailProcessedFilePath = "BulkTrails"

            });
            // Default config, can be overridden per test.
            _settingsOptions = Options.Create(new FileValidationSettings
            {
                // e.g., "DefaultFolder" for downloads, "MaximumFileSizeMb" for uploads, etc.
                DefaultFolder = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest"),
                MaxDownloadFileSizeMb = 5,
                MaximumFileSizeMb = 2
            });
        }

        #region ValidateFileName Tests

        [Fact]
        public void ValidateFileName_WhenNull_ThrowsArgumentNullException()
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, _settingsOptions, _fileSettings);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                utility.ValidateFileName(null, out _));
        }

        [Theory]
        [InlineData("myFile.exe")]          // Valid extension
        [InlineData("Report_2023.xlsx")]    // Valid pattern
        [InlineData("My File.txt")]         // Space is allowed
        public void ValidateFileName_WhenValid_ReturnsTrueEmptyMessage(string fileName)
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, _settingsOptions, _fileSettings);

            // Act
            var isValid = utility.ValidateFileName(fileName, out var message);

            // Assert
            Assert.True(isValid);
            Assert.Equal(string.Empty, message);
        }

        [Theory]
        [InlineData("invalid@.txt")]
        [InlineData("NoExtension")]
        [InlineData("")]
        public void ValidateFileName_WhenInvalid_ReturnsFalseWithMessage(string fileName)
        {
            // Arrange
            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, _settingsOptions, _fileSettings);

            // Act
            var isValid = utility.ValidateFileName(fileName, out var message);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(message);
        }

        #endregion ValidateFileName Tests

        #region ValidateDownloadFile Tests

        [Fact]
        public void ValidateDownloadFile_WhenPathIsEmpty_ReturnsFalse()
        {
            // Arrange
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = string.Empty,
                MaxDownloadFileSizeMb = 5
            };
            var options = Options.Create(customSettings);

            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            // Act
            var result = utility.ValidateDownloadFile("someFile.txt", out var message);

            // Assert
            Assert.False(result);
            Assert.Contains("Internal Error", message);
        }

        [Fact]
        public void ValidateDownloadFile_WhenFileDoesNotExist_ReturnsFalseWithMessage()
        {
            // Arrange
            var baseDir = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest");
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = baseDir,
                MaxDownloadFileSizeMb = 5
            };
            var options = Options.Create(customSettings);

            _fileSystem = new MockFileSystem(); // no files
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            // Act
            var result = utility.ValidateDownloadFile("missing.txt", out var message);

            // Assert
            Assert.False(result);
            Assert.Equal("Invalid File", message);
        }

        [Fact]
        public void ValidateDownloadFile_WhenFileExistsButTooLarge_ReturnsFalse()
        {
            // Arrange
            var baseDir = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest");
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = baseDir,
                MaxDownloadFileSizeMb = 1 // 1 MB
            };
            var options = Options.Create(customSettings);

            // Create a file of size ~2 MB
            var hugeFileData = new MockFileData(new byte[2 * 1024 * 1024]); // 2 MB
            var fileName = "huge.bin";
            var filePath = Path.Combine(baseDir, fileName);

            var mockFiles = new Dictionary<string, MockFileData>
            {
                { filePath, hugeFileData }
            };

            _fileSystem = new MockFileSystem(mockFiles);
            _fileSystem.Directory.CreateDirectory(baseDir);

            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            // Act
            var result = utility.ValidateDownloadFile(fileName, out var message);

            // Assert
            Assert.False(result);
            Assert.Contains("File size is more than 1MB", message);
        }

        [Fact]
        public void ValidateDownloadFile_WhenFileIsWithinSize_ReturnsTrue()
        {
            // Arrange
            var baseDir = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest");
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = baseDir,
                MaxDownloadFileSizeMb = 5 // 5 MB
            };
            var options = Options.Create(customSettings);

            var mockFiles = new Dictionary<string, MockFileData>
            {
                { Path.Combine(baseDir, "okFile.bin"), new MockFileData(new byte[1024 * 1024]) } // 1 MB
            };
            _fileSystem = new MockFileSystem(mockFiles);
            _fileSystem.Directory.CreateDirectory(baseDir);

            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            // Act
            var result = utility.ValidateDownloadFile("okFile.bin", out var message);

            // Assert
            Assert.True(result);
            Assert.Empty(message);
        }

        #endregion ValidateDownloadFile Tests

        #region ValidateUploadFile Tests

        [Fact]
        public void ValidateUploadFile_WhenFileIsNull_ReturnsFalseWithMessage()
        {
            // Arrange
            var baseDir = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest");
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = baseDir,
                MaximumFileSizeMb = 2
            };
            var options = Options.Create(customSettings);

            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            // Act
            var result = utility.ValidateUploadFile(null, out var message);

            // Assert
            Assert.False(result);
            Assert.Equal("Invalid File", message);
        }

        [Fact]
        public void ValidateUploadFile_WhenDefaultFolderIsEmpty_ReturnsFalse()
        {
            // Arrange
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = string.Empty,
                MaximumFileSizeMb = 2
            };
            var options = Options.Create(customSettings);

            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            var mockFormFile = new Mock<IFormFile>();

            // Act
            var result = utility.ValidateUploadFile(mockFormFile.Object, out var message);

            // Assert
            Assert.False(result);
            Assert.Contains("Internal Error", message);
        }

        [Fact]
        public void ValidateUploadFile_WhenFileExceedsMaxSize_ReturnsFalse()
        {
            // Arrange
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest"),
                MaximumFileSizeMb = 1 // 1 MB
            };
            var options = Options.Create(customSettings);

            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            var largeFormFile = new Mock<IFormFile>();
            largeFormFile.Setup(f => f.Length).Returns(2L * 1024 * 1024); // 2 MB

            // Act
            var result = utility.ValidateUploadFile(largeFormFile.Object, out var message);

            // Assert
            Assert.False(result);
            Assert.Contains("File size is more than 1MB", message);
        }

        [Fact]
        public void ValidateUploadFile_WhenValid_ReturnsTrueNoMessage()
        {
            // Arrange
            var customSettings = new FileValidationSettings
            {
                DefaultFolder = Path.Combine(Path.GetTempPath(), "ValidateUtilityTest"),
                MaximumFileSizeMb = 2
            };
            var options = Options.Create(customSettings);

            _fileSystem = new MockFileSystem();
            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, options, _fileSettings);

            var formFile = new Mock<IFormFile>();
            formFile.Setup(f => f.Length).Returns(1024 * 1024); // 1 MB
            formFile.Setup(f => f.FileName).Returns("upload.bin");

            // Act
            var result = utility.ValidateUploadFile(formFile.Object, out var message);

            // Assert
            Assert.True(result);
            Assert.Empty(message);
        }

        #endregion ValidateUploadFile Tests

        #region CheckIfFileExists Tests

        [Theory]
        [InlineData("testFolder", "fileExists.txt", true)]
        [InlineData("testFolder", "missing.txt", false)]
        public void CheckIfFileExists_VariousScenarios_ReturnsExpected(
            string dirName,
            string filename,
            bool expectedExists)
        {
            // Arrange
            var baseDir = Path.Combine(Path.GetTempPath(), dirName);
            var fullPath = Path.Combine(baseDir, filename);

            var mockFiles = new Dictionary<string, MockFileData>();
            if (expectedExists)
            {
                // create the file content
                mockFiles[fullPath] = new MockFileData("some data");
            }

            _fileSystem = new MockFileSystem(mockFiles);
            _fileSystem.Directory.CreateDirectory(baseDir);

            var utility = new FileValidationUtility(_mockLogger.Object, _fileSystem, _settingsOptions, _fileSettings);

            // Act
            var result = utility.CheckIfFileExists(baseDir, filename);

            // Assert
            Assert.Equal(expectedExists, result);
        }

        #endregion CheckIfFileExists Tests
    }
}