using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAccount.NET.Logic
{
    public class Context
    {
        public const int MAIN = 1;

        string _basePath;
        public string basePath { 
            get { return _basePath; }
            set { _basePath = value; } 
        }
        public ISubject subject { get; set; }
        public int actualAction { get; set; }
        
    }
}
