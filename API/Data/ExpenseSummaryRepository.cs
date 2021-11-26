using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class ExpenseSummaryRepository : IExpenseSummaryRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ExpenseSummaryRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<ExpenseSummaryDto> GetExpenseSummary(int year, int id)
    {
      return await _context.ExpenseSummary
                    .Where(u => u.ExpYear == year && u.FilesVersionId == id)
                    .ProjectTo<ExpenseSummaryDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
    }
  }
}