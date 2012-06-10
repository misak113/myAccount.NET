using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            listBox.ItemsSource = context.dataLoader.GetActionItems();
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
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            context.dataLoader.RemoveActionItem(actionItem);
            context.actualAction = Context.STATISTICS;
            context.dataLoader.Save();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            context.actualAction = Context.EDIT_ACTION_ITEM;
        }

        private int CompareByDate(ActionItem x, ActionItem y)
        {
            // @todo
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            if (x.DateTime.Ticks == x.DateTime.Ticks)
            {
                return 0;
            }
            return x.DateTime.Ticks > x.DateTime.Ticks ? 1 : -1;
        }

    }
}
