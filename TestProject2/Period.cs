#region

using System;

#endregion

namespace TestProject2
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime End { get; private set; }
        public DateTime Start { get; private set; }

        public decimal OverlappingDays(Budget budget)
        {
            var another = new Period(budget.FirstDay(), budget.LastDay());
            var firstDay = budget.FirstDay();
            var lastDay = budget.LastDay();
            
            DateTime overlappingStart = Start > firstDay
                ? Start
                : firstDay;

            DateTime overlappingEnd = End < lastDay
                ? End
                : lastDay;

            return (overlappingEnd.Date - overlappingStart.Date).Days + 1;
        }
    }
}