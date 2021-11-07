using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IDocRepository
    {
        Task<Docs> GetPresDocByIdAsync(int Id);
        Task<Docs> GetPayDocByIdAsync(int Id);
    }
}