using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Menu: INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string category { get; set; }
        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public partial class MainWindow : Window
    {
        ObservableCollection<Menu> data = new ObservableCollection<Menu>();
        public MainWindow()
        {
            InitializeComponent();
            datagrid.ItemsSource = data;
        }

        private void onAddBtnClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var d = new Menu()
                {
                    Name = foodNameBox.Text,
                    category = categoryBox.Text,
                    Price = double.Parse(priceBox.Text),
                };
                for(int i=0;i<data.Count; i++)
                {
                    if(data[i].Name ==d.Name)
                    {

                        data[i].Price += d.Price;
                        foodNameBox.Text = "";
                        priceBox.Text = "";
                        totalBtllBox.Text = "";
                        return;
                    }
                }
                data.Add(d);
                foodNameBox.Text = "";
                priceBox.Text = "";
                totalBtllBox.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the integer value in price box");
            }
            


        }

        private void selectRowEvent(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            var s = dataGrid.SelectedItem as Menu;

            if (s != null)
            {
                data.Remove(s);
                totalBtllBox.Text = "";
            }
        }

        private void onGenerateBillBtnClicked(object sender, RoutedEventArgs e)
        {
            double s = 0;
            data.ToList().ForEach(e => s =s +  e.Price);
            totalBtllBox.Text = "Total Bill is : " + s;
        }
    }
}
