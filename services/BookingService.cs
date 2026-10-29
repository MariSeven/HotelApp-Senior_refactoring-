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

        public string Create(string guestName, string roomNumber, DateOnly checkIn, DateOnly checkOut, string promo)
        {
            if (checkOut <= checkIn) throw new ArgumentException("Дата выезда должна быть позже заезда");

            var guest = _guestService.FindByName(guestName) ?? throw new InvalidOperationException("Гость не найден");
            var room = _roomService.FindNumber(roomNumber) ?? throw new InvalidOperationException("Номер не найден");
            if (!room.Available) throw new InvalidOperationException("Номер уже занят");

            int nights = checkOut.DayNumber - checkIn.DayNumber;
            double Sum = _costCalculatorService.Calculate(room, nights, promo, guest, checkIn);

            //создание (фабрика)
            var booking = _bookingFactory.Create(guest, room, checkIn, checkOut, Sum);

            _bookings.Add(booking);
            _roomService.MarkRoomUnavailable(roomNumber);
            _guestService.IncreaseLoyal(guest);

            return booking.Id;
        }

        public void Cancel(string bookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == bookingId) ?? throw new InvalidOperationException("Бронирование не найдено");
            if (booking.Status == "cancelled") throw new InvalidOperationException("Уже отменено");

            double penalty = _pricingService.CalculatePenalty(booking.SumPrice);
            booking.Status = "cancelled";
            _roomService.MarkRoomAvailable(booking.Room.Number);

            Console.WriteLine($"Бронирование {bookingId} отменено. Штраф: {penalty:C}");
        }
        public Booking FindById(string bookingId) => _bookings.FirstOrDefault(b => b.Id == bookingId);
        public List<Booking> GetAll() => _bookings;
        public double GetRevenue() => _bookings.Where(b => b.Status == "confirmed").Sum(b => b.SumPrice);
    }
}
    
