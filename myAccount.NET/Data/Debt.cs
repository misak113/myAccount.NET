﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Data
{
    public class Debt : ActionItem
    {
        public Debt() : base() {
            Type = "debt";
            Name = "Dluh";
        }

       override public double RealValue() {
            return -1 * ConvertToDefaultCurrency(Value, Currency);
        }
    }
}
