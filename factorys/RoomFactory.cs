using System;
using System.Collections.Generic;
using System.Text;

namespace Senior_Bilet_refactoring_27_10_26.factorys
{
    public class RoomFactory
    {
        public Rooms Create(string number, string type, double price)
        {
            return new Rooms
            {
                Number = number,
                Type = type,
                Price = price,
                Available = true
            };
        }
    }
}
