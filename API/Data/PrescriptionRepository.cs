using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<dynamic>> GetPrescriptionsAsync()
        {
            return await _context.Prescriptions.AsNoTracking()
                .GroupBy(p => p.Dispensing_Month)
                .Select(x => new
                {              
                    Month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(int.Parse(
                        x.Key.Substring(x.Key.Length - 1, 2))),
                    Year = x.Key.Substring(0, 4),
                    AnnualSum = x.Sum(t => t.Item_Number),
                    AnnualAvg = (int)Math.Round(x.Average(t => t.Item_Number))
                }).ToListAsync();
        }

        public async Task<IEnumerable<PrescriptionReportDto>> GetPrescriptionReportsAsync()
        {
            return await _context.Prescriptions.AsNoTracking()
                .GroupBy(p => p.Dispensing_Year)
                .Select(x => new PrescriptionReportDto
                {
                    Year = x.Key,
                    Jan = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "01").Sum(t => t.Item_Number),
                    Feb = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "02").Sum(t => t.Item_Number),
                    Mar = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "03").Sum(t => t.Item_Number),
                    Apr = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "04").Sum(t => t.Item_Number),
                    May = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "05").Sum(t => t.Item_Number),
                    Jun = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "06").Sum(t => t.Item_Number),
                    Jul = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "07").Sum(t => t.Item_Number),
                    Aug = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "08").Sum(t => t.Item_Number),
                    Sep = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "09").Sum(t => t.Item_Number),
                    Oct = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "10").Sum(t => t.Item_Number),
                    Nov = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "11").Sum(t => t.Item_Number),
                    Dec = x.Where(u => u.Dispensing_Month.Substring(4, 2) == "12").Sum(t => t.Item_Number)
                }).ToListAsync();
        }
    }
}