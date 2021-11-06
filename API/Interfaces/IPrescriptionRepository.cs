using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<PrescriptionReportDto>> GetPrescriptionsAsync();
    }
}