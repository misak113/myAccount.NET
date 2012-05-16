using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace myAccount.NET.Logic
{
    class MyAccount: ISubject
    {
        public Context context { get; private set; }
        private List<IObserver> observers = new List<IObserver>();
        
        public MyAccount() {
            Init();
        }

        private void Init() {
            context = new Context();
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            context.basePath = System.IO.Path.GetDirectoryName(assemblyPath);
            context.subject = this;
        }

        public void registerObserver(IObserver observer) {
            observers.Add(observer);
        }
        public void unregisterObserver(IObserver observer) {
            observers.Remove(observer);
        }
        public void notifyObservers() { 
            foreach (var observer in observers) {
                observer.notify(this);
            }
        }
    }
}
