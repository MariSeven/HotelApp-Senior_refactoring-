using System;
using System.Collections.Generic;

namespace Senior_Bilet_refactoring_27_10_26.factorys
{
    public class GuestFactory
    {
        public Guests Create(string name, string phone, string email)
        {
            return new Guests
            {
                Name = name,
                Phone = phone,
                Email = email,
                LoylLvl = 0
            };
        }
    }
}
