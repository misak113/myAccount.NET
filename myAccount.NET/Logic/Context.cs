using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Logic
{
    public class Context : ISubject
    {
        public const string MAIN = "main";
        public const string PAYMENT = "payment";
        public const string DEBT = "debt";
        public const string LOAN = "loan";
        public const string WITHDRAW = "withdraw";
        public const string INCOME = "income";

        public const string PEOPLE = "people";
        public const string PLACES = "places";
        public const string MACHINES = "machines";
        public const string STATISTICS = "statistics";
        public const string SYNCHRONIZATION = "synchronization";

        public const string EDIT_PERSON = "edit_person";
        public const string EDIT_PLACE = "edit_place";
        public const string EDIT_ACTION_ITEM = "edit_action_item";





        private List<IObserver> observers = new List<IObserver>();

        private string _basePath;
        public string basePath { 
            get { return _basePath; }
            internal set { _basePath = value; } 
        }
        public ISubject subject { get; set; }
        private string _actualAction;
        public string actualAction {
            get { return _actualAction; } 
            internal set {
                _actualAction = value;
                notifyObservers();
            }
        }

        private MyAccount _myAccount;
        public MyAccount myAccount
        {
            get { return _myAccount; }
            internal set
            {
                _myAccount = value;
            }
        }

        public DataLoader dataLoader { get; internal set; }


        public Context() {
            subject = this;
        }


        public void registerObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void unregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void notifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.notify(this);
            }
        }
    }
}
