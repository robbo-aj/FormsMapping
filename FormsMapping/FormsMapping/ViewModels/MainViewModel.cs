using Prism.Mvvm;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace FormsMapping.ViewModels
{
    public class MainViewModel : BindableBase
    {
        Position _position;
        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public MainViewModel()
        {
            this.Position = new Position(-37.8141, 144.9633);
            this.Pins.Add(new Pin
            {
                Position = this.Position,
                Type = PinType.Place,
                Label = "Centre of '10 Miles from Home'"
            });
        }
    }
}
