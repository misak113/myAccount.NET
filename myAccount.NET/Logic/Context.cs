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
