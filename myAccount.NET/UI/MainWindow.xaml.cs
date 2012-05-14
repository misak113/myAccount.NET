using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ICON_SOURCE = @"images/icon29.png";

        public Context context { get; private set; }
        
        public MainWindow()
        {
            InitializeComponent();
            MyAccount myAccount = new MyAccount();
            context = myAccount.context;
            Init();
        }

        private void Init() {
            Uri iconUri = new Uri(context.basePath+"/"+ICON_SOURCE);
            BitmapFrame img = BitmapFrame.Create(iconUri);
            MainWindowInstance.Icon = img;
        }
    }
}
