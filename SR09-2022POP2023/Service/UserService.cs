using HotelReservations.Model;
using HotelReservations.Repository;
using HotelReservations.Windows;
using SR09_2022POP2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Service
{
    public class UserService
    {
        IUserRepository userRepository;
        public static User loggedUser;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public List<User> GetAllUsers()
        {
            return Hotel.GetInstance().Users;
        }
        public List<User> GetAllActiveUsers()
        {
            return Hotel.GetInstance().Users.Where(r => r.IsActive).ToList();

        }


        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                user.Id = GetNextIdValue();
                Hotel.GetInstance().Users.Add(user);
            }
            else
            {
                var index = Hotel.GetInstance().Users.FindIndex(r => r.Id == user.Id);
                Hotel.GetInstance().Users[index] = user;
            }
        }

        public int GetNextIdValue()
        {
            return Hotel.GetInstance().Users.Max(r => r.Id) + 1;
        }

        public void Delete(int userId)
        {
            int idToRemove = userId;

            User userToRemove = Hotel.GetInstance().Users.Find(user => user.Id == idToRemove);
            if (userToRemove != null)
            {
                Hotel.GetInstance().Users.Remove(userToRemove);
            }

        }

        public List<User> GetAllUsersByUsername(string startingWith)
        {
            var users = Hotel.GetInstance().Users;
            var filteredUsers = users.FindAll((u) => u.Username.StartsWith(startingWith));
            return filteredUsers;
        }

        public User ValidateLogin(string username, string password)
        {
            // Dobavi korisnika sa datim korisničkim imenom
            var user = userRepository.GetAll().FirstOrDefault(u => u.Username == username);

            // Ako korisnik ne postoji ili lozinka nije ispravna, vrati false
            if (user == null || user.Password != password)
            {
                return null;
            }

            return user;
        }
       


    }
}
