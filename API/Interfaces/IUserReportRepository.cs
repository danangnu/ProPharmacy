using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IUserReportRepository
  {
    Task<bool> SaveAllAsync();
    Task<IEnumerable<UserReport>> GetFVersions();
    Task<UserReportDto> GetUserReportByEmail(int Id);
    Task<UserReport> GetUserReportByIdAsync(int Id);
    void DeleteReport(UserReport userReport);
  }
}