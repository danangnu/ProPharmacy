using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IVersionRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<FilesVersion>> GetFVersions();
        Task<FilesVersion> GetVersionByUserIdAsync(int Id);
        void DeleteVersion(FilesVersion filesVersion);
    }
}