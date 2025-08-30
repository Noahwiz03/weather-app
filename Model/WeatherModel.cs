using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_app.Model
{
    public class WeatherModel
    {
        public string? Name { get; set; }
        public Coord? Coord { get; set; }
        public List<WeatherCondition>? Weather { get; set; }
        public TemperatureInfo? Main { get; set; }
        public WindInfo? Wind { get; set; }
        public SysInfo? Sys { get; set; }
        public int Timezone { get; set; }     // Offset in seconds from UTC
        public int Dt { get; set; }           // Time of data calculation (Unix)
        public int Visibility { get; set; }   // Visibility in meters
    }

    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class WeatherCondition
    {
        public int Id { get; set; }
        public string Main { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }

    public class TemperatureInfo
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
        public int Deg { get; set; }      // Wind direction
        public double Gust { get; set; }  // Optional
    }

    public class SysInfo
    {
        public string Country { get; set; } = string.Empty;
        public long Sunrise { get; set; } // Unix timestamp
        public long Sunset { get; set; }
    }
}
