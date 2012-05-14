using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Logic
{
    public class Context
    {
        string _basePath;
        public string basePath { 
            get { return _basePath; }
            set { _basePath = value; } 
        }
        
    }
}
