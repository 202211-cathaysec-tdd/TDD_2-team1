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
                DateTime overlappingStart;
                DateTime overlappingEnd;
                if (currentDate.ToString("yyyyMM") == start.ToString("yyyyMM"))
                {
                    overlappingStart = start;
                    overlappingEnd = new DateTime(start.Year, start.Month, 1).AddMonths(1).AddDays(-1);
                }
                else if (currentDate.ToString("yyyMM") == end.ToString("yyyyMM"))
                {
                    overlappingStart = new DateTime(end.Year, end.Month, 1);
                    overlappingEnd = end;
                }
                else
                {
                    overlappingStart = new DateTime(currentDate.Year, currentDate.Month, 1);
                    overlappingEnd = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).AddDays(-1);
                }

                result += GetDayBudget(overlappingStart, overlappingEnd);

                currentDate = currentDate.AddMonths(1);
            }

            return result;
        }

        /// <summary>
        ///     同年月跨日
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private decimal GetDayBudget(DateTime start, DateTime end)
        {
            // decimal result = 0;

            var queryStart = start.ToString("yyyyMM");

            var diff = end.Date - start.Date;

            decimal diffStart = diff.Days + 1; // 同年月跨日

            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month); // 當月有幾天

            var budgetResult = _budget.GetAll().FirstOrDefault(a => a.YearMonth == queryStart);

            if (budgetResult != null)
            {
                return (diffStart * budgetResult.Amount / daysInMonth);
            }
            else
            {
                return 0;
            }

            // return result;
        }
    }
}