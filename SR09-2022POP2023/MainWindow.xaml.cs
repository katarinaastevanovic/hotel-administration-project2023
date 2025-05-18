using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using SR09_2022POP2023.Model;
using SR09_2022POP2023.Windows;
using System;
using System.Collections.Generic;
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

namespace HotelReservations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      //  private User user;

        public MainWindow()
        {
            InitializeComponent();
            //this.user = user;
            AdjustMenuButtonsVisibility();
        }

      
        private void RoomsMI_Click(object sender, RoutedEventArgs e)
        {
            // Provera da li je trenutni korisnik administrator
            if (UserService.loggedUser != null && UserService.loggedUser.UserType == UserType.Administrator.ToString())
            {
            
                var roomsWindow = new Rooms();
                roomsWindow.Show();
            }
            else
            {
                MessageBox.Show("Nemate dozvolu za pristup ovoj funkciji.");
            }
        }

        private void UsersMI_Click(object sender, RoutedEventArgs e)
        {
            if (UserService.loggedUser != null && UserService.loggedUser.UserType == UserType.Administrator.ToString())
            {
                var usersWindow = new Users();
                usersWindow.Show();
            }
            else
            {
                MessageBox.Show("Nemate dozvolu za pristup ovoj funkciji.");
            }
        }
        private void GuestsMI_Click(object sender, RoutedEventArgs e)
        {
            if (UserService.loggedUser != null && UserService.loggedUser.UserType == UserType.Administrator.ToString())
            {
                var guestsWindow = new Guests();
            guestsWindow.Show();
            }
            else
            {
                MessageBox.Show("Nemate dozvolu za pristup ovoj funkciji.");
            }
        }
        private void RoomTypesMI_Click(object sender, RoutedEventArgs e)
        {
            if (UserService.loggedUser != null && UserService.loggedUser.UserType == UserType.Administrator.ToString())
            {
                var roomTypesWindow = new RoomTypes();
                roomTypesWindow.Show();
            }
            else
            {
                MessageBox.Show("Nemate dozvolu za pristup ovoj funkciji.");
            }
        }

        private void PricesMI_Click(object sender, RoutedEventArgs e)
        {
            if (UserService.loggedUser != null && UserService.loggedUser.UserType == UserType.Administrator.ToString())
            {
                var pricesWindow = new Prices();
                pricesWindow.Show();
            }
            else
            {
                MessageBox.Show("Nemate dozvolu za pristup ovoj funkciji.");
            }
        }

        private void ReservationsMI_Click(object sender, RoutedEventArgs e)
        {
            if (UserService.loggedUser != null && UserService.loggedUser.UserType == UserType.Receptionist.ToString())
            {
                var reservationsWindow = new Reservations();
                reservationsWindow.Show();
            }
            else
            {
                MessageBox.Show("Nemate dozvolu za pristup ovoj funkciji.");
            }
        }

        private void LogoutMI_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Logout clicked!");
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
        private void AdjustMenuButtonsVisibility()
        {/*
            
            if (user.UserType == UserType.Administrator)
            {
                // Administrator može videti sve dugmadi
                ReservationsMI.Visibility = Visibility.Collapsed;
                RoomsMI.Visibility = Visibility.Visible;
                UsersMI.Visibility = Visibility.Visible;
                GuestsMI.Visibility = Visibility.Visible;
                RoomTypesMI.Visibility = Visibility.Visible;
                PricesMI.Visibility = Visibility.Visible;
            }
            else if (user.UserType == UserType.Receptionist)
            {
                // Receptionist može videti samo dugme za Reservations
                RoomsMI.Visibility = Visibility.Collapsed;
                UsersMI.Visibility = Visibility.Collapsed;
                GuestsMI.Visibility = Visibility.Visible;
                RoomTypesMI.Visibility = Visibility.Collapsed;
                PricesMI.Visibility = Visibility.Collapsed;
                ReservationsMI.Visibility = Visibility.Visible;
            }*/
        }





    }
}
