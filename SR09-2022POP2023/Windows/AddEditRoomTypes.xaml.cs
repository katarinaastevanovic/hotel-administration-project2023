using HotelReservations.Model;
using HotelReservations.Service;
using SR09_2022POP2023.Model;
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
    /// Interaction logic for AddEditRoomType.xaml
    /// </summary>
    public partial class AddEditRoomType : Window
    {
        private RoomTypeService roomTypeService;

        private RoomType contextRoomType;
        public AddEditRoomType(RoomType? roomType = null)
        {
            if (roomType == null)
            {
                contextRoomType = new RoomType();
            }
            else
            {
                contextRoomType = roomType.Clone();
            }

            InitializeComponent();
            roomTypeService = new RoomTypeService();

            AdjustWindow(roomType);

            this.DataContext = contextRoomType;
        }

        private void AdjustWindow(RoomType roomType = null)
        {
            // Adjust window properties based on whether it's an edit or add operation.
            if (roomType != null)
            {
                Title = "Edit RoomType";
            }
            else
            {
                Title = "Add RoomType";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(contextRoomType.Name))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!roomTypeService.IsIdNumberUnique(contextRoomType.Name))
            {
                MessageBox.Show("IDNumber must be unique. A user with the same Name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            roomTypeService.SaveRoomType(contextRoomType);

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
