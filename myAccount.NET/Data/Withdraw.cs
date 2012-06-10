using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Data
{
    public class Withdraw : ActionItem
    {
        public Withdraw() : base() {
            Type = "withdraw";
            Name = "Výběr";
        }

        override public double RealValue()
        {
            return 1 * ConvertToDefaultCurrency(Value, Currency);
        }
    }
}
