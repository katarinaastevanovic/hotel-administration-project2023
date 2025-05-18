using HotelReservations.Exceptions;
using HotelReservations.Model;
using SR09_2022POP2023;
using SR09_2022POP2023.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HotelReservations.Windows;
using SR09_2022POP2023.Windows;

namespace HotelReservations.Repository
{
    // Koristimo repozitorijume da bismo izolovali operacije
    // vezane za eksternu memoriju. Sad koristimo csv, sutra bazu,
    // potrebno nam je lako uvođenje promena.
    // Na narednim terminima ćemo "apstrahovati" repozitorijume
    // interfejsima.
    public class UserRepository : IUserRepository
    {
        public List<User> GetAll()
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                var commandText = "SELECT * FROM dbo.[user]";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "user");

                foreach (DataRow row in dataSet.Tables["user"]!.Rows)
                {
                    var user = new User()
                    {
                        Id = (int)row["user_id"],
                        Name = (string)row["user_name"],
                        Surname = (string)row["user_surname"],
                        JMBG = (string)row["user_id_number"],
                        Username = (string)row["user_username"],
                        Password = (string)row["user_password"],
                        UserType = (string)row["user_type"],
                        IsActive = (bool)row["user_is_active"]

                    };

                    users.Add(user);
                }
            }
            return users;
        }

        public int Insert(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
            INSERT INTO dbo.[user] (user_name,user_surname,user_id_number,user_username,user_password,user_type, user_is_active)
            OUTPUT inserted.user_id
            VALUES (@user_name,@user_surname,@user_id_number,@user_username,@user_password,@user_type,@user_is_active)
        ";

                command.Parameters.Add(new SqlParameter("user_name", user.Name));
                command.Parameters.Add(new SqlParameter("user_surname", user.Surname));
                command.Parameters.Add(new SqlParameter("user_id_number", user.JMBG));
                command.Parameters.Add(new SqlParameter("user_username", user.Username));
                command.Parameters.Add(new SqlParameter("user_password", user.Password));
                command.Parameters.Add(new SqlParameter("user_type", user.UserType));
                command.Parameters.Add(new SqlParameter("user_is_active", user.IsActive));
                return (int)command.ExecuteScalar();
            }
        }

        public void Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
            UPDATE dbo.[user]
            SET user_name=@user_name,user_surname=@user_surname,
            user_id_number=@user_id_number,user_username=@user_username,user_password=@user_password, user_type=@user_type,
            user_is_active=@user_is_active
            WHERE user_id=" + user.Id;

                command.Parameters.Add(new SqlParameter("@user_name", user.Name));
                command.Parameters.Add(new SqlParameter("@user_surname", user.Surname));
                command.Parameters.Add(new SqlParameter("@user_id_number", user.JMBG));
                command.Parameters.Add(new SqlParameter("@user_username", user.Username));
                command.Parameters.Add(new SqlParameter("@user_password", user.Password));
                command.Parameters.Add(new SqlParameter("user_type", user.UserType));
                command.Parameters.Add(new SqlParameter("@user_is_active", user.IsActive));

                command.ExecuteNonQuery();
            }
        }


        public bool UserIdExists(int userId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM dbo.[user] WHERE user_id = @user_id";
                command.Parameters.Add(new SqlParameter("user_id", userId));

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public void Save(List<User> userList)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (User user in userList)
                        {
                            if (UserIdExists(user.Id))
                            {
                                Update(user);
                            }
                            else
                            {
                                user.Id = Insert(user);
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

