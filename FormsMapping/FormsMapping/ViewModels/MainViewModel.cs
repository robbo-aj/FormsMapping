using Prism.Mvvm;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace FormsMapping.ViewModels
{
    public class MainViewModel : BindableBase
    {
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

        public MainViewModel()
        {
            this.CentrePosition = new Position(-37.8141, 144.9633);
            this.Pins.Add(new Pin
            {
                Position = this.CentrePosition,
                Type = PinType.Place,
                Label = "Centre of '10 Miles from Home'"
            });
            this.Radius = 10;
        }
    }
}
