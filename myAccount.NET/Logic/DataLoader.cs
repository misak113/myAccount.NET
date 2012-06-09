using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myAccount.NET.Data;

namespace myAccount.NET.Logic
{
    public class DataLoader
    {
        private List<Debt> _Debts = new List<Debt>();
        public List<Debt> Debts {
            get { return _Debts; }
            private set { _Debts = value; }
        }
        private List<Income> _Incomes = new List<Income>();
        public List<Income> Incomes
        {
            get { return _Incomes; }
            private set { _Incomes = value; }
        }
        private List<Loan> _Loans = new List<Loan>();
        public List<Loan> Loans
        {
            get { return _Loans; }
            private set { _Loans = value; }
        }
        private List<Payment> _Payments = new List<Payment>();
        public List<Payment> Payments
        {
            get { return _Payments; }
            private set { _Payments = value; }
        }
        private List<Withdraw> _Withdraws = new List<Withdraw>();
        public List<Withdraw> Withdraws
        {
            get { return _Withdraws; }
            private set { _Withdraws = value; }
        }
        private List<Person> _Persons = new List<Person>();
        public List<Person> Persons
        {
            get { return _Persons; }
            private set { _Persons = value; }
        }
        private List<Place> _Places = new List<Place>();
        public List<Place> Places
        {
            get { return _Places; }
            private set { _Places = value; }
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
        }

        public void AddPerson(Person person) {
            Persons.Add(person);
        }

        public void AddPlace(Place place) {
            Places.Add(place);
        }

        public void Load() { 
            
        }

        public void Save() { 
        
        }
    }
}
