using System.Collections.Generic;
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
  public class UserReportRepository : IUserReportRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public UserReportRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public void DeleteReport(UserReport userReport)
    {
      _context.UserReport.Remove(userReport);
    }

    public async Task<IEnumerable<UserReport>> GetFVersions()
    {
      return await _context.UserReport
                .Include(d => d.VersionCreated)
                .ToListAsync();
    }

    public async Task<UserReportDto> GetUserReportByEmail(int Id)
    {
      return await _context.UserReport
                .Where(x => x.Id == Id)
                .ProjectTo<UserReportDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
    }

    public async Task<UserReport> GetUserReportByIdAsync(int Id)
    {
      return await _context.UserReport 
              .Include(u => u.VersionCreated)
              .FirstOrDefaultAsync(v => v.Id == Id);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}