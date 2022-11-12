﻿#region

using System;
using System.Linq;

#endregion

namespace TestProject2
{
    public class BudgetService
    {
        readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepoRepo)
        {
            _budgetRepo = budgetRepoRepo;
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
            var period = new Period(start, end);

            foreach (var budget in _budgetRepo.GetAll())
            {
                // }
                // while (currentDate < new DateTime(end.Year, end.Month, 1).AddMonths(1))
                // {
                //     var budget = _budgetRepo.GetAll().FirstOrDefault(a => a.YearMonth == currentDate.ToString("yyyyMM"));
                //     if (budget != null)
                //     {
                result += budget.OverlappingAmount(period);
                // }
                //
                // currentDate = currentDate.AddMonths(1);
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
            decimal diffStart = (end.Date - start.Date).Days + 1; // 同年月跨日

            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month); // 當月有幾天

            var budget = _budgetRepo.GetAll().FirstOrDefault(a => a.YearMonth == start.ToString("yyyyMM"));

            if (budget != null)
            {
                return (diffStart * budget.Amount / daysInMonth);
            }

            return 0;
        }
    }
}