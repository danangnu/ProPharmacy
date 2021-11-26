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
        Task<FilesVersion> GetVersionPrescByIdAsync(int Id);
        Task<FilesVersion> GetVersionMurByIdAsync(int Id);
        Task<FilesVersion> GetVersionSalesByIdAsync(int Id);
        Task<FilesVersion> GetVersionExpenseByIdAsync(int Id);
        Task<FilesVersion> GetVersionSettingByIdAsync(int Id);
        void DeleteVersion(FilesVersion filesVersion);
    }
}