using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly DataContext _context;
        public PrescriptionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PrescriptionReportDto>> GetPrescriptionsAsync()
        {
            return await _context.Prescriptions.AsNoTracking()
                .GroupBy(p => p.Dispensing_Month)
                .Select(x => new PrescriptionReportDto
                {
                    Dispensing_Month = x.Key,
                    Month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(int.Parse(
                        x.Key.Substring(x.Key.Length - 1, 2))),
                    Year = x.Key.Substring(0, 4),
                    AnnualSum = x.Sum(t => t.Item_Number),
                    AnnualAvg = (int)Math.Round(x.Average(t => t.Item_Number))
                }).ToListAsync();
        }
    }
}