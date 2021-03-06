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
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<FilesVersion> GetVersionPrescByIdAsync(int Id)
        {
            return await _context.FilesVersion
                .Include(d => d.PrescriptionSummary)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<FilesVersion> GetVersionMurByIdAsync(int Id)
        {
            return await _context.FilesVersion
                .Include(d => d.Mur)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<FilesVersion> GetVersionSalesByIdAsync(int Id)
        {
            return await _context.FilesVersion
                .Include(d => d.SalesSummary)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<FilesVersion> GetVersionExpenseByIdAsync(int Id)
        {
            return await _context.FilesVersion
                .Include(d => d.ExpenseSummary)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<FilesVersion> GetVersionSettingByIdAsync(int Id)
        {
            return await _context.FilesVersion
                .Include(d => d.VersionSetting)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<IEnumerable<FilesVersion>> GetFVersions()
        {
            return await _context.FilesVersion
                .Include(d => d.Documents)
                .ToListAsync();
        }

        public void DeleteVersion(FilesVersion filesVersion)
        {
            _context.FilesVersion.Remove(filesVersion);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}