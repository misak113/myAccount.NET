using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myAccount.NET.Logic
{
    public interface ISubject
    {
        void notifyObservers();
        void registerObserver(IObserver observer);
        void unregisterObserver(IObserver observer);
    }
}
