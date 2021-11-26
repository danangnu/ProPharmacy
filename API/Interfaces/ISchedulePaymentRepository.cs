using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface ISchedulePaymentRepository
    {
        Task<SchedulePaymentReportDto> GetScheduleReportAsync(int year);
    }
}