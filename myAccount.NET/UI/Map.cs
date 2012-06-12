using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
//using Google.Api.Maps.Service.StaticMaps;

namespace myAccount.NET.UI
{
    public class Map: Grid
    {
        public const string BASE_MAP_URL = "https://maps.google.com/maps?ie=UTF8&sll={0}&z=17";

        private WebBrowser wb;

        public Map() {
            /*var map = new StaticMap();
            map.Center = "1000 7h Ave"; // or a lat/lng coordinate
            map.Zoom = "14";
            map.Size = "400x400";
            map.Sensor = "true";
            var uriMap = map.ToUri();
            */
            Uri uriMap = new Uri("http://maps.google.com/");
            wb = new WebBrowser();
            Children.Add(wb);
            wb.Navigate(uriMap);
        }

        public void SetLatLng(double lat, double lng) {
            CultureInfo culture = new CultureInfo("en-US");
            string latlng = lat.ToString(culture) + "," + lng.ToString(culture);
            string url = String.Format(BASE_MAP_URL, latlng);
            wb.Navigate(url);
        }

    }
}
