using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class SalesSummaryController : BaseApiController
  {
    private readonly ISalesSummaryRepository _salesSummaryRepository;
    private readonly IMapper _mapper;
    public SalesSummaryController(ISalesSummaryRepository salesSummaryRepository, IMapper mapper)
    {
      _mapper = mapper;
      _salesSummaryRepository = salesSummaryRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<SalesSummaryDto> GetSalesSummary(int year, int id)
    {
      return await _salesSummaryRepository.GetSalesSummary(year, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrescSum(int id, UpdateSalesSummaryDto updateSalesSummaryDto)
    {
      var salesum = await _salesSummaryRepository.GetSalesOriSummary(updateSalesSummaryDto.SalesYear, id);

      _mapper.Map(updateSalesSummaryDto, salesum);

      _salesSummaryRepository.Update(salesum);

      if (await _salesSummaryRepository.SaveAllAsync()) return NoContent();

      return BadRequest("Failed to update Sales Summary");
    }
  }
}