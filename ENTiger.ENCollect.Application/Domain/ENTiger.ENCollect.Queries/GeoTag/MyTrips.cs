using System.Data;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public class MyTrips : FlexiQueryEnumerableBridgeAsync<GeoTagDetails, MyTripsDto>
    {
        protected readonly ILogger<MyTrips> _logger;
        protected MyTripsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        string googleApiKey;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public MyTrips(ILogger<MyTrips> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual MyTrips AssignParameters(MyTripsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<MyTripsDto>> Fetch()
        {
            List<MyTripsDto> result = new List<MyTripsDto>();

            var geoTagDetails = await Build<GeoTagDetails>().OrderBy(a => a.CreatedDate).ToListAsync();
            if (geoTagDetails.Count() > 0)
            {
                googleApiKey = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(x => x.Parameter == "GoogleMapApiKey")
                                        .Select(a => a.Value).FirstOrDefaultAsync();
                var firstRecord = geoTagDetails.FirstOrDefault();

                geoTagDetails.Insert(0, firstRecord);

                for (int i = 0; i < geoTagDetails.Count; i++)
                {
                    if (i != (geoTagDetails.Count - 1))
                    {
                        var dist = GetDistance(
                                        geoTagDetails[i].Latitude,
                                        geoTagDetails[i].Longitude,
                                        geoTagDetails[i + 1].Latitude,
                                        geoTagDetails[i + 1].Longitude);

                        MyTripsDto getGeoTagOutput = new MyTripsDto();
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
            string userid = _flexAppContext.UserId;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByGeoLoggedInUserId(_params?.UserId)
                                    .ByTripDate(_params?.TripDate);

            //Build Your Query Here

            return query;
        }

        public double GetDistance(double? lat1, double? lng1, double? lat2, double? lng2)
        {
            _logger.LogInformation("GetGeoTagDetails : GetDistance - Start | Point A : " + lat1 + "," + lng1 + " | Point B : " + lat2 + "," + lng2);
            try
            {
                double resultInKM = 0;

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
    public class MyTripsParams : DtoBridge
    {
        public string? UserId { get; set; }
        public DateTime TripDate { get; set; }
    }
}