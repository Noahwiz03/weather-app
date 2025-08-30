using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using weather_app.Model;

namespace weather_app.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private WeatherModel? _weatherData;
        
        public WeatherModel? WeatherData
        {
            get => _weatherData;
            set 
            {
                if (SetField(ref _weatherData, value))
                {
                    OnPropertyChanged(nameof(DayProgressPercentage));
                    OnPropertyChanged(nameof(FormattedSunrise));
                    OnPropertyChanged(nameof(FormattedSunset));
                    OnPropertyChanged(nameof(Visbility));
                }
            }
        }

        public double Visbility
        {
            get
            {
                if(WeatherData?.Visibility == null)
                {
                    return 0;
                }
                double visibilityM = WeatherData.Visibility;
                double Vis = visibilityM / 1609.34;
                double rounded = Math.Round(Vis, 2);
                return rounded;

            }
           
        }
        public double DayProgressPercentage
        {
            get
            {
                if (WeatherData?.Sys == null) return 0;
                
                var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var sunrise = WeatherData.Sys.Sunrise;
                var sunset = WeatherData.Sys.Sunset;
                
                if (now < sunrise) return 0;
                if (now > sunset) return 100;
                
                var totalDay = sunset - sunrise;
                var elapsed = now - sunrise;
                return (elapsed / (double)totalDay) * 100;
            }
        }

        public string FormattedSunrise
        {
            get
            {
                if (WeatherData?.Sys?.Sunrise == null) return "Sunrise: N/A";
                long unixTime = WeatherData.Sys.Sunrise;
                int TimeZoneOffset = WeatherData.Timezone;
                DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeSeconds(unixTime);

                TimeSpan offset = TimeSpan.FromSeconds(TimeZoneOffset);
                DateTimeOffset localTime = utcTime.ToOffset(offset);

                var sunriseTime = localTime.ToString("hh:mm tt");
                return $"Sunrise: {sunriseTime}";
            }
        }

        public string FormattedSunset
        {
            get
            {
                if (WeatherData?.Sys?.Sunset == null) return "Sunset: N/A";
                long unixTime = WeatherData.Sys.Sunset;
                int TimeZoneOffset = WeatherData.Timezone;
                DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeSeconds(unixTime);

                TimeSpan offset = TimeSpan.FromSeconds(TimeZoneOffset);
                DateTimeOffset localTime = utcTime.ToOffset(offset);

                var sunsetTime = localTime.ToString("hh:mm tt");
                return $"Sunset: {sunsetTime}";
            }
        }
    }
}
