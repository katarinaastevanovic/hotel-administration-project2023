using SR09_2022POP2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelReservations.Model
{
    public class Reservation
    {
        public int Id { get; set; }

        public Room Room { get; set; }

        public int GuestId { get; set; }
        public ReservationType ReservationType { get; set; }
       
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TotalPrice { get; set; }
       // public string GuestsIds => string.Join(", ", Guests.Select(g => g.Id));
        public bool IsActive { get; set; } = true;
        //public List<Guest> Guests { get; set; }



        public Reservation Clone()
        {
            var clone = new Reservation();
            clone.Id = Id;
            clone.Room = Room;
            clone.ReservationType = ReservationType;
            clone.GuestId = GuestId;
            clone.StartDateTime = StartDateTime;
            clone.EndDateTime = EndDateTime;
            clone.TotalPrice = TotalPrice;
            clone.IsActive = IsActive;

            return clone;
        }

    }
}
