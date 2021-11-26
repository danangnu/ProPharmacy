using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class PrescriptionSummaryController : BaseApiController
  {
    private readonly IPrescriptionSummaryRepository _prescriptionSummaryRepository;
    public PrescriptionSummaryController(IPrescriptionSummaryRepository prescriptionSummaryRepository)
    {
      _prescriptionSummaryRepository = prescriptionSummaryRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<PrescriptionSummaryDto> GetPrescriptionSummary(int year, int id)
    {
        return await _prescriptionSummaryRepository.GetPrescriptionSummary(year, id);
    }
  }
}