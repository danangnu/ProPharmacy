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

    public async Task<SchedulePaymentReportDto> GetScheduleReportAsync(int year, int id)
    {
        var doc = await _context.Documents.Where(x => x.FilesVersionId == id && x.FileType == "application/pdf").SingleOrDefaultAsync();
        int yearStart = int.Parse(year + "04");
        int yearEnd = int.Parse((year + 1) + "03");
        var query = _context.ScheduleOfPayments.AsQueryable();
        query = query.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).AsNoTracking();
        
        if (doc != null) query = query.Where(q => q.DocsId == doc.Id);

        var map = await query.ProjectTo<SchedulePaymentReportDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

        var query2 = _context.ScheduleOfPayments.AsQueryable();
        var tot_others = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Other) ?? 0;
        if (tot_others != 0) map.Total_Others = tot_others;
        var other_Fee_Medicine_Services = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Medicine_Service) ?? 0;
        if (other_Fee_Medicine_Services != 0)
          map.Other_Fee_Medicine_Services = other_Fee_Medicine_Services;
        var Other_Fee_Appliance_Premise = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Appliance_Premise) ?? 0;
        var Other_Fee_Stoma_Custom = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Stoma_Custom) ?? 0;
        var Other_Fee_Appliance_Patient = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Other_Fee_Appliance_Patient) ?? 0;
        if (Other_Fee_Appliance_Premise != 0)
          map.Adv_Others = Other_Fee_Appliance_Premise;
        if (Other_Fee_Stoma_Custom != 0)
          map.Adv_Others += Other_Fee_Stoma_Custom;
        if (Other_Fee_Appliance_Patient != 0)
          map.Adv_Others += Other_Fee_Appliance_Patient;
        var Total_Authorised = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Authorised) ?? 0;
        var Total_Authorised_LPP = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Authorised_LPP) ?? 0;
        if (Total_Authorised != 0)
          map.Enhanced_Services = Total_Authorised;
        if (Total_Authorised_LPP != 0)
          map.Enhanced_Services += Total_Authorised_LPP;
        var Total_Charges = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Total_Charges) ?? 0;
        if (Total_Authorised_LPP != 0)
          map.Total_Charges = Total_Charges;
        var Transitional_Pay = query2.Where(u => u.Dispensing_Month >= yearStart && u.Dispensing_Month <= yearEnd).Sum(l => (double?) l.Transitional_Pay) ?? 0;
        if (Transitional_Pay != 0)
          map.Transitional_Pay = Transitional_Pay;

        return map;
    }
  }
}