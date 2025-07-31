using Elastic.Clients.Elasticsearch;
using ENTiger.ENCollect.DomainModels.Enum;
using Sumeru.Flex;
using System.Device.Location;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class GetGeoTagDetailsExtensionMethods
    {
        public static IQueryable<T> ByGeoLoggedInUserId<T>(this IQueryable<T> geoTagDetails, string userId) where T : GeoTagDetails
        {
            if (!string.IsNullOrEmpty(userId))
            {
                geoTagDetails = geoTagDetails.Where(x => x.CreatedBy == userId);
            }
            return geoTagDetails;
        }

        public static IQueryable<T> ByTripDate<T>(this IQueryable<T> geoTagDetails, DateTime? tripDate) where T : GeoTagDetails
        {
            if (tripDate != null && tripDate != DateTime.MinValue)
            {
                DateTime startDate = tripDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                geoTagDetails = geoTagDetails.Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDate);
            }

            return geoTagDetails;
        }

        //public static IQueryable<T> ByDistance<T>(this IQueryable<T> geoSearchLocDetails, double latitude, double longitude, double radius) where T : GeoTagDetails
        //{
        //    GeoCoordinate coord = new GeoCoordinate(latitude, longitude);

        //    geoSearchLocDetails = geoSearchLocDetails.Where(a => coord.GetDistanceTo(new GeoCoordinate(a.Latitude, a.Longitude)) / 1000.00 <= radius);

        //    return geoSearchLocDetails;
        //}

        public static IQueryable<T> ByIncludeUserCurrentLocation<T>(this IQueryable<T> geoUserDetails) where T : ApplicationUser
        {
            geoUserDetails = geoUserDetails.FlexInclude(x => x.UserCurrentLocationDetails);
            return geoUserDetails;
        }

        public static IQueryable<T> ByGEOUserId<T>(this IQueryable<T> userDetails, string userId) where T : ApplicationUser
        {
            userDetails = userDetails.Where(x => x.Id == userId);
            return userDetails;
        }

        public static IQueryable<T> ByGEOMobilenumberId<T>(this IQueryable<T> userDetails, string mobileNumber) where T : ApplicationUser
        {
            if (!string.IsNullOrEmpty(mobileNumber))
            {
                userDetails = userDetails.Where(x => x.PrimaryMobileNumber == mobileNumber);
            }
            return userDetails;
        }

        public static IQueryable<T> ByGEOCustomId<T>(this IQueryable<T> userDetails, string customId) where T : ApplicationUser
        {
            if (!string.IsNullOrEmpty(customId))
            {
                userDetails = userDetails.Where(x => x.CustomId == customId);
            }
            return userDetails;
        }

        public static IQueryable<T> UserDetailByDistance<T>(this IQueryable<T> geoSearchLocDetails, double latitude, double longitude, double radius) where T : ApplicationUser
        {
            GeoCoordinate coord = new GeoCoordinate(latitude, longitude);

            geoSearchLocDetails = geoSearchLocDetails
                                    .FlexInclude(x => x.UserCurrentLocationDetails)
                                    .Where(a => coord.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(a.UserCurrentLocationDetails.Latitude), Convert.ToDouble(a.UserCurrentLocationDetails.Longitude))) / 1000.00 <= radius);

            return geoSearchLocDetails;
        }

        public static IQueryable<T> ByGeoTagDateRange<T>(this IQueryable<T> model, DateTime value1, DateTime value2) where T : GeoTagDetails
        {
            DateTime startDate = value1.Date;
            DateTime endDate = value2.Date.AddDays(1);

            return model.Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDate);
        }

        public static IQueryable<T> ByUsers<T>(this IQueryable<T> model, List<string> values) where T : GeoTagDetails
        {
            return model.Where(a => values.Contains(a.ApplicationUserId));
        }

        public static IQueryable<T> IncludeUser<T>(this IQueryable<T> model) where T : GeoTagDetails
        {
            return model.FlexInclude(x => x.ApplicationUser);
        }

        public static IQueryable<T> ByDate<T>(this IQueryable<T> model, DateTime value) where T : GeoTagDetails
        {
            DateTime startDate = value.Date;
            DateTime endDate = startDate.AddDays(1);
            return model.Where(x => x.CreatedDate >= startDate.ToDateOnlyOffset() && x.CreatedDate < endDate.ToDateOnlyOffset());
        }

        public static IQueryable<T> ByGeoTagUserId<T>(this IQueryable<T> model, string value) where T : GeoTagDetails
        {
            return model.Where(a => a.ApplicationUserId == value);
        }

        public static IQueryable<T> ByToday<T>(this IQueryable<T> model) where T : GeoTagDetails
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);

            return model.Where(a => a.CreatedDate >= startDate.ToDateOnlyOffset() && a.CreatedDate < endDate.ToDateOnlyOffset());
        }

        public static IQueryable<T> ByGeoTagUsers<T>(this IQueryable<T> model, List<string> values) where T : GeoTagDetails
        {
            return model.Where(a => a.ApplicationUserId != null && values.Contains(a.ApplicationUserId));
        }

        public static IQueryable<T> ByReceiptOrTrailTransactionType<T>(this IQueryable<T> model) where T : GeoTagDetails
        {
            return model.Where(a => a.TransactionType != null && 
                            (a.TransactionType == TransactionTypeEnum.Trail.Value || a.TransactionType == TransactionTypeEnum.Receipt.Value));
        }
        public static IQueryable<T> IsMobileTransaction<T>(this IQueryable<T> query) where T : GeoTagDetails
        {
            return query.Where(x => x.TransactionSource != null && x.TransactionSource == TransactionSourceEnum.Mobile.Value);
        }
    }
}