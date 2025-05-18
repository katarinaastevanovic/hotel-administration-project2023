using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using SR09_2022POP2023.Model;
using SR09_2022POP2023.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddEditPrice : Window
    {
        private PriceService priceService;

        private Price contextPrice;
        public AddEditPrice(Price? price = null)
        {
            if (price == null)
            {
                contextPrice = new Price();
            }
            else
            {
                contextPrice = price.Clone();
            }

            InitializeComponent();
            priceService = new PriceService();

            AdjustWindow(price);

            this.DataContext = contextPrice;
        }

        public void AdjustWindow(Price? price = null)
        {
            if (price != null)
            {
                Title = "Edit Price";
            }
            else
            {
                Title = "Add Price";
            }

            // DOHVATI OVE PODATKE PREKO SERVISA, PLS
            RoomTypeService roomTypeService = new RoomTypeService();
            var roomTypes = roomTypeService.GetAllActiveRoomTypes();
            var reservationTypes = Enum.GetValues(typeof(ReservationType)).Cast<ReservationType>().ToList();

            RoomTypesCB.ItemsSource = roomTypes;
            ReservationTypesCB.ItemsSource = reservationTypes;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextPrice.RoomType == null || contextPrice.ReservationType == null || contextPrice.PriceValue == null || contextPrice.PriceValue <= 0)
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            priceService.SavePrice(contextPrice);

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