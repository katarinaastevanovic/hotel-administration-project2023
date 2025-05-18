using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using SR09_2022POP2023.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SR09_2022POP2023.Windows
{
    public partial class Prices : Window
    {
        private PriceService priceService;
        private ICollectionView view;

        public Prices()
        {
            priceService = new PriceService();

            InitializeComponent();
            FillData();
        }

        // TODO: Korisničke lozinke ne bi trebalo prikazati
        private void FillData()
        {
            var prices = priceService.GetAllActivePrices();

            view = CollectionViewSource.GetDefaultView(prices);

            PricesDG.ItemsSource = null;
            PricesDG.ItemsSource = view;
            PricesDG.IsSynchronizedWithCurrentItem = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addPriceWindow = new AddEditPrice();

            Hide();
            if (addPriceWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrice = (Price)view.CurrentItem;

            if (selectedPrice != null)
            {
                var editPriceWindow = new AddEditPrice(selectedPrice);

                Hide();

                if (editPriceWindow.ShowDialog() == true)
                {
                    FillData();

                }
                view.Refresh();
                Show();
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedPrice = view.CurrentItem as Price;

            if (MessageBox.Show($"Are you sure that you want to delete price {selectedPrice!.Id}?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedPrice.IsActive = false;
                FillData();
            }
            else
            {

            }
        }
        private void PricesDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
