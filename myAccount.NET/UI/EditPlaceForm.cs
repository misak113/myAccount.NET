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
    class EditPlaceForm: Grid
    {

        public Place Place;
        private Context context;

        private TextBox name;
        private TextBox latitude;
        private TextBox longitude;


        public EditPlaceForm(Context context, Place place) {
            this.context = context;
            this.Place = place;
        }

        public void Init() {
            InitDefinitions();
            InitForm();
        }

        private void InitDefinitions() {
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

        private void InitForm() {
            Label nameL = new Label();
            nameL.Content = "Název:";
            Grid.SetColumn(nameL, 0);
            Grid.SetRow(nameL, 0);
            Children.Add(nameL);

            Label latL = new Label();
            latL.Content = "Latitude:";
            Grid.SetColumn(latL, 0);
            Grid.SetRow(latL, 1);
            Children.Add(latL);

            Label longL = new Label();
            longL.Content = "Longitude:";
            Grid.SetColumn(longL, 0);
            Grid.SetRow(longL, 2);
            Children.Add(longL);

            name = new TextBox();
            name.Text = Place.Name;
            name.Height = 25;
            Grid.SetColumn(name, 1);
            Grid.SetRow(name, 0);
            Children.Add(name);

            latitude = new TextBox();
            latitude.TextChanged += latlng_Changed;
            latitude.Text = Convert.ToString(Place.Latitude);
            latitude.Height = 25;
            Grid.SetColumn(latitude, 1);
            Grid.SetRow(latitude, 1);
            Children.Add(latitude);
            
            longitude = new TextBox();
            longitude.TextChanged += latlng_Changed;
            longitude.Text = Convert.ToString(Place.Longitude);
            longitude.Height = 25;
            Grid.SetColumn(longitude, 1);
            Grid.SetRow(longitude, 2);
            Children.Add(longitude);
            
            Button save = new Button();
            save.Content = "Uložit";
            save.Height = 25;
            save.Width = 100;
            save.Click += save_Click;
            Grid.SetColumnSpan(save, 2);
            Grid.SetRow(save, 3);
            Children.Add(save);

        }

        private void latlng_Changed(object sender, TextChangedEventArgs e)
        {
            double lat = 0;
            double lng = 0;
            try
            {
                lat = Convert.ToDouble(latitude.Text);
            }
            catch (NullReferenceException ex)
            {
                lat = 0;
            }
            catch (FormatException ex)
            {
                lat = 0;
            }
            try {
                lng = Convert.ToDouble(longitude.Text);
            } catch (NullReferenceException ex) {
                lng = 0;
            } catch (FormatException ex) {
                lng = 0;
            }
            context.actualMap.SetLatLng(lat, lng);
        }

        private void save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Place.Name = name.Text;
            try
            {
                Place.Latitude = Convert.ToDouble(latitude.Text);
            } catch (FormatException ex) {
                ErrorMessage("Latitude musí být číslo");
                latitude.Background = new SolidColorBrush(Color.FromRgb(200, 100, 100));
                return;
            }
            try
            {
                Place.Longitude = Convert.ToDouble(longitude.Text);
            }
            catch (FormatException ex)
            {
                ErrorMessage("Longitude musí být číslo");
                longitude.Background = new SolidColorBrush(Color.FromRgb(200, 100, 100));
                return;
            }
            context.dataLoader.EditPlace(Place);
            context.dataLoader.Save();
            context.actualAction = Context.PLACES;
        }

        public void ErrorMessage(string text)
        {
            // @todo
        }
    }
}
