using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class SalesSummaryRepository : ISalesSummaryRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public SalesSummaryRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<SalesSummaryDto> GetSalesSummary(int year, int id)
    {
      return await _context.SalesSummary
                    .Where(u => u.SalesYear == year && u.FilesVersionId == id)
                    .ProjectTo<SalesSummaryDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
    }
  }
}