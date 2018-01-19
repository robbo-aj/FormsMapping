using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.Maps;

namespace FormsMapping.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region commands

        public ICommand LondonCommand => new DelegateCommand(() =>
        {
            IsLondonSelected = true;
            this.CentrePosition = new Position(51.5074, -0.1278);
            this.Pins.Clear();
            this.Pins.Add(new Pin
            {
                Position = this.CentrePosition,
                Type = PinType.Generic,
                Label = $"Centre of '{this.Radius} miles from London'"
            });
            this.CityAndRadius = $"City = London, Radius = {this.Radius} miles";
        });

        public ICommand MoscowCommand => new DelegateCommand(() =>
        {
            IsLondonSelected = false;
            this.CentrePosition = new Position(55.7558, 37.6173);
            this.Pins.Clear();
            this.Pins.Add(new Pin
            {
                Position = this.CentrePosition,
                Type = PinType.Generic,
                Label = $"Centre of '{this.Radius} miles from Moscow'"
            });
            this.CityAndRadius = $"City = Moscow, Radius = {this.Radius} miles";
        });

        #endregion

        #region properties

        public bool IsLondonSelected { get; set; } = true;

        Position _centrePosition;
        public Position CentrePosition
        {
            get => _centrePosition;
            set => SetProperty(ref _centrePosition, value);
        }

        ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                var roundedValue = Math.Round(value / 1.0);
                SetProperty(ref _radius, roundedValue);
                var city = IsLondonSelected ? "London" : "Moscow";
                this.Pins.Clear();
                this.Pins.Add(new Pin
                {
                    Position = this.CentrePosition,
                    Type = PinType.Generic,
                    Label = $"Centre of '{this.Radius} miles from {city}'"
                });
                this.CityAndRadius = $"City = {city}, Radius = {this.Radius} miles";
            }
        }

        string _cityAndRadius;
        public string CityAndRadius
        {
            get => _cityAndRadius;
            set => SetProperty(ref _cityAndRadius, value);
        }

        #endregion

        #region ctor

        public MainViewModel()
        {
            this.Radius = 10;
            this.LondonCommand.Execute(null);
        }

        #endregion
    }
}
