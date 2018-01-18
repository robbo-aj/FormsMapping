using Xamarin.Forms.Maps;

namespace FormsMapping.Models
{
    public class MapCircle
    {
        public Position CentrePoint { get; set; }
        public double Radius { get; set; }

        public MapCircle()
        {
            this.CentrePoint = new Position(0, 0);
            this.Radius = 0;
        }
    }
}