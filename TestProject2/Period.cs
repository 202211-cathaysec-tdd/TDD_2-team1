using System;

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
            DateTime overlappingStart;
            DateTime overlappingEnd;
            if (budget.YearMonth == Start.ToString("yyyyMM"))
            {
                overlappingStart = Start;
                overlappingEnd = budget.LastDay();
            }
            else if (budget.YearMonth == End.ToString("yyyyMM"))
            {
                overlappingStart = budget.FirstDay();
                overlappingEnd = End;
            }
            else
            {
                overlappingStart = budget.FirstDay();
                overlappingEnd = budget.LastDay();
            }

            return (overlappingEnd.Date - overlappingStart.Date).Days + 1;
        }
    }
}