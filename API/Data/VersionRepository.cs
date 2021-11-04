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
                .FirstOrDefaultAsync(x => x.AppUserId == Id);
        }
    }
}