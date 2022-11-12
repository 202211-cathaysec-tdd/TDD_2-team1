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
    }
}