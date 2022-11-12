﻿#region

using System;
using System.Linq;

#endregion

namespace TestProject2
{
    public class BudgetService
    {
        readonly IBudgetRepo _budget;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budget = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (start > end)
            {
                return 0;
            }

            if (start.ToString("yyyyMM") == end.ToString("yyyyMM"))
            {
                return GetDayBudget(start, end);
            }

            var currentDate = start;
            decimal result = 0;
            while (currentDate < new DateTime(end.Year, end.Month, 1).AddMonths(1))
            {
                var budget = _budget.GetAll().FirstOrDefault(a => a.YearMonth == currentDate.ToString("yyyyMM"));
                if (budget != null)
                {
                    var overlappingDays = OverlappingDays(new Period(start, end), budget);

                    var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month); // 當月有幾天

                    result += overlappingDays * budget.Amount / daysInMonth;
                }

                currentDate = currentDate.AddMonths(1);
            }

            return result;
        }

        private static decimal OverlappingDays(Period period, Budget budget)
        {
            DateTime overlappingStart;
            DateTime overlappingEnd;
            if (budget.YearMonth == period.Start.ToString("yyyyMM"))
            {
                overlappingStart = period.Start;
                overlappingEnd = budget.LastDay();
            }
            else if (budget.YearMonth == period.End.ToString("yyyyMM"))
            {
                overlappingStart = budget.FirstDay();
                overlappingEnd = period.End;
            }
            else
            {
                overlappingStart = budget.FirstDay();
                overlappingEnd = budget.LastDay();
            }

            return (overlappingEnd.Date - overlappingStart.Date).Days + 1;
        }

        /// <summary>
        ///     同年月跨日
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private decimal GetDayBudget(DateTime start, DateTime end)
        {
            decimal diffStart = (end.Date - start.Date).Days + 1; // 同年月跨日

            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month); // 當月有幾天

            var budget = _budget.GetAll().FirstOrDefault(a => a.YearMonth == start.ToString("yyyyMM"));

            if (budget != null)
            {
                return (diffStart * budget.Amount / daysInMonth);
            }

            return 0;
        }
    }
}