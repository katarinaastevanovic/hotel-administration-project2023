using SR09_2022POP2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    [Serializable]
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public bool IsActive { get; set; } = true;

        public RoomType Clone()
        {
            var clone = new RoomType();
            clone.Id = Id;
            clone.Name = Name;
            clone.IsActive = IsActive;

            return clone;
        }
    }
}
