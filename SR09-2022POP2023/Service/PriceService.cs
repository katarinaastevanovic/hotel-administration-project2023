using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR09_2022POP2023.Service
{
    public class PriceService
    {
        public List<Price> GetAllPrices()
        {
            return Hotel.GetInstance().Prices;
        }

        //public List<Price> GetAllFreePrices()
        //{
        //    return Hotel.GetInstance().GuePricessts.Where(g => g.ReservationId == 0).ToList();
        //}

        public List<Price> GetAllActivePrices()
        {
            return Hotel.GetInstance().Prices.Where(g => g.IsActive).ToList();
        }

        public Price GetPriceById(int id)
        {
            return Hotel.GetInstance().Prices.FirstOrDefault(x => x.Id == id);
        }

        public void SavePrice(Price price)
        {
            if (price.Id == 0)
            {
                price.Id = GetNextIdValue();
                Hotel.GetInstance().Prices.Add(price);
            }
            else
            {
                var index = Hotel.GetInstance().Prices.FindIndex(r => r.Id == price.Id);
                Hotel.GetInstance().Prices[index] = price;
            }
        }



        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().Prices.Count == 0)
            {
                return 1;
            }
            else
            {
                return Hotel.GetInstance().Prices.Max(r => r.Id) + 1;
            }
        }

        public void Delete(int priceId)
        {
            int idToRemove = priceId;

            Price priceToRemove = Hotel.GetInstance().Prices.Find(price => price.Id == idToRemove);
            if (priceToRemove != null)
            {
                Hotel.GetInstance().Prices.Remove(priceToRemove);
            }

        }

    }
}


