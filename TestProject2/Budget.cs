#region

using System;

#endregion

namespace TestProject2
{
    public class Budget
    {
        public int Amount { get; set; }
        public string YearMonth { get; set; }

        public DateTime FirstDay()
        {
            return DateTime.ParseExact(YearMonth, "yyyyMM", null);
        }

        public DateTime LastDay()
        {
            var firstDay = FirstDay();
            var daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
            return new DateTime(firstDay.Year, firstDay.Month, daysInMonth);
        }

        public Period CreatePeriod()
        {
            return new Period(FirstDay(), LastDay());
        }
    }
}