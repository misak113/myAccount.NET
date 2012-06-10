using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Data
{
    public class Loan : ActionItem
    {
        public Loan() : base() {
            Type = "loan";
            Name = "Půjčení";
        }

        override public double RealValue()
        {
            return 1 * ConvertToDefaultCurrency(Value, Currency);
        }
    }
}
