using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using myAccount.NET.Data;
using myAccount.NET.Helper;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    class AddForm: Grid
    {
        public Context context { get; private set; }
        public ActionItem DataObject { get; set;}

        private TextBox value;
        private ComboBox currency;
        private TextBox place;
        private TextBox person;
        private DatePicker date;
        private RichTextBox note;

        public bool editing = false;
        public string savedPage = Context.MAIN;

        public Currency[] currencies = new Currency[2] {
            new Currency("czk", "Kč"),
            new Currency("eur", "€")
        };

        private string label;

        public AddForm(Context context, string label) {
            this.context = context;
            this.label = label;
            //Init();
        }

        public void Init() {
            InitDefinitions();

            Label labelEl = new Label();
            Grid.SetColumnSpan(labelEl, 2);
            labelEl.Content = label;
            labelEl.FontSize = 15;
            labelEl.FontWeight = FontWeights.Bold;
            Children.Add(labelEl);

            AddInputs();
            
        }

        private void InitDefinitions() {
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());

            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            
        }

        private void AddInputs() {
            value = new NumericTextBox();
            if (DataObject.Value != 0)
            {
                value.Text = Convert.ToString(DataObject.Value);
            }
            else
            {
                value.Text = "Zadejte částku";
            }
            Children.Add(value);
            Grid.SetRow(value, 1);
            value.Height = 25;

            currency = new ComboBox();
            foreach (Currency cur in currencies) {
                currency.Items.Add(cur);
                if (DataObject != null && DataObject.Currency == cur.Code) {
                    currency.SelectedItem = cur;
                }
            }
            if (currency.SelectedItem == null) {
                currency.SelectedIndex = 0;
            }
            currency.Width = 50;
            currency.Height = 25;
            Grid.SetColumn(currency, 1);
            Grid.SetRow(currency, 1);
            Children.Add(currency);

            place = new TextBox();
            place.TextChanged += place_TextChanged;
            if (DataObject.Place != null)
            {
                place.Text = DataObject.Place.Name;
            }
            else
            {
                place.Text = "Zadejte název umístění";
            }
            //place.AddHandler(Control.GotFocusEvent, new EventHandler(PlaceholderDispatcher));
            place.Height = 25;
            Grid.SetColumnSpan(place, 2);
            Grid.SetRow(place, 2);
            Children.Add(place);

            person = new TextBox();
            if (DataObject.Person != null)
            {
                person.Text = DataObject.Person.Name;
            }
            else
            {
                person.Text = "Zadejte osobu";
            }
            person.Height = 25;
            Grid.SetColumnSpan(person, 2);
            Grid.SetRow(person, 3);
            Children.Add(person);

            date = new DatePicker();
            if (DataObject.DateTime != null)
            {
                date.SelectedDate = DataObject.DateTime;
            }
            else {
                date.SelectedDate = DateTime.Now;
            }
            date.Height = 25;
            Grid.SetColumnSpan(date, 2);
            Grid.SetRow(date, 4);
            Children.Add(date);

            note = new RichTextBox();
            note.Document.Blocks.Remove(note.Document.Blocks.First());
            if (DataObject.Note != null)
            {
                note.Document.Blocks.Add(new Paragraph(new Run(DataObject.Note)));
            }
            else
            {
                note.Document.Blocks.Add(new Paragraph(new Run("Zde zadejte poznámku")));
            }
            note.Height = 40;
            Grid.SetColumnSpan(note, 2);
            Grid.SetRow(note, 5);
            Children.Add(note);

            Button save = new Button();
            save.Height = 30;
            save.Content = "Uložit";
            Grid.SetColumnSpan(save, 2);
            Grid.SetRow(save, 6);
            save.Click += this.Save;
            Children.Add(save);
        }

        private void place_TextChanged(object sender, TextChangedEventArgs e)
        {
            Place placeNow = context.dataLoader.GetExistsPlace(place.Text);
            if (placeNow == null) {
                return;
            }
            double lat = placeNow.Latitude;
            double lng = placeNow.Longitude;
            context.actualMap.SetLatLng(lat, lng);
        }


        public void Save(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                DataObject.Value = Convert.ToDouble(value.Text);
            }
            catch (FormatException ex)
            {
                ErrorMessage("Hodnota musí být číslo");
                value.Background = new SolidColorBrush(Color.FromRgb(200, 100, 100));
                return;
            }
            DataObject.Note = new TextRange(note.Document.ContentStart, note.Document.ContentEnd).Text;
            DataObject.Currency = ((Currency)currency.SelectedItem).Code;
            Person person = context.dataLoader.GetPerson(this.person.Text);
            Place place = context.dataLoader.GetPlace(this.place.Text);
            DataObject.Place = place;
            DataObject.Person = person;
            DataObject.DateTime = date.SelectedDate.Value.Date;
            if (editing) {
                context.dataLoader.EditActionItem(DataObject);
            } else {
                context.dataLoader.AddActionItem(DataObject);
            }
            context.dataLoader.Save();
            InfoMessage("Uloženo");
            context.actualAction = savedPage;
        }

        void PlaceholderDispatcher(object sender, EventArgs e) { 
            return;
        }

        public void ErrorMessage(string text) { 
            // @todo
        }
        public void InfoMessage(string text)
        {
            // @todo
        }

    }

    public class Currency { 
        public string Name { get; private set; }
        public string Code { get; private set; }

        public Currency(string code, string name) {
            Name = name;
            Code = code;
        }

        override public string ToString() {
            return Name;
        }
    }
}
