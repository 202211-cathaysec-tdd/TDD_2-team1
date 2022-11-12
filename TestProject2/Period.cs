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

        public decimal OverlappingDays(Period another)
        {
            DateTime overlappingStart = Start > another.Start
                ? Start
                : another.Start;

            DateTime overlappingEnd = End < another.End
                ? End
                : another.End;

            return (overlappingEnd.Date - overlappingStart.Date).Days + 1;
        }
    }
}