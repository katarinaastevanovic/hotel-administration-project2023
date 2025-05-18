using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace SR09_2022POP2023.Windows
{
    public partial class Guests : Window
    {
        private GuestService guestService;
        private ICollectionView view;

        public Guests()
        {
            guestService = new GuestService();

            InitializeComponent();
            FillData();
        }

        // TODO: Korisničke lozinke ne bi trebalo prikazati
        private void FillData()
        {
           
            var guests = guestService.GetAllActiveGuests();

            view = CollectionViewSource.GetDefaultView(guests);
            view.Filter = DoFilter;
            GuestsDG.ItemsSource = null;
            GuestsDG.ItemsSource = view;
            GuestsDG.IsSynchronizedWithCurrentItem = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addGuestWindow = new AddEditGuest();

            Hide();
            if (addGuestWindow.ShowDialog() == true)
            {
                FillData();
            }
            view.Refresh();
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest)view.CurrentItem;

            if (selectedGuest != null)
            {
                var editGuestWindow = new AddEditGuest(selectedGuest);

                Hide();

                if (editGuestWindow.ShowDialog() == true)
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

            var selectedGuest = view.CurrentItem as Guest;

            if (MessageBox.Show($"Are you sure that you want to delete guest {selectedGuest!.IDNumber}?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedGuest.IsActive = false;
                FillData();
            }
            else
            {

            }
            view.Refresh();
        }


        private bool DoFilter(object guestObject)
        {
            var guest = guestObject as Guest;

            var iDNumberSearchParam = IDNumberSearchTB.Text;

            if (guest.IDNumber.Contains(iDNumberSearchParam))
            {
                return true;
            }
            return false;
        }
        private void GuestsDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void IDNumberSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
    }
}
