using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using myAccount.NET.Logic;

namespace myAccount.NET.Data
{
    abstract public class ActionItem
    {
        public const double RATE_EUR_TO_CZK = 25;
        public const string CZK = "czk";
        public const string EUR = "eur";


        private static int ActualId = 0;

        public int Id { get; set;}
        public double Value { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public Place Place { get; set; }
        public Person Person { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }

        public string Name = "Akce";

        public ActionItem() {
            Id = ++ActualId;
            DateTime = DateTime.Now;
        }

        public XElement GetXElement() {
            XElement elAI = new XElement("ActionItem");
            elAI.Add(new XElement("Value", Value));
            elAI.Add(new XElement("DateTime", DateTime.ToBinary()));
            elAI.Add(new XElement("Note", Note));
            elAI.Add(new XElement("Place", Place.Name));
            elAI.Add(new XElement("Person", Person.Name));
            elAI.Add(new XElement("Type", Type));
            elAI.Add(new XElement("Currency", Currency));
            return elAI;
        }

        public void SetXElement(XElement elActionItem, DataLoader dataLoader) {
            Value = Convert.ToDouble(elActionItem.Element("Value").Value);
            DateTime = DateTime.FromBinary(Convert.ToInt64(elActionItem.Element("DateTime").Value));
            Note = elActionItem.Element("Note").Value;
            Place = dataLoader.GetPlace(elActionItem.Element("Place").Value);
            Person = dataLoader.GetPerson(elActionItem.Element("Person").Value);
            Currency = elActionItem.Element("Currency").Value;
        }

        public override string ToString()
        {
            return Name+": "+Value+" "+Currency+", "+DateTime.ToLongDateString()+" "+DateTime.ToShortTimeString();
        }

        public double ConvertToDefaultCurrency(double value, string currency) {
            if (currency == CZK) {
                return value;
            }
            if (currency == EUR) {
                return value * RATE_EUR_TO_CZK;
            }
            throw new NotImplementedException("Měna není implementována");
        }

        abstract public double RealValue();
    }
}
