using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;

namespace WeatherAPIApp.Models
{
    public class GetLocation
    {

        public static double lat;
        public static double lon;
        public static void GetLocationProp()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

            GeoCoordinate coord = watcher.Position.Location;

            if (coord.IsUnknown != true)
            {
                lat = coord.Latitude;
                lon = coord.Longitude;
            }
            else
            {
               /* lat = 59.7596;
                lon = 18.7014;*/

                /*lat = 59.3293;
                lon = 18.0686;*/
                //Chalmers location
                 lat = 57.688988;
                 lon = 11.974486;
            }
        }
    }
}