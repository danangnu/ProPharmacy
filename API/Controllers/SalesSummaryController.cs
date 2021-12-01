using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class SalesSummaryController : BaseApiController
  {
    private readonly ISalesSummaryRepository _salesSummaryRepository;
    public SalesSummaryController(ISalesSummaryRepository salesSummaryRepository)
    {
      _salesSummaryRepository = salesSummaryRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<SalesSummaryDto> GetSalesSummary(int year, int id)
    {
        return await _salesSummaryRepository.GetSalesSummary(year, id);
    }
  }
}