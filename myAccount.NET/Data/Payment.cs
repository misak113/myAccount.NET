using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Data
{
    public class Payment: ActionItem
    {
        public Payment() : base() {
            Type = "payment";
            Name = "Platba";
        }

        override public double RealValue()
        {
            return -1 * ConvertToDefaultCurrency(Value, Currency);
        }
    }
}
