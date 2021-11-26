using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface ISalesSummaryRepository
    {
         Task<SalesSummaryDto> GetSalesSummary(int year, int id);
    }
}