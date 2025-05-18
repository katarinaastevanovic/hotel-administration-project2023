using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Repository
{
    internal interface IPriceRepository
    {
        List<Price> GetAll();
        void Save(List<Price> priceList);
    }
}
