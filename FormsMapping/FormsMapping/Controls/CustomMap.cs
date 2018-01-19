using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FormsMapping.Controls
{
    public class CustomMap : Map
    {
        public double CircleRadius { get; set; }

        public static readonly BindableProperty CircleRadiusProperty = BindableProperty.Create(
            nameof(CircleRadius),
            typeof(double),
            typeof(CustomMap),
            0d,
            propertyChanging: (b, o, n) =>
            {
                var bindable = (CustomMap)b;
                bindable.CircleRadius = Distance.FromMiles((double)n).Meters;
            });

        public Position MapPosition { get; set; }

        public static readonly BindableProperty MapPositionProperty = BindableProperty.Create(
                     nameof(MapPosition),
                     typeof(Position),
                     typeof(CustomMap),
                     new Position(0, 0),
                     propertyChanging: (b, o, n) =>
                     {
                        var bindable = (CustomMap)b;
                        bindable.MapPosition = (Position)n;
                        bindable.MoveToRegion(MapSpan.FromCenterAndRadius((Position)n, Distance.FromMiles(40)));
                     });

        public IList<Pin> MapPins { get; set; }

        public static readonly BindableProperty MapPinsProperty = BindableProperty.Create(
                     nameof(Pins),
                     typeof(ObservableCollection<Pin>),
                     typeof(CustomMap),
                     new ObservableCollection<Pin>(),
                     propertyChanged: (b, o, n) =>
                     {
                         var bindable = (CustomMap)b;
                         bindable.Pins.Clear();

                         var collection = (ObservableCollection<Pin>)n;
                         foreach (var item in collection)
                             bindable.Pins.Add(item);
                         collection.CollectionChanged += (sender, e) =>
                         {
                             Device.BeginInvokeOnMainThread(() =>
                             {
                                 switch (e.Action)
                                 {
                                     case NotifyCollectionChangedAction.Add:
                                     case NotifyCollectionChangedAction.Replace:
                                     case NotifyCollectionChangedAction.Remove:
                                         if (e.OldItems != null)
                                             foreach (var item in e.OldItems)
                                                 bindable.Pins.Remove((Pin)item);
                                         if (e.NewItems != null)
                                             foreach (var item in e.NewItems)
                                                 bindable.Pins.Add((Pin)item);
                                         break;
                                     case NotifyCollectionChangedAction.Reset:
                                         bindable.Pins.Clear();
                                         break;
                                 }
                             });
                         };
                     });
    }
}