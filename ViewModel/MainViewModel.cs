using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_app.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public HomeViewModel HomeVM { get; set; }
        public WeatherViewModel WeatherVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            WeatherVM = new WeatherViewModel();
            HomeVM = new HomeViewModel();
            
            // Subscribe to weather data changes in WeatherViewModel
            WeatherVM.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(WeatherVM.WeatherData))
                {
                    HomeVM.WeatherData = WeatherVM.WeatherData;
                }
            };
            
            CurrentView = WeatherVM;
        }
    }
}
