using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Text;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetGeoTagDetails : FlexiQueryEnumerableBridgeAsync<GeoTagDetails, GetGeoTagDetailsDto>
    {
        protected readonly ILogger<GetGeoTagDetails> _logger;
        protected GetGeoTagDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        string? applicationuserid;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetGeoTagDetails(ILogger<GetGeoTagDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetGeoTagDetails AssignParameters(GetGeoTagDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetGeoTagDetailsDto>> Fetch()
        {
            List<GetGeoTagDetailsDto> result = new List<GetGeoTagDetailsDto>();
            var geoTagDetails = await Build<GeoTagDetails>().OrderBy(a => a.CreatedDate).ToListAsync();

            if (geoTagDetails.Count() > 0)
            {
                var firstRecord = geoTagDetails.FirstOrDefault();

                geoTagDetails.Insert(0, firstRecord);

                for (int i = 0; i < geoTagDetails.Count; i++)
                {
                    if (i != (geoTagDetails.Count - 1))
                    {
                        var dist = await GetDistance(
                                        geoTagDetails[i].Latitude,
                                        geoTagDetails[i].Longitude,
                                        geoTagDetails[i + 1].Latitude,
                                        geoTagDetails[i + 1].Longitude);

                        GetGeoTagDetailsDto getGeoTagOutput = new GetGeoTagDetailsDto();
                        getGeoTagOutput.CreatedDate = geoTagDetails[i + 1].CreatedDate;
                        getGeoTagOutput.CreatedBy = geoTagDetails[i + 1].CreatedBy;
                        getGeoTagOutput.GeoTagReason = geoTagDetails[i + 1].GeoTagReason;
                        getGeoTagOutput.Latitude = geoTagDetails[i + 1].Latitude;
                        getGeoTagOutput.Longitude = geoTagDetails[i + 1].Longitude;
                        getGeoTagOutput.LocationName = geoTagDetails[i + 1].GeoTagReason;
                        getGeoTagOutput.TransactionType = geoTagDetails[i + 1].TransactionType;
                        getGeoTagOutput.Distance = Convert.ToString(Math.Round(dist, 2));
                        result.Add(getGeoTagOutput);
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            _flexAppContext = _params.GetAppContext();  //do not remove this line
            applicationuserid = _flexAppContext.UserId;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByGeoLoggedInUserId(_params.UserId)
                                        .ByTripDate(_params?.TripDate);

            //Build Your Query Here

            return query;
        }

        public async Task<double> GetDistance(double? lat1, double? lng1, double? lat2, double? lng2)
        {
            _logger.LogInformation("GetGeoTagDetails : GetDistance - Start | Point A : " + lat1 + "," + lng1 + " | Point B : " + lat2 + "," + lng2);
            try
            {
                double resultInKM = 0;
                string googleApiKey;

                googleApiKey = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(x => x.Parameter == "GoogleMapApiKey").Select(a => a.Value).FirstOrDefaultAsync();

                string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + lat1 + "," + lng1 + "&destinations=" + lat2 + "," + lng2 + "&key=" + googleApiKey;
                _logger.LogInformation("GetGeoTagDetails : GetDistance - Distance Url : " + url);
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);
                        if (dsResult.Tables["distance"] != null)
                        {
                            _logger.LogInformation("Get distance : distance - " + dsResult.Tables["distance"].Rows[0]["text"].ToString());
                            string distance = dsResult.Tables["distance"].Rows[0]["text"]?.ToString().Replace("km", "", StringComparison.OrdinalIgnoreCase) ?? string.Empty;

                            if (distance.Contains("m"))
                            {
                                distance = distance?.Replace("m", "", StringComparison.OrdinalIgnoreCase) ?? string.Empty;
                                resultInKM = Convert.ToDouble(distance) * 0.001;
                            }
                            else
                            {
                                resultInKM = Convert.ToDouble(distance);
                            }
                        }
                    }
                }
                return resultInKM;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetGeoTagDetails : GetDistance - " + ex + ex?.Message + ex?.StackTrace + ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace);
                throw ex;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetGeoTagDetailsParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid UserId")]
        public string? UserId { get; set; }
        public DateTime? TripDate { get; set; }
        public string? Zone { get; set; }
        public string? Region { get; set; }
    }
}