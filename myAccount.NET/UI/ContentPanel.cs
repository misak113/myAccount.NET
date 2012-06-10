using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using myAccount.NET.Data;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    class ContentPanel: Grid, IObserver
    {
        const string BACKGROUND_SOURCE = @"images/myAccount-big.png";
        const string NOT_IMPLEMENTED_SOURCE = @"images/not-implemented.png";

        private Context context;
        public ContentPanel(Context context)
        {
            this.context = context;
            Init();
            context.subject.registerObserver(this);
        }

        private void Init() { 
            
        }

        public void notify(ISubject subject) {
            ShowPage(context.actualAction);
        }

        public void ShowPage(string page) {
            this.Children.RemoveRange(0, 10);
            switch (page) {
                case Context.MAIN:
                    ShowMain();
                    break;
                case Context.PAYMENT:
                    ShowPayment();
                    break;
                case Context.DEBT:
                    ShowDebt();
                    break;
                case Context.INCOME:
                    ShowIncome();
                    break;
                case Context.LOAN:
                    ShowLoan();
                    break;
                case Context.WITHDRAW:
                    ShowWithdraw();
                    break;
                case Context.PEOPLE:
                    ShowPeople();
                    break;
                case Context.PLACES:
                    ShowPlaces();
                    break;
                case Context.STATISTICS:
                    ShowStatistics();
                    break;
                default:
                    ShowNotImplemented();
                    break;
            }
        }

        private void ShowMain()
        {
            LetsChangeDefinitions(1, 1);
            Image image = GetBackgroundImage(BACKGROUND_SOURCE);
            Children.Add(image);
        }

        private void ShowPayment() {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);
            
            AddForm addPayment = new AddForm(context, "Nová platba");
            addPayment.DataObject = new Payment();
            Children.Add(addPayment);

            Map paymentMap = new Map();
            Grid.SetColumn(paymentMap, 1);
            Children.Add(paymentMap);
        }
        private void ShowDebt()
        {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);
            
            AddForm addDebt = new AddForm(context, "Nový dluh");
            addDebt.DataObject = new Debt();
            Children.Add(addDebt);

            Map debtMap = new Map();
            Grid.SetColumn(debtMap, 1);
            Children.Add(debtMap);
        }
        private void ShowIncome()
        {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);
            
            AddForm addIncome = new AddForm(context, "Nový příjem");
            addIncome.DataObject = new Income();
            Children.Add(addIncome);

            Map incomeMap = new Map();
            Grid.SetColumn(incomeMap, 1);
            Children.Add(incomeMap);
        }
        private void ShowLoan()
        {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);
            
            AddForm addLoan = new AddForm(context, "Nové půjčení");
            addLoan.DataObject = new Loan();
            Children.Add(addLoan);

            Map loanMap = new Map();
            Grid.SetColumn(loanMap, 1);
            Children.Add(loanMap);
        }
        private void ShowWithdraw()
        {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);
            
            AddForm addWithdraw = new AddForm(context, "Nový výběr");
            addWithdraw.DataObject = new Withdraw();
            Children.Add(addWithdraw);

            Map withdrawMap = new Map();
            Grid.SetColumn(withdrawMap, 1);
            Children.Add(withdrawMap);
        }

        private void ShowPeople() {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);

            InfoBox infoBox = new InfoBox();
            Grid.SetColumn(infoBox, 1);
            Children.Add(infoBox);

            PeopleView peopleView = new PeopleView(context, infoBox);
            Children.Add(peopleView);
        }
        private void ShowPlaces()
        {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);

            InfoBox infoBox = new InfoBox();
            Grid.SetColumn(infoBox, 1);
            Children.Add(infoBox);

            PlacesView placesView = new PlacesView(context, infoBox);
            Children.Add(placesView);
        }
        private void ShowStatistics()
        {
            LetsChangeDefinitions(1, 2);
            ColumnDefinition column = ColumnDefinitions.First();
            column.Width = new GridLength(180);

            InfoBox infoBox = new InfoBox();
            Grid.SetColumn(infoBox, 1);
            Children.Add(infoBox);

            StatisticsView statisticsView = new StatisticsView(context, infoBox);
            Children.Add(statisticsView);
        }

        private void ShowNotImplemented() {
            LetsChangeDefinitions(1, 1);
            Image image = GetBackgroundImage(NOT_IMPLEMENTED_SOURCE);
            //Grid.SetRow(image, 2);
            Children.Add(image);
        }


        private Image GetBackgroundImage(String source) {
            // Open a Stream and decode a PNG image
            string url = context.basePath + "/" + source;
            Stream imageStreamSource = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            Image image = new Image();
            //BitmapImage imageSource = new BitmapImage();
            //Uri uri = new Uri(url);
            //imageSource.UriSource = uri;
            //imageSource.EndInit();
            image.Source = bitmapSource;
            image.Width = 200;
            image.Height = 200;
            return image;
        }

        private void LetsChangeDefinitions(int rows, int columns) {
            try
            {
                while (true)
                {
                    ColumnDefinition cd = ColumnDefinitions.ElementAt(0);
                    ColumnDefinitions.Remove(cd);
                }
            } catch (ArgumentOutOfRangeException e) {}
            for (int i = 0; i<columns;i++ )
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
            try
            {
                while (true)
                {
                    RowDefinition cd = RowDefinitions.ElementAt(0);
                    RowDefinitions.Remove(cd);
                }
            }
            catch (ArgumentOutOfRangeException e) { }
            for (int i = 0; i < rows; i++)
            {
                RowDefinitions.Add(new RowDefinition());
            }
        }
    }


}
