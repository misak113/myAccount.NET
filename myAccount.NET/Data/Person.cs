using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myAccount.NET.Data
{
    public class Person
    {
        private static int ActualId = 0;
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Person() {
            Id = ++ActualId;
        }

        public XElement GetXElement()
        {
            XElement elAI = new XElement("Person");
            elAI.Add(new XElement("Name", Name));
            elAI.Add(new XElement("Phone", Phone));
            return elAI;
        }

        public void SetXElement(XElement el) {
            Name = el.Element("Name").Value;
            Phone = el.Element("Phone").Value;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return this.Name == ((Person)obj).Name;
        }
    }
}
