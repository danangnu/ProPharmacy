using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPrescriptionSummaryRepository
    {
         Task<PrescriptionSummaryDto> GetPrescriptionSummary(int year, int id);
         Task<PrescriptionSummary> GetPrescriptionOriSummary(int year, int id);
         void Update(PrescriptionSummary prescriptionSummary);
         Task<bool> SaveAllAsync();
    }
}