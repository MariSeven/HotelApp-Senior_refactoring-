using Senior_Bilet_refactoring_27_10_26.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senior_Bilet_refactoring_27_10_26
{
    //интерфейс пользовательский
    public class ConsoleUI
    {
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;

        public ConsoleUI(BookingService bookingService, RoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }
        //"Печать" чека
        public void PrintReceipt(string bookingId)
        {
            var booking = _bookingService.FindById(bookingId);
            if (booking == null) { Console.WriteLine("Не найдено"); return; }

            Console.WriteLine("\n===== ЧЕК ====="); //Михаил Александрович, пощадите
            Console.WriteLine($"ID: {booking.Id}"); //Я занимаюсь этим уже 4 часа
            Console.WriteLine($"Гость: {booking.Guest.Name}"); //Не надо так больше, пожалуйста (Т_Т)
            Console.WriteLine($"Номер: {booking.Room.Number} ({booking.Room.Type})");
            Console.WriteLine($"Заезд: {booking.CheckIn}"); 
            Console.WriteLine($"Выезд: {booking.CheckOut}");
            Console.WriteLine($"Статус: {booking.Status}");
            Console.WriteLine($"ИТОГО: {booking.SumPrice:F2} р.");
            Console.WriteLine("===============\n");
        }

        public void PrintAllBookings()
        {
            Console.WriteLine("\n=== Все бронирования ==="); // <-- вместо коммента
            foreach (var q in _bookingService.GetAll())
                Console.WriteLine($"{q.Id} | {q.Guest.Name} | {q.Room.Number} | {q.Status} | {q.SumPrice:F2} р.");
        }

        public void PrintAvailableRooms()
        {
            Console.WriteLine("\n=== Свободные номера ==="); // <-- вместо коммента
            foreach (var q in _roomService.GetAvailableRooms())
                Console.WriteLine($"{q.Number} | {q.Type} | {q.Price:F2} р.");
        }
    }
}
