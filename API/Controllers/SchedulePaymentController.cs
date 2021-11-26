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

        [HttpGet("report/{year}")]
        public async Task<SchedulePaymentReportDto> GetScheduleReport(int year)
        {
            var sched = await _schedulePaymentRepository.GetScheduleReportAsync(year);

            return sched;
        }
    }
}