using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myAccount.NET.Logic
{
    public interface IObserver
    {
        void notify(ISubject subject);
    }
}
