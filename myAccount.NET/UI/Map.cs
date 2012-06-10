using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
//using Google.Api.Maps.Service.StaticMaps;

namespace myAccount.NET.UI
{
    class Map: Grid
    {

        public Map() {
            /*var map = new StaticMap();
            map.Center = "1000 7h Ave"; // or a lat/lng coordinate
            map.Zoom = "14";
            map.Size = "400x400";
            map.Sensor = "true";
            var uriMap = map.ToUri();
            */
            Uri uriMap = new Uri("http://maps.google.com/");
            WebBrowser wb = new WebBrowser();
            Children.Add(wb);
            wb.Navigate(uriMap);
        }

    }
}
