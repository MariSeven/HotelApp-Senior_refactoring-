using System;
using System.Collections.Generic;
using System.Text;

namespace Senior_Bilet_refactoring_27_10_26.services
{
    //класс расчета итоговой стоимости с учетом всех скидок и наценок
    public class CostCalculatorService
    {
        private readonly RoomService _roomService;
        private readonly GuestService _guestService;

        public CostCalculatorService(RoomService roomService, GuestService guestService)
        {
            _roomService = roomService;
            _guestService = guestService;
        }
        //основной метод расчета по всем параметрам (вызывает вспомогательные методы для каждого пункта)
        public double Calculate(Rooms room, int nights, string promoCode, Guests guest, DateOnly checkIn)
        {
            double total = room.Price * nights;
            total *= _roomService.GetRoomTypeMultiplier(room.Type);
            total = DurationDiscount(total, nights);
            total = PromoDiscount(total, promoCode);
            total = LoyalDiscount(total, guest);
            total = SeasonalMarkup(total, checkIn.Month);
            total *= (1 + AppConst.NalogPlus);

            return Math.Max(total, AppConst.MinSum);
        }
        //скидка за длительность брони
        private double DurationDiscount(double total, int nights)
        {
            if (nights >= AppConst.LongStayDays) return total * (1 - AppConst.LongStayDiscount);
            if (nights >= AppConst.MediumStayDays) return total * (1 - AppConst.MediumStayDiscount);
            return total;
        }
        //скидка по промокоду
        private double PromoDiscount(double total, string promo)
        {
            return promo?.ToUpper() switch
            {
                AppConst.PromoWelcome10 => total * (1 - AppConst.Welcome10Discount),
                AppConst.PromoSummer20 => total * (1 - AppConst.Summer20Discount),
                AppConst.PromoVip30 => total * (1 - AppConst.Vip30Discount),
                _ => total
            };
        }
        //скидка за уровень лояльности
        private double LoyalDiscount(double total, Guests guest)
        {
            return total * (1 - _guestService.GetLoyaltyDiscount(guest));
        }
        //сезонная скидка
        private double SeasonalMarkup(double total, int month)
        {
            if (month == 12 || month == 1 || month == 2) return total * (1 + AppConst.WhinterPlus);
            if (month == 6 || month == 7 || month == 8) return total * (1 + AppConst.SummerPlus);
            return total;
        }
        //штраф
        public double CalculatePenalty(double totalPrice)
        {
            return totalPrice * AppConst.PenaltyPlus;
        }
    }
}
