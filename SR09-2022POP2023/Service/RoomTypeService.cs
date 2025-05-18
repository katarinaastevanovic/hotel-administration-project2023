using HotelReservations.Model;
using HotelReservations.Repository;
using HotelReservations.Windows;
using SR09_2022POP2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR09_2022POP2023.Service
{
    public class RoomTypeService
    {
    IRoomTypeRepository roomTypeRepository;
        public RoomTypeService()
    {
            roomTypeRepository = new RoomTypeRepository();
    }



        public RoomType GetRoomTypeById(int Id)
        {
            return Hotel.GetInstance().RoomTypes.FirstOrDefault(r => r.Id == Id);
        }
    public List<RoomType> GetAllRoomTypes()
    {
        return Hotel.GetInstance().RoomTypes;
    }
    public List<RoomType> GetAllActiveRoomTypes()
    {
        return Hotel.GetInstance().RoomTypes.Where(r => r.IsActive).ToList();
    }


    public void SaveRoomType(RoomType roomType)
    {
        if (roomType.Id == 0)
        {
            roomType.Id = GetNextIdValue();
            Hotel.GetInstance().RoomTypes.Add(roomType);
        }
        else
        {
            var index = Hotel.GetInstance().RoomTypes.FindIndex(r => r.Id == roomType.Id);
            Hotel.GetInstance().RoomTypes[index] = roomType;
        }
    }

    public int GetNextIdValue()
    {
        return Hotel.GetInstance().RoomTypes.Max(r => r.Id) + 1;
    }

    public void Delete(int roomTypeId)
    {
        int idToRemove = roomTypeId;

        RoomType roomTypeToRemove = Hotel.GetInstance().RoomTypes.Find(roomType => roomType.Id == idToRemove);
        if (roomTypeToRemove != null)
        {
            Hotel.GetInstance().RoomTypes.Remove(roomTypeToRemove);
        }

    }
        public bool IsIdNumberUnique(string idNumber)
        {
            return Hotel.GetInstance().RoomTypes.All(g => g.Name != idNumber);
        }
    }
}
