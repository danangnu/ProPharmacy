using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IVersionRepository
    {
        Task<FilesVersion> GetVersionByUserIdAsync(int Id);
    }
}