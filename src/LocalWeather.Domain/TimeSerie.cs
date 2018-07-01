using System;
using System.Collections.Generic;
using System.Text;

namespace LocalWeather.Domain
{
    public class TimeSerie
    {
        public DateTime ValidTime { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
