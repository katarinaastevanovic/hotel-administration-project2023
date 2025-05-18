using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Windows;
using SR09_2022POP2023.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservations.Repository;

namespace SR09_2022POP2023.Repository
{
    internal class PriceRepository : IPriceRepository
    {
        public List<Price> GetAll()
        {
            var priceList = new List<Price>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                var commandText = "SELECT p.*, rt.* FROM dbo.price p INNER JOIN dbo.room_type rt ON p.price_room_type_id = rt.room_type_id";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "price");

                foreach (DataRow row in dataSet.Tables["price"]!.Rows)
                {
                    var price = new Price()
                    {/*price_room_type_id, price_reservation_type, price_value, price_is_active*/
                        Id = (int)row["price_id"],
                        RoomType = new RoomType()
                        {
                            Id = (int)row["room_type_id"],//ne diraj ovo
                            Name = (string)row["room_type_name"],
                            IsActive = (bool)row["room_type_is_active"]
                        },
                        ReservationType = (ReservationType)row["price_reservation_type"],
                        PriceValue = (double)row["price_value"],
                        IsActive = (bool)row["price_is_active"],
                       
                    };

                    priceList.Add(price);
                }
            }

            return priceList;
        }

        public int Insert(Price price)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
            INSERT INTO dbo.price (price_room_type_id, price_reservation_type, price_value, price_is_active)
            OUTPUT inserted.price_id
            VALUES (@price_room_type_id, @price_reservation_type, @price_value, @price_is_active)
        ";

                command.Parameters.Add(new SqlParameter("price_room_type_id", price.RoomType.Id));
                command.Parameters.Add(new SqlParameter("price_reservation_type", price.ReservationType));
                command.Parameters.Add(new SqlParameter("price_value", price.PriceValue));
                command.Parameters.Add(new SqlParameter("price_is_active", price.IsActive));


                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Price price)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
            UPDATE dbo.price 
            SET price_room_type_id=@price_room_type_id, price_reservation_type=@price_reservation_type, 
            price_value=@price_value, price_is_active=@price_is_active
            WHERE price_id=@price_id
        ";

                command.Parameters.Add(new SqlParameter("price_id", price.Id));
                command.Parameters.Add(new SqlParameter("price_room_type_id", price.RoomType.Id));
                command.Parameters.Add(new SqlParameter("price_reservation_type", price.ReservationType));
                command.Parameters.Add(new SqlParameter("price_value", price.PriceValue));
                command.Parameters.Add(new SqlParameter("price_is_active", price.IsActive));

                command.ExecuteNonQuery();
            }
        }

        public bool PriceIdExists(int priceId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM dbo.price WHERE price_id = @price_id";
                command.Parameters.Add(new SqlParameter("price_id", priceId));

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public void Save(List<Price> priceList)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (Price price in priceList)
                        {
                            if (PriceIdExists(price.Id))
                            {
                                Update(price);
                            }
                            else
                            {
                                price.Id = Insert(price);
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
