using Senior_Bilet_refactoring_27_10_26.factorys;
using Senior_Bilet_refactoring_27_10_26.services;

namespace Senior_Bilet_refactoring_27_10_26
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //фабрики
            var guestFactory = new GuestFactory();
            var bookingFactory = new BookingFactory();
            var roomFactory = new RoomFactory();

            //сервисы
            var roomService = new RoomService(roomFactory);
            var guestService = new GuestService(guestFactory);
            var costCalculatorService = new CostCalculatorService(roomService, guestService);
            var bookingService = new BookingService(roomService, guestService, costCalculatorService, bookingFactory);

            //UI
            var ui = new ConsoleUI(bookingService, roomService);

            //TEST
            roomService.AddRoom("400", "standard");
            roomService.AddRoom("408", "lux");
            roomService.AddRoom("412", "suite");
            guestService.AddGuest("Алексей", "+7991*******", "leshaMolodez@mail.ru");
            guestService.AddGuest("Марина", "+7901*******", "MarinaUstala@mail.ru");

            //бронирование стандартного номера на 2 дня (ожидание 7080)
            string id1 = bookingService.Create("Алексей", "400", new DateOnly(2026, 10, 30), new DateOnly(2026, 11, 1), "");
            Console.WriteLine($"Создано: {id1}");

            //бронирование люкса на 7 дней по промо WELCOME10 (ожидание 7168,5)
            string id2 = bookingService.Create("Марина", "408", new DateOnly(2026, 10, 30), new DateOnly(2026, 11, 6), "WELCOME10");
            Console.WriteLine($"Создано: {id2}");

            //отмена бронирования (ожидается штраф 1560)
            bookingService.Cancel("BK0001");

            //попытка занять уже забронированный номер
            try { bookingService.Create("Марина", "408", new(2026, 10, 30), new(2026, 11, 1), ""); }
            catch (Exception e) { Console.WriteLine($"Ошибка: {e.Message}"); }


            //Гость с уровнем лояльности 3 
            var alexey = guestService.FindByName("Алексей");
            if (alexey != null) alexey.LoylLvl = 3; //для тестирования искусственно повышен уровень до 3

            string id4 = bookingService.Create("Алексей", "400", new DateOnly(2026, 10, 30), new DateOnly(2026, 11, 1), "");
            Console.WriteLine($"Создано (лояльность 3 ур.): {id4}");

            //бронирование в декабре
            string id3 = bookingService.Create("Алексей", "412", new DateOnly(2026, 12, 20), new DateOnly(2026, 12, 30), "");
            Console.WriteLine($"Создано: {id3}");

            
            //инфо
            ui.PrintAllBookings();

            ui.PrintAvailableRooms();

            Console.WriteLine($"\nОбщая выручка: {bookingService.GetRevenue():F2}");

            ui.PrintReceipt("BK0004");
        }
    }
}
