using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class VersionRepository : IVersionRepository
    {
        private readonly DataContext _context;
        public VersionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<FilesVersion> GetVersionByUserIdAsync(int Id)
        {
            return await _context.FilesVersion
                .Include(d => d.Documents)
                .Include(d => d.Prescription)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<IEnumerable<FilesVersion>> GetFVersions()
        {
            return await _context.FilesVersion
                .Include(d => d.Documents)
                .Include(d => d.Prescription)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}