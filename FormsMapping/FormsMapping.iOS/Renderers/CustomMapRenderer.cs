using System.ComponentModel;
using FormsMapping.Controls;
using FormsMapping.iOS.Renderers;
using MapKit;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace FormsMapping.iOS.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        MKCircleRenderer _circleRenderer;
        MKCircle _circleOverlay;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveOverlays(nativeMap.Overlays);
                    nativeMap.OverlayRenderer = null;
                    _circleRenderer = null;
                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                var centre = formsMap.MapPosition;
                var radius = formsMap.CircleRadius;

                nativeMap.OverlayRenderer = GetOverlayRenderer;

                _circleOverlay = MKCircle.Circle(new CoreLocation.CLLocationCoordinate2D(centre.Latitude, centre.Longitude), radius);
                nativeMap.AddOverlay(_circleOverlay);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var formsMap = Element as CustomMap;
            var centre = formsMap.MapPosition;
            var radius = formsMap.CircleRadius;

            var nativeMap = Control as MKMapView;

            if (e.PropertyName == "CircleRadius" || e.PropertyName == "MapPosition")
            {
                nativeMap.RemoveOverlay(_circleOverlay);
                nativeMap.OverlayRenderer = null;
                _circleRenderer = null;

                nativeMap.OverlayRenderer = GetOverlayRenderer;
                _circleOverlay = MKCircle.Circle(new CoreLocation.CLLocationCoordinate2D(centre.Latitude, centre.Longitude), radius);
                nativeMap.AddOverlay(_circleOverlay);
            }
        }

        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (_circleRenderer == null && !Equals(overlayWrapper, null))
            {
                var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                _circleRenderer = new MKCircleRenderer(overlay as MKCircle)
                {
                    FillColor = UIColor.FromRGB(211,47,47),
                    Alpha = 0.3f
                };
            }
            return _circleRenderer;
        }
    }
}
