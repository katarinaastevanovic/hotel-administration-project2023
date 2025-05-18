// LoginForm.xaml.cs
using HotelReservations.Model;
using HotelReservations.Service;
using SR09_2022POP2023.Model;
using System.Text.Json;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class LoginForm : Window
    {
        
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Pozovite UserService kako biste proverili validnost korisničkog imena i lozinke
            UserService userService = new UserService();
            User isValidLogin = userService.ValidateLogin(username, password);
          

            if (isValidLogin != null)
            {
               // MessageBox.Show("Uspešno ste se prijavili!");
                UserService.loggedUser = isValidLogin;

                ShowMainWindow();
                Close(); // Zatvori login prozor nakon uspešne prijave
            }
            else
            {
                MessageBox.Show("Pogrešno korisničko ime ili lozinka. Pokušajte ponovo.");
            }
        }
        private void ShowMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Zatvori trenutni prozor nakon što se prikaže MainWindow
        }




        

    }
}

