using HotelReservations.Model;
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
using System.Windows.Shapes;

using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;
using SR09_2022POP2023.Windows;
using HotelReservations.Repository;

namespace HotelReservations.Windows
{

    public partial class AddEditReservation : Window
    {
        
        private ICollectionView view;
        private ReservationService reservationService;
        private GuestService guestService;
        private RoomService roomService;
        private Reservation contextReservation;

        public AddEditReservation(Reservation? reservation = null)
        {
            var AddNew = true;
            if (reservation == null)
            {
                contextReservation = new Reservation();
                contextReservation.Room = new Room();
                contextReservation.StartDateTime = DateTime.Now;
                contextReservation.EndDateTime = DateTime.Now;  
                AddNew = true;
            }
            else
            {
                contextReservation = reservation.Clone();
                AddNew = false;
            }
            InitializeComponent();
            guestService = new GuestService();
            reservationService = new ReservationService();
            roomService = new RoomService();
            AdjustWindow(contextReservation, AddNew);
            StartDateTimePicker.SelectedDate = contextReservation.StartDateTime;
            EndDateTimePicker.SelectedDate = contextReservation.EndDateTime;
            this.DataContext = contextReservation;
        }
       
        private void AdjustWindow(Reservation reservation, bool AddNew)
        {
            ReservationTypesCB.ItemsSource = Enum.GetValues(typeof(ReservationType));
            ReservationTypesCB.SelectedItem = reservation.ReservationType;
            GuestNameCB.ItemsSource = guestService.GetAllActiveGuests();
            GuestNameCB.DisplayMemberPath = "Name";
            RoomNameCB.ItemsSource = roomService.GetAllActiveRooms();
            RoomNameCB.DisplayMemberPath = "Id";
            StartDateTimePicker.SelectedDate = reservation.StartDateTime;
            EndDateTimePicker.SelectedDate = reservation.EndDateTime;
            GuestNameCB.SelectedItem = reservation.GuestId;
            RoomNameCB.SelectedItem = reservation.Room.Id;
            ReservationTypesCB.IsEnabled = true;
            RoomNameCB.IsEnabled = true;
            GuestNameCB.IsEnabled = true;

            if (!AddNew)
            {
                Title = "Edit reservation";
            }
            else
            {
                Title = "Add reservation";
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
           if (contextReservation.StartDateTime == DateTime.MinValue || contextReservation.TotalPrice <= 0 || contextReservation.GuestId <= 0)
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            reservationService.SaveReservation(contextReservation);
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
