using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Data
{
    public class Income : ActionItem
    {
        public Income() : base() {
            Type = "income";
            Name = "Příjem";
        }

        override public double RealValue()
        {
            return 1 * ConvertToDefaultCurrency(Value, Currency);
        }
    }
}
