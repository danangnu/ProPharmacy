using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class VersionSettingRepository : IVersionSettingRepository
  {
    private readonly DataContext _context;
    public VersionSettingRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<VersionSetting> GetVersionSetting(int id)
    {
      return await _context.VersionSetting
                        .Where(v => v.FilesVersionId == id)
                        .SingleOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(VersionSetting versionSetting)
    {
      _context.Entry(versionSetting).State = EntityState.Modified;
    }
  }
}