using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR09_2022POP2023.Repository
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        void Save(List<Reservation> reservationList);


    }
}
