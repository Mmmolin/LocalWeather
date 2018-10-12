using LocalWeather.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalWeather.Business
{
    public class LocationData
    {
        private List<Location> _locations;

        public LocationData()
        {
            _locations = new List<Location>();
        }

        public void SetSwedishLocations(List<Location> locations)
        {
            var SwedishLocations = new List<Location>();
            if(!IsNullOrEmpty(locations))
            {
                foreach (var location in locations)
                {
                    var splitLocation = location.Display_Name.Split(",");
                    var country = splitLocation[splitLocation.Length - 1].Trim();
                    if (country == "Sverige")
                    {
                        SwedishLocations.Add(location);
                    }
                }
            }
            this._locations = SwedishLocations;
        }

        public List<Location> GetSwedishLocations()
        {
            return _locations;
        }

        private bool IsNullOrEmpty(List<Location> locations)
        {
            return (locations == null || locations.Count == 0);
        }
    }
}
