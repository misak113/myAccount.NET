﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace myAccount.NET.Logic
{
    public class MyAccount
    {
        public Context context { get; private set; }
        
        public MyAccount() {
            Init();
        }

        private void Init() {
            context = new Context();
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            context.basePath = System.IO.Path.GetDirectoryName(assemblyPath);
            context.myAccount = this;
            context.dataLoader = new DataLoader(context);
            context.dataLoader.Load();
        }

        public void Start() {
            context.actualAction = Context.MAIN;
        }

    }
}
