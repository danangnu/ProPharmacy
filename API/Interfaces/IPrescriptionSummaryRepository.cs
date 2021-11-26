using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IPrescriptionSummaryRepository
    {
         Task<PrescriptionSummaryDto> GetPrescriptionSummary(int year, int id);
    }
}