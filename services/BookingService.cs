using Senior_Bilet_refactoring_27_10_26.factorys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senior_Bilet_refactoring_27_10_26.services
{
    public class BookingService
    {
        private readonly List<Bookings> _bookings = new List<Bookings>();

        private readonly RoomService _roomService;
        private readonly GuestService _guestService;
        private readonly CostCalculatorService _costCalculatorService;
        private readonly BookingFactory _bookingFactory;

        public BookingService(RoomService roomService, GuestService guestService, CostCalculatorService costCalculatorService, BookingFactory bookingFactory)
        {
            _roomService = roomService;
            _guestService = guestService;
            _costCalculatorService = costCalculatorService;
            _bookingFactory = bookingFactory;
        }
        //создание бронирования с валидацией дат заезда и выезда + проверка на наличие гостя и номера (со статусом)
        public string Create(string guestName, string roomNumber, DateOnly checkIn, DateOnly checkOut, string promo)
        {
            if (checkOut <= checkIn) throw new ArgumentException("Дата выезда должна быть позже заезда");

            var guest = _guestService.FindByName(guestName) ?? throw new InvalidOperationException("Гость не найден");
            var room = _roomService.FindNumber(roomNumber) ?? throw new InvalidOperationException("Номер не найден");
            if (!room.Available) throw new InvalidOperationException("Номер уже занят");

            int nights = checkOut.DayNumber - checkIn.DayNumber; //подсчет кол-ва ночей бронирования
            double Sum = _costCalculatorService.Calculate(room, nights, promo, guest, checkIn); //вызов метода расчета общей суммы за проживание

            //создание (фабрика)
            var booking = _bookingFactory.Create(guest, room, checkIn, checkOut, Sum);

            _bookings.Add(booking);
            _roomService.MarkRoomUnavailable(roomNumber);
            _guestService.IncreaseLoyal(guest);

            return booking.Id;
        }
        //отмена брони (статус)
        public void Cancel(string bookingId)
        {
            var booking = _bookings.FirstOrDefault(item => item.Id == bookingId) ?? throw new InvalidOperationException("бронирование не найдено");
            if (booking.Status == "cancell") throw new InvalidOperationException("бронирование уже отменено");

            double penalty = _costCalculatorService.CalculatePenalty(booking.SumPrice);
            booking.Status = "cancell";
            _roomService.MarkRoomAvailable(booking.Room.Number);

            Console.WriteLine($"бронирование {bookingId} отменено. Штраф: {penalty:C}");
        }
        //поиск брони по ИД
        public Bookings FindById(string bookingId) => _bookings.FirstOrDefault(item => item.Id == bookingId);
        //получение списка всех бронец
        public List<Bookings> GetAll() => _bookings;
        //расчет дохода по подтвержденным броням
        public double GetRevenue() => _bookings.Where(item => item.Status == "confirm").Sum(item => item.SumPrice);
    }
}
    
