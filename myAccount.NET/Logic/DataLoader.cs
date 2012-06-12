using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using myAccount.NET.Data;

namespace myAccount.NET.Logic
{
    public class DataLoader
    {
        public string DATA_FILE = @"Resource/data.xml";


        private List<Debt> _Debts;
        public List<Debt> Debts {
            get { return _Debts; }
            private set { _Debts = value; }
        }
        private List<Income> _Incomes;
        public List<Income> Incomes
        {
            get { return _Incomes; }
            private set { _Incomes = value; }
        }
        private List<Loan> _Loans;
        public List<Loan> Loans
        {
            get { return _Loans; }
            private set { _Loans = value; }
        }
        private List<Payment> _Payments;
        public List<Payment> Payments
        {
            get { return _Payments; }
            private set { _Payments = value; }
        }
        private List<Withdraw> _Withdraws;
        public List<Withdraw> Withdraws
        {
            get { return _Withdraws; }
            private set { _Withdraws = value; }
        }
        private List<Person> _Persons;
        public List<Person> Persons
        {
            get { return _Persons; }
            private set { _Persons = value; }
        }
        private List<Place> _Places;
        public List<Place> Places
        {
            get { return _Places; }
            private set { _Places = value; }
        }

        private Context context { get; set; }

        public DataLoader(Context context) {
            this.context = context;
            DATA_FILE = context.basePath + "/" + DATA_FILE;
            Load();
        }

        public void AddActionItem(ActionItem actionItem) {
            if (actionItem is Debt) {
                Debts.Add((Debt)actionItem);
            }
            if (actionItem is Income)
            {
                Incomes.Add((Income)actionItem);
            }
            if (actionItem is Loan)
            {
                Loans.Add((Loan)actionItem);
            }
            if (actionItem is Payment)
            {
                Payments.Add((Payment)actionItem);
            }
            if (actionItem is Withdraw)
            {
                Withdraws.Add((Withdraw)actionItem);
            }
            //Save();
        }
        public void RemoveActionItem(ActionItem actionItem)
        {
            if (actionItem is Debt)
            {
                Debts.Remove((Debt)actionItem);
            }
            if (actionItem is Income)
            {
                Incomes.Remove((Income)actionItem);
            }
            if (actionItem is Loan)
            {
                Loans.Remove((Loan)actionItem);
            }
            if (actionItem is Payment)
            {
                Payments.Remove((Payment)actionItem);
            }
            if (actionItem is Withdraw)
            {
                Withdraws.Remove((Withdraw)actionItem);
            }
        }
        public void EditActionItem(ActionItem actionItem)
        {
            RemoveActionItem(actionItem);
            AddActionItem(actionItem);
            //Save();
        }

        public void AddPerson(Person person) {
            Persons.Add(person);
        }

        public void RemovePerson(Person person) {
            Persons.Remove(person);
        }
        public void EditPerson(Person person)
        {
            RemovePerson(person);
            AddPerson(person);
        }

        public void AddPlace(Place place) {
            Places.Add(place);
        }
        public void RemovePlace(Place place)
        {
            Places.Remove(place);
        }
        public void EditPlace(Place place) {
            RemovePlace(place);
            AddPlace(place);
        }

        public Person GetPerson(string name) {
            Person person = null;
            foreach (Person pers in Persons) {
                if (pers.Name == name) {
                    person = pers;
                    break;
                }
            }
            if (person == null) {
                person = new Person();
                person.Name = name;
                Persons.Add(person);
                //Save();
            }
            return person;
        }

        public Place GetExistsPlace(string name)
        {
            Place place = null;
            foreach (Place plac in Places)
            {
                if (plac.Name == name)
                {
                    place = plac;
                    break;
                }
            }
            return place;
        }

        public Place GetPlace(string name) {
            Place place = null;
            foreach (Place plac in Places)
            {
                if (plac.Name == name)
                {
                    place = plac;
                    break;
                }
            }
            if (place == null)
            {
                place = new Place();
                place.Name = name;
                Places.Add(place);
                //Save();
            }
            return place;
        }

        public void InitData() {
            Debts = new List<Debt>();
            Incomes = new List<Income>();
            Loans = new List<Loan>();
            Payments = new List<Payment>();
            Withdraws = new List<Withdraw>();
            Persons = new List<Person>();
            Places = new List<Place>();
            
        }

        public void Load() {
            XDocument doc = XDocument.Load(DATA_FILE);
            // @todo pozor na double tecku v longitude apod.
            InitData();

            XElement elPersons = doc.Root.Element("Persons");
            foreach (XElement elPerson in elPersons.Elements("Person")) {
                Person person = new Person();
                person.SetXElement(elPerson);
                Persons.Add(person);
            }

            XElement elPlaces = doc.Root.Element("Places");
            foreach (XElement elPlace in elPlaces.Elements("Place"))
            {
                Place place = new Place();
                place.SetXElement(elPlace);
                Places.Add(place);
            }

            XElement elActionItems = doc.Root.Element("ActionItems");
            foreach (XElement elActionItem in elActionItems.Elements("ActionItem")) {
                switch (elActionItem.Element("Type").Value) {
                    case "debt":
                        Debt debt = new Debt();
                        debt.SetXElement(elActionItem, this);
                        Debts.Add(debt);
                        break;
                    case "income":
                        Income income = new Income();
                        income.SetXElement(elActionItem, this);
                        Incomes.Add(income);
                        break;
                    case "loan":
                        Loan loan = new Loan();
                        loan.SetXElement(elActionItem, this);
                        Loans.Add(loan);
                        break;
                    case "payment":
                        Payment payment = new Payment();
                        payment.SetXElement(elActionItem, this);
                        Payments.Add(payment);
                        break;
                    case "withdraw":
                        Withdraw withdraw = new Withdraw();
                        withdraw.SetXElement(elActionItem, this);
                        Withdraws.Add(withdraw);
                        break;
                }
            }

            
        }

        public void Save() {
            
            XElement ActionItems = new XElement("ActionItems");
            List<ActionItem> items = GetActionItems();
            foreach (var item in items) {
                XElement el = item.GetXElement();
                ActionItems.Add(el);
            }
            
            XElement persons = new XElement("Persons");
            foreach (var item in this.Persons)
            {
                XElement el = item.GetXElement();
                persons.Add(el);
            }
            
            XElement places = new XElement("Places");
            foreach (var item in this.Places)
            {
                XElement el = item.GetXElement();
                places.Add(el);
            }

            XElement root = new XElement("root");
            root.Add(ActionItems);
            root.Add(persons);
            root.Add(places);
            XDocument doc = new XDocument(root);
            
            doc.Save(DATA_FILE);
        }

        public List<ActionItem> GetActionItems() {
            List<ActionItem> items = new List<ActionItem>();
            foreach (var item in Debts) {
                items.Add(item);
            }
            foreach (var item in Incomes)
            {
                items.Add(item);
            }
            foreach (var item in Loans)
            {
                items.Add(item);
            }
            foreach (var item in Payments)
            {
                items.Add(item);
            }
            foreach (var item in Withdraws)
            {
                items.Add(item);
            }
            return items;
        }

        public List<ActionItem> GetActionItems(Person person) {
            var actionItems = GetActionItems();
            List<ActionItem> personActions = new List<ActionItem>();
            foreach (var actionItem in actionItems) {
                if (actionItem.Person.Equals(person)) {
                    personActions.Add(actionItem);
                }
            }
            return personActions;
        }
        public List<ActionItem> GetActionItems(Place place)
        {
            var actionItems = GetActionItems();
            List<ActionItem> placeActions = new List<ActionItem>();
            foreach (var actionItem in actionItems)
            {
                if (actionItem.Place.Equals(place))
                {
                    placeActions.Add(actionItem);
                }
            }
            return placeActions;
        }
    }
}
