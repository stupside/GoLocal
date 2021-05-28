using System;
using GoLocal.Shared.Locate.Models.Search;

namespace GoLocal.Shared.Locate.Helpers
{
    public static class CoordinatesHelper
    {
        private static readonly int EarthRadius = 6371;
        
        public static bool IsInside(this Feature feature, double longitude, double latitude, double distance)
            => DistanceBetween(feature.Latitude, feature.Longitude, latitude, longitude) <= distance;

        public static double DistanceBetween(double lat1, double lon1, double lat2, double lon2) {
            
            var dLat = DegreesToRadians(lat2-lat1);
            var dLon = DegreesToRadians(lon2-lon1);
            
            var a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Sin(dLon/2) * Math.Sin(dLon/2) * Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)); 
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a)); 
            return EarthRadius * c;
        }

        private static double DegreesToRadians(double degrees) {
            return degrees * Math.PI / 180;
        }
    }
}