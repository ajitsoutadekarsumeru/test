using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTravelReportDetails : FlexiQueryEnumerableBridgeAsync<GeoTagDetails, GetTravelReportDetailsDto>
    {
        protected readonly ILogger<GetTravelReportDetails> _logger;
        protected GetTravelReportDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetTravelReportDetails(ILogger<GetTravelReportDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTravelReportDetails AssignParameters(GetTravelReportDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTravelReportDetailsDto>> Fetch()
        {
            List<GetTravelReportDetailsDto> result = new List<GetTravelReportDetailsDto>();
            var geoTagTravelReportDetails = await Build<GeoTagDetails>().ToListAsync();

            double dist = 0.0;
            GetTravelReportDetailsDto getGeoTagTravelReportOutput = null;
            if (geoTagTravelReportDetails.Count() > 0)
            {
                var firstRecord = geoTagTravelReportDetails.FirstOrDefault();
                geoTagTravelReportDetails.Insert(0, firstRecord);
                for (int i = 0; i < geoTagTravelReportDetails.Count; i++)
                {
                    if (i != (geoTagTravelReportDetails.Count - 1))
                    {
                        dist = GetDistance(
                                    geoTagTravelReportDetails[i].Latitude,
                                    geoTagTravelReportDetails[i].Longitude,
                                    geoTagTravelReportDetails[i + 1].Latitude,
                                    geoTagTravelReportDetails[i + 1].Longitude);

                        getGeoTagTravelReportOutput = null;
                        getGeoTagTravelReportOutput = new GetTravelReportDetailsDto();
                        getGeoTagTravelReportOutput.CreatedDate = geoTagTravelReportDetails[i + 1].CreatedDate.DateTime;
                        getGeoTagTravelReportOutput.Latitude = geoTagTravelReportDetails[i + 1].Latitude;
                        getGeoTagTravelReportOutput.Longitude = geoTagTravelReportDetails[i + 1].Longitude;
                        getGeoTagTravelReportOutput.TransactionType = geoTagTravelReportDetails[i + 1].TransactionType;
                        getGeoTagTravelReportOutput.AgentName = geoTagTravelReportDetails[i + 1].ApplicationUser?.FirstName + " " + geoTagTravelReportDetails[i + 1].ApplicationUser?.LastName;
                        getGeoTagTravelReportOutput.Distance = Convert.ToString(Math.Round(dist, 2));
                        result.Add(getGeoTagTravelReportOutput);
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .FlexInclude(x => x.ApplicationUser)
                                    .ByGeoLoggedInUserId(_params.UserId)
                                    .ByTripDate(_params.TripDate);

            //Build Your Query Here

            return query;
        }

        #region Calculate Distance

        public double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            try
            {
                var coordinate1 = new GeoCoordinate(lat1, lng1);
                var coordinate2 = new GeoCoordinate(lat2, lng2);

                var resultInMeter = coordinate1.GetDistanceTo(coordinate2); //in meters

                //convert to KM : 1 meter = 0.001 KM
                var resultInKM = resultInMeter * 0.001; //in KM's

                return resultInKM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Calculate Distance
    }

    /// <summary>
    ///
    /// </summary>
    public class GetTravelReportDetailsParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid UserId")]
        public string? UserId { get; set; }

        public DateTime? TripDate { get; set; }
    }
}