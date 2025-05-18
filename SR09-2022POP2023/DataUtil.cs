using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Repository;
using SR09_2022POP2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR09_2022POP2023
{
    public class DataUtil
    {
        public static void LoadData()
        {
            Hotel hotel = Hotel.GetInstance();
            hotel.Id = 1;
            hotel.Name = "Hotel Park";
            hotel.Address = "Kod Futoskog parka...";

          

            
            try
            {
                IRoomTypeRepository roomTypeRepository = new RoomTypeRepository();
                var loadedRoomTypes = roomTypeRepository.GetAll();

                if (loadedRoomTypes != null)
                {
                    Hotel.GetInstance().RoomTypes = loadedRoomTypes;
                }


                IRoomRepository roomRepository = new RoomRepository();
                var loadedRooms = roomRepository.GetAll();

                if (loadedRooms != null)
                {
                    Hotel.GetInstance().Rooms = loadedRooms;
                }

                IGuestRepository guestRepository = new GuestRepository();
                var loadedGuests = guestRepository.GetAll();

                if (loadedGuests != null)
                {
                    Hotel.GetInstance().Guests = loadedGuests;
                }

                IUserRepository userRepository = new UserRepository();
                var loadedUsers = userRepository.GetAll();

                if (loadedUsers != null)
                {
                    Hotel.GetInstance().Users = loadedUsers;
                }


                IPriceRepository priceRepository = new PriceRepository();
                var loadedPrices = priceRepository.GetAll();

                if (loadedPrices != null)
                {
                    Hotel.GetInstance().Prices = loadedPrices;
                }


                IReservationRepository reservationRepository = new ReservationRepository();
                var loadedReservations = reservationRepository.GetAll();

                if (loadedReservations != null)
                {
                    Hotel.GetInstance().Reservations = loadedReservations;
                }

                // Samo za primer...
                //BinaryRoomRepository binaryRoomRepository = new BinaryRoomRepository();
                //var loadedRoomsFromBin = binaryRoomRepository.Load();
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
        }

        public static void PersistData()
        {
            try
            {
                // Kada se gasi program, čuvamo u rooms.txt
                // Posle toga će rooms.txt postojati (ako nešto ne pođe po zlu)
                IRoomRepository roomRepository = new RoomRepository();
                roomRepository.Save(Hotel.GetInstance().Rooms);

                IUserRepository userRepository = new UserRepository();
                userRepository.Save(Hotel.GetInstance().Users);

                IGuestRepository guestRepository = new GuestRepository();
                guestRepository.Save(Hotel.GetInstance().Guests);

                IRoomTypeRepository roomTypeRepository = new RoomTypeRepository();
                roomTypeRepository.Save(Hotel.GetInstance().RoomTypes);

                IPriceRepository priceRepository = new PriceRepository();
                priceRepository.Save(Hotel.GetInstance().Prices);

                IReservationRepository reservationRepository = new ReservationRepository();
                reservationRepository.Save(Hotel.GetInstance().Reservations);

                //BinaryRoomRepository binaryRoomRepository = new BinaryRoomRepository();
                //binaryRoomRepository.Save(Hotel.GetInstance().Rooms);

            }
            catch (CouldntPersistDataException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
        }
    }
}
