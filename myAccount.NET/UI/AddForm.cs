using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using myAccount.NET.Data;
using myAccount.NET.Helper;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    class AddForm: Grid
    {
        public Context context { get; private set; }
        public ActionItem DataObject {get; set;}

        private TextBox value;
        private ComboBox currency;
        private TextBox place;
        private TextBox person;
        private DatePicker date;
        private RichTextBox note;

        public AddForm(Context context) {
            this.context = context;
            Init();
        }

        private void Init() {
            InitDefinitions();
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
            
        }

        private void AddInputs() {
            value = new NumericTextBox();
            value.Text = "Zadejte částku";
            Children.Add(value);
            value.Height = 25;

            currency = new ComboBox();
            currency.Items.Add("Kč");
            currency.Items.Add("€");
            currency.SelectedIndex = 0;
            currency.Width = 50;
            currency.Height = 25;
            Grid.SetColumn(currency, 1);
            Children.Add(currency);

            place = new TextBox();
            place.Text = "Zadejte název umístění";
            //place.AddHandler(Control.GotFocusEvent, new EventHandler(PlaceholderDispatcher));
            place.Height = 25;
            Grid.SetColumnSpan(place, 2);
            Grid.SetRow(place, 1);
            Children.Add(place);

            person = new TextBox();
            person.Text = "Zadejte osobu";
            person.Height = 25;
            Grid.SetColumnSpan(person, 2);
            Grid.SetRow(person, 2);
            Children.Add(person);

            date = new DatePicker();
            date.Height = 25;
            Grid.SetColumnSpan(date, 2);
            Grid.SetRow(date, 3);
            Children.Add(date);

            note = new RichTextBox();
            note.Document.Blocks.Remove(note.Document.Blocks.First());
            note.Document.Blocks.Add(new Paragraph(new Run("Zde zadejte poznámku")));
            note.Height = 40;
            Grid.SetColumnSpan(note, 2);
            Grid.SetRow(note, 4);
            Children.Add(note);

            Button save = new Button();
            save.Height = 30;
            save.Content = "Uložit";
            Grid.SetColumnSpan(save, 2);
            Grid.SetRow(save, 5);
            save.Click += this.Save;
            Children.Add(save);
        }


        public void Save(object sender, System.Windows.RoutedEventArgs e)
        {
            DataObject.Value = value.Text;
            context.dataLoader.AddActionItem(DataObject);
        }

        void PlaceholderDispatcher(object sender, EventArgs e) { 
            return;
        }

    }
}
