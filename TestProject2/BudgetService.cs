#region

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
            var period = new Period(start, end);

            return _budgetRepo.GetAll().Sum(budget => budget.OverlappingAmount(period));
        }
    }
}