using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SchedulePaymentRepository : ISchedulePaymentRepository
    {
        private readonly DataContext _context;
        public SchedulePaymentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SchedulePaymentReportDto>> GetScheduleReportAsync()
        {
            return await _context.ScheduleOfPayments.AsNoTracking()
                .GroupBy(p => p.Dispensing_Month)
                .Select(x => new SchedulePaymentReportDto
                {
                    Dispensing_Month = x.Key,
                    Month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(int.Parse(
                        x.Key.Substring(x.Key.Length - 1, 2))),
                    Year = x.Key.Substring(0, 4),
                    NHS_SalesSum = x.Sum(t => t.NHS_Sales)
                }).ToListAsync();
        }
    }
}