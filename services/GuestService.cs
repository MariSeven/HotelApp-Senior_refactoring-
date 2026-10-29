using Senior_Bilet_refactoring_27_10_26.factorys;
using System.Collections.Generic;
using System.Linq;

namespace Senior_Bilet_refactoring_27_10_26.services
{
    public class GuestService
    {
        private readonly List<Guests> _guests = new List<Guests>();
        private readonly GuestFactory _factory;
        //связь с фабрикой
        public GuestService(GuestFactory factory)
        {
            _factory = factory;
        }
        //добавление гостя (фабрика)
        public void AddGuest(string name, string phone, string email)
        {
            _guests.Add(_factory.Create(name, phone, email));
        }
        //поиск гостя по имени
        public Guests FindByName(string name)
        {
            return _guests.FirstOrDefault(item => item.Name == name);
        }
        //повышение уровня лояльности
        public void IncreaseLoyal(Guests guest)
        {
            if (guest.LoylLvl < AppConst.MaxLvlLoyal)
            {
                guest.LoylLvl++;
            }
        }
        //скидка (по ур. лояльности)
        public double GetLoyaltyDiscount(Guests guest)
        {
            return guest.LoylLvl switch
            {
                1 => AppConst.LvlLoyal1Discount,
                2 => AppConst.LvlLoyal2Discount,
                3 => AppConst.LvlLoyal3Discount,
                _ => 0
            };
        }
    }
}