using Azure.Core.GeoJson;
using ENTiger.ENCollect.DomainModels.Enum;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Sumeru.Flex;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// Plugin responsible for generating Geo Canned Reports based on login, logout, and geo-tag data.
    /// </summary>
    public partial class GenerateGeoReportPlugin : FlexiPluginBase, IFlexiPlugin<GenerateGeoReportPostBusDataPacket>
    {
        /// <inheritdoc />
        public override string Id { get; set; } = "3a13679ad266b72ba9011db50136fcd6";

        /// <inheritdoc />
        public override string FriendlyName { get; set; } = nameof(GenerateGeoReportPlugin);

        private readonly ILogger<GenerateGeoReportPlugin> _logger;
        private readonly IFlexHost _flexHost;
        private readonly IGeoTagRepository _geoTagRepository;
        private readonly IApplicationUserQueryRepository _userQueryRepo;
        private readonly List<GeoCannedReportDetailsDto> _geoReportDetails = new();
        private readonly IDistanceCalculatorService _distanceCalculatorService;
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly GoogleSettings _googleOptions;
        private readonly IFileSystem _fileSystem;
        private readonly IDataTableUtility _dataTableUtility;
        private readonly FilePathSettings _fileSettings;
        private FlexAppContextBridge? _flexAppContext;
        private string _eventCondition = string.Empty;
        private string fileName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateGeoReportPlugin"/> class.
        /// </summary>
        public GenerateGeoReportPlugin(
            ILogger<GenerateGeoReportPlugin> logger,
            IFlexHost flexHost,
            IGeoTagRepository geoTagRepository,
            IApplicationUserQueryRepository userQueryRepo,
            IDistanceCalculatorService distanceCalculatorService,
            ICsvExcelUtility csvExcelUtility,
            IOptions<GoogleSettings> googleOptions,
            IDataTableUtility dataTableUtility,
            IFileSystem fileSystem,
            IOptions<FilePathSettings> fileSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _geoTagRepository = geoTagRepository;
            _userQueryRepo = userQueryRepo;
            _distanceCalculatorService = distanceCalculatorService;
            _csvExcelUtility = csvExcelUtility;
            _googleOptions = googleOptions.Value;
            _dataTableUtility = dataTableUtility;
            _fileSystem = fileSystem;
            _fileSettings = fileSettings.Value;
        }

        /// <summary>
        /// Executes the plugin by building, enriching, calculating distances, and exporting the geo report.
        /// </summary>
        public async Task Execute(GenerateGeoReportPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            var reportDate = packet.Cmd.Dto.reportDate;

            _logger.LogInformation("Starting Geo Report generation for report date: {ReportDate}", reportDate);

            try
            {
                var userLogins = await _userQueryRepo.GetFirstMobileLoginsOnDateAsync(_flexAppContext!, reportDate);
                var loginCount = userLogins?.Count ?? 0;
                _logger.LogInformation("Fetched {LoginCount} user login(s) for report date: {ReportDate}", loginCount, reportDate);

                if (loginCount == 0)
                {
                    _logger.LogWarning("No user logins found for {ReportDate}. Geo report generation skipped.", reportDate);
                    _eventCondition = CONDITION_ONFAILURE;
                    await Fire(_eventCondition, packet.FlexServiceBusContext);
                    return;
                }

                _logger.LogInformation("Building Geo Report...");
                await BuildGeoReportAsync(userLogins, reportDate);

                _logger.LogInformation("Enriching Geo Report with agency and allocation data...");
                await EnrichGeoReportWithAgencyAndAllocationDataAsync();

                _logger.LogInformation("Calculating distance for users...");
                await CalculateDistanceForUsersAsync();

                _logger.LogInformation("Finalizing and sending Geo Report...");
                await FinalizeReportAsync(packet);

                _logger.LogInformation("Geo Report generation completed successfully for {ReportDate}", reportDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Geo Report generation failed for report date: {ReportDate}", reportDate);
                _eventCondition = CONDITION_ONFAILURE;
                await Fire(_eventCondition, packet.FlexServiceBusContext);
            }
        }


        /// <summary>
        /// Builds the report by mapping login, geo-tag, and logout data.
        /// </summary>

        private async Task BuildGeoReportAsync(List<UserAttendanceLog>? userLogins, DateTime reportDate)
        {
            _logger.LogInformation("Starting BuildGeoReportAsync for report date: {ReportDate}", reportDate);

            if (userLogins == null || !userLogins.Any())
            {
                _logger.LogWarning("UserAttendanceLog list is null or empty. Skipping report generation.");
                return;
            }

            try
            {
                var userIds = userLogins
                    .Where(x => x?.ApplicationUser != null)
                    .Select(x => x.ApplicationUser.Id)
                    .Distinct()
                    .ToList();

                _logger.LogInformation("Total distinct users found in userLogins: {UserCount}", userIds?.Count ?? 0);

                var geoTagLookup = await GetGeoTagsGroupedByUserAsync(userIds, reportDate);
                if (geoTagLookup == null || !geoTagLookup.Any())
                {
                    _logger.LogWarning("GeoTag lookup is null or empty for report date: {ReportDate}", reportDate);
                }

                var logoutLookup = await GetUserLogoutsGroupedByUserAsync(userIds, reportDate);
                if (logoutLookup == null || !logoutLookup.Any())
                {
                    _logger.LogWarning("Logout lookup is null or empty for report date: {ReportDate}", reportDate);
                }

                _logger.LogInformation("Building per-user geo report...");
                BuildPerUserGeoReport(userLogins, geoTagLookup, logoutLookup);
                _logger.LogInformation("Completed BuildGeoReportAsync for report date: {ReportDate}", reportDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while building geo report for report date: {ReportDate}", reportDate);
            }
        }

        private async Task<Dictionary<string, List<GeoTagDetails>>> GetGeoTagsGroupedByUserAsync(List<string> userIds, DateTime reportDate)
        {
            _logger.LogInformation("Fetching GeoTags for {UserCount} users on date {ReportDate}", userIds?.Count ?? 0, reportDate);

            var geoTags = await _geoTagRepository.GetGeoTagsForUsersAsync(_flexAppContext!, userIds, reportDate);

            if (geoTags == null || !geoTags.Any())
            {
                _logger.LogWarning("No GeoTags found for the specified users on {ReportDate}", reportDate);
                return new Dictionary<string, List<GeoTagDetails>>();
            }

            var groupedTags = geoTags
                .GroupBy(x => x.ApplicationUserId)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(t => t.CreatedDate).ToList()
                );

            _logger.LogInformation("Grouped {GeoTagCount} GeoTags for {UserCount} users", geoTags.Count, groupedTags.Count);
            return groupedTags;

        }

        private async Task<Dictionary<string, UserAttendanceLog>> GetUserLogoutsGroupedByUserAsync(List<string> userIds, DateTime reportDate)
        {
            _logger.LogInformation("Fetching logout entries for {UserCount} users on {ReportDate}", userIds?.Count ?? 0, reportDate);

            var logoutEntries = await _userQueryRepo.GetLastLogoutTimesForUsersAsync(_flexAppContext!, userIds, reportDate);

            if (logoutEntries == null || !logoutEntries.Any())
            {
                _logger.LogWarning("No logout entries found for users on {ReportDate}", reportDate);
                return new Dictionary<string, UserAttendanceLog>();
            }

            var groupedLogouts = logoutEntries
                .Where(x => x?.ApplicationUser?.Id != null)
                .ToDictionary(x => x.ApplicationUser.Id, x => x);

            _logger.LogDebug("Grouped {Count} logout entries by user", groupedLogouts.Count);
            return groupedLogouts;

        }

        private void BuildPerUserGeoReport(
     List<UserAttendanceLog> userLogins,
     Dictionary<string, List<GeoTagDetails>> geoTagLookup,
     Dictionary<string, UserAttendanceLog> logoutLookup)
        {
            if (userLogins == null || !userLogins.Any())
            {
                _logger.LogWarning("No user login data provided to BuildPerUserGeoReport.");
                return;
            }

            _logger.LogInformation("Starting to build per-user geo report for {UserCount} users.", userLogins.Count);

            foreach (var login in userLogins)
            {
                if (login?.ApplicationUser?.Id == null)
                {
                    _logger.LogWarning("Skipping login entry due to missing ApplicationUser or Id.");
                    continue;
                }

                var userId = login.ApplicationUser.Id;
                var userRecords = new List<GeoCannedReportDetailsDto>();

                // Add login entry
                userRecords.Add(MapLoginToDto(login));

                // Add trail entries
                if (geoTagLookup.TryGetValue(userId, out var userGeoTags) && userGeoTags?.Any() == true)
                {
                    userRecords.AddRange(userGeoTags.Select(MapGeoTagToDto));
                }
                else
                {
                    _logger.LogDebug("No geo tag entries found for user {UserId}", userId);
                }

                // Add logout entry
                if (logoutLookup.TryGetValue(userId, out var logout) && logout != null)
                {
                    userRecords.Add(MapLogoutToDto(logout));
                }
                else
                {
                    _logger.LogDebug("No logout entry found for user {UserId}", userId);
                }

                _geoReportDetails.AddRange(userRecords);
                _logger.LogDebug("Added {RecordCount} geo report records for user {UserId}", userRecords.Count, userId);
            }

            _logger.LogInformation("Completed building per-user geo report. Total records: {TotalRecordCount}", _geoReportDetails.Count);
        }



        /// <summary>
        /// Enriches report with agency and allocation owner details.
        /// </summary>
        private async Task EnrichGeoReportWithAgencyAndAllocationDataAsync()
        {
            if (!_geoReportDetails.Any()) return;

            await AddAgencyInfoToReportAsync();
            await AddAllocationOwnerInfoToReportAsync();
        }

        private async Task AddAgencyInfoToReportAsync()
        {
            var userIds = _geoReportDetails
                .Where(r => !r.IsStaff && !string.IsNullOrEmpty(r.UserENCollectId))
                .Select(r => r.UserENCollectId!)
                .Distinct()
                .ToList();

            var agencyUsers = await _userQueryRepo.GetAgencyByUserIdsAsync(_flexAppContext!, userIds);
            if (agencyUsers == null) return;

            var agencyLookup = agencyUsers.ToDictionary(
                u => u.CustomId!,
                u => new
                {
                    AgencyId = u.Agency?.CustomId,
                    AgencyName = $"{u.Agency?.FirstName} {u.Agency?.MiddleName} {u.Agency?.LastName}".Trim()
                });

            foreach (var report in _geoReportDetails.Where(r => !r.IsStaff && r.UserENCollectId is not null))
            {
                if (agencyLookup.TryGetValue(report.UserENCollectId, out var agency))
                {
                    report.UsersAgencyId = agency.AgencyId;
                    report.UsersAgencyName = agency.AgencyName;
                }
            }
        }

        /// <summary>
        /// Maps AllocationOwnerId and AllocationOwnerName to each report item using the CustomId and Full Name of the owner.
        /// </summary>
        private async Task AddAllocationOwnerInfoToReportAsync()
        {
            var ownerIds = _geoReportDetails
                .Where(r => !string.IsNullOrWhiteSpace(r.AllocationOwnerId))
                .Select(r => r.AllocationOwnerId!)
                .Distinct()
                .ToList();

            var owners = await _userQueryRepo.GetUsersByIdsAsync(_flexAppContext!, ownerIds);

            // Lookup using the actual Id (assuming AllocationOwnerId matches actual UserId)
            var ownerLookup = owners.ToDictionary(
                o => o.Id, // assuming this is the unique user id (original AllocationOwnerId)
                o => new
                {
                    CustomId = o.CustomId,
                    FullName = $"{o.FirstName} {o.LastName}".Trim()
                });

            foreach (var report in _geoReportDetails.Where(r => !string.IsNullOrWhiteSpace(r.AllocationOwnerId)))
            {
                if (ownerLookup.TryGetValue(report.AllocationOwnerId!, out var ownerInfo))
                {
                    // Overwrite AllocationOwnerId with CustomId
                    report.AllocationOwnerId = ownerInfo.CustomId;

                    // Set owner full name
                    report.AllocationOwnerName = ownerInfo.FullName;
                }
            }
        }


        /// <summary>
        /// Calculates distance between each user's consecutive geo points.
        /// </summary>
        private async Task CalculateDistanceForUsersAsync()
        {
            var userWiseReports = _geoReportDetails
                .Where(r => !string.IsNullOrEmpty(r.UserENCollectId))
                .GroupBy(r => r.UserENCollectId);

            foreach (var group in userWiseReports)
            {
                var orderedTransactions = group
                    .OrderBy(r => r.CreatedDate)
                    .ThenBy(r => r.CreatedTime)
                    .Where(r => r.Latitude.HasValue && r.Longitude.HasValue)
                    .ToList();

                if (orderedTransactions.Count < 2)
                    continue;

                for (int i = 0; i < orderedTransactions.Count - 1; i++)
                {
                    var from = orderedTransactions[i];
                    var to = orderedTransactions[i + 1];

                    double distance = _googleOptions.Distance.UseGoogleMapsDistance
                        ? await _distanceCalculatorService.CalculateTotalDistanceInKmAsync(new List<(double, double)>
                          {
                              (from.Latitude!.Value, from.Longitude!.Value),
                              (to.Latitude!.Value, to.Longitude!.Value)
                          })
                        : _distanceCalculatorService.CalculateHaversineDistance(
                            new GeoPoint(from.Latitude!.Value, from.Longitude!.Value),
                            new GeoPoint(to.Latitude!.Value, to.Longitude!.Value));

                    to.DistanceKm = distance;
                }

                // Set distance of first entry as 0
                orderedTransactions[0].DistanceKm = 0;
            }
        }

        /// <summary>
        /// Final step to write report to zip file and trigger completion event.
        /// </summary>
        private async Task FinalizeReportAsync(GenerateGeoReportPostBusDataPacket packet)
        {
            var csvContent = _csvExcelUtility.ConvertListToCsv(_geoReportDetails);
            var outputPath = _fileSystem.Path.Combine(
                _fileSettings.BasePath,
                _fileSettings.IncomingPath,
                _fileSettings.TemporaryPath);

            if (!outputPath.EndsWith("\\"))
            {
                outputPath += "\\";
            }

            fileName = $"GeoReport_{DateTime.Now:yyyyMMdd}";
            await _csvExcelUtility.GenerateZipInMemoryAsync(fileName, outputPath, csvContent);

            _eventCondition = CONDITION_ONSUCCESS;
            await Fire(_eventCondition, packet.FlexServiceBusContext);
        }

        #region Mapping Methods

        private static GeoCannedReportDetailsDto MapLoginToDto(UserAttendanceLog log) => new()
        {
            UserENCollectId = log.ApplicationUser?.CustomId,
            UserName = $"{log.ApplicationUser?.FirstName} {log.ApplicationUser?.LastName}".Trim(),
            IsStaff = log.ApplicationUser is CompanyUser,
            TransactionType = TransactionTypeEnum.Login.Value,
            CreatedDate = log.LogInTime?.ToString("dd-MM-yyyy"),
            CreatedTime = log.LogInTime?.ToString("HH:mm:ss"),
            Latitude = log.LogOutLatitude,
            Longitude = log.LogOutLongitude,
            PhysicalAddress = log.GeoLocation
        };

        private static GeoCannedReportDetailsDto MapLogoutToDto(UserAttendanceLog log) => new()
        {
            UserENCollectId = log.ApplicationUser?.CustomId,
            UserName = $"{log.ApplicationUser?.FirstName} {log.ApplicationUser?.LastName}".Trim(),
            IsStaff = log.ApplicationUser is CompanyUser,
            TransactionType = TransactionTypeEnum.Logout.Value,
            CreatedDate = log.LogOutTime?.ToString("dd-MM-yyyy"),
            CreatedTime = log.LogOutTime?.ToString("HH:mm:ss"),
            Latitude = log.LogInLatitude,
            Longitude = log.LogInLongitude,
            PhysicalAddress = log.GeoLocation
        };

        private static GeoCannedReportDetailsDto MapGeoTagToDto(GeoTagDetails tag)
        {
            var user = tag.ApplicationUser;
            var account = tag.Account;

            return new GeoCannedReportDetailsDto
            {
                UserENCollectId = user?.CustomId,
                UserName = $"{user?.FirstName} {user?.MiddleName} {user?.LastName}".Trim(),
                IsStaff = user is CompanyUser,
                TransactionType = tag.TransactionType,
                CreatedDate = tag.CreatedDate.Date.ToString("dd-MM-yyyy"),
                CreatedTime = tag.CreatedDate.ToString("HH:mm:ss"),
                Latitude = tag.Latitude,
                Longitude = tag.Longitude,
                CustomerAddressZipcode = account?.MAILINGZIPCODE,
                AccountNumber = account?.AGREEMENTID,
                CustomerID = account?.CUSTOMERID,
                CustomerName = account?.CUSTOMERNAME,
                AllocationOwnerId = account?.AllocationOwnerId,
                PhysicalAddress = tag.GeoLocation
            };
        }

        #endregion
    }
}
