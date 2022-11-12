#region

using System;

#endregion

namespace TestProject2
{
    public class Budget
    {
        public int Amount { get; set; }
        public string YearMonth { get; set; }

        public decimal OverlappingAmount(Period period)
        {
            return period.OverlappingDays(CreatePeriod()) * DailyAmount();
        }

        private Period CreatePeriod()
        {
            return new Period(FirstDay(), LastDay());
        }

        private decimal DailyAmount()
        {
            return Amount / Days();
        }

        private decimal Days()
        {
            return LastDay().Day;
        }

        private DateTime FirstDay()
        {
            return DateTime.ParseExact(YearMonth, "yyyyMM", null);
        }

        private DateTime LastDay()
        {
            var firstDay = FirstDay();
            var daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
            return new DateTime(firstDay.Year, firstDay.Month, daysInMonth);
        }
    }
}