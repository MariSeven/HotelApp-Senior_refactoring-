using System;
using System.Collections.Generic;

namespace Senior_Bilet_refactoring_27_10_26.services
{
    internal class RoomService
    {
        private readonly List<Rooms> _rooms = new List<Rooms>();
        //Добавление комнаты (аналог конструктора)
        public void AddRoom(string number, string type, double price)
        {
            var room = new Rooms
            {
                Number = number,
                Type = type,
                Price = price,
                Available = true
            };
            _rooms.Add(room);
            Console.WriteLine($"Номер {number} добавлен");
        }
        //поиск комнаты по номеру
        public Rooms FindNumber(string roomNumber)
        {
            return _rooms.FirstOrDefault(item => item.Number == roomNumber);
        }
        //свободные номера (формируется список)
        public List<Rooms> GetAvailableRooms()
        {
            return _rooms.Where(item => item.Available).ToList();
        }

        public void MarkRoomUnavailable(string roomNumber)
        {
            var room = FindNumber(roomNumber);
            if (room != null)
                room.Available = false;
        }

        public void MarkRoomAvailable(string roomNumber)
        {
            var room = FindNumber(roomNumber);
            if (room != null)
                room.Available = true;
        }

        //расчет наценки за тип номера
        public double GetRoomTypeMultiplier(string roomType)
        {
            return roomType?.ToLower() switch
            {
                "lux" => 1 + AppConst.LuxRoomPlus,
                "suite" => 1 + AppConst.SuiteRoomPlus,
                _ => 1.0
            };
        }
    }
}
