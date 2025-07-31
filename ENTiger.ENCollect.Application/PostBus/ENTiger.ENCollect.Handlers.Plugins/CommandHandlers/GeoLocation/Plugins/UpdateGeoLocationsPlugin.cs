using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateGeoLocationsPlugin : FlexiPluginBase, IFlexiPlugin<UpdateGeoLocationsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a177c14801ac917ced859ebbec9c9ec";
        public override string FriendlyName { get; set; } = "UpdateGeoLocationsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateGeoLocationsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected DateTime startDate;
        protected DateTime endDate;
        const int LatSetByWeb = 11;
        const int LongSetByWeb = 11;
        protected string _feedbackId { get; set; }
        protected string _feedbackLocation { get; set; }
        protected string _collectionId { get; set; }
        protected string _collectionLocation { get; set; }
        protected string _userAttendenceId { get; set; }
        protected string _userAttendenceLocation { get; set; }
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public UpdateGeoLocationsPlugin(ILogger<UpdateGeoLocationsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(UpdateGeoLocationsPostBusDataPacket packet)
        {
          
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _logger.LogDebug("GetCollectionDetailsAsync Tenantid :" + _flexAppContext.TenantId);
            _repoFactory.Init(packet.Cmd.Dto.GetAppContext());
            var configuration = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>();
            int GeoTrackerDateFlag = configuration.GetSection("AppSettings")["GeoTrackerDateFlag"] != null ? Convert.ToInt32(configuration.GetSection("AppSettings")["GeoTrackerDateFlag"]) : 1;

            _logger.LogDebug("GetCollectionDetailsAsync GeoTrackerDateFlag :" + GeoTrackerDateFlag);
            DateTime todayDate = DateTime.Now.Date;
            startDate = todayDate.AddDays(-GeoTrackerDateFlag);
            endDate = todayDate;

            // Collection Physical Address update
            _logger.LogDebug("UpdateGeoLocationsPlugin GetCollectionDetailsAsync Start");
            List<Collection> collections = await GetCollectionDetailsAsync();
            if (collections.Count > 0)
            {
                _logger.LogDebug("collections Count : " + collections?.Count());
                List<CoordinateDto> coordinates = GetUniqueCollectionLatLongs(collections);
                _logger.LogDebug("Unique coordinates Count : " + coordinates?.Count());
                List<CoordinateDto> PhysicalAddresses = await GetPhysicalAddressWithLatLongs(coordinates);
                foreach (var collection in collections)
                {
                    var locationName = PhysicalAddresses.Where(x => x.Latitude == Convert.ToDouble(collection.Latitude)
                                        && x.Longitude == Convert.ToDouble(collection.Longitude)).Select(x => x.LocationName).FirstOrDefault();
                    if (locationName != null)
                    {
                        EventCondition = "Oncollection";
                        _collectionId = collection.Id;
                        _collectionLocation = locationName;
                        await this.Fire(EventCondition, packet.FlexServiceBusContext);
                    }
                }               
            }
            _logger.LogDebug("UpdateGeoLocationsPlugin GetCollectionDetailsAsync END");

            // Feedback Physical Address update
            _logger.LogDebug("UpdateGeoLocationsPlugin Handle  :GetFeedbackDetailsAsync Start");
            List<Feedback> feedbacks = await GetFeedBacksAsync();
            if (feedbacks.Count > 0)
            {
                _logger.LogDebug("feedbacks Count : " + feedbacks?.Count());
                List<CoordinateDto> coordinates = GetUniqueFeebackLatLongs(feedbacks);
                _logger.LogDebug("Unique coordinates Count : " + coordinates?.Count());
                List<CoordinateDto> physicalAddresses = await GetPhysicalAddressWithLatLongs(coordinates);
                foreach (var feedback in feedbacks)
                {
                    var locationName = physicalAddresses.Where(x => x.Latitude == Convert.ToDouble(feedback.Latitude)
                                        && x.Longitude == Convert.ToDouble(feedback.Longitude)).Select(x => x.LocationName).FirstOrDefault();
                    if (locationName != null)
                    {
                        EventCondition = "Onfeedback";
                        _feedbackId = feedback.Id;
                        _feedbackLocation = locationName;
                        await this.Fire(EventCondition, packet.FlexServiceBusContext);                      
                    }
                }                
            }
            _logger.LogDebug("UpdateGeoLocationsPlugin Handle  :GetFeedbackDetailsAsync END");

            // UserAttendanceLog Physical Address update
            _logger.LogDebug("UpdateGeoLocationsPlugin Handle  :GetUserAttendanceLogDetailsAsync Start");
            List<UserAttendanceLog> userAttendanceLogs = await GetUserAttendanceLogAsync();
            if (userAttendanceLogs.Count > 0)
            {
                _logger.LogDebug("userAttendanceLogDetails Count : " + userAttendanceLogs?.Count());
                List<CoordinateDto> coordinates = GetUniqueUserAttendanceLatLongs(userAttendanceLogs);
                _logger.LogDebug("Unique coordinates Count : " + coordinates?.Count());
                List<CoordinateDto> physicalAddresses = await GetPhysicalAddressWithLatLongs(coordinates);
                foreach (var userAttendanceLogDetail in userAttendanceLogs)
                {
                    var locationName = physicalAddresses.Where(x => x.Latitude == Convert.ToDouble(userAttendanceLogDetail.LogInLatitude)
                                        && x.Longitude == Convert.ToDouble(userAttendanceLogDetail.LogInLongitude)).Select(x => x.LocationName).FirstOrDefault();
                    if (locationName != null)
                    {
                        EventCondition = "Onuser";
                        _userAttendenceId = userAttendanceLogDetail.Id;
                        _userAttendenceLocation = locationName;
                        await this.Fire(EventCondition, packet.FlexServiceBusContext);  
                    }
                }
                _logger.LogDebug("UpdateGeoLocationsPlugin ProcessUserAttendanceLogLatLongsAsync : End");
            }
            _logger.LogDebug("UpdateGeoLocationsPlugin Handle  :GetUserAttendanceLogDetailsAsync END");
            //Environment.Exit(0);
        }

        private async Task<List<Collection>> GetCollectionDetailsAsync()
        {
            try
            {
                DateTime endDateNextDate = endDate.AddDays(1);
                return await _repoFactory.GetRepo().FindAll<Collection>()
                     .Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDateNextDate
                     && string.IsNullOrEmpty(a.GeoLocation) && !string.IsNullOrEmpty(a.Longitude)
                     && !string.IsNullOrEmpty(a.Latitude)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Fetching Collection details fetch failed  for {$startDate} to {$endDate}");
                throw;
            }
        }
        private async Task<List<Feedback>> GetFeedBacksAsync()
        {
            try
            {
                DateTime endDateNextDate = endDate.AddDays(1);
                return await _repoFactory.GetRepo().FindAll<Feedback>().FlexInclude(z => z.Account)
                    .Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDateNextDate
                        && string.IsNullOrEmpty(a.GeoLocation) && a.Longitude != null
                        && a.Latitude != null).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Fetching Feedback details fetch failed  for {$startDate} to {$endDate}");
                throw;
            }
        }
        private async Task<List<UserAttendanceLog>> GetUserAttendanceLogAsync()
        {
            try
            {
                DateTime endDateNextDate = endDate.AddDays(1);
                return await _repoFactory.GetRepo().FindAll<UserAttendanceLog>()
                     .Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDateNextDate && a.IsFirstLogin == true
                     && string.IsNullOrEmpty(a.GeoLocation)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Fetching UserAttendanceLog details fetch failed  for {$startDate} to {$endDate}");
                throw;
            }
        }
        private List<CoordinateDto> GetUniqueCollectionLatLongs(List<Collection> collections)
        {
            try
            {
                var uniqueCollectionLatLongs = collections?.Where(z => z.Latitude != null
                                                     && z.Latitude != LongSetByWeb.ToString()
                                                     && z.Longitude != null
                                                     && z.Longitude != LongSetByWeb.ToString())
                                          .Select(y => new CoordinateDto
                                          {
                                              Latitude = Convert.ToDouble(y.Latitude),
                                              Longitude = Convert.ToDouble(y.Longitude)
                                          })
                                          .GroupBy(c => new { c.Latitude, c.Longitude })
                                          .Select(g => g.First())
                                          .ToList();
                _logger.LogDebug("Unique coordinates Count : " + uniqueCollectionLatLongs?.Count());
                return uniqueCollectionLatLongs;
            }
            catch(Exception ex)
            {
                _logger.LogError("GetUniqueCollectionLatLongs details fetch failed  for {$startDate} to {$endDate}");
                throw;
            }
        }

        private List<CoordinateDto> GetUniqueFeebackLatLongs(List<Feedback> feedbacks)
        {
            var feedbacksList = feedbacks?.Where(z => z.Latitude != null
                                                  && z.Latitude != LatSetByWeb
                                                  && z.Longitude != null
                                                  && z.Longitude != LongSetByWeb)
                                       .Select(y => new CoordinateDto
                                       {
                                           Latitude = Convert.ToDouble(y.Latitude),
                                           Longitude = Convert.ToDouble(y.Longitude)
                                       })
                                       .GroupBy(c => new { c.Latitude, c.Longitude })
                                       .Select(g => g.First())
                                       .ToList();
            _logger.LogDebug("Unique coordinates Count : " + feedbacksList?.Count());
            return feedbacksList;
        }

        private List<CoordinateDto> GetUniqueUserAttendanceLatLongs(List<UserAttendanceLog> userAttendanceLogs)
        {
            var userAttendanceLogsList= userAttendanceLogs.Where(z => z.LogInLatitude != null
                                                       && z.LogInLatitude != LatSetByWeb
                                                       && z.LogInLongitude != null
                                                       && z.LogInLongitude != LongSetByWeb)
                                            .Select(y => new CoordinateDto
                                            {
                                                Latitude = Convert.ToDouble(y.LogInLatitude),
                                                Longitude = Convert.ToDouble(y.LogInLongitude)
                                            })
                                            .GroupBy(c => new { c.Latitude, c.Longitude })
                                            .Select(g => g.First())
                                            .ToList();
            _logger.LogDebug("Unique coordinates Count : " + userAttendanceLogsList?.Count());
            return userAttendanceLogsList;
        }
        private async Task<List<CoordinateDto>> GetPhysicalAddressWithLatLongs(List<CoordinateDto> coordinates)
        {
            List<CoordinateDto> PhysicalAddresses = new List<CoordinateDto>();
            int batchSize = 10;
            int iterations = (int)Math.Ceiling((double)coordinates?.Count / batchSize);
            _logger.LogDebug("BindLatitudeAndLongitudeAsync iterations Count : " + iterations);
            for (int i = 0; i < iterations; i++)
            {
                _logger.LogDebug("BindLatitudeAndLongitudeAsync iterations GetDistanceAsync Start :{$i} ");
                HandleRapidAPIRateLimit();
                var currentBatch = coordinates.Skip(i * batchSize).Take(batchSize).ToList();
                PhysicalAddresses.AddRange(await GetPhysicalAddressesAsync(currentBatch));
            }
            return PhysicalAddresses;
        }
        private void HandleRapidAPIRateLimit()
        {
            Task.Delay(1000);
        }

        private async Task<List<CoordinateDto>> GetPhysicalAddressesAsync(List<CoordinateDto> coordinates)
        {
            _logger.LogDebug("GetPhysicalAddressesAsync: - Start");
            List<CoordinateDto> geoLocations = new List<CoordinateDto>();
            var geolocationData = new Dictionary<CoordinateDto, dynamic>();
            string responseContent = string.Empty;
            HttpResponseMessage response = await CallThirdPartyApiAsync(coordinates);
            responseContent = await response?.Content?.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(responseContent) && !responseContent.Trim().Contains("INVALID_REQUEST") && !responseContent.Trim().Contains("REQUEST_DENIED"))
                {
                    if (!responseContent.Trim().Contains("A005") && !responseContent.Trim().Contains("A002") && !responseContent.Trim().Contains("MT940003"))
                    {
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        var rows = jsonResponse["rows"];
                        for (int i = 0; i < rows.Count; i++)
                        {
                            var elements = rows[i]["elements"];
                            var coordinate = coordinates[i];
                            for (int j = 0; j < elements.Count; j++)
                            {
                                geolocationData[coordinate] = elements[j];
                                geolocationData[coordinate]["destination_addresses"] = jsonResponse["destination_addresses"][i];
                            }
                        }
                        foreach (var coordinate in geolocationData.Keys)
                        {
                            CoordinateDto location = new CoordinateDto();
                            location.Latitude = coordinate.Latitude;
                            location.Longitude = coordinate.Longitude;
                            string locationName = geolocationData[coordinate]["destination_addresses"];
                            string physicalAddress1 = Regex.Replace(locationName, @"[^0-9a-zA-Z\/:,]+", "  ");
                            string physicalAddress = physicalAddress1.Replace(",", " ");
                            location.LocationName = physicalAddress;
                            geoLocations.Add(location);
                        }
                        _logger.LogDebug("\\*********************jsonResponse binding Closed From Third Party Api*********************\\");
                    }
                }
                else if (responseContent.Trim().Contains("INVALID_REQUEST") || responseContent.Trim().Contains("REQUEST_DENIED"))
                {
                    _logger.LogDebug("\\*********************ResponseContent  REQUEST_DENIED From Third Party Api*********************");
                }
            }
            else
            {
                _logger.LogDebug("ResponseContent Failed ");
            }
            _logger.LogDebug("GetPhysicalAddressesAsync: - END");
            return geoLocations;
        }

        private async Task<HttpResponseMessage> CallThirdPartyApiAsync(List<CoordinateDto> coordinates)
        {
            GeoTrackerConfigDto configData = BindConfigData();
            var baseUri = new Uri(configData.DBSGoogleMapApiUrl);
            string querystring = string.Join("|", coordinates.Select(coord => $"{coord.Latitude},{coord.Longitude}"));
            var uri = new Uri(baseUri, $"?origins={querystring}&destinations={querystring}&key={configData.GoogleMapApiKey}");
            string inputRequest = uri.ToString();
            string baseUrl = baseUri.ToString();

            using (var handler = new HttpClientHandler())
            {
                //Comman need to add
                if (configData.GeoCertificateFlag == "yes" && !string.IsNullOrEmpty(configData.GeoTrackerDetailsCertifcationPath))
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    X509Certificate2 cert = new X509Certificate2(configData.GeoTrackerDetailsCertifcationPath);
                    handler.ClientCertificates.Add(cert);
                }
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.DefaultRequestHeaders.Add("X-DBS-ORG_ID", configData.DBSORGIDGoogleMapApi);
                    httpClient.DefaultRequestHeaders.Add("x-api-key", configData.XApiKeyGoogleMapApi);
                    try
                    {
                        var response = await httpClient.GetAsync(uri);
                        // Ensure the response indicates success
                        if (!response.IsSuccessStatusCode)
                        {
                            switch (response.StatusCode)
                            {
                                case System.Net.HttpStatusCode.NotFound:
                                    throw new Exception("Resource not found (404).");
                                case System.Net.HttpStatusCode.Unauthorized:
                                    throw new UnauthorizedAccessException("Unauthorized access (401).");
                                case System.Net.HttpStatusCode.Forbidden:
                                    throw new UnauthorizedAccessException("Forbidden access (403).");
                                case System.Net.HttpStatusCode.BadRequest:
                                    throw new Exception("Bad request (400). Check the request parameters.");
                                default:
                                    throw new Exception($"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}");
                            }
                        }
                        // If successful, return the response
                        return response;
                    }
                    catch (HttpRequestException httpRequestEx)
                    {
                        Console.WriteLine($"HTTP Request Error: {httpRequestEx.Message}");
                        throw;
                    }
                    catch (TaskCanceledException taskCanceledEx)
                    {
                        Console.WriteLine($"Request Timeout or Cancellation: {taskCanceledEx.Message}");
                        throw new System.TimeoutException("The request timed out. Please try again later.", taskCanceledEx);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }
        private GeoTrackerConfigDto BindConfigData()
        {
            _logger.LogDebug("DBSGeoTrackerConsole  :BindConfigData Start");
            var configuration = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>();
            GeoTrackerConfigDto config = new GeoTrackerConfigDto();
            var settings = configuration.GetSection("AppSettings");
            config.GoogleMapApiKey = settings["GoogleMapApiKey"] ?? throw new Exception("GoogleMapApiKey missing in configuration");
            config.DBSGoogleMapApiUrl = settings["DBSGoogleMapApiUrl"] ?? throw new Exception("DBSGoogleMapApiUrl missing in configuration");
            config.DBSORGIDGoogleMapApi = settings["DBSORGIDGoogleMapApi"] ?? throw new Exception("DBSGoogleMapApiUrl missing in configuration");
            config.XApiKeyGoogleMapApi = settings["xapikeyGoogleMapApi"] ?? throw new Exception("xapikeyGoogleMapApi missing in configuration");
            config.GeoTrackerDetailsCertifcationPath = settings["GeoTrackerDetailsCertifcationPath"] ?? throw new Exception("GeoTrackerDetailsCertifcationPath missing in configuration");
            config.GeoCertificateFlag = settings["GeoCertificateFlag"] ?? throw new Exception("GeoCertificateFlag missing in configuration");
            config.GeoTrackerLogFilePath = settings["GeoTrackerLogFilePath"] ?? throw new Exception("GeoTrackerLogFilePath missing in configuration");
            _logger.LogDebug("DBSGeoTrackerConsole  :BindConfigData END");
            return config;
        }
    }
}