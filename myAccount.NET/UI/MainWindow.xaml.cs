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

        private ContentPanel contentPanel;
        
        public MainWindow()
        {
            InitializeComponent();
            MyAccount myAccount = new MyAccount();
            context = myAccount.context;
            Init();
            myAccount.Start();
        }

        private void Init() {
            Uri iconUri = new Uri(context.basePath+"/"+ICON_SOURCE);
            BitmapFrame img = BitmapFrame.Create(iconUri);
            MainWindowInstance.Icon = img;

            contentPanel = new ContentPanel(context);
            Grid.SetRow(contentPanel, 1);
            ContentWindow.Children.Add(contentPanel);

            BindButtons();
        }

        private void BindButtons() {
            payment.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            withdraw.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            debt.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            loan.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            income.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);

            people.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            places.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            machines.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            statistics.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
            synchronization.AddHandler(Button.ClickEvent, new RoutedEventHandler(ShowPage), true);
        }

        void ShowPage(object sender, RoutedEventArgs e) {
            Button button = (Button) sender;
            context.actualAction = button.Name;
        }
    }
}
