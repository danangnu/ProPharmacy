using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class MurRepository : IMurRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public MurRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<MurDto> GetMurSum(int year, int id)
    {
      return await _context.Mur
                    .Where(u => u.MurYear == year && u.FilesVersionId == id)
                    .ProjectTo<MurDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
    }

    public async Task<Mur> GetMur(int year, int id)
    {
      return await _context.Mur
                     .Where(u => u.MurYear == year && u.FilesVersionId == id)
                     .SingleOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(Mur mur)
    {
      _context.Entry(mur).State = EntityState.Modified;
    }
  }
}