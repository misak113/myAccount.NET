using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using myAccount.NET.Data;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    class StatisticsView : Grid
    {
        private Context context;
        private ListBox listBox;
        private InfoBox infoBox;
        private ActionItem actionItem;

        public StatisticsView(Context context, InfoBox infoBox)
        {
            this.context = context;
            this.infoBox = infoBox;
            Init();
        }

        private void Init()
        {
            listBox = new ListBox();
            var items = context.dataLoader.GetActionItems();
            items.Sort(CompareByDate);
            listBox.ItemsSource = items;
            listBox.SelectionMode = SelectionMode.Single;
            listBox.SelectionChanged += listBox_SelectionChanged;

            Children.Add(listBox);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            infoBox.Children.Clear();
            infoBox.RowDefinitions.Clear();
            infoBox.ColumnDefinitions.Clear();
            infoBox.ColumnDefinitions.Add(new ColumnDefinition());
            infoBox.ColumnDefinitions.Add(new ColumnDefinition());
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(30);
            infoBox.RowDefinitions.Add(row);
            row = new RowDefinition();
            row.Height = new GridLength(30);
            infoBox.RowDefinitions.Add(row);
            infoBox.RowDefinitions.Add(new RowDefinition());

            actionItem = (ActionItem)((ListBox)sender).SelectedItem;

            Label label = new Label();
            Grid.SetColumnSpan(label, 2);
            label.Content = actionItem;
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            infoBox.Children.Add(label);


            Button edit = new Button();
            edit.Content = "Editovat";
            edit.Click += edit_Click;
            Grid.SetRow(edit, 1);
            infoBox.Children.Add(edit);

            Button delete = new Button();
            delete.Content = "Smazat";
            delete.Click += delete_Click;
            Grid.SetRow(delete, 1);
            Grid.SetColumn(delete, 1);
            infoBox.Children.Add(delete);



            Grid description = new Grid();
            description.RowDefinitions.Add(new RowDefinition());
            description.RowDefinitions.Add(new RowDefinition());
            description.RowDefinitions.Add(new RowDefinition());
            description.RowDefinitions.Add(new RowDefinition());
            description.RowDefinitions.Add(new RowDefinition());
            description.ColumnDefinitions.Add(new ColumnDefinition());
            description.ColumnDefinitions.Add(new ColumnDefinition());

            Label valueLabel = new Label();
            valueLabel.Content = "Hodnota:";
            Grid.SetRow(valueLabel, 0);
            description.Children.Add(valueLabel);
            Label value = new Label();
            value.Content = actionItem.Value + " " + actionItem.Currency;
            Grid.SetColumn(value, 1);
            Grid.SetRow(value, 0);
            description.Children.Add(value);

            Label datetimeLabel = new Label();
            datetimeLabel.Content = "Vloženo:";
            Grid.SetRow(datetimeLabel, 1);
            description.Children.Add(datetimeLabel);
            Label datetime = new Label();
            datetime.Content = actionItem.DateTime.ToShortDateString() + ", " + actionItem.DateTime.ToShortTimeString();
            Grid.SetColumn(datetime, 1);
            Grid.SetRow(datetime, 1);
            description.Children.Add(datetime);

            Label noteLabel = new Label();
            noteLabel.Content = "Poznámka:";
            Grid.SetRow(noteLabel, 2);
            description.Children.Add(noteLabel);
            RichTextBox note = new RichTextBox();
            note.Document.Blocks.Add(new Paragraph(new Run(actionItem.Note)));
            note.IsReadOnly = true;
            Grid.SetColumn(note, 1);
            Grid.SetRow(note, 2);
            description.Children.Add(note);

            Label placeLabel = new Label();
            placeLabel.Content = "Místo:";
            Grid.SetRow(placeLabel, 3);
            description.Children.Add(placeLabel);
            Label place = new Label();
            place.Content = actionItem.Place.Name;
            Grid.SetColumn(place, 1);
            Grid.SetRow(place, 3);
            description.Children.Add(place);

            Label personLabel = new Label();
            personLabel.Content = "Osoba:";
            Grid.SetRow(personLabel, 4);
            description.Children.Add(personLabel);
            Label person = new Label();
            person.Content = actionItem.Person.Name;
            Grid.SetColumn(person, 1);
            Grid.SetRow(person, 4);
            description.Children.Add(person);


            Grid.SetRow(description, 2);
            Grid.SetColumnSpan(description, 2);
            infoBox.Children.Add(description);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            context.dataLoader.RemoveActionItem(actionItem);
            context.actualAction = Context.STATISTICS;
            context.dataLoader.Save();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            context.editedItem = actionItem;
            context.actualAction = Context.EDIT_ACTION_ITEM;
        }

        private int CompareByDate(ActionItem x, ActionItem y)
        {
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            if (x.DateTime.Ticks == y.DateTime.Ticks)
            {
                return 0;
            }
            return x.DateTime.Ticks < y.DateTime.Ticks ? 1 : -1;
        }

    }
}
