using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IExpenseSummaryRepository
    {
         Task<ExpenseSummaryDto> GetExpenseSummary(int year, int id);
    }
}