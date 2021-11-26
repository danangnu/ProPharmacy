using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class SchedulePaymentRepository : ISchedulePaymentRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public SchedulePaymentRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<SchedulePaymentReportDto> GetScheduleReportAsync(int year)
    {
        int yearStart = int.Parse(year + "04");
        int yearEnd = int.Parse((year + 1) + "03");
        var query = await _context.ScheduleOfPayments
            .Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd)
            .ProjectTo<SchedulePaymentReportDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        var query2 = _context.ScheduleOfPayments.AsQueryable();
        query.Total_Others = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Other) ?? 0;
        query.Other_Fee_Medicine_Services = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Medicine_Service) ?? 0;
        var Other_Fee_Appliance_Premise = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Appliance_Premise) ?? 0;
        var Other_Fee_Stoma_Custom = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Stoma_Custom) ?? 0;
        var Other_Fee_Appliance_Patient = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Appliance_Patient) ?? 0;
        query.Adv_Others = Other_Fee_Appliance_Premise + Other_Fee_Stoma_Custom + Other_Fee_Appliance_Patient;
        var Total_Authorised = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Authorised) ?? 0;
        var Total_Authorised_LPP = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Authorised_LPP) ?? 0;
        query.Enhanced_Services = Total_Authorised + Total_Authorised_LPP;
        query.Total_Charges = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Charges) ?? 0;

        return query;
    }
  }
}