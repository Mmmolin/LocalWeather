using System;
using System.Collections.Generic;
using System.Text;

namespace LocalWeather.Domain
{
    public class Location
    {        
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Display_Name { get; set; }
        public string Type { get; set; }
    }
}
