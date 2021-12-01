using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class SchedulePaymentController : BaseApiController
    {
        private readonly ISchedulePaymentRepository _schedulePaymentRepository;
        public SchedulePaymentController(ISchedulePaymentRepository schedulePaymentRepository)
        {
            _schedulePaymentRepository = schedulePaymentRepository;
        }

        [HttpGet("report/{year}/{id}")]
        public async Task<SchedulePaymentReportDto> GetScheduleReport(int year, int id)
        {
            var sched = await _schedulePaymentRepository.GetScheduleReportAsync(year, id);

            return sched;
        }
    }
}