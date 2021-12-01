using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ISalesSummaryRepository
    {
         Task<SalesSummaryDto> GetSalesSummary(int year, int id);
         Task<SalesSummary> GetSalesOriSummary(int year, int id);
         void Update(SalesSummary salesSummary);
         Task<bool> SaveAllAsync();
    }
}