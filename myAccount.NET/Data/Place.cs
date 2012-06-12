using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myAccount.NET.Data
{
    public class Place
    {
        private static int ActualId = 0;
        
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
        public Place() {
            Id = ++ActualId;
        }

        public XElement GetXElement()
        {
            XElement elAI = new XElement("Place");
            elAI.Add(new XElement("Name", Name));
            //CultureInfo culture = new CultureInfo("cz-CS");
            elAI.Add(new XElement("Longitude", Longitude.ToString()));
            elAI.Add(new XElement("Latitude", Latitude.ToString()));
            return elAI;
        }
        
        public void SetXElement(XElement el) {
            Name = el.Element("Name").Value;
            Longitude = Convert.ToDouble(el.Element("Longitude").Value);
            Latitude = Convert.ToDouble(el.Element("Latitude").Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return this.Name == ((Place)obj).Name;
        }
    }
}
