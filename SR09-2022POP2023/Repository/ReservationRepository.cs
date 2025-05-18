using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using SR09_2022POP2023;
using SR09_2022POP2023.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Policy;

namespace HotelReservations.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        public List<Reservation> GetAll()
        {

            var reservations = new List<Reservation>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                var commandText = "SELECT r.*,rm.*,rt.* FROM dbo.reservation r " +
                    "INNER JOIN dbo.room rm ON rm.room_id = r.reservation_room_id " +
                    "INNER JOIN dbo.room_type rt ON rt.room_type_id = rm.room_type_id";

                SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "reservation");

                foreach (DataRow row in dataSet.Tables["reservation"]!.Rows)
                {
                    var reservation = new Reservation()
                    {
                        Id = (int)row["reservation_id"],
                        Room  = new Room()
                        {
                            Id = (int)row["reservation_room_id"],
                            RoomNumber = (string)row["room_number"],
                            HasTV = (bool)row["has_TV"],
                            HasMiniBar = (bool)row["has_mini_bar"],
                            IsActive = (bool)row["room_is_active"],
                            RoomType = new RoomType()
                            {
                                Id = (int)row["room_type_id"],
                                Name = (string)row["room_type_name"],
                                IsActive = (bool)row["room_type_is_active"]
                            }
                        },
                       
                        GuestId = (int)row["reservation_guest_id"],
                        ReservationType = (ReservationType)row["reservation_type"],
                        StartDateTime = (DateTime)row["reservation_start_date_time"],
                        EndDateTime = (DateTime)row["reservation_end_date_time"],
                        TotalPrice = (double)row["reservation_total_price"],
                        IsActive = (bool)row["reservation_is_active"],

                  
                    };
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public int Insert(Reservation reservation)

        {
            
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                INSERT INTO dbo.reservation (reservation_room_id,reservation_guest_id, reservation_type, reservation_start_date_time, reservation_end_date_time,
                reservation_total_price, reservation_is_active)
                OUTPUT inserted.reservation_id
                VALUES (@reservation_room_id,@reservation_guest_id, @reservation_type, @reservation_start_date_time, @reservation_end_date_time, @reservation_total_price,
                @reservation_is_active)";

                command.Parameters.Add(new SqlParameter("reservation_room_id", reservation.Room.Id));
                command.Parameters.Add(new SqlParameter("reservation_guest_id", reservation.GuestId));
                command.Parameters.Add(new SqlParameter("reservation_type", reservation.ReservationType));
                command.Parameters.Add(new SqlParameter("reservation_start_date_time", reservation.StartDateTime));
                command.Parameters.Add(new SqlParameter("reservation_end_date_time", reservation.EndDateTime));
                command.Parameters.Add(new SqlParameter("reservation_total_price", reservation.TotalPrice));
                command.Parameters.Add(new SqlParameter("reservation_is_active", reservation.IsActive));

                
                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Reservation reservation)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
            UPDATE dbo.reservation 
            SET reservation_room_id=@reservation_room_id, reservation_guest_id=@reservation_guest_id, reservation_type=@reservation_type,
            reservation_start_date_time=@reservation_start_date_time,reservation_end_date_time=@reservation_end_date_time,
            reservation_total_price=@reservation_total_price
            WHERE reservation_id=@reservation_id AND reservation_is_active=@reservation_is_active";

                command.Parameters.Add(new SqlParameter("reservation_id", reservation.Id));
                command.Parameters.Add(new SqlParameter("reservation_room_id", reservation.Room.Id));
                command.Parameters.Add(new SqlParameter("reservation_guest_id", reservation.ReservationType));
                command.Parameters.Add(new SqlParameter("reservation_type", reservation.ReservationType));
                command.Parameters.Add(new SqlParameter("reservation_start_date_time", reservation.StartDateTime));
                command.Parameters.Add(new SqlParameter("reservation_end_date_time", reservation.EndDateTime));
                command.Parameters.Add(new SqlParameter("reservation_total_price", reservation.TotalPrice));
                command.Parameters.Add(new SqlParameter("reservation_is_active", reservation.IsActive));

                command.ExecuteNonQuery();
            }
        }

        public bool ReservationIdExists(int reservationId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM dbo.reservation WHERE reservation_id = @reservation_id";
                command.Parameters.Add(new SqlParameter("reservation_id", reservationId));

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public void Save(List<Reservation> reservationList)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (Reservation reservation in reservationList)
                        {
                            if (ReservationIdExists(reservation.Id))
                            {
                                Update(reservation);
                            }
                            else
                            {
                                reservation.Id = Insert(reservation);
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