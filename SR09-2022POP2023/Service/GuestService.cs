using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class GuestService
    {
        public List<Guest> GetAllGuests()
        {
            return Hotel.GetInstance().Guests;
        }

     
        public List<Guest> GetAllActiveGuests()
        {
            return Hotel.GetInstance().Guests.Where(g => g.IsActive).ToList();
        }

        public Guest GetGuestById(int id)
        {
            return Hotel.GetInstance().Guests.FirstOrDefault(x => x.Id == id);
        }

        public void SaveGuest(Guest guest)
        {
            if (guest.Id == 0)
            {
                guest.Id = GetNextIdValue();
                Hotel.GetInstance().Guests.Add(guest);
            }
            else
            {
                var index = Hotel.GetInstance().Guests.FindIndex(r => r.Id == guest.Id);
                Hotel.GetInstance().Guests[index] = guest;
            }
        }

        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().Guests.Count == 0)
            {
                return 1;
            }
            else
            {
                return Hotel.GetInstance().Guests.Max(r => r.Id) + 1;
            }
        }

        public void Delete(int guestId)
        {
            int idToRemove = guestId;

            Guest guestToRemove = Hotel.GetInstance().Guests.Find(guest => guest.Id == idToRemove);
            if (guestToRemove != null)
            {
                Hotel.GetInstance().Guests.Remove(guestToRemove);
            }

        }

        public bool IsIdNumberUnique(string idNumber)
        {
            return Hotel.GetInstance().Guests.All(g => g.IDNumber != idNumber);
        }
    }
}