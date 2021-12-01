using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class PrescriptionsController : BaseApiController
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        public PrescriptionsController(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<PrescriptionReportDto>>> GetPrescriptionReport()
        {
            var pres = await _prescriptionRepository.GetPrescriptionReportsAsync();

            return Ok(pres);
        }
    }
}