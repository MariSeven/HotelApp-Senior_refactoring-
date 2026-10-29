using System;
using System.Collections.Generic;

namespace Senior_Bilet_refactoring_27_10_26
{
    public class Bookings
    {
        public string Id { get; set; }
        public Guests Guest {  get; set; }
        public Rooms Room { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public double SumPrice { get; set; }
        public string Status { get; set; }
    }
}
