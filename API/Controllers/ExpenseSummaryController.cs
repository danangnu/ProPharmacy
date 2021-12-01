using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class ExpenseSummaryController : BaseApiController
  {
    private readonly IExpenseSummaryRepository _expenseSummaryRepository;
    private readonly IMapper _mapper;
    public ExpenseSummaryController(IExpenseSummaryRepository expenseSummaryRepository, IMapper mapper)
    {
      _mapper = mapper;
      _expenseSummaryRepository = expenseSummaryRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<ExpenseSummaryDto> GetExpenseSummary(int year, int id)
    {
      return await _expenseSummaryRepository.GetExpenseSummary(year, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrescSum(int id, UpdateExpenseSummaryDto updateExpenseSummaryDto)
    {
      var expsum = await _expenseSummaryRepository.GetExpenseOriSummary(updateExpenseSummaryDto.ExpYear, id);

      _mapper.Map(updateExpenseSummaryDto, expsum);

      _expenseSummaryRepository.Update(expsum);

      if (await _expenseSummaryRepository.SaveAllAsync()) return NoContent();

      return BadRequest("Failed to update Expense Summary");
    }
  }
}