#region

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

            int month = GetMonth(start, end);

            decimal result = 0;
            for (int i = 0; i < month; i++)
            {
                if (i == 0)
                {
                    result += GetDayBudget(start, new DateTime(start.Year, start.Month, 1).AddMonths(1).AddDays(-1));
                }
                else if (i == month - 1)
                {
                    result += GetDayBudget(new DateTime(end.Year, end.Month, 1), end);
                }
                else
                {
                    var temp = start.AddMonths(i);
                    result += GetDayBudget(new DateTime(temp.Year, temp.Month, 1), new DateTime(temp.Year, temp.Month, 1).AddMonths(1).AddDays(-1));
                }
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
            decimal result = 0;

            var queryStart = start.ToString("yyyyMM");

            var diff = end.Date - start.Date;

            decimal diffStart = diff.Days + 1; // 同年月跨日

            var monthsday = DateTime.DaysInMonth(start.Year, start.Month); // 當月有幾天

            var budgetResult = _budget.GetAll().FirstOrDefault(a => a.YearMonth == queryStart);

            if (budgetResult != null)
            {
                result = (diffStart * budgetResult.Amount / monthsday);
            }
            else
            {
                result = 0;
            }

            return result;
        }

        private int GetMonth(DateTime start, DateTime end)
        {
            var count = 0;
            var currentDate = start;
            while (currentDate < new DateTime(end.Year, end.Month, 1).AddMonths(1))
            {
                count++;
                currentDate = currentDate.AddMonths(1);
            }

            return count;
        }
    }
}