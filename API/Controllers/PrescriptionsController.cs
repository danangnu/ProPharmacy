using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PrescriptionsController : BaseApiController
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        public PrescriptionsController(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        [HttpGet("report")]
        public async Task<IEnumerable<PrescriptionReportDto>> GetPrescriptionReport()
        {
            var pres = await _prescriptionRepository.GetPrescriptionsAsync();

            return pres;
        }
    }
}