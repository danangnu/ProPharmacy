using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class PrescriptionSummaryController : BaseApiController
  {
    private readonly IPrescriptionSummaryRepository _prescriptionSummaryRepository;
    private readonly IMapper _mapper;
    public PrescriptionSummaryController(IPrescriptionSummaryRepository prescriptionSummaryRepository, IMapper mapper)
    {
      _mapper = mapper;
      _prescriptionSummaryRepository = prescriptionSummaryRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<PrescriptionSummaryDto> GetPrescriptionSummary(int year, int id)
    {
      return await _prescriptionSummaryRepository.GetPrescriptionSummary(year, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrescSum(int id, UpdatePrescriptionSummaryDto updatePrescriptionSummaryDto)
    {
      var prescsum = await _prescriptionSummaryRepository.GetPrescriptionOriSummary(updatePrescriptionSummaryDto.PrescMonth, id);

      _mapper.Map(updatePrescriptionSummaryDto, prescsum);

      _prescriptionSummaryRepository.Update(prescsum);

      if (await _prescriptionSummaryRepository.SaveAllAsync()) return NoContent();

      return BadRequest("Failed to update Prescription Summary");
    }
  }
}