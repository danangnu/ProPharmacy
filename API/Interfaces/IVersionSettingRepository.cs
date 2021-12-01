using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IVersionSettingRepository
    {
         Task<VersionSetting> GetVersionSetting(int id);
         void Update(VersionSetting versionSetting);
         Task<bool> SaveAllAsync();
    }
}