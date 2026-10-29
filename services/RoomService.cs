using Senior_Bilet_refactoring_27_10_26.factorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Senior_Bilet_refactoring_27_10_26.services
{
    public class RoomService
    {
        private readonly List<Rooms> _rooms = new List<Rooms>();

        //связь с фабрикой
        private readonly RoomFactory _roomFactory;

        //конструктор
        public RoomService(RoomFactory roomFactory)
        {
            _roomFactory = roomFactory;
        }
        //добавление комнаты (фабрикой)
        public void AddRoom(string number, string type)
        {
            var room = _roomFactory.Create(number, type, AppConst.MinSum * GetRoomTypeMultiplier(type));

            _rooms.Add(room);

        }
        //поиск комнаты по номеру
        public Rooms FindNumber(string roomNumber)
        {
            return _rooms.FirstOrDefault(item => item.Number == roomNumber);
        }
        //получение списка свободных комнат
        public List<Rooms> GetAvailableRooms()
        {
            return _rooms.Where(item => item.Available).ToList();
        }
        //смена статуса комнаты на занято
        public void MarkRoomUnavailable(string roomNumber)
        {
            var room = FindNumber(roomNumber);
            if (room != null)
            {
                room.Available = false;
            }
        }
        //смена статуса комнаты на свободно 
        public void MarkRoomAvailable(string roomNumber)
        {
            var room = FindNumber(roomNumber);
            if (room != null)
            {
                room.Available = true;
            }
        }
        //наценка за тип номера
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