using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class ExpenseSummaryController : BaseApiController
  {
    private readonly IExpenseSummaryRepository _expenseSummaryRepository;
    public ExpenseSummaryController(IExpenseSummaryRepository expenseSummaryRepository)
    {
      _expenseSummaryRepository = expenseSummaryRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<ExpenseSummaryDto> GetExpenseSummary(int year, int id)
    {
        return await _expenseSummaryRepository.GetExpenseSummary(year, id);
    }
  }
}