using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;

namespace HotelReservations.Windows
{
    public partial class AddEditGuest : Window
    {
        private GuestService guestService;
        private Guest contextGuest;

        public AddEditGuest(Guest guest = null)
        {
            if (guest == null)
            {
                contextGuest = new Guest();
                //contextGuest.ReservationId = 0;
            }
            else
            {
                contextGuest = guest.Clone();
            }

            InitializeComponent();
            guestService = new GuestService();
            AdjustWindow(guest);
            this.DataContext = contextGuest;
        }

        private void AdjustWindow(Guest guest = null)
        {
            // Adjust window properties based on whether it's an edit or add operation.
            if (guest != null)
            {
                Title = "Edit Guest";
            }
            else
            {
                Title = "Add Guest";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(contextGuest.Name) || string.IsNullOrEmpty(contextGuest.Surname) || string.IsNullOrEmpty(contextGuest.IDNumber))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Provera dužine IDNumber
            if (contextGuest.IDNumber.Length != 13)
            {
                MessageBox.Show("IDNumber must have a length of 13 characters.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            guestService.SaveGuest(contextGuest);
            DialogResult = true;
            Close();
        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
