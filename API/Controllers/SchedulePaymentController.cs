using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SchedulePaymentController : BaseApiController
    {
        private readonly ISchedulePaymentRepository _schedulePaymentRepository;
        public SchedulePaymentController(ISchedulePaymentRepository schedulePaymentRepository)
        {
            _schedulePaymentRepository = schedulePaymentRepository;
        }

        [HttpGet("report")]
        public async Task<IEnumerable<SchedulePaymentReportDto>> GetScheduleReport()
        {
            var sched = await _schedulePaymentRepository.GetScheduleReportAsync();

            return sched;
        }
    }
}