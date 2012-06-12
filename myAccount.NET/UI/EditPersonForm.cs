using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using myAccount.NET.Data;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    class EditPersonForm : Grid
    {

        public Person Person;
        private Context context;

        private TextBox name;
        private TextBox phone;


        public EditPersonForm(Context context, Person person)
        {
            this.context = context;
            this.Person = person;
        }

        public void Init()
        {
            InitDefinitions();
            InitForm();
        }

        private void InitDefinitions()
        {
            var col = new ColumnDefinition();
            col.Width = new GridLength(80);
            ColumnDefinitions.Add(col);
            ColumnDefinitions.Add(new ColumnDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());

        }

        private void InitForm()
        {
            Label nameL = new Label();
            nameL.Content = "Název:";
            Grid.SetColumn(nameL, 0);
            Grid.SetRow(nameL, 0);
            Children.Add(nameL);

            Label phoneL = new Label();
            phoneL.Content = "Telefon:";
            Grid.SetColumn(phoneL, 0);
            Grid.SetRow(phoneL, 1);
            Children.Add(phoneL);


            name = new TextBox();
            name.Text = Person.Name;
            name.Height = 25;
            Grid.SetColumn(name, 1);
            Grid.SetRow(name, 0);
            Children.Add(name);

            phone = new TextBox();
            phone.Text = Person.Phone;
            phone.Height = 25;
            Grid.SetColumn(phone, 1);
            Grid.SetRow(phone, 1);
            Children.Add(phone);


            Button save = new Button();
            save.Content = "Uložit";
            save.Height = 25;
            save.Width = 100;
            save.Click += save_Click;
            Grid.SetColumnSpan(save, 2);
            Grid.SetRow(save, 3);
            Children.Add(save);

        }

        private void save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Person.Name = name.Text;
            Person.Phone = phone.Text;
            context.dataLoader.EditPerson(Person);
            context.dataLoader.Save();
            context.actualAction = Context.PEOPLE;
        }

        public void ErrorMessage(string text)
        {
            // @todo
        }
    }
}
