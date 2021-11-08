using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface ISchedulePaymentRepository
    {
        Task<IEnumerable<SchedulePaymentReportDto>> GetScheduleReportAsync();
    }
}