using System;
using System.Collections.Generic;

namespace Senior_Bilet_refactoring_27_10_26
{
    //Класс с константами (защита от магических выражений)
    public static class AppConst
    {
        //наценки по типу номера
        public const double LuxRoomPlus = 0.5;
        public const double SuiteRoomPlus = 0.8;

        //Скидки за длительность брони
        public const int LongStayDays = 7;
        public const double LongStayDiscount = 0.1;
        public const int MediumStayDays = 3;
        public const double MediumStayDiscount = 0.05;

        //promocode
        public const string PromoWelcome10 = "WELCOME10";
        public const double Welcome10Discount = 0.1;
        public const string PromoSummer20 = "SUMMER20";
        public const double Summer20Discount = 0.2;
        public const string PromoVip30 = "VIP30";
        public const double Vip30Discount = 0.3;

        //Лояльность
        public const int MaxLvlLoyal = 3;
        public const double LvlLoyal1Discount = 0.05;
        public const double LvlLoyal2Discount = 0.1;
        public const double LvlLoyal3Discount = 0.15;

        //сезон (наценка)
        public const double WhinterPlus = 0.2;
        public const double SummerPlus = 0.3;

        //налог
        public const double NalogPlus = 0.18;

        //min sum
        public const double MinSum = 3000;

        //Форматы
        public const string BookingIDPrefix = "BK";
        public const string BookingIDFormat = "D4";

        //Штраф
        public const double PenaltyPlus = 0.2;
    }
}
