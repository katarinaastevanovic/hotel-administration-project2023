using HotelReservations.Model;
using HotelReservations.Service;
using SR09_2022POP2023.Model;
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
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        private UserService userService;

        private User contextUser;
        public AddEditUser(User? user = null)
        {
            if (user == null)
            {
                contextUser = new User();
            }
            else
            {
                contextUser = user.Clone();
            }

            InitializeComponent();
            userService = new UserService();

            AdjustWindow(user);

            this.DataContext = contextUser;
        }

        private void AdjustWindow(User user = null)
        {
            // TODO: Inicijalizovati combobox za selekciju tipa korisnika
            UserTypeCB.Items.Add(typeof(Receptionist).Name);
            UserTypeCB.Items.Add(typeof(Administrator).Name);

            if (user != null)
            {
                Title = "Edit user";

                UserTypeCB.SelectedItem = user.GetType().Name;
                UserTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add user";
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(contextUser.Username) || string.IsNullOrEmpty(contextUser.Surname) || string.IsNullOrEmpty(contextUser.JMBG) || string.IsNullOrEmpty(contextUser.Password))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Provera dužine JMBG
            if (contextUser.JMBG.Length != 13)
            {
                MessageBox.Show("JMBG must have a length of 13 characters.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

   

            userService.SaveUser(contextUser);

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
