using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingMapsRESTToolkit;
using BingMapsRESTToolkit.Extensions;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace BingMap
{
    public class BuildMapLocations
    {
        public List<Location> locations { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BuildMapLocations()
        {
            this.locations = new List<Location>();
        }


        /// <summary>
        /// Gets the Geocode request for the specified location.
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="location"></param>
        /// <returns>GeocodeRequest</returns>
        public GeocodeRequest GetGeocode(string sessionKey, string location)
        {
            var requestGeocode = new GeocodeRequest()
            {
                Query = location,
                IncludeIso2 = true,
                MaxResults = 25,
                BingMapsKey = sessionKey
            };

            return requestGeocode;
        }

        /// <summary>
        /// Converts a Geocode to a location.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Location GetLocation(Response response)
        {
            var location = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;
            var latitude = location.GeocodePoints[0].Coordinates[0];
            var longitude = location.GeocodePoints[0].Coordinates[1];
            var center = new Location(latitude, longitude);
            locations.Add(center);
            return center;
        }

        /// <summary>
        /// Build rectangle with list of locations.
        /// </summary>
        /// <returns></returns>
        public LocationRect BuildRectangle()
        {
            LocationRect locationRect = new LocationRect(locations);
            return locationRect;
        }

        public void EmptyLists()
        {
            locations.Clear();
        }

    }
}
