using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IExpenseSummaryRepository
    {
         Task<ExpenseSummaryDto> GetExpenseSummary(int year, int id);
         Task<ExpenseSummary> GetExpenseOriSummary(int year, int id);
         void Update(ExpenseSummary expenseSummary);
         Task<bool> SaveAllAsync();
    }
}