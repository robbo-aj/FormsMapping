using Prism.Commands;
using Prism.Mvvm;
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
            this.CentrePosition = new Position(51.5074, -0.1278);
            this.Pins.Clear();
            this.Pins.Add(new Pin
            {
                Position = this.CentrePosition,
                Type = PinType.Generic,
                Label = $"Centre of {Radius} miles from London"
            });
        });

        public ICommand MoscowCommand => new DelegateCommand(() =>
        {
            this.CentrePosition = new Position(55.7558, 37.6173);
            this.Pins.Clear();
            this.Pins.Add(new Pin
            {
                Position = this.CentrePosition,
                Type = PinType.Generic,
                Label = $"Centre of {Radius} miles from Moscow"
            });
        });

        #endregion

        #region properties

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
            set => SetProperty(ref _radius, value);
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
