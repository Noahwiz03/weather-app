using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using weather_app.Commands;
using weather_app.Model;

namespace weather_app.ViewModel
{
       public class WeatherViewModel : BaseViewModel
    {
        private string? cityName;
        private WeatherModel? weatherData;
        private bool isLoading;
        private static readonly HttpClient httpClient = new HttpClient();

        public string? CityName
        {
            get => cityName;
            set 
            {
                if (SetField(ref cityName, value))
                {
                    
                    GetWeatherCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public WeatherModel? WeatherData
        {
            get => weatherData;
            set
            {
                if(SetField(ref weatherData, value))
                {
                    
                }
            }
           
        }

        
        public bool IsLoading
        {
            get => isLoading;
            set => SetField(ref isLoading, value);
        }


        private BitmapImage? _weatherIcon;
        public BitmapImage? WeatherIcon
        {
            get => _weatherIcon;
            set => SetField(ref _weatherIcon, value);
        }

        private string? _weatherMain;
        public string? WeatherMain
        {
            get => _weatherMain;
            set => SetField(ref _weatherMain, value);
        }
        private string? _weatherDesc;
        public string? WeatherDesc
        {
            get => _weatherDesc;
            set => SetField(ref _weatherDesc, value);
        }

        private double? _visibility;
        public double? Visibility
        {
            get => _visibility;
            set => SetField(ref _visibility, value);
        }





        public AsyncRelayCommand GetWeatherCommand { get; }
        public RelayCommand CloseApp { get; }
        public WeatherViewModel()
        {
            GetWeatherCommand = new AsyncRelayCommand(
                async (parameter) =>
                {
                    try
                    {
                        await GetWeather();
                    }
                    catch (Exception ex)
                    {
                        // Handle/log exception, show message to user, etc.
                        Console.WriteLine($"Error in command: {ex.Message}");
                        System.Windows.MessageBox.Show($"Error: {ex.Message}");
                    }
                },
                (parameter) => !string.IsNullOrWhiteSpace(CityName)
            );

            CloseApp = new RelayCommand(parameter => Close(), parameter =>true);
        }

        private void Close()
        {
            Application.Current.Shutdown();
        }
        private async Task GetWeather()
        {
            if (string.IsNullOrWhiteSpace(CityName))
            {
                
                return;
            }
            try
            {
                IsLoading = true;
                string apiKey = "your api key here"; 
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={CityName}&appid={apiKey}&units=imperial";
                Console.WriteLine($"Fetching weather for: {CityName}");
                Console.WriteLine($"URL: {url}");
                
                
                    var response = await httpClient.GetStringAsync(url);
                    Console.WriteLine($"API Response: {response}");
                    WeatherData = JsonConvert.DeserializeObject<WeatherModel>(response);
                    var iconCode = WeatherData?.Weather?.FirstOrDefault()?.Icon;
                    WeatherMain = WeatherData?.Weather?.FirstOrDefault()?.Main;
                    WeatherDesc = WeatherData?.Weather?.FirstOrDefault()?.Description;
                   
                if (!string.IsNullOrWhiteSpace(iconCode))
                {
                    try
                    {
                        var uri = new Uri($"your uri path for icons", UriKind.Absolute);
                        WeatherIcon = new BitmapImage(uri);
                    }
                    catch(Exception ex)
                    {
                        WeatherIcon = null;
                    }
                }
                else
                {
                    WeatherIcon = null;
                }

                    Console.WriteLine($"Deserialized data - City: {WeatherData?.Name}, Temp: {WeatherData?.Main?.Temp}");
                
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, invalid city name)
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
