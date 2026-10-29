using System;
using System.Collections.Generic;

namespace Senior_Bilet_refactoring_27_10_26.factorys
{
    public class BookingFactory
    {
        private int _counter = 0;

        public Bookings Create(Guests guest, Rooms room, DateOnly checkIn, DateOnly checkOut, double sumPrice)
        {
            _counter++;
            return new Bookings
            {
                Id = $"{AppConst.BookingIDPrefix}{_counter.ToString(AppConst.BookingIDFormat)}",
                Guest = guest,
                Room = room,
                CheckIn = checkIn,
                CheckOut = checkOut,
                SumPrice = sumPrice,
                Status = "confirm"
            };
        }
    }
}
