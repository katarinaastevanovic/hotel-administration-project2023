using HotelReservations.Model;
using HotelReservations.Service;
using SR09_2022POP2023.Service;
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

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for AddEditRoom.xaml
    /// </summary>
    public partial class AddEditRoom : Window
    {
        private RoomService roomService;

        private Room contextRoom;
        public AddEditRoom(Room? room = null)
        {
            if(room == null)
            {
                contextRoom = new Room();
            }
            else
            {
                contextRoom = room.Clone();
            }
                      
            InitializeComponent();
            roomService = new RoomService();

            AdjustWindow(room);

            this.DataContext = contextRoom;
        }

        public void AdjustWindow(Room? room = null)
        {
            if (room != null)
            {
                Title = "Edit Room";
            }
            else
            {
                Title = "Add Room";
            }

            
            roomService = new RoomService();
            var roomTypes = roomService.GetAllRoomTypes();
            RoomTypesCB.ItemsSource = roomTypes;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(contextRoom.RoomNumber))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!roomService.IsIdNumberUnique(contextRoom.RoomNumber))
            {
                MessageBox.Show("RoomNumber must be unique. A user with the same Name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            roomService.SaveRoom(contextRoom);

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
