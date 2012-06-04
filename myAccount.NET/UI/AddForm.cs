using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace myAccount.NET.UI
{
    class AddForm: Grid
    {

        public AddForm() {
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
            RowDefinition row = new RowDefinition();
            row.
            RowDefinitions.Add(row);
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            
        }

        private void AddInputs() {
            TextBox value = new TextBox();
            value.Text = "Zadejte částku";
            Children.Add(value);

            TextBox currency = new TextBox();
            currency.Text = "Měna";
            currency.Width = 50;
            Grid.SetColumn(currency, 1);
            Children.Add(currency);

            TextBox place = new TextBox();
            place.Text = "Zadejte název umístění";
            Grid.SetRow(place, 1);
            Children.Add(place);
        }

    }
}
