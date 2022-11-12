#region

using System;

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

            decimal result = 0;
            var period = new Period(start, end);

            foreach (var budget in _budgetRepo.GetAll())
            {
                result += budget.OverlappingAmount(period);
            }

            return result;
        }
    }
}