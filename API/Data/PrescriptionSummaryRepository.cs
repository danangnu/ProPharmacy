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
  public class PrescriptionSummaryRepository : IPrescriptionSummaryRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public PrescriptionSummaryRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<PrescriptionSummaryDto> GetPrescriptionSummary(int year, int id)
    {
      var query = await _context.PrescriptionSummary
                    .Where(u => u.PrescMonth == year && u.FilesVersionId == id)
                    .ProjectTo<PrescriptionSummaryDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();

      return query;
    }

    public async Task<PrescriptionSummary> GetPrescriptionOriSummary(int year, int id)
    {
      var query = await _context.PrescriptionSummary
                    .Where(u => u.PrescMonth == year && u.FilesVersionId == id)
                    .SingleOrDefaultAsync();
      return query;
    }

    public void Update(PrescriptionSummary prescriptionSummary)
    {
      _context.Entry(prescriptionSummary).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}