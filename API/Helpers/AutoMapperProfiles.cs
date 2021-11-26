using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<UserReport, UserReportDto>();
            CreateMap<FilesVersion, FilesVersionDto>();
            CreateMap<Docs, DocsDto>();
            CreateMap<PrescriptionSummary, PrescriptionSummaryDto>();
            CreateMap<ScheduleOfPayment, SchedulePaymentReportDto>();
            CreateMap<Mur, MurDto>();
            CreateMap<SalesSummary, SalesSummaryDto>();
            CreateMap<AddSalesSummaryDto, SalesSummary>();
            CreateMap<ExpenseSummary, ExpenseSummaryDto>();
            CreateMap<AddExpenseSummaryDto, ExpenseSummary>();
            CreateMap<VersionSetting, VersionSettingDto>();
            CreateMap<AddVersionSettingDto, VersionSetting>();
        }
    }
}