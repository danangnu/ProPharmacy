using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IMurRepository
    {
        Task<MurDto> GetMurSum(int year, int id);
         Task<Mur> GetMur(int year, int id);
         void Update(Mur mur);
         Task<bool> SaveAllAsync();
    }
}